using ClinicTool.DataAccess.UnitOfWork.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ClinicTool.DataAccess.UnitOfWork.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration configuration;

        public UnitOfWork(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IUnitOfWorkAdapter Create()
        {
            var connectionString = configuration.GetConnectionString("SqlConnectionString");

            return new UnitOfWorkAdapter(connectionString);
        }
    }
}

