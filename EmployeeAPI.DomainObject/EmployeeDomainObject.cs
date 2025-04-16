using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.DomainObject
{
    [Table("Employee")]
    public class EmployeeDomainObject
    {
        [Key]
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Departement { get; set; }
        public string Email { get; set; }
    }
}
