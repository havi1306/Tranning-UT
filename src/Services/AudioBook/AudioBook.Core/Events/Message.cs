using System;
using System.Collections.Generic;
using System.Text;

namespace AudioBook.Core.Events
{
    public abstract class Message
    {
        public string MessageType { get; set; }

        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        protected Message()
        {
            MessageType = GetType().Name;
            Timestamp = DateTime.Now;
        }
    }
}
