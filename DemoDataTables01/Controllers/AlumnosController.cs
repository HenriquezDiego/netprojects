using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoDataTables01.Models;
using Microsoft.Ajax.Utilities;
using System.Linq.Dynamic;

namespace DemoDataTables01.Controllers
{
    public class AlumnosController : Controller
    {
        // ------------------ Client Side ---------------------
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult GetList()
        {
            using (BDModels bd = new BDModels())
            {
                var aluList = bd.alu.ToList<alu>();
                aluList[0].fecha_nac.ToString();
                var jsonResult = Json(new { data = aluList }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
     
                return jsonResult;
            }
        }
        
        // ------------------- Normal Way -----------------------
        public ActionResult ServerList()
        {
            using (BDModels bd = new BDModels())
            {
                return View(bd.alu.ToList<alu>());
            }
        }

        // --------------- Server Side Processing ---------------
        public ActionResult ServerListAjax()
        {
            return View();
        }
                
        [HttpPost]
        public ActionResult GetListServer()
        {
            // Server Side Parameters
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirecction = Request["order[0][dir]"];
                       
            List<alu> aluListServer = new List<alu>();
            using (BDModels bd = new BDModels())
            {
                aluListServer = bd.alu.ToList<alu>();
                int totalRows = aluListServer.Count;
                if (!string.IsNullOrEmpty(searchValue))//Filter
                {
                    
                    aluListServer = aluListServer.
                        Where(x => x.carnet.ToLower().Contains(searchValue.ToLower()) || x.apellido1.ToLower().Contains(searchValue.ToLower()) ||
                        x.apellido2.ToLower().Contains(searchValue.ToLower()) || x.apellido3.ToLower().Contains(searchValue.ToLower()) || 
                        x.nombre1.ToLower().Contains(searchValue.ToLower()) || x.nombre2.ToLower().Contains(searchValue.ToLower()) || 
                        x.cod_carr.ToString().ToLower().Contains(searchValue.ToLower()) || x.activo.ToString().ToLower().Contains(searchValue.ToLower()) ||
                        x.curso_asp.ToString().ToLower().Contains(searchValue.ToLower()) || x.proceso.ToString().ToLower().Contains(searchValue.ToLower()) ||
                        x.fecha_pro.ToString().ToLower().Contains(searchValue.ToLower()) || x.fecha_nac.ToString().ToLower().Contains(searchValue.ToLower()) ||
                        x.dui.ToLower().Contains(searchValue.ToLower()) || x.nit.ToLower().Contains(searchValue.ToLower()) ||
                        x.recibo_ins.ToString().ToLower().Contains(searchValue.ToLower()) || x.ciclo_in.ToString().ToLower().Contains(searchValue.ToLower()) ||
                        x.año_in.ToString().ToLower().Contains(searchValue.ToLower()) || x.cod_carr_old.ToString().ToLower().Contains(searchValue.ToLower()) ||
                        x.carnet_old.ToLower().Contains(searchValue.ToLower()) || x.equi.ToString().ToLower().Contains(searchValue.ToLower()) ||
                        x.sexo.ToString().ToLower().Contains(searchValue.ToLower())).ToList<alu>();

                }
                int totalRowsAfterFiltering = aluListServer.Count;
                //Sorting
                aluListServer = aluListServer.OrderBy(sortColumnName + " " + sortDirecction).ToList<alu>();

                //Paging
                aluListServer = aluListServer.Skip(start).Take(length).ToList<alu>();
                
                var jsonResult = Json(new { data = aluListServer, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = totalRowsAfterFiltering }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;

                return jsonResult;            
            }
        }
    }
}