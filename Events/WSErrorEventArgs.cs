using System;

namespace hitaBot.WS.Events
{
    public class WSErrorEventArgs : EventArgs
    {
        public Exception Exception { get; private set; }

        public WSErrorEventArgs(Exception exception)
        {
            this.Exception = exception;
        }
    }
}