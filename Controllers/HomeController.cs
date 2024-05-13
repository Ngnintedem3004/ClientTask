using ClientTask.Interfaces;
using ClientTask.Models;
using ClientTask.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace ClientTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPerformHttpClient<MTask> _performHttpClient;
        private readonly IDeserialiseTask _deserialiseTask;
        public HomeController(ILogger<HomeController> logger, IPerformHttpClient<MTask> performHttpClient, IDeserialiseTask deserialiseTask)
        {
            _logger = logger;
            _performHttpClient = performHttpClient;
            _deserialiseTask = deserialiseTask;
        }

        public async Task<IActionResult> Index()
        {
            var response1 = await _performHttpClient.GetAsync(ClientTask.Services.Url.GetUrl("https://localhost:7022/api/Tasks"));// reponse de la requete get vers le serveur 
            _logger.LogInformation($"response:{response1.StatusCode},{response1}");
            var apiresponse1 = await SerializeContentTask.SerialiseHttpContent(response1);//serialiser le contenu de la reponse

            var tasks = _deserialiseTask.EnumerableDeserialise(apiresponse1);// deseraliser le contenu de la reponse en objet .net 



            /* var httpClient=new HttpClient();
             var response= await httpClient.GetAsync("https://localhost:7022/api/Tasks");
             _logger.LogInformation($"response:{response.StatusCode}");
             string apiResponse = await response.Content.ReadAsStringAsync();
            var  tasks = JsonConvert.DeserializeObject<IEnumerable<MTask>>(apiResponse);*/


            return View(tasks);
        }
        public ViewResult GetTask() => View();

        [HttpPost]
        public async Task<IActionResult> GetTask(int Id)
        {
            string apiresponse1;
            MTask mTask;
            var response1 = await _performHttpClient.GetAsync(ClientTask.Services.Url.GetUrl("https://localhost:7022/api/Tasks", Id));// reponse de la requete get vers le serveur 
            if (PerformSatusCode.IsOk(response1))
            {
                apiresponse1 = await SerializeContentTask.SerialiseHttpContent(response1);//serialiser le contenu de la reponse
                mTask = _deserialiseTask.Deserialise(apiresponse1);
            }
            else
            {
                ViewBag.StatusCode = response1.StatusCode;
                mTask = new MTask();
            }


            /* MTask mtask = new MTask();
             using (var httpClient = new HttpClient())
             {
                 using (var response = await httpClient.GetAsync("https://localhost:7022/api/Tasks" + Id))
                 {
                     if (response.StatusCode == System.Net.HttpStatusCode.OK)
                     {
                         string apiResponse = await response.Content.ReadAsStringAsync();//serialiser le contenu de la reponse en une chaine de caratère
                         mtask = JsonConvert.DeserializeObject<MTask>(apiResponse);//Deserialiser le contenu de la reponse en un objet .net 
                     }
                     else
                         ViewBag.StatusCode = response.StatusCode;
                 }
             }*/


            return View(mTask);
        }
        public ViewResult AddTask() => View();

        [HttpPost]
        public async Task<IActionResult> AddTask([FromForm] MTask mtask)
        {

            MTask receivedmtask = new MTask();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(mtask), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7022/api/Tasks", content))
                {

                    string apiResponse = await response.Content.ReadAsStringAsync();//serialiser le contenu de la reponse en une chaine de caratère
                    receivedmtask = JsonConvert.DeserializeObject<MTask>(apiResponse);//Deserialiser le contenu de la reponse en un objet .net 
                }
            }
            return View(receivedmtask);

        }
        public async Task<IActionResult> UpdateTask(int Id)
        {
            string apiresponse1;
            MTask mTask;
            var response1 = await _performHttpClient.GetAsync(ClientTask.Services.Url.GetUrl("https://localhost:7022/api/Tasks", Id));// reponse de la requete get vers le serveur 
            if (PerformSatusCode.IsOk(response1))
            {
                apiresponse1 = await SerializeContentTask.SerialiseHttpContent(response1);//serialiser le contenu de la reponse
                mTask = _deserialiseTask.Deserialise(apiresponse1);
            }
            else
            {
                ViewBag.StatusCode = response1.StatusCode;
                mTask = new MTask();
            }

            return View(mTask);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTask( MTask mtask)
        {
            MTask  receivedMtask = new MTask();
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(mtask.Id.ToString()), "Id");
                content.Add(new StringContent(mtask.Name), "Name");
                content.Add(new StringContent(mtask.Description), "Description");
                content.Add(new StringContent(mtask.Priority.ToString()), "Priority");
                content.Add(new StringContent(mtask.DueDate.ToString()), "DueDate");
                content.Add(new StringContent(mtask.status.ToString()), "status");

                using (var response = await httpClient.PutAsync("https://localhost:7022/api/Tasks", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedMtask = JsonConvert.DeserializeObject<MTask>(apiResponse);
                }
            }
            return View(receivedMtask);
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteTask(int Id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("https://localhost:7022/api/Tasks" + Id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
