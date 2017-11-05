using SQLiteNetExtensions.Attributes;
using System;

namespace Matricis.Models {
    public class Criteria : BaseModel {

        public double Weight { get; set; } = 1.0;
        public String Descripion { get; set; }

        [ForeignKey(typeof(Evaluation))]
        public int EvaluationId { get; set; }
 

        public Criteria() {
        }
    }
}