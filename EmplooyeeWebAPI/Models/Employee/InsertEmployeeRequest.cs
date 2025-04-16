namespace EmplooyeeWebAPI.Models.Employee
{
    public class InsertEmployeeRequest
    {
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Departement { get; set; }
        public string Email { get; set; }
    }
}
