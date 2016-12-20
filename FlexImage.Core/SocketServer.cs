// ============================================
// 
// FlexImage.Core
// SocketServer.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

#endregion

namespace FlexImage.Core
{
    public class SocketServer
    {
        #region Delegates

        public delegate void ConnectionDelegate(Socket soc);

        public delegate void ErrorDelegate(string ErroMessage, Socket soc, int ErroCode);

        public delegate void ListenDelegate();

        public delegate void SocketLogDelegate(string msg);

        #endregion

        #region Eventos

        public event ConnectionDelegate OnConnect;
        public event ConnectionDelegate OnDisconnect;
        public event ConnectionDelegate OnRead;
        public event ConnectionDelegate OnWrite;
        public event ErrorDelegate OnError;
        public event ListenDelegate OnListen;
        public event SocketLogDelegate OnSocketLog;

        #endregion

        #region Variaveis

        private readonly ArrayList Clientes = ArrayList.Synchronized(new ArrayList());
        private readonly IPEndPoint ipLocal;
        private readonly int mPorta;
        private readonly Socket mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private AsyncCallback WorkerCallBack;
        private byte[] mBytesRecebidos;
        private string mTextoEnviado = "";
        private string mTextoRecebido = "";

        #endregion

        #region Propriedades

        /// <summary>
        ///   Porta na qual os clientes podem se conectar
        /// </summary>
        public int Port
        {
            get { return (this.mPorta); }
        }

        /// <summary>
        ///   Bytes que chegaram ao Socket
        /// </summary>
        public byte[] ReceivedBytes
        {
            get
            {
                byte[] temp = null;
                if (this.mBytesRecebidos != null)
                {
                    temp = this.mBytesRecebidos;
                    this.mBytesRecebidos = null;
                }
                return (temp);
            }
        }

        /// <summary>
        ///   Messagem que chegou ao Socket
        /// </summary>
        public string ReceivedText
        {
            get
            {
                string temp = this.mTextoRecebido;
                this.mTextoRecebido = "";
                return (temp);
            }
        }

        /// <summary>
        ///   Messagem enviada pelo Socket
        /// </summary>
        public string WriteText
        {
            get
            {
                string temp = this.mTextoEnviado;
                this.mTextoEnviado = "";
                return (temp);
            }
        }

        /// <summary>
        ///   Número de conexões ativas
        /// </summary>
        public int ActiveConnections
        {
            get { return (this.Clientes.Count); }
        }

        #endregion

        #region Construtor

        /// <summary>
        ///   Construtor padrão da classe
        /// </summary>
        /// <param name="port"> Porta para conexão </param>
        public SocketServer(int port)
        {
            try
            {
                this.mPorta = port;
                this.ipLocal = new IPEndPoint(IPAddress.Any, this.mPorta);
            }
            catch (Exception se)
            {
                if (this.OnError != null)
                    this.OnError(se.Message, null, 0);
            }
        }

        #endregion

        #region Classes

        private class SocketPacket
        {
            public readonly byte[] dataBuffer = new byte[1024];
            public Socket m_currentSocket;

            public SocketPacket(Socket socket)
            {
                this.m_currentSocket = socket;
            }
        }

        #endregion

        #region Funcoes e Eventos

        /// <summary>
        ///   Ativa o ServerSocket que fica escutando na porta pré-definida na construção da Classe
        /// </summary>
        public bool Active()
        {
            try
            {
                this.mainSocket.Bind(this.ipLocal);
                this.mainSocket.Listen(0);
                this.mainSocket.BeginAccept(this.OnClientConnect, null);
                if (this.OnListen != null)
                    this.OnListen();
                return true;
            }
            catch (SocketException se)
            {
                if (this.OnError != null)
                    this.OnError(se.Message, this.mainSocket, se.ErrorCode);
                return false;
            }
        }

        private void OnClientConnect(IAsyncResult asyn)
        {
            try
            {
                Socket workSocket = this.mainSocket.EndAccept(asyn);

                try
                {
                    this.WaitForData(workSocket);
                    if (this.OnConnect != null)
                        this.OnConnect(workSocket);
                    lock (this)
                    {
                        this.Clientes.Add(workSocket);
                    }
                    this.mainSocket.BeginAccept(this.OnClientConnect, null);
                }
                catch (SocketException se)
                {
                    if (this.OnError != null)
                        this.OnError(se.Message, workSocket, se.ErrorCode);
                }
            }
            catch (ObjectDisposedException se)
            {
                if (this.OnError != null)
                    this.OnError(se.Message, null, 0);
            }
        }

        private void WaitForData(Socket soc)
        {
            try
            {
                if (this.WorkerCallBack == null)
                    this.WorkerCallBack = this.OnDataReceived;
                var theSocPkt = new SocketPacket(soc);
                soc.BeginReceive(theSocPkt.dataBuffer, 0,
                                 theSocPkt.dataBuffer.Length,
                                 SocketFlags.None,
                                 this.WorkerCallBack,
                                 theSocPkt);
            }
            catch (SocketException se)
            {
                if (this.OnError != null)
                    this.OnError(se.Message, soc, se.ErrorCode);
            }
        }

