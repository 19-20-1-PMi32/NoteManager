using System;

namespace NoteManager.Infrastructure.DataStorage
{
    public class RepositoryResponse
    {
        public RepositoryResponse(string message, ResponseStatus status)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Message = message;
            }
            else
            {
                throw new ArgumentException("Message is null or empty", "message");
            }

            Status = status;
        }

        public string Message { get; set; }

        public ResponseStatus Status { get; set; }
    }
}
