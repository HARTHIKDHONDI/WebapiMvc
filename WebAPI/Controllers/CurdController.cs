using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
namespace WebAPI.Controllers
{
    public class CurdController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Customers> Customers = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7123/api/");
                var responseTask = client.GetAsync("Operations/GetSPCustomers");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Customers>>();
                    readTask.Wait();
                    Customers = readTask.Result;
                }
                else
                {
                    Customers = Enumerable.Empty<Customers>();
                    ModelState.AddModelError(string.Empty, "Server error.Please contact administrator.");
                }
            }
            return View(Customers);
        }
        
        public ActionResult Index_parameter(string CustomerID)
        {
            IEnumerable<Customers> Customers = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7123/api/");
                var responseTask = client.GetAsync($"Operations/GetAllDetails?CustomerID={CustomerID}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Customers>>();
                    readTask.Wait();
                    Customers = readTask.Result;
                }
                else
                {
                    Customers = Enumerable.Empty<Customers>();
                    ModelState.AddModelError(string.Empty, "Server error.Please contact administrator.");
                }
            }
            return View(Customers);
        }
        public ActionResult Index_Two_parameter(string id,string phone)
        {
            IEnumerable<Customers> Customers = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7123/api/");
                var responseTask = client.GetAsync($"Operations/GetCustomerId?id={id}&phone={phone}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Customers>>();
                    readTask.Wait();
                    Customers = readTask.Result;
                }
                else
                {
                    Customers = Enumerable.Empty<Customers>();
                    ModelState.AddModelError(string.Empty,"Server error.Please contact administrator.");
                }
            }
            return View(Customers);
        }

        
        public ActionResult Insertion()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Insertion(Customers cus)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7123/api/");
                var responseTask = client.PostAsJsonAsync<Customers>("Operations/InsertdataLinq", cus);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    ViewBag.msg = "SuccessFullly Insertion";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.msg = "Failed Insertion";
                    return View();
                }
            }
         
         
            
        }
        [HttpGet]
        public ActionResult Edit(string CustomerID)
        {
            Customers customers = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7123/api/");
               // var responseTask = client.GetAsync($"Operations/GetAllDetails?CustomerID={CustomerID}");
                var responseTask = client.GetAsync("Operations/GetAllDetails?CustomerID="+CustomerID);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Customers>();
                    readTask.Wait();
                    customers = readTask.Result;
                }

                return View(customers);
            }
            
        }

        [HttpPost]
        
        public ActionResult Edit(Customers obj)
        {
            
            using (var client = new HttpClient())
            {
                Customers c = null;
                client.BaseAddress = new Uri("https://localhost:7123/api/");
                var responseTask = client.PostAsJsonAsync<Customers>("Operations/UpdatedataLinq", obj);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.msg = "Successfully Updated";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.msg = "Failed Insertion";
                    return View(c);

                }
                
            }
        }
        [HttpGet]
        public ActionResult Delete(string CustomerID)
        {
            Customers customers = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7123/api/");
                // var responseTask = client.GetAsync($"Operations/GetAllDetails?CustomerID={CustomerID}");
                var responseTask = client.GetAsync("Operations/GetAllDetails?CustomerID=" + CustomerID);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Customers>();
                    readTask.Wait();
                    customers = readTask.Result;
                }

                return View(customers);
            }

        }
        [HttpPost,ActionName("Delete")]

        public ActionResult Delete1(string CustomerID)
        {

            using (var client = new HttpClient())
            {
                Customers c = null;
                client.BaseAddress = new Uri("https://localhost:7123/api/");
                var responseTask = client.DeleteAsync("Operations/Deletedatalinq?CustomerID="+ CustomerID);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.msg = "Successfully Deleted";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.msg = "Failed Deleted";
                    return View(c);

                }

            }
        }






    }
        

 }



