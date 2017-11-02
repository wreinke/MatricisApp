using Matricis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForms.SQLite.SQLite;

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
