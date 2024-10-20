using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Controllers
{
    public class StudentController : Controller
    {
        private string url = "http://localhost:5000/api/StudentAPI/";
        private HttpClient client = new HttpClient();

        [HttpGet]
        public IActionResult Index()
        {
            List<Student> students = new List<Student>();

            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                var data = JsonConvert.DeserializeObject<List<Student>>(result);

                if (data != null)
                {
                    students = data;
                }
            }
            else
            {
                // Error Handling: Add a proper view or message if API fails
                return View("Error");
            }

            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student std)
        {
            string data = JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["insert_message"] = "Student Added.";
                return RedirectToAction("Index");
            }
            else
            {
                // Handle error, return view with some error message
                ViewBag.ErrorMessage = "There was a problem adding the student.";
                return View(std); // Returning the same view with model data
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result); // For a single student

                if (data != null)
                {
                    std = data;
                }
                return View(std);
            }
            else
            {
                // Handle error if API fails
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult Edit(Student std)
        {
            string data = JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(url+std.Id, content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["Update_message"] = "Student Updated...";
                return RedirectToAction("Index");
            }
            else
            {
                // Handle error, return view with some error message
                ViewBag.ErrorMessage = "There was a problem adding the student.";
                return View(std); // Returning the same view with model data
            }
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result); // For a single student

                if (data != null)
                {
                    std = data;
                }
                return View(std);
            }
            else
            {
                // Handle error if API fails
                return View("Error");
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student std = new Student();
            HttpResponseMessage response = client.GetAsync(url + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result); // For a single student

                if (data != null)
                {
                    std = data;
                }
                return View(std);
            }
            else
            {
                // Handle error if API fails
                return View("Error");
            }
        }
        [HttpPost, ActionName ("Delete")]
        public IActionResult DeleteConfrimed(int id)
        {
            
            HttpResponseMessage response = client.DeleteAsync(url + id).Result;
            if(response.IsSuccessStatusCode)
            {
                TempData["Delete_message"] = "Student Deleted...";
                return RedirectToAction("Index");
            }
             return View("Error");
            
        }
    }
}
