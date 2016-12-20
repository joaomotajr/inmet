// ============================================
// 
// FlexImage.Core
// WaitCursor.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

#endregion

namespace FlexImage.Core
{
    ///<summary>
    ///  Utility class to make showing (usually) a Wait Cursor much simpler and to remove the
    ///  possibility of the Cursor not being restored due to an uncaught exception or forgetfulness to restore
    ///  the cursor manually.
    /// 
    ///  2 Possible uses for this class :-
    /// 
    ///  1.  Single instance usage of the StCursor ..
    ///  Instead of
    /// 
    ///  public void DoSomeLengthyWork()
    ///  {
    ///  try
    ///  {
    ///  Screen.Cursor = Cursors.Wait;
    ///			
    ///  SlowlyCountToTenBillion();
    ///  }
    ///  finally
    ///  {
    ///  Screen.Cursor = Cursors.Default;
    ///  }
    ///  }
    /// 
    ///  do this ..
    /// 
    ///  public void DoSomeLengthyWork()
    ///  {
    ///  using (new StCursor(Cursors.Wait, new TimeSpan(0, 0, 0, 0, 100)))
    ///  {
    ///  SlowlyCountToTenBillion();
    ///  }
    ///  }
    /// 
    ///  Above code will show the Wait cursor after 100ms of 'work'.  
    ///  It makes use of the 'using' statement and IDispose to *make sure* the Cursor is always restored
    ///
    ///  2.  Global usage of the StCursor (<see cref="ApplicationWaitCursor" /> class for usage)
    ///</summary>
    public class StCursor : StThreadAttachedDelayedCallback, IDelayedCallbackHandler
    {
        #region Consts

        public static readonly TimeSpan DEFAULT_DELAY = new TimeSpan(0, 0, 0, 0, 500);

        #endregion

        #region Member Variables

        private Cursor _newCursor; // New cursor to show
        private Cursor _oldCursor; // Remember old cursor

        #endregion

        #region Constructors

        /// <summary>
        ///   Member initialising Constructor
        /// </summary>
        /// <param name="newCursor"> The Cursor to use </param>
        /// <param name="delay"> Delay period before showing Cursor </param>
        /// <param name="enabled"> Enable or Not </param>
        public StCursor(Cursor newCursor, TimeSpan delay, bool enabled)
            : base(delay, enabled)
        {
            this._newCursor = newCursor;
        }

        /// <summary>
        ///   Member initialising Constructor
        /// </summary>
        /// <param name="newCursor"> The Cursor to use </param>
        /// <param name="delay"> Delay period before showing Cursor </param>
        public StCursor(Cursor newCursor, TimeSpan delay)
            : this(newCursor, delay, true)
        {
        }

        /// <summary>
        ///   Member initialising Constructor
        /// </summary>
        /// <param name="newCursor"> The Cursor to use </param>
        public StCursor(Cursor newCursor)
            : this(newCursor, DEFAULT_DELAY)
        {
        }

        /// <summary>
        ///   Member initialising Constructor
        /// </summary>
        /// <param name="newCursor"> The Cursor to use </param>
        /// <param name="enabled"> Enable or Not </param>
        public StCursor(Cursor newCursor, bool enabled)
            : this(newCursor, DEFAULT_DELAY, enabled)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///   Start showing the Cursor now
        /// </summary>
        public override void Start()
        {
            base.Start();
            this._oldCursor = Cursor.Current;
            Cursor.Current = this._newCursor;
        }

        /// <summary>
        ///   Finish showing the Cursor (switch back to previous Cursor)
        /// </summary>
        public override void Finish()
        {
            Cursor.Current = this._oldCursor;
            base.Finish();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Get/Set the Cursor to show
        /// </summary>
        public Cursor Cursor
        {
            get { return this._newCursor; }
            set { this._newCursor = value; }
        }

        #endregion
    }

    ///<summary>
    ///  Singleton Utility class which is used to show a Wait Cursor when the Application is busy.  
    ///  If the Application is busy then the Idle event will not be called during the busy period 
    ///  and hence the Screen Cursor is automatically changed to a (by default) WaitCursor.
    ///
    ///  To use, simply insert the following line in your Application startup code
    /// 
    ///  ApplicationWaitCursor.Cursor = Cursors.Wait;
    ///  ApplicationWaitCursor.Delay  = new TimeSpan(0, 0, 0, 0, 100);
    ///		
    ///  This installs a StCursor to activate after 100ms of 'work' (Application.Idle not being called)
    ///</summary>
    public class ApplicationWaitCursor : IMessageFilter
    {
        #region Member Variables

        /// <summary>
        ///   None Client Area Button Down Windows Message
        /// </summary>
        private const int WM_NCLBUTTONDOWN = 0xa1;

