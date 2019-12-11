using System;

namespace NoteManager.Infrastructure.DataContracts
{
    public interface IReminder : ITextRecord
    {
        DateTime TriggerDate { get; set; }
    }
}
