using System.Collections.Generic;
using System.Windows.Controls;
using Coding4Fun.DiceShaker.Application.Controls;

namespace Coding4Fun.DiceShaker.Application.Helper
{
    public class ProfileHelper
    {
        private static readonly Profile ProfileMonoploy = new Profile {Name = "Monopoly"};
        private static readonly Profile ProfileRisk = new Profile {Name = "Risk"};
        private static readonly Profile ProfileGeneral = new Profile {Name = "General"};

        private static List<Profile> _profiles;

        public static Profile DefaultProfile
        {
            get { return ProfileMonoploy; }
        }

        public static List<Profile> Profiles
        {
            get
            {
                return _profiles ?? (_profiles = new List<Profile>
                                                     {
                                                         ProfileMonoploy,
                                                         ProfileRisk,
                                                         ProfileGeneral,
                                                     });
            }
        }

        public static UserControl GetControlByName(string profile)
        {
            if(profile.Equals(ProfileMonoploy.Name))
                return new Monopoly();

            if (profile.Equals(ProfileRisk.Name))
                return new Risk();

            return new SixSidedDice();
        }
    }
}

public class Profile
{
    public string Name { get; set; }
}