        /// <summary>
        ///   The Cursor to use during busy periods
        /// </summary>
        private static readonly StCursor _cursor = new StCursor(Cursors.WaitCursor, false);

        private static readonly EventHandler _applicationIdleEventHandler;
        private static readonly ApplicationWaitCursor _singleton;

        #endregion

        #region Constructors

        /// <summary>
        ///   Static constructor which attaches to the Singleton Application instance
        /// </summary>
        static ApplicationWaitCursor()
        {
            _singleton = new ApplicationWaitCursor();
            _applicationIdleEventHandler = OnApplicationIdle;
        }

        /// <summary>
        ///   Default Constructor.  Hidden
        /// </summary>
        private ApplicationWaitCursor()
        {
        }

        #endregion

        #region Public Static Properties

        /// <summary>
        ///   Gets and Sets the Cursor to use during Application busy periods.  Setting this to NULL will disable the
        ///   monitoring of busy periods.
        /// </summary>
        public static Cursor Cursor
        {
            get { return _cursor.Cursor; }
            set
            {
                if ((value != null) && !_cursor.Enabled)
                {
                    Application.Idle += _applicationIdleEventHandler;
                    Application.AddMessageFilter(_singleton);
                }
                else if ((value == null) && _cursor.Enabled)
                {
                    Application.Idle -= _applicationIdleEventHandler;
                    Application.RemoveMessageFilter(_singleton);
                }

                _cursor.Cursor = value;
                _cursor.Enabled = value != null;
            }
        }

        /// <summary>
        ///   Get/Set the period of Time to wait before showing the WaitCursor whilst Application is working
        /// </summary>
        public static TimeSpan Delay
        {
            get { return _cursor.Delay; }
            set { _cursor.Delay = value; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///   Pre-Filters Windows messages.  During Window Moves/Resizes the Application Idle is not called (appears busy)
        ///   so we filter for these events so we can temporarily turn off the WaitCursor
        /// </summary>
        bool IMessageFilter.PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_NCLBUTTONDOWN)
                _cursor.Enabled = false;
            else
                _cursor.Enabled = true;

            // Always let the real Message through
            return false;
        }

        /// <summary>
        ///   Process the Idle event.  Simply reset the StWaitCursor
        /// </summary>
        private static void OnApplicationIdle(object sender, EventArgs e)
        {
            _cursor.Reset();
        }

        #endregion
    }

    /// <summary>
    ///   Implement this interface to participate with a StDelayedCallback instance
    /// </summary>
    public interface IDelayedCallbackHandler
    {
        void Start();
        void Finish();
    }

    /// <summary>
    ///   This class manages a IDelayedCallbackHandler.  After a specified delay the 
    ///   <see cref="IDelayedCallbackHandler.Start" /> method is called.
    ///   If the <see cref="IDelayedCallbackHandler.Start" /> is called then this guarantees that the 
    ///   <see cref="IDelayedCallbackHandler.Finish" /> method is called when this instance is Disposed.
    ///   <seealso cref="StCursor" /> for an implementation
    /// </summary>
    public class StDelayedCallback : IDisposable
    {
        #region Member Variables

        /// <summary>
        ///   WaitHandle for notifications
        /// </summary>
        private readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);

        /// <summary>
        ///   The callback
        /// </summary>
        private IDelayedCallbackHandler _callbackHandler;

        /// <summary>
        ///   Thread to perform the wait and callback
        /// </summary>
        private Thread _callbackThread;

        /// <summary>
        ///   Delay to wait before calling back
        /// </summary>
        private TimeSpan _delay;

        /// <summary>
        ///   Have we been Disposed or not ?
        /// </summary>
        private bool _disposed;

        /// <summary>
        ///   Enabled or not ?
        /// </summary>
        private bool _enabled = true;

        /// <summary>
        ///   Has callback Start been called ?
        /// </summary>
        private bool _startCalled;

        #endregion

        #region Constructors

        /// <summary>
        ///   Default Constructor.  Hidden.
        /// </summary>
        protected StDelayedCallback()
        {
            // Derived Class MUST call Init in their constructor	
        }

