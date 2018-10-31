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
        public ActionResult AddOrEdit( int id = 0) {
            if (id==0)
                return View(new mvcEmployeeModel());
            else
            {
                HttpResponseMessage responde = GlobalVariables.WebApiClient.GetAsync("Employee/"+id.ToString()).Result;
                return View(responde.Content.ReadAsAsync<mvcEmployeeModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(mvcEmployeeModel emp)
        {
            if (emp.EmployeeID ==0)
            {
                HttpResponseMessage responde = GlobalVariables.WebApiClient.PostAsJsonAsync("Employee", emp).Result;
                TempData["SuccessMessage"] = "Save Successfully";
            }
            else
            {
                HttpResponseMessage responde = GlobalVariables.WebApiClient.PutAsJsonAsync("Employee/"+emp.EmployeeID, emp).Result;
                TempData["SuccessMessage"] = "Update Successfully";
            }
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id) {
            HttpResponseMessage responde = GlobalVariables.WebApiClient.DeleteAsync("Employee/"+ id).Result;
            TempData["SuccessMessage"] = "Delete Successfully";
            return RedirectToAction("Index");
        }
    }
}