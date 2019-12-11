using NoteManager.Infrastructure.DataContracts;
using NoteManager.Infrastructure.DataStorage;
using NoteManager.Persistence.DataModel;

using System;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace NoteManager.Pesistence.API
{
    public class Commands : ICommands
    {
        private UserIdentity _authorizedIdentity;
        private IDbContextFactory<NoteManagerDatabase> _contextFactory;

        public Commands(IDbContextFactory<NoteManagerDatabase> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void Authorize(string login, string password)
        {
            using (var context = _contextFactory.Create())
            {
                var existingIdentity = context.UserIdentities.FirstOrDefault(x => x.Login == login);
                if (existingIdentity != null && existingIdentity.Password == PasswordHasher.GetHash(password))
                {
                    _authorizedIdentity = existingIdentity;
                }
            }
        }

        public RepositoryResponse AddMediaRecord(ref IMediaRecord mediaRecord, DateTime dailyNoteTime)
        {
            throw new NotImplementedException();
        }

        public RepositoryResponse AddTextRecord(ref ITextRecord textRecord, DateTime dailyNoteTime)
        {
            using (var context = _contextFactory.Create())
            {
                if (_authorizedIdentity != null)
                {
                    var dailyRecord = context.DailyRecords.FirstOrDefault(x => x.Date == dailyNoteTime);
                    if (dailyRecord == null)
                    {
                        if (_authorizedIdentity.UserData == null)
                        {
                            _authorizedIdentity.UserData = new UserData()
                            {
                                UserIdentity = _authorizedIdentity
                            };
                        }
                        var newDailyRecord = new DailyRecord()
                        {
                            Date = dailyNoteTime,
                            Title = "NewTitle"
                        };

                        _authorizedIdentity.UserData.DailyRecords.Add(newDailyRecord);
                    }
                }
                return null;
            }
        }

        public RepositoryResponse CreateNewUser(string login, string password)
        {
            using (var context = _contextFactory.Create())
            {
                if (_authorizedIdentity == null)
                {
                    var identity = new UserIdentity
                    {
                        Login = login,
                        Password = PasswordHasher.GetHash(password)
                    };

                    context.UserIdentities.Add(identity);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        return new RepositoryResponse(e.Message, ResponseStatus.Fail);
                    }

                    return new RepositoryResponse("User was successfully created", ResponseStatus.Success);
                }
                else
                {
                    return new RepositoryResponse("User with this login already exists", ResponseStatus.Fail);
                }
            }
        }
    }
}
