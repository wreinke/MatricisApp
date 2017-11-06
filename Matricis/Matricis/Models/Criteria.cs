using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace Matricis.Models {
    public class Criteria : BaseModel {

        public double Weight { get; set; } = 1.0;
        public String Descripion { get; set; }

        [ManyToOne]      // Many to one relationship with Stock
        public Evaluation Evaluation { get; set; }

        public Criteria() {
        }
    }
}