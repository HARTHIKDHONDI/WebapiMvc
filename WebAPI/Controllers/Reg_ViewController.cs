using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class Reg_ViewController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<reg> Customers = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7123/api/");
                var responseTask = client.GetAsync("Register/GetReg");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<reg>>();
                    readTask.Wait();
                    Customers = readTask.Result;
                }
                else
                {
                    Customers = Enumerable.Empty<reg>();
                    ModelState.AddModelError(string.Empty, "Server error.Please contact administrator.");
                }
            }
            return View(Customers);
        }

        public ActionResult Insertion()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Insertion(reg r)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7123/api/");
                var responseTask = client.PostAsJsonAsync<reg>("Register/InsertdataLinq", r);
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
        public ActionResult Login()
        {

            return View();
        }
        /*[HttpPost,ActionName("Login")]
        public ActionResult LoginConfirm(string Email, string Password)
        {
            reg data = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7123/api/");
                var responseTask = client.GetAsync($"Register/LoginLinq?Email={Email}&Password={Password}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<reg>();
                    readTask.Wait();
                    data = readTask.Result;
                    if (data != null)
                    {
                        ViewBag.msg = "SuccessFullly Insertion";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.msg = "Error";
                        return View();
                    }
                }
                else
                {
                    ViewBag.msg = "Failed Insertion";
                    return View();
                }
            }
        }*/
        [HttpPost, ActionName("Login")]
        public ActionResult LoginConfirm(string Email, string Password)
        {
            //reg data = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7123/api/");
                var responseTask = client.GetAsync($"Register/LoginLinq?Email={Email}&Password={Password}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result != null)
                {

                    var readTask = result.Content.ReadAsAsync<reg>();
                    readTask.Wait();
                    var t = readTask.Result;
                    if (t != null)
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
                else
                {
                    ViewBag.msg = "Failed Insertion";
                    return View();
                }
            }
        }

    }
}
