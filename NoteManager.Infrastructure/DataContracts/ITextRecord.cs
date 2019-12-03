namespace NoteManager.Infrastructure.DataContracts
{
    public interface ITextRecord : IDataRecord
    {
        string TextData { get; set; }
    }
}
