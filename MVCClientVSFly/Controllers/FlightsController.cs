using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVCClientVSFly.Controllers
{
    public class FlightsController : Controller
    {
        private static HttpClient _httpClient;

        // GET: FlightsController
        static FlightsController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress= new Uri("https://localhost:44349/");
        }

        public async Task<ActionResult> IndexAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/Flights");
            response.EnsureSuccessStatusCode();
            string message = await response.Content.ReadAsStringAsync();

            IEnumerable<Models.Flight> listFlight = JsonConvert.DeserializeObject<IEnumerable<Models.Flight>>(message);
            List<Models.Flight> listFlightM = new List<Models.Flight>();

            foreach (Models.Flight f in listFlight)
            {
                   listFlightM.Add(f);  
            }

            return View("IndexAsync", listFlightM);
        }
    }
}
