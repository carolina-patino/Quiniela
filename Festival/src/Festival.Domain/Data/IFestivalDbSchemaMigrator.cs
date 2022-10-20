using System.Threading.Tasks;

namespace Festival.Data;

public interface IFestivalDbSchemaMigrator
{
    Task MigrateAsync();
}
