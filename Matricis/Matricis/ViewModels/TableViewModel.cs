using Matricis.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Matricis.ViewModels {
    public class TableViewModel : BaseViewModel {
        private Evaluation currentEvaluation;
        private ObservableCollection<CriteriaOption> criteriaOptions;

        public ObservableCollection<CriteriaOption> CriteriaOptions {
            get
            {
                return criteriaOptions;
            }
            set
            {
                SetProperty(ref criteriaOptions, value);
            }
        }

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
                //GetScores();
            });
        }

        private void GetScores() {
            CriteriaOptions = new ObservableCollection<CriteriaOption>();
            foreach (var option in CurrentEvaluation.Options) {
                foreach (var criteria in currentEvaluation.Criterias) {
                    CriteriaOptions.Add(
                    new CriteriaOption() {
                        OptionId = option.Id,
                        CriteriaId = criteria.Id,
                        Score = 0
                    });
                }
            }
        }
    }
}
