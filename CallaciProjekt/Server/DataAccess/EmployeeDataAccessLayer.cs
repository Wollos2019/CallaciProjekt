using CallaciProjekt.Shared;
using CallaciProjekt.Shared.Models;
using Google.Cloud.Firestore;

using System.Text.Json;

namespace CallaciProjekt.Server.DataAccess
{
    public class EmployeeDataAccessLayer
    {
        string projectId;
        FirestoreDb firestoreDb;
        public EmployeeDataAccessLayer()
        {
            string filePath = "C:\\Users\\Ndoko\\Documents\\MesProjects\\Mes clés\\callaci-firebase-adminsdk-awe7t-1b68e871a7.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filePath);
            projectId = "callaci";
            firestoreDb = FirestoreDb.Create(projectId);
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            try
            {
                Query employeeQuery = firestoreDb.Collection("Employees");
                QuerySnapshot employeeQuerySnapshot = await employeeQuery.GetSnapshotAsync();
                List<Employee> listEmployee = new List<Employee>();
                foreach(DocumentSnapshot documentSnapshot in employeeQuerySnapshot.Documents)
                {
                    if(documentSnapshot.Exists)
                    {
                        Dictionary<string, object> employee = documentSnapshot.ToDictionary();
                        string json = JsonSerializer.Serialize(employee);
                        Employee? newUser = JsonSerializer.Deserialize<Employee>(json);
                        newUser.EmployeeId = documentSnapshot.Id;
                        newUser.date = documentSnapshot.CreateTime.Value.ToDateTime();
                        listEmployee.Add(newUser);


                    }
                }
                List<Employee> storedEmployeeList = listEmployee.OrderBy(x => x.date).ToList();
                return storedEmployeeList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async void addEmployee(Employee employee)
        {
            try
            {
                CollectionReference colRef = firestoreDb.Collection("Employees");
                await colRef.AddAsync(employee);
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
        }

        public async void UpdateEmployee(Employee employee)
        {
            try
            {
                DocumentReference empRef = firestoreDb.Collection("employees").Document(employee.EmployeeId);
                await empRef.SetAsync(employee, SetOptions.Overwrite);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Employee> GetEmployeeData (string id)
        {
            try
            {
                DocumentReference docRef = firestoreDb.Collection("employees").Document(id);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
                if(snapshot.Exists)
                {
                    Employee emp = snapshot.ConvertTo<Employee>();
                    emp.EmployeeId = snapshot.Id;
                    return emp;
                }
                else
                {
                    return new Employee();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async void DeleteEmployee(string id)
        {
            try
            {
                DocumentReference empRef = firestoreDb.Collection("employees").Document(id);
                await empRef.DeleteAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Cities>> GetCityData()
        {
            try
            {
                Query citiesQuery = firestoreDb.Collection("cities");
                QuerySnapshot citiesQuerySnapShot = await citiesQuery.GetSnapshotAsync();
                List<Cities> listCity = new List<Cities> ();
                foreach (DocumentSnapshot documentSnapshot in citiesQuerySnapShot.Documents)
                {
                    if (documentSnapshot.Exists)
                    {
                        Dictionary<string, object> city = documentSnapshot.ToDictionary();
                        string json = JsonSerializer.Serialize(city);
                        Cities? newCity = JsonSerializer.Deserialize<Cities>(json);
                        listCity.Add(newCity);
                    }
                }
                return listCity;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
