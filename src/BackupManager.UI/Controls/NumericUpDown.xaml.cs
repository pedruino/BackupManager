using System.Windows;
using System.Windows.Controls;

namespace BackupManager.UI.Controls
{
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    /// Interaction logic for TemplateNumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        public static readonly DependencyProperty MaxProperty =
            DependencyProperty.Register(nameof(Max),
            typeof(int),
            typeof(NumericUpDown),
            new PropertyMetadata(int.MaxValue));

        public static readonly DependencyProperty MinProperty =
            DependencyProperty.Register(nameof(Min),
            typeof(int),
            typeof(NumericUpDown),
            new PropertyMetadata(0));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value),
                typeof(int),
                typeof(NumericUpDown),
                new FrameworkPropertyMetadata(0,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    propertyChangedCallback: OnValueChanged));

        public NumericUpDown()
        {
            InitializeComponent();
            PART_NumericTextBox.Text = Min.ToString();
        }

        public int Max
        {
            get => (int)GetValue(MaxProperty);
            set => SetValue(MaxProperty, value);
        }

        public int Min
        {
            get => (int)GetValue(MinProperty);
            set => SetValue(MinProperty, value);
        }

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var instance = (NumericUpDown)sender;

            if (args.NewValue is int newValue && newValue >= instance.Min)
            {
                instance.PART_NumericTextBox.Text = newValue.ToString();
            }
            else if (args.OldValue is int oldValue && oldValue >= instance.Min)
            {
                instance.PART_NumericTextBox.Text = oldValue.ToString();
            }
            else
            {
                instance.PART_NumericTextBox.Text = instance.Min.ToString();
            }
        }

        private void Decrement(object sender, RoutedEventArgs e)
        {
            if (Value > Min) Value--;
        }

        private void Increment(object sender, RoutedEventArgs e)
        {
            if (Value < Max) Value++;
        }
    }
}