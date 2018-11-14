using System;
using System.Collections.Generic;

namespace Webapp.Models
{
    public partial class Employee
    {
        public string Gid { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string ManagerGid { get; set; }
        public string ManagerName { get; set; }
        public string Costcentre { get; set; }
        public string Grade { get; set; }
        public int EntityStatusId { get; set; }
        public string Password { get; set; }
        public int? EntityTypeId { get; set; }
        public string Username { get; set; }
        public int DepartmentId { get; set; }
        public int LocationId { get; set; }
    }
}
