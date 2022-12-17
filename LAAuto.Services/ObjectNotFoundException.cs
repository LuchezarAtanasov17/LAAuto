using System.Runtime.Serialization;

namespace LAAuto.Services
{
    /// <summary>
    /// Represents an object not found exception.
    /// </summary>
    public class ObjectNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectNotFoundException"/> class.
        /// </summary>
        public ObjectNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectNotFoundException"/> class with specified message.
        /// </summary>
        public ObjectNotFoundException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectNotFoundException"/> class with specified message and/or inner exception.
        /// </summary>
        public ObjectNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectNotFoundException"/> class with specified serialization info and streaming context.
        /// </summary>
        protected ObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
