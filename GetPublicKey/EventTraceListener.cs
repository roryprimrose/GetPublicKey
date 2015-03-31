using System;
using System.Diagnostics;

namespace GetPublicKey
{
    /// <summary>
    /// The <see cref="EventTraceListener"/>
    /// class is used to allow a client to handle an event for when a Trace writes a message.
    /// </summary>
    internal class EventTraceListener : TraceListener
    {
        /// <summary>
        /// Raised when a message is written to the TraceListener.
        /// </summary>
        public event EventHandler<EventTraceListenerEventArgs> MessageWritten;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventTraceListener"/> class.
        /// </summary>
        /// <param name="name">The name of the <see cref="T:System.Diagnostics.TraceListener"/>.</param>
        public EventTraceListener(String name)
            : base(name)
        {
        }

        /// <summary>
        /// When overridden in a derived class, writes the specified message to the listener you create in the derived class.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void Write(String message)
        {
            OnMessageWritten(new EventTraceListenerEventArgs(message));
        }

        /// <summary>
        /// When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void WriteLine(String message)
        {
            OnMessageWritten(new EventTraceListenerEventArgs(message + Environment.NewLine));
        }

        /// <summary>
        /// Raises the <see cref="E:MessageWritten"/> event.
        /// </summary>
        /// <param name="e">The <see cref="GetPublicKey.EventTraceListenerEventArgs"/> instance containing the event data.</param>
        protected virtual void OnMessageWritten(EventTraceListenerEventArgs e)
        {
            if (MessageWritten != null)
            {
                MessageWritten(this, e);
            }
        }
    }
}