using System.Data.Common;

namespace TecNM.NotesApp.Api.DataAccess.Interfaces;

public interface IDbContext
{
    DbConnection Connection { get; }
}