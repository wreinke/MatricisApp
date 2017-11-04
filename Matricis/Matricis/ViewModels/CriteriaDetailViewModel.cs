using Matricis.Models;
using Xamarin.Forms;

namespace Matricis.ViewModels {

    public class CriteriaDetailViewModel : BaseViewModel {
        private Criteria criteria;

        public Criteria Criteria
        {
            get
            {
                return criteria;
            }
            set
            {
                SetProperty(ref criteria, value);
            }
        }

        public CriteriaDetailViewModel() {
            Criteria = new Criteria() {
                Title = "test"
        };
            MessagingCenter.Subscribe<CriteriasViewModel, Criteria>(this, "CriteriaSelectedM", (sender,args) => {
                Criteria = args;
            });
        }
    }
}
