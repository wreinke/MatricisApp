using Matricis.Models;
using System.Linq;
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

        private async Task SaveClickedAsync() {
            //if (Criteria != null) {
            //    try 
            //        {
            //        SqLiteConnection.Insert(Criteria);
            //        }
            //    catch (SQLite.SQLiteException e) 
            //    {
            //        if(e.Message == "no such table: Criteria")
            //            {
            //            SqLiteConnection.CreateTable<Criteria>();
            //            SqLiteConnection.Insert(Criteria);
            //        }
            //    } 
            //    finally {
                    MessagingCenter.Send<NewCriteriaViewModel,Criteria>(this,"AddCriteriaM",Criteria);
                    SqLiteConnection.Insert(Criteria);
                    var page = Application.Current.MainPage as TabbedPage;
                    await page.Children[1].Navigation.PopToRootAsync();
                //}
            }
        }
     
}