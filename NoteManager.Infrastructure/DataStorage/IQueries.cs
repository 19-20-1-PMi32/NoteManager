using NoteManager.Infrastructure.DataContracts;

using System;
using System.Collections.Generic;

namespace NoteManager.Infrastructure.DataStorage
{
    public interface IQueries : IAuthorizable
    {
        /// <summary>
        /// Checks if user credentials maches an existing user in storage. If provided credentials 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        RepositoryResponse AuthorizeUser(string login, string password);

        /// <summary>
        /// Get users DailyNote by its date
        /// </summary>
        /// <returns></returns>
        RepositoryResponse<IDailyNote> GetDailyNotes(DateTime assignmentDate);

        /// <summary>
        /// Get text records (title, editDate), from specific DailyNote
        /// </summary>
        /// <param name="dailyNoteDate">DailyNote's date</param>
        /// <returns></returns>
        RepositoryResponse<IEnumerable<IDataRecord>> GetTextRecords(DateTime dailyNoteDate);

        /// <summary>
        /// Get media records (title, editDate), from spesific DailyNote by it's MediaType
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="userPassword"></param>
        /// <param name="dailyNoteDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        RepositoryResponse<IEnumerable<IDataRecord>> GetMediaRecords(DateTime dailyNoteDate, MediaType type);

        /// <summary>
        /// Get ITextData by it's label and date of parent DailyRecord
        /// </summary>
        /// <param name="label"></param>
        /// <param name="dailyNoteDate"></param>
        /// <returns></returns>
        RepositoryResponse<ITextRecord> GetTextData(string label, DateTime dailyNoteDate);

        /// <summary>
        /// Get IMediaData with specified MediaType by it's label and date of parent DailyRecord 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="dailyNoteDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        RepositoryResponse<ITextRecord> GetTextData(string label, DateTime dailyNoteDate, MediaType type);
    }
}
