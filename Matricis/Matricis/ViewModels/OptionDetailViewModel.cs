using Matricis.Helpers;
using Matricis.Models;
using Xamarin.Forms;

namespace Matricis.ViewModels {

    public class OptionDetailViewModel : BaseViewModel {
        private Option option;

        public Option Option
        {
            get
            {
                return option;
            }
            set
            {
                SetProperty(ref option, value);
            }
        }

        private ObservableRangeCollection<Criteria> criterias;

        public ObservableRangeCollection<Criteria> Criterias
        {
            get
            {
                return criterias;
            }
            set
            {
                SetProperty(ref criterias, value);
            }
        }

        public OptionDetailViewModel() {
            Option = new Option() {
                Title = "test"
            };
            MessagingCenter.Subscribe<OptionsViewModel, Option>(this, "OptionSelectedM", (sender, args) => {
                Option = args;
                if(Option.Evaluation.Criterias != null) {
                    Criterias = new ObservableRangeCollection<Criteria>(Option.Evaluation.Criterias);
                } else {
                    Criterias = new ObservableRangeCollection<Criteria>();
                }
            });
        }
    }
}
