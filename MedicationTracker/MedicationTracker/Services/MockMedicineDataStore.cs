using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using MedicationTracker.Models;

namespace MedicationTracker.Services
{
    public class MockMedicineDataStore : IDataStore<Medicine>
    {
        List<Medicine> medicines;

        public MockMedicineDataStore()
        {
            medicines = new List<Medicine>();
            if (medicines.Count == 0)
            {
                Task.Run(async () => { medicines = await RestService.GetMedicinesAsync(); }).GetAwaiter().GetResult();
            }
        }

        public async Task<bool> AddItemAsync(Medicine medicine)
        {
            medicines.Add(medicine);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Medicine medicine)
        {
            var oldMedicine = medicines.Where((Medicine arg) => arg.ID == medicine.ID).FirstOrDefault();
            medicines.Remove(oldMedicine);
            medicines.Add(medicine);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldMedicine = medicines.Where((Medicine arg) => arg.ID == id).FirstOrDefault();
            medicines.Remove(oldMedicine);

            return await Task.FromResult(true);
        }

        public async Task<Medicine> GetItemAsync(string id)
        {
            return await Task.FromResult(medicines.FirstOrDefault(s => s.ID == id));
        }

        public async Task<IEnumerable<Medicine>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(medicines);
        }
    }
}