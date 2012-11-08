using System.Windows.Controls;

namespace Coding4Fun.DiceShaker.Application.Views
{
    public interface IMainPageView
    {
        /// <summary>
        /// Loads a dynamic control
        /// </summary>
        /// <param name="control"></param>
        void LoadDynamicControl(UserControl control);

        void InitializeAccelerometer();
    }
}