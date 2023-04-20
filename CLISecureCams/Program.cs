using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HttpClientSample
{
    public class SecurityCameraMdl
    {
        public int Id { get; set; }
        public string CameraName { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
    }

    class Program
    {
        static HttpClient client = new HttpClient();

        static void ShowCameras(List<SecurityCameraMdl> securityCameraMdlList)
        {
            //501 | UTR-CM-501 Neude rijbaan voor Postkantoor | 52.093421 | 5.118278
            foreach(var securityCameraMdl in securityCameraMdlList)
            {
                Console.WriteLine($"Id: {securityCameraMdl.Id}\tCamera Name : " + $"{securityCameraMdl.CameraName}\tLattitude: {securityCameraMdl.Lattitude}\tLongitude: {securityCameraMdl.Longitude}");
            }            
        }

        static async Task<List<SecurityCameraMdl>> GetProductAsync(string path)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(path);
            var listSecurityCameraMdl = new List<SecurityCameraMdl>();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                listSecurityCameraMdl = JsonConvert.DeserializeObject<List<SecurityCameraMdl>>(content);
            }

            return listSecurityCameraMdl;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Please enter security cam name to proceed...");
            var strSearchName = Console.ReadLine();
            if (strSearchName == null)
            {
                Console.WriteLine("Nothing entered, please try again..");
                strSearchName = Console.ReadLine();
            }
            else
            {
                RunAsync(strSearchName).GetAwaiter().GetResult();
            }            
        }

        static async Task RunAsync(string SearchName)
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:60611/api/SecurityCameraAPI");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                List<SecurityCameraMdl> securityCameraMdl = new List<SecurityCameraMdl>();

                // Get the product
                securityCameraMdl = await GetProductAsync("http://localhost:60611/api/SecurityCameraAPI/" + SearchName);
                ShowCameras(securityCameraMdl);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}