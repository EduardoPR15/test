using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net;
using test.Models;

namespace test.Controllers
{
    public class UserController : Controller
    {
        // GET: UserController
        public ActionResult Index()
        {
            //variable con credenciales
            var token = HttpContext.Session.GetString("_UserToken");

            if (token != null)
            {

                return RedirectToAction("Bienvenida");
            }
            else
                return View();



            //return View();
        }



        // POST: UserController/Create
        [HttpPost]
        //[HttpPost]
        public async Task<IActionResult> Index(UserModel userModel)
        {


            string error = "Credenciales incorrectas";
            var client = new RestClient("https://crazy-stonebraker.68-168-208-58.plesk.page/api/Auth/Auth");
            var request = new RestSharp.RestRequest();
            request.Method = Method.Post;
            request.AddParameter("nombre", userModel.nombre);
            request.AddParameter("password", userModel.password);

            request.AlwaysMultipartFormData = true;
            RestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContext.Session.SetString("_UserToken", "ok");
                return RedirectToAction("index");
            }
            else
            {
                Console.WriteLine(response.Content);
                return View();
            }

        }
        public ActionResult Bienvenida()
        {
            return View();
        }





    }
}
