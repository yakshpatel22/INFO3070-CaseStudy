using System;
using System.Collections.Generic;

#nullable disable

namespace HelpdeskDAL
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public byte[] Timer { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
