using Matricis.Models;
using Xamarin.Forms;

namespace Matricis.ViewModels {

    public class OptionDetailViewModel : BaseViewModel
    {
        private Option criteria;

        public Option Option
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

        public OptionDetailViewModel() {
            Option = new Option() {
                Title = "test"
            };
            MessagingCenter.Subscribe<OptionsViewModel, Option>(this, "OptionSelectedM", (sender, args) => {
                Option = args;
            });
        }
    }
}
