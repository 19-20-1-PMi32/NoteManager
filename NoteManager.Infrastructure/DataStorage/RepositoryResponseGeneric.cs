using System;

namespace NoteManager.Infrastructure.DataStorage
{
    public class RepositoryResponse<ResponseDataType> : RepositoryResponse
    {
        public RepositoryResponse(string message, ResponseStatus status, ResponseDataType data)
            : base(message, status)
        {
            if (data != null)
            {
                Data = data;
            }
            else
            {
                throw new ArgumentException("Data is null", "data");
            }
        }

        public ResponseDataType Data { get; set; }
    }
}
