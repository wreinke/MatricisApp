using SQLiteNetExtensions.Attributes;

namespace Matricis.Models {

    public class Option : BaseModel {

        public int Score { get; set; }

        [ForeignKey(typeof(Evaluation))]
        public int EId { get; set; }
    }
}
