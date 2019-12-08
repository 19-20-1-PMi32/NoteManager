using NoteManager.Infrastructure.DataContracts;

using System;

namespace NoteManager.Infrastructure.DataStorage
{
    public interface ICommands
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
        /// <param name="userLogin"></param>
        /// <param name="userPassword"></param>
        /// <param name="textRecord"></param>
        /// <param name="dailyNoteTime"></param>
        /// <returns></returns>
        RepositoryResponse AddTextRecord(string userLogin, string userPassword, ref ITextRecord textRecord, DateTime dailyNoteTime);

        /// <summary>
        /// Add MediaRecord of to DailyNote by it's <paramref name="dailyNoteTime"/>
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="binaryData"></param>
        /// <param name="dailyNoteTime"></param>
        /// <returns></returns>
        RepositoryResponse AddMediaRecord(string login, string password, ref IMediaRecord mediaRecord, DateTime dailyNoteTime);
    }
}
