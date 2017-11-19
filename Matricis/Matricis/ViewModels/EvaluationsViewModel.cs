using Matricis.Helpers;
using Matricis.Models;
using Matricis.Views;
using SQLite;
using SQLiteNetExtensions.Extensions;
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

                if(selectedItem != null) {
                    try {
                        selectedItem.Criterias = SqLiteConnection.GetAllWithChildren<Criteria>().Where(c => c.EvaluationID == selectedItem.Id).ToList();
                        selectedItem.Options = SqLiteConnection.GetAllWithChildren<Option>().Where(o => o.EvaluationId == selectedItem.Id).ToList();
                        MessagingCenter.Send<EvaluationsViewModel, Evaluation>(this, "EvaluationSelectedM", selectedItem);
                    } catch (Exception e) {
                        throw new NotImplementedException();
                    }
                }
            }
        }

        //public Command LoadEvaluationsCommand { get; set; }
        public Command AddItemClickedCommand { get; set; }

        public EvaluationsViewModel() {
            LoadEvaluations();

            //LoadEvaluationsCommand = new Command(() => LoadEvaluations());
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

            //            For testing
            //var x = SqLiteConnection.DropTable<Evaluation>();
            //x = SqLiteConnection.DropTable<Option>();
            //x = SqLiteConnection.DropTable<CriteriaOption>();
            //x = SqLiteConnection.DropTable<Criteria>();

            if (IsBusy)
                return;

            IsBusy = true;

            try {
                Evaluations = new ObservableRangeCollection<Evaluation>(SqLiteConnection.GetAllWithChildren<Evaluation>());

            } catch (Exception ex) {
                if (ex.Message == "no such table: Evaluation"  || ex.Message == "no such table: Criteria" || ex.Message == "no such table: Option") {
                    SqLiteConnection.CreateTable<Criteria>();
                    SqLiteConnection.CreateTable<Option>();
                    SqLiteConnection.CreateTable<Evaluation>();
                    SqLiteConnection.CreateTable<CriteriaOption>();
                    Evaluations = new ObservableRangeCollection<Evaluation>(SqLiteConnection.Table<Evaluation>().ToList());
                }
            } finally {
                IsBusy = false;
            }
        }
    }
}
