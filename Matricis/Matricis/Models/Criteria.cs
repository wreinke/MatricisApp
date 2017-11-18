using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace Matricis.Models {
    public class Criteria : BaseModel {

        public double Weight { get; set; } = 1.0;
        public String Descripion { get; set; }

        [ManyToOne]      // Many to one relationship with Stock
        public Evaluation Evaluation { get; set; }

        [ForeignKey(typeof(Evaluation))]
        public int EvaluationID { get; set; }

        [ManyToMany(typeof(CriteriaOption))]
        public List<Option> Subjects { get; set; }

        public Criteria() {
        }
    }
}