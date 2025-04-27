
namespace FinanceApp.Models
{
    public partial class EpargneModel : ContentView
    {

        public static readonly BindableProperty TitreProperty =
            BindableProperty.Create(nameof(Titre), typeof(string), typeof(EpargneModel), string.Empty, propertyChanged: OnTitreChanged);
        public static readonly BindableProperty DescriptionProperty =
            BindableProperty.Create(nameof(Description), typeof(string), typeof(string), string.Empty, propertyChanged: OnDescriptionChanged);
        public static readonly BindableProperty PourcentageProperty =
        BindableProperty.Create(nameof(Pourcentage), typeof(string), typeof(EpargneModel), string.Empty, propertyChanged: OnPourcentageChanged);
        public static readonly BindableProperty MontantProperty =
            BindableProperty.Create(nameof(Montant), typeof(string), typeof(EpargneModel), string.Empty, propertyChanged: OnMontantChanged);
        public static readonly BindableProperty CurrentAmountProperty =
            BindableProperty.Create(nameof(CurrentAmount), typeof(string), typeof(EpargneModel), string.Empty, propertyChanged: OnCurrentAmountChanged);
        public static readonly BindableProperty ProgressionProperty =
             BindableProperty.Create(nameof(Progression), typeof(double), typeof(EpargneModel), default(double), propertyChanged: OnProgressionChanged);
        public EpargneModel()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public string Titre
        {
            get => (string)GetValue(TitreProperty);
            set => SetValue(TitreProperty, value);
        }

        public double Progression
        {
            get => (double)GetValue(ProgressionProperty);
            set => SetValue(ProgressionProperty, value);
        }

        public string CurrentAmount
        {
            get => (string)GetValue(CurrentAmountProperty);
            set => SetValue(CurrentAmountProperty, value);
        }
        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public string Pourcentage
        {
            get => (string)GetValue(PourcentageProperty);
            set => SetValue(PourcentageProperty, value);
        }


        public string Montant
        {
            get => (string)GetValue(MontantProperty);
            set => SetValue(MontantProperty, value);
        }

        private static void OnTitreChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is EpargneModel model)
            {
                model.titreLabel.Text = (string)newValue;
            }
        }

        private static void OnDescriptionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is EpargneModel model)
            {
                model.descriptionLabel.Text = (string)newValue;
            }

        }
        private static void OnMontantChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is EpargneModel model)
            {
                model.montantLabel.Text = (string)newValue;
            }
        }

        private static void OnPourcentageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is EpargneModel model)
            {
                model.pourcentageLabel.Text = (string)newValue;
            }
        }
        private static void OnCurrentAmountChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is EpargneModel model)
            {
                model.CurrentAmontLabel.Text = (string)newValue;
            }
        }

        private static void OnProgressionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is EpargneModel model)
            {
                model.progressBar.Progress = (double)newValue;
            }
        }

    }
}
