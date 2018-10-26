using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Net.Http;

namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            /*EN GlobalVariables SE OBTENDAN LOS CODIGOS PARA CONECTARSE A LA API*/
            IEnumerable<mvcEmployeeModel> empList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Employee").Result;
            //se guarda en la lista todos los datos obtenidos de la api
            empList = response.Content.ReadAsAsync<IEnumerable<mvcEmployeeModel>>().Result;
            return View(empList);
        }
        public ActionResult AddOrEdit() {
            return View(new mvcEmployeeModel());
        }
        [HttpPost]
        public ActionResult AddOrEdit(mvcEmployeeModel emp)
        {
            HttpResponseMessage responde = GlobalVariables.WebApiClient.PostAsJsonAsync("Employee", emp).Result;
            TempData["SuccessMessage"] = "Save Successfully";
            return RedirectToAction("Index");
        }
    }
}