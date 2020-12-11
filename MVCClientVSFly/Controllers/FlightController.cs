using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MVCClientVSFly.Controllers
{
    public class FlightController : Controller
    {
        private static HttpClient _httpClient;

        static FlightController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44311/");
        }
        // GET: FlightController
        public async Task<ActionResult> IndexAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/Flights");
            response.EnsureSuccessStatusCode();
            string message = await response.Content.ReadAsStringAsync();

            IEnumerable<Models.Flight> listFlight = JsonConvert.DeserializeObject<IEnumerable<Models.Flight>>(message);

            return View(listFlight);
        }

        // GET: FlightController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FlightController/Create
        public async Task<ActionResult> CreateAsync(Models.Flight f)
        {
            try
            {
                string flightJson = JsonConvert.SerializeObject(f);
                HttpContent stringContent = new StringContent(flightJson, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/PostFlight", stringContent);
                response.EnsureSuccessStatusCode();

                var data = await response.Content.ReadAsStringAsync();
                Models.Message m = new Models.Message();
                m.Content = data;

                return View("ShowMessage");

                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // POST: FlightController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: FlightController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FlightController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: FlightController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FlightController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
