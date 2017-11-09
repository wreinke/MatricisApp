using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace Matricis.Models {

    public class Option : BaseModel {

        public int Score { get; set; }

        [ManyToOne]
        public Evaluation Evaluation { get; set; }

        [ForeignKey(typeof(Evaluation))]
        public int EvaluationId { get; set; }

        [ManyToMany(typeof(CriteriaOption))]
        public List<Criteria> Criterias { get; set; }
    }
}
