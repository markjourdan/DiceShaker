using System.Linq;
using Coding4Fun.DiceShaker.Application.Controls;
using Coding4Fun.DiceShaker.Application.Helper;
using Coding4Fun.DiceShaker.Application.Views;

namespace Coding4Fun.DiceShaker.Application.Presenters
{
    public class SettingsPresenter
    {
        private readonly ISettingsView _view;

        public SettingsPresenter(ISettingsView view)
        {
            _view = view;
        }

        public void Initialize()
        {
            _view.InitializeProfiles(ProfileHelper.Profiles.Select(p => p.Name).ToList());
            _view.InitializeDice(SixSidedDice.Configurations.Select(c => c.Name).ToList());
            SetInitialValues();
        }

        private void SetInitialValues()
        {
            _view.NumberOfDice =
                SixSidedDice.GetConfigurationNameFromNumberOfDice(SettingsHelper.ApplicationSettings.NumberOfDice);
            _view.DiceProfile = SettingsHelper.ApplicationSettings.DefaultProfile;
        }

        public void Save()
        {
            SettingsHelper.ApplicationSettings.NumberOfDice =
                SixSidedDice.GetNumberOfDiceFromConfigurationName(_view.NumberOfDice);
            SettingsHelper.ApplicationSettings.DefaultProfile = _view.DiceProfile;
            SettingsHelper.Save();
        }
    }
}