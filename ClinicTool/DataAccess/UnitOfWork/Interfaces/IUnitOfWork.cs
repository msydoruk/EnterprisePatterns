using System.Data.SqlClient;

namespace ClinicTool.DataAccess.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        IUnitOfWorkAdapter Create();
    }
}
