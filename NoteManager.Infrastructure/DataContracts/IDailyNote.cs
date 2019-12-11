using System;
using System.Collections.Generic;

namespace NoteManager.Infrastructure.DataContracts
{
    public interface IDailyNote : IDataRecord
    {
        IEnumerable<ITextRecord> TextRecords { get; }

        IEnumerable<IMediaRecord> MediaRecords { get; }

        DateTime AssignedDate { get; set; }
    }
}
