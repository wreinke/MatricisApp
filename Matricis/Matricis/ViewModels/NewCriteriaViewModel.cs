using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Matricis.ViewModels {
    public class NewCriteriaViewModel {

        public Command SaveClickedCommand { get; set; }

        public NewCriteriaViewModel() {
            SaveClickedCommand = new Command(() => SaveClicked());
        }

        private void SaveClicked() {

        }

    }
}
