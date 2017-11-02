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

        public ObservableRangeCollection<Criteria> Criterias { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command AddItemClickedCommand { get; set; }

        public CriteriasViewModel() {
            Title = "Browse";
            Criterias = new ObservableRangeCollection<Criteria>(SqLiteConnection.Table<Criteria>().ToList());
            //LoadItemsCommand = new Command(() => ExecuteLoadItemsCommand());
            //AddItemClickedCommand = new Command(async() => await AddItemClickedAsync());

            MessagingCenter.Subscribe<NewCriteriaPage, Criteria>(this, "AddItem", async (obj, item) => {
                var _criteria = item as Criteria;
                Criterias.Add(_criteria);
                SqLiteConnection.Insert(new Criteria());
            });
        }

        //private async Task AddItemClickedAsync() {
        //    //await App.Current.MainPage.Navigation.PushAsync(new NewCriteriaPage());
        //}

        //private void ExecuteLoadItemsCommand() {
        //    if (IsBusy)
        //        return;

        //    IsBusy = true;

        //    try {
        //        Criterias.Clear();
        //        var criterias = SqLiteConnection.Table<Criteria>();
        //        Criterias.ReplaceRange(criterias);
        //    } catch (Exception ex) {
        //        Debug.WriteLine(ex);
        //        MessagingCenter.Send(new MessagingCenterAlert {
        //            Title = "Error",
        //            Message = "Unable to load items.",
        //            Cancel = "OK"
        //        }, "message");
        //    } finally {
        //        IsBusy = false;
        //    }
    }
}
