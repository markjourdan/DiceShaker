using System;
using System.Windows.Controls;
using Coding4Fun.DiceShaker.Application.Presenters;
using Coding4Fun.DiceShaker.Application.Views;
using Coding4Fun.Phone.Controls;
using Microsoft.Devices.Sensors;
using Microsoft.Phone.Controls;

namespace Coding4Fun.DiceShaker
{
    public partial class MainPage : IMainPageView
    {
        private readonly Accelerometer _accelSensor = new Accelerometer();
        private MainPagePresenter _presenter;
        private bool IsInit { get; set; }

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            IsInit = true;
        }

		protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
		{
            if (IsInit)
            {
                _presenter = new MainPagePresenter(this);
                _presenter.Initialize();
                IsInit = false;
            }
		}

        #region IMainPageView Members

        public void LoadDynamicControl(UserControl control)
        {
            Content.Children.Clear();
            Content.Children.Add(control);
        }

        public void InitializeAccelerometer()
        {
            _accelSensor.CurrentValueChanged += AccelerometerReadingChanged;
            _accelSensor.Start();
        }

        #endregion

        private void AccelerometerReadingChanged(object sender, SensorReadingEventArgs<AccelerometerReading> sensorReadingEventArgs)
        {

            _presenter.AccelerometerReading(sensorReadingEventArgs.SensorReading.Acceleration.X, sensorReadingEventArgs.SensorReading.Acceleration.Y, sensorReadingEventArgs.SensorReading.Acceleration.Z);
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.RelativeOrAbsolute));
            IsInit = true;
        }

        private void OnFlick(object sender, FlickGestureEventArgs e)
        {
            _presenter.RollDice();
        }

        private void LaunchAbout_Click(object sender, EventArgs e)
        {
            var about = new AboutPrompt {Footer = new About()};

            about.Show("Mark Jourdan", "MarkJourdan", "mjourdan@markjourdan.com", @"http://www.markjourdan.name");
        }
    }
}