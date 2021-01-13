using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MVCClientVSFly.Controllers
{
    public class PassengersController : Controller
    {
        private static HttpClient _httpClient;
        // GET: FlightsController

        static PassengersController()
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

            return View("IndexAsync", listFlight);
        }

        // GET: FlightsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FlightsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Models.Passenger p)
        {
            Models.Message m= new Models.Message();
            try
            {
                string flightJson = JsonConvert.SerializeObject(p);
                HttpContent stringContent = new StringContent(flightJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("api/Passengers", stringContent);
                response.EnsureSuccessStatusCode();
                string data = await response.Content.ReadAsStringAsync();
                m.Content = data;


                return View("ShowMessage", m);
            }
            catch(Exception e)
            {
                m.Content = e.Message;
                return View("ShowMessage", m);
            }
        }
    }
}
