using Matricis.Helpers;
using Matricis.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Matricis.ViewModels {
    public class CriteriaViewModel : BaseViewModel {
        public ObservableRangeCollection<Criteria> Criterias { get; set; }
        public Command LoadItemsCommand { get; set; }

        public CriteriaViewModel() {
            Title = "Browse";
            Criterias = new ObservableRangeCollection<Criteria>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Criteria>(this, "AddItem", async (obj, item) => {
                var _criteria = item as Criteria;
                Criterias.Add(_criteria);
                _sqLiteConnection.Insert(new Criteria());
            });
        }

        async Task ExecuteLoadItemsCommand() {
            if (IsBusy)
                return;

            IsBusy = true;

            try {
                Criterias.Clear();
                var criterias = _sqLiteConnection.Table<Criteria>();
                Criterias.ReplaceRange(criterias);
            } catch (Exception ex) {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            } finally {
                IsBusy = false;
            }
        }
    }
}