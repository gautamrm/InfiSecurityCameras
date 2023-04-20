using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfiSecurityCameras.Models
{
    public class SecuritycameraMdlList
    {
        public List<SecurityCameraMdl> securityCameraMdls {get; set;}
    }
    public class SecurityCameraMdl
    {
        public int Id { get; set; }
        public string CameraName { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set;}
    }
}