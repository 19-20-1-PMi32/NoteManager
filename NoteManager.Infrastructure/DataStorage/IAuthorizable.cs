namespace NoteManager.Infrastructure.DataStorage
{
    public interface IAuthorizable
    {
        void Authorize(string login, string password);
    }
}
