using Matricis.Models;
using SQLiteNetExtensions.Extensions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Matricis.ViewModels {
    public class NewCriteriaViewModel : BaseViewModel {

        public Criteria Criteria { get; set; }

        public Command SaveClickedCommand { get; set; }

        public NewCriteriaViewModel() {
            Criteria = new Criteria();
            SaveClickedCommand = new Command(async () => await SaveClickedAsync());
        }

        /// <summary>
        /// Save Criteria locally and popAsync
        /// </summary>
        private async Task SaveClickedAsync() {
            if (Criteria != null) {
                try {

                    SqLiteConnection.InsertWithChildren(Criteria);

                } catch (SQLite.SQLiteException e) {

                    if (e.Message == "no such table: Criteria") {
                        SqLiteConnection.CreateTable<Criteria>();
                        SqLiteConnection.Insert(Criteria);
                    }

                } finally {
                    SqLiteConnection.InsertWithChildren(Criteria);
                    MessagingCenter.Send<NewCriteriaViewModel, Criteria>(this, "AddCriteriaM", Criteria);
                    var page = Application.Current.MainPage as TabbedPage;
                    await page.Children[2].Navigation.PopAsync();
                }
            }
        }
    }
}