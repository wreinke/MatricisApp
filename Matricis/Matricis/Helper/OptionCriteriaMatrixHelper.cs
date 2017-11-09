using Matricis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricis.Helper {
    public class OptionCriteriaMatrixHelper {
        
        public Option Option { get; set; }
        public List<Criteria> Criterias { get; set; }


        public OptionCriteriaMatrixHelper(Option options, List<Criteria>criteria) {

            Option = Option;


        }

    }
}
