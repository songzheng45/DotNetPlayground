using System;

namespace DapperSamples.Repository
{
    public class Employee
    {
        public int emp_no { get; set; }

        public DateTime birth_date { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public Gender gender { get; set; }

        public DateTime hire_date { get; set; }
    }

    public enum Gender
    {
        M,
        F
    }
}