using System.Collections.Generic;

namespace NoteManager.Infrastructure.DataContracts
{
    public interface IUser
    {
        string Login { get; set; }

        string Password { get; set; }

        IEnumerable<IDailyNote> DailyNotes { get; set; }

        IEnumerable<IReminder> Reminders { get; set; }
    }
}
