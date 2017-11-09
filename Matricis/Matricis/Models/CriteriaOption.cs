using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricis.Models {
    public class CriteriaOption : BaseModel {

        [Ignore]
        public List<Criteria> Criterias { get; set; }
        [Ignore]
        public List<Option> Options { get; set; }

        [ForeignKey(typeof(Criteria))]
        public int CriteriaId { get; set; }

        [ForeignKey(typeof(Option))]
        public int OptionId { get; set; }

        public int Score { get; set; }

    }
}