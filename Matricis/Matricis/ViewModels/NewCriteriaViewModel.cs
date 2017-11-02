using Matricis.Models;
using Xamarin.Forms;

namespace Matricis.ViewModels {
    public class NewCriteriaViewModel : BaseViewModel {

        public Criteria Criteria { get; set; }

        public Command SaveClickedCommand { get; set; }

        public NewCriteriaViewModel() {
            Criteria = new Criteria();

            SaveClickedCommand = new Command(() => SaveClicked());
        }

        private void SaveClicked() {
            if (Criteria != null) {
                try {
                    SqLiteConnection.Insert(Criteria);
                } catch (SQLite.SQLiteException e) {
                    if(e.Message == "no such table: Criteria") {
                        SqLiteConnection.CreateTable<Criteria>();
                        SqLiteConnection.Insert(Criteria);
                    }
                }
            }
        }
    }
}
