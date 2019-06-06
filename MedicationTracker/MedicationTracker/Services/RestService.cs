using MedicationTracker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedicationTracker.Services
{
    public static class RestService
    {
        public static HttpClient client { get; set; } = new HttpClient();

        public static async Task<List<Medicine>> GetMedicinesAsync()
        {
            HttpResponseMessage response = await client.GetAsync("https://medicationtrackerrest20190606010800.azurewebsites.net/api/Medications");
            if(response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return MapToMedicine(JsonConvert.DeserializeObject<List<MedicineREST>>(data));
            }
            return null;
        }


        public static List<Medicine> MapToMedicine(List<MedicineREST> rest)
        {
            List<Medicine> medicines = new List<Medicine>();
            foreach(MedicineREST mrest in rest)
            {
                medicines.Add(new Medicine()
                {
                    ID  = Guid.NewGuid().ToString(),
                    Name = mrest.Name,
                    Description = mrest.Description,
                    Image = new Image()
                    {
                        Source = ImageSource.FromUri(new Uri(mrest.Image_Uri))
                    }
                });
            }
            return medicines;
        }

    }
}
