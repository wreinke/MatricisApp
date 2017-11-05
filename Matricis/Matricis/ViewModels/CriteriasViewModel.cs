using Matricis.Helpers;
using Matricis.Models;
using Matricis.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Matricis.ViewModels {
    public class CriteriasViewModel : BaseViewModel {

        Criteria selectedItem;

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

        public Criteria SelectedItem {
            get {

                return this.selectedItem;

            } set {

                this.selectedItem = value;
                Application.Current.MainPage.Navigation.PushAsync(new CriteriaDetailPage());
                MessagingCenter.Send<CriteriasViewModel, Criteria>(this, "CriteriaSelectedM", SelectedItem);

            }
        }



        public Command LoadCriteriasCommand { get; set; }
        public Command AddItemClickedCommand { get; set; }

        public CriteriasViewModel() {
            Title = "Browse";
            LoadCriterias();
            LoadCriteriasCommand = new Command(() => LoadCriterias());
            AddItemClickedCommand = new Command(async () => await AddItemClickedAsync());

            MessagingCenter.Subscribe<NewCriteriaViewModel>(this, "AddCriteriaM", (sender) => {
                LoadCriterias();
            });
        }

        private async Task AddItemClickedAsync() {

            await Application.Current.MainPage.Navigation.PushAsync(new NewCriteriaPage());
        }

        private void LoadCriterias() {
            if (IsBusy)
                return;

            IsBusy = true;

            try {
                Criterias = new ObservableRangeCollection<Criteria>(SqLiteConnection.Table<Criteria>().ToList());
            } catch (Exception ex) {
                if(ex.Message == "no such table: Criteria") {
                    SqLiteConnection.CreateTable<Criteria>();
                }
            } finally {
                IsBusy = false;
            }
        }
    }
}
