using CsvHelper.Configuration;
using CsvHelper;
using InfiSecurityCameras.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace InfiSecurityCameras.Controllers
{
    public class SecurityCameraAPIController : ApiController
    {
        //Update file path...
        const string fileName = "C:\\Users\\gauta\\source\\repos\\InfiSecurityCameras\\InfiSecurityCameras\\cameras-defb.csv";

        // GET: api/SecurityCameraAPI
        public IEnumerable<SecurityCameraMdl> Get(string searchName = null)
        {
            List<SecurityCameraMdl> securityCameraMdl = new List<SecurityCameraMdl>();

            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                MissingFieldFound = null,
                Delimiter = ";"
            };

            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, configuration))
            {
                csv.Context.RegisterClassMap<SecurityCamMap>();
                var records = csv.GetRecords<SecurityCameraMdl>();
                securityCameraMdl = records.ToList();
            }
            if (searchName != null)
            {
                return securityCameraMdl.Where(m => m.CameraName.Contains(searchName));
            }
            else
            {
                return securityCameraMdl;
            }
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
    }
}
