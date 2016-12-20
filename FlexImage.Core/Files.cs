// ============================================
// 
// FlexImage.Core
// Files.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Threading;
using System.Diagnostics;
using System.Drawing;

#endregion

namespace FlexImage.Core
{
    public static class Files
    {
        public static string GetWorkDir()
        {
            string aux = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return aux;
        }

        public static string GetFormattedPath(string id)
        {
            string fileName = id;
            string auxfileName = fileName.Replace("F.", ".");
            auxfileName = auxfileName.Replace("V.", ".");
            auxfileName = String.Format("{0:D12}", Convert.ToInt64(auxfileName));
            string auxDir = Convert.ToInt16(auxfileName.Substring(0, 3)) + "\\" +
                            Convert.ToInt16(auxfileName.Substring(3, 3)) + "\\" +
                            Convert.ToInt16(auxfileName.Substring(6, 3)) + "\\";
            return auxDir;
        }

        /// <summary>
        ///   Function to save byte array to a file
        /// </summary>
        /// <param name="_FileName"> File name to save byte array </param>
        /// <param name="_ByteArray"> Byte array to save to external file </param>
        /// <returns> Return true if byte array save successfully, if not return false </returns>
        public static bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            try
            {
                // Open file for reading
                var _FileStream = new FileStream(_FileName, FileMode.Create, FileAccess.Write);

                // Writes a block of bytes to this stream using data from a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                // close file stream
                _FileStream.Close();

                return true;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception);
            }

            // error occured, return false
            return false;
        }

        public static byte[] ReadByteArrayFromFile(string fileName)
        {
            byte[] buff = null;
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var br = new BinaryReader(fs))
                {
                    long numBytes = new FileInfo(fileName).Length;
                    buff = br.ReadBytes((int) numBytes);
                }
            }

            return buff;
        }

        public static string OpenFileDialog(string title, string initialDir, string filter)
        {
            //Open File
            var fdlg = new OpenFileDialog();
            fdlg.Title = title;
            fdlg.InitialDirectory = initialDir;
            //fdlg.Filter = filter;
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
                return fdlg.FileName;
            return "";
        }

        public static string ReadConfigTag(string tag)
        {
            string fileName = Application.StartupPath + "\\CONFIG\\CONFIG.XML";
            string ret = "";

            if (File.Exists(fileName))
            {
                var xtr = new XmlTextReader(fileName);
                xtr.WhitespaceHandling = WhitespaceHandling.None;
                var xml = new XmlDocument();
                xml.Load(xtr);
                xtr.Close();

                XmlNode element = xml.SelectSingleNode("/ROOT/" + tag);
                if (element != null)
                    ret = element.InnerText;
            }

            return ret;
        }

        /// <summary>
        /// Executes a shell command synchronously.
        /// </summary>
        /// <param name="command">string command</param>
        /// <returns>string, as output of the command.</returns>
        public static void ExecuteCommandSync(object command)
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();
                // Display the command output.
                Console.WriteLine(result);
            }
            catch (Exception objException)
            {
                // Log the exception
            }
        }


        /// <summary>
        /// Execute the command Asynchronously.
        /// </summary>
        /// <param name="command">string command.</param>
        public static void ExecuteCommandAsync(string command)
        {
            try
            {
                //Asynchronously start the Thread to process the Execute command request.
                Thread objThread = new Thread(new ParameterizedThreadStart(ExecuteCommandSync));
                //Make the thread as background thread.
                objThread.IsBackground = true;
                //Set the Priority of the thread.
                objThread.Priority = ThreadPriority.AboveNormal;
                //Start the thread.
                objThread.Start(command);
            }
            catch (ThreadStartException objException)
            {
                // Log the exception
            }
            catch (ThreadAbortException objException)
            {
                // Log the exception
            }
            catch (Exception objException)
            {
                // Log the exception
            }
        }

        public static Size GetDimensions(string path)
        {
            try
            {
                using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(path)))
                {
                    try
                    {
                        return DecodeJfif(binaryReader);
                    }
                    catch (ArgumentException e)
                    {
                        string newMessage = string.Format("{0} file: '{1}' ", e, path);

                        throw new ArgumentException(newMessage, "path", e);
                    }
                }
            }
            catch (ArgumentException)
            {
                //do it the old fashioned way

                using (Bitmap b = new Bitmap(path))
                {
                    return b.Size;
                }
            }
        }

        private static Size DecodeJfif(BinaryReader binaryReader)
        {
            while (binaryReader.ReadByte() == 0xff)
            {
                byte marker = binaryReader.ReadByte();
                short chunkLength = ReadLittleEndianInt16(binaryReader);
                if (marker == 0xc0)
                {
                    binaryReader.ReadByte();
                    int height = ReadLittleEndianInt16(binaryReader);
                    int width = ReadLittleEndianInt16(binaryReader);
                    return new Size(width, height);
                }

                if (chunkLength < 0)
                {
                    ushort uchunkLength = (ushort)chunkLength;
                    binaryReader.ReadBytes(uchunkLength - 2);
                }
                else
                {
                    binaryReader.ReadBytes(chunkLength - 2);
                }
            }

            throw new ArgumentException("Decodejfif");
        }

        private static short ReadLittleEndianInt16(this BinaryReader binaryReader)
        {
            byte[] bytes = new byte[sizeof(short)];
            for (int i = 0; i < sizeof(short); i += 1)
            {
                bytes[sizeof(short) - 1 - i] = binaryReader.ReadByte();
            }
            return BitConverter.ToInt16(bytes, 0);
        }

        private static int ReadLittleEndianInt32(this BinaryReader binaryReader)
        {
            byte[] bytes = new byte[sizeof(int)];
            for (int i = 0; i < sizeof(int); i += 1)
            {
                bytes[sizeof(int) - 1 - i] = binaryReader.ReadByte();
            }
            return BitConverter.ToInt32(bytes, 0);
        }
    }
    
    public static class IniFile
    {
        public static string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
                                                             string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                                                          string key, string def, StringBuilder retVal,
                                                          int size, string filePath);


        public static void IniWriteValue(string path, string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }

        public static string IniReadValue(string path, string Section, string Key)
        {
            string ret = "";

            if (File.Exists(path))
            {
                var temp = new StringBuilder(255);
                int i = GetPrivateProfileString(Section, Key, "", temp, 255, path);
                ret = temp.ToString().Trim();
            }

            return ret;
        }

        public static void AlterTo200Dpi(string imagemToProcess)
        {
            var parameter = new ProcessStartInfo
            {
                FileName = @"C:\Program Files (x86)\ImageMagick-6.7.6-Q16\convert.exe",
                Arguments = string.Format(
                    "{0} {1}x{1} {2} {3}",
                    @"-density",
                    "200",
                    imagemToProcess.ToLower(),
                    imagemToProcess.ToLower().Replace(".jpg", "_newResolution.jpg")),
                WindowStyle = ProcessWindowStyle.Hidden
            };

            var imageMagic = new Process { StartInfo = parameter };

            imageMagic.Start();
            imageMagic.WaitForExit();
            imageMagic.Close();
            imageMagic.Dispose();

            File.Copy(imagemToProcess.ToLower().Replace(".jpg", "_newResolution.jpg"), imagemToProcess.ToLower());

            //this.RenameTransformedFile(imagemToProcess);
        }        
    }

}