using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace atheneum_app.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Input : ContentView
    {
        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(Input));

        public string Placeholder
        {
            get => (string) GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(Input));

        public bool IsPassword
        {
            get => (bool) GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create(nameof(MaxLength), typeof(bool), typeof(Input));

        public int MaxLength
        {
            get => (int) GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(Input));

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty IsReadOnlyProperty =
            BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(Input));

        public bool IsReadOnly
        {
            get => (bool) GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        public static readonly BindableProperty KeyboardProperty =
            BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(Input));

        public Keyboard Keyboard
        {
            get => (Keyboard) GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        public Input()
        {
            InitializeComponent();
        }
        
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == KeyboardProperty.PropertyName)
            {
                txtEntry.Keyboard = Keyboard;
            }

            if (propertyName == TextProperty.PropertyName)
            {
                txtEntry.Text = Text;
            }

            if (propertyName == PlaceholderProperty.PropertyName)
            {
                txtEntry.Placeholder = Placeholder;
            }

            if (propertyName == IsPasswordProperty.PropertyName)
            {
                txtEntry.IsPassword = IsPassword;

                if (!IsPassword)
                {
                    txtEntry.IsTextPredictionEnabled = true;
                }
            }

            if (propertyName == IsReadOnlyProperty.PropertyName)
            {
                txtEntry.IsReadOnly = IsReadOnly;
            }

            if (propertyName == MaxLengthProperty.PropertyName)
            {
                txtEntry.MaxLength = MaxLength;
            }
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = e.NewTextValue;
        }
    }
}