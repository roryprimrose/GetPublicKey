using System;

namespace GetPublicKey
{
    /// <summary>
    /// The <see cref="EventTraceListenerEventArgs"/>
    /// class is used to define the message written to the <see cref="EventTraceListener"/>.
    /// </summary>
    internal class EventTraceListenerEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventTraceListenerEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public EventTraceListenerEventArgs(String message)
        {
            Message = message;
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public String Message
        {
            get;
            set;
        }
    }
}