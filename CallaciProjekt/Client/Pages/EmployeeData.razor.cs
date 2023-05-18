using CallaciProjekt.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace CallaciProjekt.Client.Pages
{
    public class EmployeeDataModel : ComponentBase
    {
        [Inject]
        public HttpClient? HttpClient { get; set; }
        protected List<Employee> empList = new List<Employee>();
        protected Employee employee = new Employee();
        protected string? SearchString { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await GetEmployeeList();
        }
        protected async Task GetEmployeeList()
        {
            empList = await HttpClient.GetFromJsonAsync<List<Employee>>("api/Employee");
        }

        protected async Task SearchEmployee()
        {
            await GetEmployeeList();
            if(!string.IsNullOrEmpty(SearchString))
            {
                empList = empList.Where(x => x.EmployeeName.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) != -1).ToList();
            }
        }
    }
}
