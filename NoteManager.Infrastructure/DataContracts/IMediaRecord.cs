namespace NoteManager.Infrastructure.DataContracts
{
    public interface IMediaRecord : IDataRecord
    {
        MediaType Type { get; }

        byte[] BinaryData { get; set; }
    }
}
