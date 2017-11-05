using Matricis.Helpers;
using Matricis.Models;
using Matricis.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Matricis.ViewModels {
    public class EvaluationsViewModel : BaseViewModel {

        private ObservableRangeCollection<Evaluation> evalutions;
        private Evaluation selectedItem;

        public ObservableRangeCollection<Evaluation> Evaluations
        {
            get     
            {
                return evalutions;
            }

            set
            {
                SetProperty(ref evalutions, value);
            }
        }
        public Evaluation SelectedItem
        {
            get
            {

                return this.selectedItem;

            }
            set
            {

                this.selectedItem = value;

                MessagingCenter.Send<EvaluationsViewModel, Evaluation>(this, "EvaluationSelectedM", selectedItem);

                //Application.Current.MainPage.Navigation.PushAsync(new OptionDetailPage());
                //MessagingCenter.Send<OptionsViewModel, Option>(this, "OptionSelectedM", SelectedItem);

            }
        }

        public Command LoadEvaluationsCommand { get; set; }
        public Command AddItemClickedCommand { get; set; }

        public EvaluationsViewModel() {
            LoadEvaluations();
            LoadEvaluationsCommand = new Command(() => LoadEvaluations());
            AddItemClickedCommand = new Command(async () => await AddItemClickedAsync());

            MessagingCenter.Subscribe<NewEvaluationViewModel>(this, "AddEvaluationM", (sender) => {
                LoadEvaluations();
            });
        }

        private async Task AddItemClickedAsync() {
            var page = Application.Current.MainPage as TabbedPage;
            await page.Children.First().Navigation.PushAsync(new NewEvaluationPage());
        }


        private void LoadEvaluations() {

            // For testing
            var x = SqLiteConnection.DropTable<Evaluation>();

            if (IsBusy)
                return;

            IsBusy = true;

            try {

                Evaluations = new ObservableRangeCollection<Evaluation>(SqLiteConnection.Table<Evaluation>().ToList());

            } catch (Exception ex) {
                if (ex.Message == "no such table: Evaluation") {
                    SqLiteConnection.CreateTable<Evaluation>();
                    Evaluations = new ObservableRangeCollection<Evaluation>(SqLiteConnection.Table<Evaluation>().ToList());
                }
            } finally {
                IsBusy = false;
            }
        }
    }
}
