using System;
using System.Windows.Controls;
using System.Windows.Threading;
using Coding4Fun.DiceShaker.Application.Controls;
using Coding4Fun.DiceShaker.Application.Helper;
using Coding4Fun.DiceShaker.Application.Views;
using Microsoft.Phone.Shell;

namespace Coding4Fun.DiceShaker.Application.Presenters
{
    public class MainPagePresenter
    {
        private const double AccelerometerThreshold = .2;
        private const int MaxTimesRolled = 1;
        private readonly AccelerometerHelper _accelerometerHelper;

        private readonly UserControl _diceControl;
        private readonly DispatcherTimer _diceRollerTimer;
        private readonly IMainPageView _view;
        private int _timesRolled;

        public MainPagePresenter(IMainPageView view)
        {
            _view = view;
            _diceControl = ProfileHelper.GetControlByName(SettingsHelper.ApplicationSettings.DefaultProfile);
            _accelerometerHelper = new AccelerometerHelper(0, 0, 0, AccelerometerThreshold);
            _diceRollerTimer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 0, 0, 60)};
            _diceRollerTimer.Tick += RollDiceAnnimation;

            PhoneApplicationService.Current.Deactivated += Current_Deactivated;
        }

        private void Current_Deactivated(object sender, DeactivatedEventArgs e)
        {
            _diceRollerTimer.Stop();
        }

        public void Initialize()
        {
            _view.InitializeAccelerometer();
            _view.LoadDynamicControl(_diceControl);
            RollDice();
        }

        public void RollDice()
        {
            if (!_diceRollerTimer.IsEnabled)
            {
                _diceControl.Dispatcher.BeginInvoke(() =>
                                                        {
                                                            ((IDiceControl) _diceControl).FillDice();
                                                            _diceRollerTimer.Start();
                                                        });
            }
        }

        private void RollDiceAnnimation(object sender, EventArgs eventArgs)
        {
            if (_timesRolled < MaxTimesRolled)
            {
                ((IDiceControl) _diceControl).Dice.ThrowDice();

                ((IDiceControl) _diceControl).SetValues();
                _timesRolled++;
            }
            else
            {
                _timesRolled = 0;
                _diceRollerTimer.Stop();
                ((IDiceControl) _diceControl).SetTotals();
            }
        }

        public void AccelerometerReading(double x, double y, double z)
        {
            if (_accelerometerHelper.IsShook(x, y, z))
                RollDice();
        }
    }
}