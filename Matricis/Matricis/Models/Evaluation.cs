using Matricis.Helpers;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricis.Models {
    public class Evaluation : BaseModel {

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Criteria> Criterias { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Option> Options { get; set; }

    }
}
