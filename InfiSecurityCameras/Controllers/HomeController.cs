using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using InfiSecurityCameras.Models;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.IO;

namespace InfiSecurityCameras.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var taskResult = Task.Run(() => GetSecurityCamsAsync());
            List<SecurityCameraMdl> securityCameraMdl = new List<SecurityCameraMdl>();
            securityCameraMdl = taskResult.Result;
            ViewBag.Title = "Infi | Security cameras Utrecht";
            return View(securityCameraMdl);
        }


        public async Task<List<SecurityCameraMdl>> GetSecurityCamsAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:60611/api/SecurityCameraAPI");
            var listSecurityCameraMdl = new List<SecurityCameraMdl>();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                listSecurityCameraMdl = JsonConvert.DeserializeObject<List<SecurityCameraMdl>>(content);
            }

            return listSecurityCameraMdl;
        }
    }
}