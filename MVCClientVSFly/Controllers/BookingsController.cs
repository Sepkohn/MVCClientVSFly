using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MVCClientVSFly.Controllers
{
    public class BookingsController : Controller
    {
        private static HttpClient _httpClient;
        // GET: FlightsController

        static BookingsController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44349/");
        }


        // GET: BookingsController/Create
        public ActionResult Create(Models.Flight f)
        {
            Models.Booking b = new Models.Booking();

            b.FlightNo = f.FlightNo;
            b.Price = f.SeatPrice;

            return View(b);
        }

        

        // POST: BookingsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Models.Booking b)
        {
            Models.Message m = new Models.Message();
                string bookingJson = JsonConvert.SerializeObject(b);
                HttpContent stringContent = new StringContent(bookingJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(("api/Bookings"), stringContent);
            try
            {
                response.EnsureSuccessStatusCode();
                
                m.Content = "Réservation effectué";

                return View("ShowMessage", m);
            }
            catch
            {
                m.Content = await response.Content.ReadAsStringAsync();
                return View("ShowMessage", m);
            }
        }


    }
       
}
