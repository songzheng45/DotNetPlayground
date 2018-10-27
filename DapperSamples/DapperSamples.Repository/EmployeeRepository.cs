using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;
using System.Linq;

namespace DapperSamples.Repository
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration config)
            : base(config)
        {

        }

        public async Task<Employee> GetById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT emp_no, birth_date, first_name, last_name, gender, hire_date FROM employees WHERE emp_no = @no";
                conn.Open();
                var result = await conn.QueryAsync<Employee>(sQuery, new { no = id });
                return result.FirstOrDefault();
            }
        }
    }
}