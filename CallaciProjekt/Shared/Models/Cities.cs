using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallaciProjekt.Shared.Models
{
    [FirestoreData]
    public class Cities
    {
        public string CityName { get; set; }
    }
}
