using NoteManager.Infrastructure.DataContracts;

using System;

namespace NoteManager.Infrastructure.DataStorage
{
    public interface ICommands : IAuthorizable
    {
        /// <summary>
        /// Creates new user record in storage. Returns true if user was created. Returns false if user with <paramref name="login"/> already exists.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        RepositoryResponse CreateNewUser(string login, string password);

        /// <summary>
        /// Add TextRecord to DailyNote by it's <paramref name="dailyNoteTime"/>
        /// </summary>
        /// <param name="textRecord"></param>
        /// <param name="dailyNoteTime"></param>
        /// <returns></returns>
        RepositoryResponse AddTextRecord(ref ITextRecord textRecord, DateTime dailyNoteTime);

        /// <summary>
        /// Add MediaRecord of to DailyNote by it's <paramref name="dailyNoteTime"/>
        /// </summary>
        /// <param name="binaryData"></param>
        /// <param name="dailyNoteTime"></param>
        /// <returns></returns>
        RepositoryResponse AddMediaRecord(ref IMediaRecord mediaRecord, DateTime dailyNoteTime);
    }
}
