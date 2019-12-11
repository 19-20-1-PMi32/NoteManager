using Microsoft.Extensions.Configuration;

using System;
using System.Data.Entity.Infrastructure;

namespace NoteManager.Persistence.DataModel
{
    public class NoteManagerContextFactory : IDbContextFactory<NoteManagerDatabase>
    {
        public NoteManagerDatabase Create()
        {
            var basePath = AppContext.BaseDirectory;

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            return new NoteManagerDatabase(configuration.GetConnectionString("NoteManagerDatabase"));
        }
    }
}
