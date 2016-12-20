// ============================================
// 
// FlexImage.SingleInstance
// SingleInstanceTracker.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Threading;

#endregion

namespace FlexImage.SingleInstance
{
    /// <summary>
    ///   Represents an object used to check for a previous instance of an application, and sending messages to it.
    /// </summary>
    public class SingleInstanceTracker : IDisposable
    {
        #region Member Variables

        private readonly bool isFirstInstance;
        private readonly SingleInstanceProxy proxy;
        private bool disposed;
        private IChannel ipcChannel;
        private Mutex singleInstanceMutex;

        #endregion

        #region Construction / Destruction

        /// <summary>
        ///   Instantiates a new SingleInstanceTracker object.
        ///   When using this constructor it is assumed that there is no need for sending messages to the first application instance.
        ///   To enable the tracker's ability to send messages to the first instance, use the SingleInstanceTracker(string name, SingleInstanceEnforcerRetriever enforcerRetriever) constructor overload instead.
        /// </summary>
        /// <param name="name"> The unique name used to identify the application. </param>
        /// <exception cref="System.ArgumentNullException">name is null or empty.</exception>
        /// <exception cref="SingleInstancing.SingleInstancingException">A general error occured while trying to instantiate the SingleInstanceInteractor. See InnerException for more details.</exception>
        public SingleInstanceTracker(string name)
            : this(name, null)
        {
        }

        /// <summary>
        ///   Instantiates a new SingleInstanceTracker object.
        ///   When using this constructor overload and enforcerRetriever is null, the SingleInstanceTracker object can only be used to determine whether the application is already running.
        /// </summary>
        /// <param name="name"> The unique name used to identify the application. </param>
        /// <param name="enforcerRetriever"> The method which would be used to retrieve an ISingleInstanceEnforcer object when instantiating the new object. </param>
        /// <exception cref="System.ArgumentNullException">name is null or empty.</exception>
        /// <exception cref="SingleInstancing.SingleInstancingException">A general error occured while trying to instantiate the SingleInstanceInteractor. See InnerException for more details.</exception>
        public SingleInstanceTracker(string name, SingleInstanceEnforcerRetriever enforcerRetriever)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name", "name cannot be null or empty.");

            try
            {
                this.singleInstanceMutex = new Mutex(true, name, out this.isFirstInstance);

                // Do not attempt to construct the IPC channel if there is no need for messages
                if (enforcerRetriever != null)
                {
                    string proxyObjectName = "SingleInstanceProxy";
                    string proxyUri = "ipc://" + name + "/" + proxyObjectName;

                    // If no previous instance was found, create a server channel which will provide the proxy to the first created instance
                    if (this.isFirstInstance)
                    {
                        // Create an IPC server channel to listen for SingleInstanceProxy object requests
                        this.ipcChannel = new IpcServerChannel(name);
                        // Register the channel and get it ready for use
                        ChannelServices.RegisterChannel(this.ipcChannel, false);
                        // Register the service which gets the SingleInstanceProxy object, so it can be accessible by IPC client channels
                        RemotingConfiguration.RegisterWellKnownServiceType(typeof (SingleInstanceProxy), proxyObjectName,
                                                                           WellKnownObjectMode.Singleton);

                        // Attempt to retrieve the enforcer from the delegated method
                        ISingleInstanceEnforcer enforcer = enforcerRetriever();
                        // Validate that an enforcer object was returned
                        if (enforcer == null)
                        {
                            throw new InvalidOperationException(
                                "The method delegated by the enforcerRetriever argument returned null. The method must return an ISingleInstanceEnforcer object.");
                        }

                        // Create the first proxy object
                        this.proxy = new SingleInstanceProxy(enforcer);
                        // Publish the first proxy object so IPC clients requesting a proxy would receive a reference to it
                        RemotingServices.Marshal(this.proxy, proxyObjectName);
                    }
                    else
                    {
                        // Create an IPC client channel to request the existing SingleInstanceProxy object.
                        this.ipcChannel = new IpcClientChannel();
                        // Register the channel and get it ready for use
                        ChannelServices.RegisterChannel(this.ipcChannel, false);

                        // Retreive a reference to the proxy object which will be later used to send messages
                        this.proxy = (SingleInstanceProxy) Activator.GetObject(typeof (SingleInstanceProxy), proxyUri);

                        // Notify the first instance of the application that a new instance was created
                        this.proxy.Enforcer.OnNewInstanceCreated(new EventArgs());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SingleInstancingException(
                    "Failed to instantiate a new SingleInstanceTracker object. See InnerException for more details.", ex);
            }
        }

        /// <summary>
        ///   Releases all resources used by the object.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Releases all unmanaged resources used by the object.
        /// </summary>
        ~SingleInstanceTracker()
        {
            this.Dispose(false);
        }

        /// <summary>
        ///   Releases all unmanaged resources used by the object, and potentially releases managed resources.
        /// </summary>
        /// <param name="disposing"> true to dispose of managed resources; otherwise false. </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.singleInstanceMutex != null)
                    {
                        this.singleInstanceMutex.Close();
                        this.singleInstanceMutex = null;
                    }

                    if (this.ipcChannel != null)
                    {
                        ChannelServices.UnregisterChannel(this.ipcChannel);
                        this.ipcChannel = null;
                    }
                }

                this.disposed = true;
            }
        }

        #endregion

        #region Member Functions

        /// <summary>
        ///   Sends a message to the first instance of the application.
        /// </summary>
        /// <param name="message"> The message to send to the first instance of the application. The message must be serializable. </param>
        /// <exception cref="System.InvalidOperationException">The object was constructed with the SingleInstanceTracker(string name) constructor overload, or with the SingleInstanceTracker(string name, SingleInstanceEnforcerRetriever enforcerRetriever) cosntructor overload, with enforcerRetriever set to null.</exception>
        /// <exception cref="SingleInstancing.SingleInstancingException">The SingleInstanceInteractor has failed to send the message to the first application instance. The first instance might have terminated.</exception>
        public void SendMessageToFirstInstance(object message)
        {
            if (this.disposed)
                throw new ObjectDisposedException("The SingleInstanceTracker object has already been disposed.");

            if (this.ipcChannel == null)
            {
                throw new InvalidOperationException(
                    "The object was constructed with the SingleInstanceTracker(string name) constructor overload, or with the SingleInstanceTracker(string name, SingleInstanceEnforcerRetriever enforcerRetriever) constructor overload, with enforcerRetriever set to null, thus you cannot send messages to the first instance.");
            }

            try
            {
                this.proxy.Enforcer.OnMessageReceived(new MessageEventArgs(message));
            }
            catch (Exception ex)
            {
                throw new SingleInstancingException(
                    "Failed to send message to the first instance of the application. The first instance might have terminated.",
                    ex);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets a value indicating whether this instance of the application is the first instance.
        /// </summary>
        public bool IsFirstInstance
        {
            get
            {
                if (this.disposed)
                    throw new ObjectDisposedException("The SingleInstanceTracker object has already been disposed.");

                return this.isFirstInstance;
            }
        }

        /// <summary>
        ///   Gets the single instance enforcer (the first instance of the application) which would receive messages.
        /// </summary>
        public ISingleInstanceEnforcer Enforcer
        {
            get
            {
                if (this.disposed)
                    throw new ObjectDisposedException("The SingleInstanceTracker object has already been disposed.");

                return this.proxy.Enforcer;
            }
        }

        #endregion
    }
}