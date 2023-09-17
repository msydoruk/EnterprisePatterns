using System.Data;

namespace DataTableGateway
{
    public interface IPersonGateway
    {
        DataTable GetAll();

        DataRow GetById(int id);

        DataRow GetByEmail(string email);

        int Add(string firstName, string lastName, string email);

        void Update(int id, string firstName, string lastName, string email);

        bool Delete(int id);
    }
}
