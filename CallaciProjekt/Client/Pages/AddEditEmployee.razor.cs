using CallaciProjekt.Shared.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Threading;



namespace CallaciProjekt.Client.Pages
{
    public class AddEditEmployeeModel:ComponentBase
    {
        [Inject]
        protected HttpClient Http { get; set; }
        
        [Inject]
        public NavigationManager urlNavigationManager { get; set; }
        [Parameter]
        public string empID { get; set; }
        protected string Title = "Add";
        public Employee emp = new Employee();
        protected List<Cities> cityList = new List<Cities>();

       


        protected override async Task OnInitializedAsync()
        {
            await GetCityList();
        }
        protected override async Task OnParametersSetAsync()
        {
            if(!string.IsNullOrEmpty(empID))
            {
                Title = "Edit";
                emp = await Http.GetFromJsonAsync<Employee>("api/Employee/" +  empID);
            }
        }
        protected async Task GetCityList()
        {
            cityList = await Http.GetFromJsonAsync<List<Cities>>("api/Employee/GetCities");
        }

        protected async Task SaveEmployee()
        {
            if (emp.EmployeeId != null)
            {
                await Http.PutAsJsonAsync("api/Employee/", emp);
            }
            else
            {
                HttpResponseMessage hrm = await Http.PostAsJsonAsync("api/Employee/PostEmp", emp);
                Console.WriteLine(hrm);
            }
            Cancel();
        }
        
        public void Cancel()
        {
            urlNavigationManager.NavigateTo("/employeerecords");
        }
    }
}
