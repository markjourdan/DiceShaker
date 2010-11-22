using System;
using System.Collections.Generic;

namespace Coding4Fun.DiceShaker.Application.Views
{
    public interface ISettingsView
    {
        string NumberOfDice { get; set; }
        string DiceProfile { get; set; }
        void InitializeProfiles(List<string> profiles);
        void InitializeDice(List<string> numberOfDice);
    }
}