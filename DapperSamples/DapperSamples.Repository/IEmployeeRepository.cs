using System.Threading.Tasks;

namespace DapperSamples.Repository
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetById(int id);
    }
}