        /// <summary>
        ///   Creates a StDelayedCallback instance prepared with a <see cref="IDelayedCallbackHandler" /> and the specified <see
        ///    cref="TimeSpan" /> delay
        /// </summary>
        /// <param name="callbackHandler"> The CallbackHandler to use </param>
        /// <param name="delay"> Initial Delay value </param>
        /// <param name="enabled"> Initial Enabled state </param>
        public StDelayedCallback(IDelayedCallbackHandler callbackHandler, TimeSpan delay, bool enabled)
        {
            this.Init(callbackHandler, delay, enabled);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        ///   Prepares the class.  Creates the Thread that will call Start & Finish
        /// </summary>
        /// <param name="callbackHandler"> </param>
        /// <param name="delay"> </param>
        /// <param name="enabled"> </param>
        protected void Init(IDelayedCallbackHandler callbackHandler, TimeSpan delay, bool enabled)
        {
            this._callbackHandler = callbackHandler;
            this._delay = delay;
            this._enabled = enabled;

            this._callbackThread = new Thread(this.CallbackThread);
            this._callbackThread.Name = this.GetType().Name + " DelayedCallback Thread";
            this._callbackThread.IsBackground = true;
            this._callbackThread.Start();
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///   Thread method.  Loops calling Start & Finish until Disposed, honours the Enabled flag
        /// </summary>
        private void CallbackThread()
        {
            do
            {
                // Initial State around loop
                this._startCalled = false;

                this.WaitToStart();
                if (this._startCalled)
                    this.WaitForReset();
            } while (!this._disposed);
        }

        /// <summary>
        ///   Waits for either the ResetEvent or the Wait period to expire.  If Wait period expires then Start is called
        /// </summary>
        private void WaitToStart()
        {
            bool waited = this._resetEvent.WaitOne(this._delay, false);
            this._resetEvent.Reset();

            if (!waited)
            {
                if (this._enabled)
                {
                    try
                    {
                        this._callbackHandler.Start();
                    }
                    finally
                    {
                        this._startCalled = true;
                    }
                }
            }
        }

        /// <summary>
        ///   Waits for the ResetEvent (set by Dispose & Reset), since Start has been called we *have* to call Finish
        /// </summary>
        private void WaitForReset()
        {
            this._resetEvent.WaitOne();
            this._resetEvent.Reset();

            // Always calls Finish even if we are Disabled or we Aborted since Start/Finish *always* go in Pairs
            this._callbackHandler.Finish();
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///   On Disposal terminates the Thread, calls Finish (on thread) if Start has been called
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
                return;

            this._disposed = true; // Kills the Thread loop
            this._resetEvent.Set();
        }

        /// <summary>
        ///   Resets the Wait period to start Waiting again
        /// </summary>
        public void Reset()
        {
            this._resetEvent.Set();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Enable/Disable the call to Start (note, once Start is called it *always* calls the paired Finish)
        /// </summary>
        public bool Enabled
        {
            get { return this._enabled; }
            set { this._enabled = value; }
        }

        /// <summary>
        ///   Get/Set the period of Time to wait before calling the Start method
        /// </summary>
        public TimeSpan Delay
        {
            get { return this._delay; }
            set { this._delay = value; }
        }

        #endregion
    }

    /// <summary>
    ///   Base class for StDelayedCallback classes that require ThreadInput from the Main thread
    /// 
    ///   This class is a client of itself in that it implements the IDelayedCallbackHandler interface.
    /// </summary>
    public class StThreadAttachedDelayedCallback : StDelayedCallback, IDelayedCallbackHandler
    {
        #region Member Variables

        /// <summary>
        ///   GUI Thread Id
        /// </summary>
        private readonly uint _mainThreadId;

        /// <summary>
        ///   Callback Thread Id
        /// </summary>
        private uint _callbackThreadId;

        #endregion

        #region PInvoke imports

        [DllImport("USER32.DLL")]
        private static extern uint AttachThreadInput(uint attachTo, uint attachFrom, bool attach);

        [DllImport("KERNEL32.DLL")]
        private static extern uint GetCurrentThreadId();

        #endregion

        #region Constructors

        /// <summary>
        ///   Member Initialising Constructor.
        /// </summary>
        /// <param name="delay"> Delay to wait for </param>
        /// <param name="enabled"> Enabled or not </param>
        public StThreadAttachedDelayedCallback(TimeSpan delay, bool enabled)
        {
            // Constructor is called from (what is treated as) the Main thread, grab its Thread Id
            this._mainThreadId = GetCurrentThreadId();
            base.Init(this, delay, enabled);
        }

        /// <summary>
        ///   Member Initialising Constructor.
        /// </summary>
        /// <param name="delay"> Delay to wait for </param>
        public StThreadAttachedDelayedCallback(TimeSpan delay)
            : this(delay, true)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///   Start.  Called when the Delay has expired and operation is to begin.
        ///   This implementation attaches this Thread to the Main Thread's Input.
        /// </summary>
        public virtual void Start()
        {
            // Start is called in a new Thread, grab the new Thread Id so we can attach to Main thread's input
            this._callbackThreadId = GetCurrentThreadId();
            AttachThreadInput(this._callbackThreadId, this._mainThreadId, true);
        }

        /// <summary>
        ///   Finish.  Called when the operation is to finish (usually IDispose)
        ///   This implementation detaches this Thread from the Main Thread's Input.
        /// </summary>
        public virtual void Finish()
        {
            // Detach from Main thread input
            AttachThreadInput(this._callbackThreadId, this._mainThreadId, false);
        }

        #endregion
    }
}