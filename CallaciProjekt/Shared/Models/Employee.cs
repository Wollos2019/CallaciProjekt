using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallaciProjekt.Shared.Models
{
    [FirestoreData]
    public class Employee
    {
        public string? EmployeeId { get; set; } 
        public DateTime date { get; set;}
        [FirestoreProperty]
        public string EmployeeName { get; set; } = string.Empty;
        [FirestoreProperty]
        public string CityName { get; set; } = string.Empty;
        [FirestoreProperty]
        public string Designation { get; set; } = string.Empty;
        [FirestoreProperty]
        public string Gender { get; set; } = string.Empty;
    }
}
