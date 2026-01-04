using AspCoreConsumeWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace AspCoreConsumeWebApi.Controllers
{
    public class EmployeeController : Controller
    {

        //private string baseUrl = "https://localhost:7253/api/";
        //private HttpClient client=new HttpClient();
        private HttpClient _client;
        public EmployeeController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("ApiClient");
        }
        public async Task<IActionResult> Index()
        {
            var responce = await _client.GetAsync("employee/");
            if (responce.IsSuccessStatusCode)
            {
                string data = await responce.Content.ReadAsStringAsync();
                if (data != null) {
                    var employee = JsonConvert.DeserializeObject<List<Employee>>(data);
                    if (employee != null)
                    {
                        return View(employee);
                    }
                    ViewBag.ErrorMessage = "Employee is not found";

                }

            }
            return BadRequest("Data not found");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (employee == null) {
                ViewBag.ErrorMessage = "Invalid data";
                return View(employee);
            }
            var content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
            var responce = await _client.PostAsync("employee", content);
            if (responce.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Employee added successfully";
                return RedirectToAction("Index");
            }
            ViewBag.ErrorMessage = "failed to create employee ";
            return View(employee);

        }

        public async Task<IActionResult> Edit(int Id)
        {
            var responce = await _client.GetAsync($"employee/{Id}");
            if (responce.IsSuccessStatusCode)
            {
                string data = await responce.Content.ReadAsStringAsync();
                if (data != null)
                {
                    var employee = JsonConvert.DeserializeObject<Employee>(data);
                    if (employee != null)
                    {
                        return View(employee);
                    }
                    ViewBag.ErrorMessage = "Employee is not found";

                }

            }
            return BadRequest("Data not found");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (employee == null)
            {
                ViewBag.ErrorMessage = "Invalid data";
                return View(employee);
            }
            var content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
            var responce = await _client.PutAsync($"employee/{employee.empId}", content);
            if (responce.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Employee updated successfully";
                return RedirectToAction("Index");
            }
            ViewBag.ErrorMessage = "failed to create employee ";
            return View(employee);

        }

        public async Task<IActionResult> Details(int Id)
        {
            var responce = await _client.GetAsync($"employee/{Id}");
            if (responce.IsSuccessStatusCode)
            {
                string data = await responce.Content.ReadAsStringAsync();
                if (data != null)
                {
                    var employee = JsonConvert.DeserializeObject<Employee>(data);
                    if (employee != null)
                    {
                        return View(employee);
                    }
                    ViewBag.ErrorMessage = "Employee is not found";

                }

            }
            return BadRequest("Data not found");
        }
        public async Task<IActionResult> Delete(int Id)
        {
            var response = await _client.DeleteAsync($"employee/{Id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Employee Deleted";
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to delete employee";
            }
            return RedirectToAction("Index");
        }

    }
}
