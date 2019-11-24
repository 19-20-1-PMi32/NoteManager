using System;

namespace NoteManager.Infrastructure.DataContracts
{
    public interface IDataRecord
    {
        string Title { get; set; }

        DateTime? LastEditDate { get; set; }
    }
}
