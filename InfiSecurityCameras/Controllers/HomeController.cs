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
            //var taskResult = Task.Run(() => GetSecurityCamsAsync());
            //List<SecurityCameraMdl> securityCameraMdl = new List<SecurityCameraMdl>();
            List<SecurityCameraMdl> securityCameraMdl = new List<SecurityCameraMdl>();

            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                MissingFieldFound = null,
                Delimiter = ";"
            };

            var fileName = "C:\\Users\\gauta\\source\\repos\\InfiSecurityCameras\\InfiSecurityCameras\\cameras-defb.csv";

            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, configuration))
            {
                csv.Context.RegisterClassMap<SecurityCamMap>();
                var records = csv.GetRecords<SecurityCameraMdl>();
                securityCameraMdl = records.ToList();
            }

            ViewBag.Title = "Infi | Security cameras Utrecht";
            return View(securityCameraMdl);
        }

        public class SecurityCamMap : ClassMap<SecurityCameraMdl>
        {
            public SecurityCamMap()
            {
                Map(p => p.Id).Index(0);
                Map(p => p.CameraName).Index(1);
                Map(p => p.Longitude).Index(2);
                Map(p => p.Lattitude).Index(3);
            }
        }

        //public async Task<SecurityCameraMdl> GetSecurityCamsAsync()
        //{
        //    HttpClient client = new HttpClient();
        //    HttpResponseMessage response = await client.GetAsync("http://localhost:60611/api/SecurityCameraAPI");
        //    var listSecurityCameraMdl = new List<SecuritycameraMdlList>();
        //    var securityCameraMdl = new SecurityCameraMdl();

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadAsStringAsync();
        //        listSecurityCameraMdl = JsonConvert.DeserializeObject<SecuritycameraMdlList>(content);
        //    }


        //    return securityCameraMdl;
        //}
    }
}