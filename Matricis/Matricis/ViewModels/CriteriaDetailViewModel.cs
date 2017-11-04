using Matricis.Models;

namespace Matricis.ViewModels {

    public class CriteriaDetailViewModel : BaseViewModel {
        
        public Criteria Criteria { get; set; }

        public CriteriaDetailViewModel() {
            //Title = item.Title;
            //Criteria = item;
        }

        int quantity = 1;

        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }
    }
}
