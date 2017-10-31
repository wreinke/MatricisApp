using Matricis.Helpers;
using SQLite;
using Xamarin.Forms;
using XamarinForms.SQLite.SQLite;

namespace Matricis.ViewModels {
    public class BaseViewModel : ObservableObject {
        /// <summary>
        /// Get the azure service instance
        /// </summary>
        protected readonly SQLiteConnection _sqLiteConnection = DependencyService.Get<ISQLite>().GetConnection();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
    }
}

