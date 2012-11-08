using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using Coding4Fun.DiceShaker.Application.Presenters;
using Coding4Fun.DiceShaker.Application.Views;

using Microsoft.Phone.Controls;

namespace Coding4Fun.DiceShaker
{
    public partial class SettingsPage : ISettingsView
    {
        private readonly SettingsPresenter _presenter;
        
        public SettingsPage()
        {
            _presenter = new SettingsPresenter(this);
            InitializeComponent();

            _presenter.Initialize();
        }

        #region ISettingsView Members

        public string NumberOfDice
        {
			get { return cbNumberOfDice.SelectedItem.ToString(); }
			set { SetPickerValue(cbNumberOfDice, value); }
        }

        public string DiceProfile
        {
            get { return cbProfiles.SelectedItem.ToString(); }
            set { SetPickerValue(cbProfiles, value); }
        }

        public void InitializeProfiles(List<String> profiles)
        {
            cbProfiles.ItemsSource = profiles;
        }

        public void InitializeDice(List<string> numberOfDice)
        {
            cbNumberOfDice.ItemsSource = numberOfDice;
        }

        #endregion

        private static void SetPickerValue(ListPicker picker, object value)
        {
            picker.SelectedIndex = picker.Items.IndexOf(value);
        }

        private void Save_Click(object sender, EventArgs eventArgs)
        {
            _presenter.Save();

			if (NavigationService.CanGoBack)
				NavigationService.GoBack();
        }
		
		private void cbProfiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			cbNumberOfDice.Visibility = (DiceProfile.Equals("General")) ? Visibility.Visible : Visibility.Collapsed;
		}
	}
}