        private void OnDataReceived(IAsyncResult asyn)
        {
            var socketData = (SocketPacket) asyn.AsyncState;
            try
            {
                int iRx = socketData.m_currentSocket.EndReceive(asyn);

                if (this.OnSocketLog != null)
                    this.OnSocketLog("OnDataReceived: [" + iRx + "]");

                if (iRx < 1)
                {
                    socketData.m_currentSocket.Close();

                    if (!socketData.m_currentSocket.Connected)
                    {
                        if (this.OnDisconnect != null)
                            this.OnDisconnect(socketData.m_currentSocket);
                        this.Clientes.Remove(socketData.m_currentSocket);
                        socketData.m_currentSocket = null;
                    }
                }
                else
                {
                    this.mBytesRecebidos = socketData.dataBuffer;
                    var chars = new char[iRx + 1];
                    Decoder d = Encoding.UTF8.GetDecoder();
                    d.GetChars(socketData.dataBuffer, 0, iRx, chars, 0);
                    this.mTextoRecebido = new String(chars);

                    if (this.OnSocketLog != null)
                        this.OnSocketLog("OnDataReceived::" + this.mTextoRecebido);

                    if (this.OnRead != null)
                        this.OnRead(socketData.m_currentSocket);
                    this.WaitForData(socketData.m_currentSocket);
                }
            }
            catch (InvalidOperationException se)
            {
                if (socketData.m_currentSocket.Connected)
                    socketData.m_currentSocket.Close();
                if (!socketData.m_currentSocket.Connected)
                {
                    if (this.OnDisconnect != null)
                        this.OnDisconnect(socketData.m_currentSocket);
                    this.Clientes.Remove(socketData.m_currentSocket);
                    socketData.m_currentSocket = null;
                }
                if (this.OnError != null)
                    this.OnError(se.Message, null, 0);
            }
            catch (SocketException se)
            {
                if (this.OnError != null)
                    this.OnError(se.Message, socketData.m_currentSocket, se.ErrorCode);
                if (!socketData.m_currentSocket.Connected)
                {
                    if (this.OnDisconnect != null)
                        this.OnDisconnect(socketData.m_currentSocket);
                    this.Clientes.Remove(socketData.m_currentSocket);
                    socketData.m_currentSocket = null;
                }
            }
        }

        /// <summary>
        ///   Envia uma mensagem de texto pela conexão selecionada no índice
        /// </summary>
        /// <param name="mens"> Mensagem para ser enviada </param>
        /// <param name="SocketIndex"> Índice da conexão </param>
        public bool SendText(string mens, Socket workerSocket)
        {
            try
            {
                byte[] byData = Encoding.ASCII.GetBytes(mens);
                int NumBytes = workerSocket.Send(byData);
                if (NumBytes == byData.Length)
                {
                    if (this.OnWrite != null)
                    {
                        this.mTextoEnviado = mens;
                        this.OnWrite(workerSocket);
                    }
                    return true;
                }
                else
                    return false;
            }
            catch (Exception se)
            {
                if (this.OnError != null)
                    this.OnError("Socket Error SEND:" + se.Message, null, 0);

                return false;
            }
        }


        /// <summary>
        ///   Desativa o ServerSocket perdendo todas as conexões
        /// </summary>
        public bool Deactive()
        {
            bool err = true;
            if (this.mainSocket != null)
                this.mainSocket.Close();
            int total = this.Clientes.Count;
            for (int i = 0; i < total; i++)
            {
                var workerSocket = (Socket) this.Clientes[i];
                if (workerSocket != null)
                {
                    if (this.OnDisconnect != null)
                        this.OnDisconnect(workerSocket);
                    workerSocket.Close();
                    err = err && workerSocket.Connected;
                }
            }
            return err;
        }

        /// <summary>
        ///   Termina uma conexão específica de acordo com o Índice da conexão
        /// </summary>
        /// <param name="SocketIndex"> Índice da conexão </param>
        public bool CloseConnection(Socket workerSocket)
        {
            if (workerSocket != null)
                workerSocket.Close();
            if (!workerSocket.Connected)
                return true;
            else
                return false;
        }

        /// <summary>
        ///   Verifica o status da conexão
        /// </summary>
        public bool Connected(Socket soc)
        {
            return soc.Connected;
        }

        /// <summary>
        ///   Retorna o IP remoto de acordo com o índice informado
        /// </summary>
        public string RemoteAddress(Socket soc)
        {
            try
            {
                string temp = soc.RemoteEndPoint.ToString();
                return temp.Substring(0, temp.IndexOf(":"));
            }
            catch (ArgumentException se)
            {
                if (this.OnError != null)
                    this.OnError(se.Message, null, 0);
                return "";
            }
            catch (SocketException se)
            {
                if (this.OnError != null)
                    this.OnError(se.Message, null, 0);
                return "";
            }
        }

        /// <summary>
        ///   Retorna o Host remoto de acordo com o índice informado
        /// </summary>
        public string RemoteHost(Socket soc)
        {
            try
            {
                string temp = soc.RemoteEndPoint.ToString();
                temp = temp.Substring(0, temp.IndexOf(":"));
                IPHostEntry retorno = Dns.GetHostEntry(temp);
                return retorno.HostName;
            }
            catch (ArgumentException se)
            {
                if (this.OnError != null)
                    this.OnError(se.Message, null, 0);
                return "";
            }
            catch (SocketException se)
            {
                if (this.OnError != null)
                    this.OnError(se.Message, null, 0);
                return "";
            }
        }

        #endregion
    }
}