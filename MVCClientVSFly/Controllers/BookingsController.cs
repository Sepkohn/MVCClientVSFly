using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
            b.Price = calculatePrice(f);

            return View(b);
        }

        private double calculatePrice(Models.Flight f) {
            double Price = f.SeatPrice;


            return Price;
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
                
                // string data = await response.Content.ReadAsStringAsync();
                m.Content = "Réservation effectué";


                return View("ShowMessage", m);
            }
            catch (Exception e)
            {
                //res
                //m.Content = e.Message;
                m.Content = await response.Content.ReadAsStringAsync();
                return View("ShowMessage", m);
            }
        }


    }
       
}
