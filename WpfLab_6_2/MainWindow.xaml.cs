using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfLab_6_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
    class WeatherControl : DependencyObject
    {
        public static readonly DependencyProperty TemperatureProperty;
        public string Date { get; set; }
        public string WindDirection { get; set; }
        public int WindSpeed { get; set; }
        RainDependants RainDependant;
        public int Temperature
        {
            get => (int)GetValue(TemperatureProperty);
            set
            {
                SetValue(TemperatureProperty, value);
            }
        }
        public WeatherControl(string date, string windDirection, int windSpeed, RainDependants raindependant, int temperature)
        {
            Date = date;
            WindDirection = windDirection;
            WindSpeed = windSpeed;
            RainDependant = raindependant;
            Temperature = temperature;
        }

        static WeatherControl()
        {
            TemperatureProperty = DependencyProperty.Register(
                nameof(Temperature),
                typeof(int),
                typeof(WeatherControl),
                new FrameworkPropertyMetadata(
                    0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null,
                    new CoerceValueCallback(CoerceTemperature)),
                new ValidateValueCallback(ValidateTemperature));
        }

        private static bool ValidateTemperature(object value)
        {
            int v = (int)value;
            if (v >= -50 && v <= 50)
                return true;
            else
                return false;
        }

        private static object CoerceTemperature(DependencyObject d, object baseValue)
        {
            int v = (int)baseValue;
            if (v >= -50 && v <= 50)
                return v;
            else
                return 0;
        }
        public string Print()
        {
            return $"{Date} {Temperature} {WindDirection} {WindSpeed} {RainDependant}";
        }

    }
    enum RainDependants
    {
        None = 0,
        Sanny = 1,
        Cloudy = 2,
        Rain = 4,
        Snow = 8
    }
}
