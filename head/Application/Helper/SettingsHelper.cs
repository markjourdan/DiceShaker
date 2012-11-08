using System.IO.IsolatedStorage;

namespace Coding4Fun.DiceShaker.Application.Helper
{
	public static class SettingsHelper
	{
		private static readonly object SaveLock = new object();

		public static Settings ApplicationSettings
		{
			get { return _applicationSettings ?? (_applicationSettings = new Settings()); }
			set { _applicationSettings = value; }
		}
		private static Settings _applicationSettings;

		public static void Save()
		{
			lock (SaveLock)
			{
				IsolatedStorageSettings.ApplicationSettings.Save();               
			}
		}

		#region Nested type: Settings

		public class Settings
		{
			private const string SettingsNumberOfDice = "NumberOfDice";
			private const string SettingsDefaultProfile = "DefaultProfile";
			private static readonly IsolatedStorageSettings UserSettings = IsolatedStorageSettings.ApplicationSettings;

			/// <summary>
			/// The default number of dice.
			/// </summary>
			public int NumberOfDice
			{
				get
				{
					int numberOfDice;
					return UserSettings.TryGetValue(SettingsNumberOfDice, out numberOfDice) ? numberOfDice : 1;
				}
				set { UserSettings[SettingsNumberOfDice] = value; }
			}

			/// <summary>
			/// The default profile.
			/// </summary>
			public string DefaultProfile
			{
				get
				{
					string defaultProfile;
					return UserSettings.TryGetValue(SettingsDefaultProfile, out defaultProfile)
							   ? defaultProfile
							   : ProfileHelper.DefaultProfile.Name;
				}
				set { UserSettings[SettingsDefaultProfile] = value; }
			}
		}

		#endregion
	}
}