using System;
using System.Collections.Generic;
using System.Text;

namespace MedicationTracker.Models
{
    public class MedicineREST
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image_Uri { get; set; }
    }
}
