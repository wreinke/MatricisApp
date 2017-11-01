namespace Matricis.ViewModels {

    public class CriteriaDetailViewModel {

        public Criteria criteria { get; set; }

        public CriteriaDetailViewModel(Criteria item = null) {
            Title = item.Text;
            Item = item;
        }

        int quantity = 1;

        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }
    }
}
