using Matricis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Matricis.ViewModels {
    public class TableViewModel : BaseViewModel {
        private Evaluation currentEvaluation;

        public Evaluation CurrentEvaluation
        {
            get
            {
                return currentEvaluation;
            }

            set
            {
                SetProperty(ref currentEvaluation, value);
            }
        }

        public TableViewModel() {
            MessagingCenter.Subscribe<EvaluationsViewModel, Evaluation>(this, "EvaluationSelectedM", (sender, args) => {
                CurrentEvaluation = args;
            });
        }



    }
}
