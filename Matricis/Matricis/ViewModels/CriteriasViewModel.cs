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

        public ObservableRangeCollection<Criteria> Criterias { get; set; }
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
            Criterias = new ObservableRangeCollection<Criteria>(SqLiteConnection.Table<Criteria>().ToList());
            LoadCriteriasCommand = new Command(() => ExecuteLoadCriteriasCommand());
            AddItemClickedCommand = new Command(async () => await AddItemClickedAsync());

            MessagingCenter.Subscribe<NewCriteriaPage, Criteria>(this, "AddItem", async (obj, item) => {
                var _criteria = item as Criteria;
                Criterias.Add(_criteria);
                SqLiteConnection.Insert(new Criteria());
            });
        }

        private async Task AddItemClickedAsync() {

            await Application.Current.MainPage.Navigation.PushAsync(new NewCriteriaPage());
        //    TabbedPage navpa = Application.Current.MainPage as TabbedPage;
        //    NavigationPage x = navpa.Children.FirstOrDefault() as NavigationPage;
        //    await navpa.Navigation.PushAsync(new NewCriteriaPage());
        }

        private void ExecuteLoadCriteriasCommand() {
            if (IsBusy)
                return;

            IsBusy = true;

            try {
                Criterias = new ObservableRangeCollection<Criteria>(SqLiteConnection.Table<Criteria>().ToList());
            } catch (Exception ex) {
            } finally {
                IsBusy = false;
            }
        }
    }
}
