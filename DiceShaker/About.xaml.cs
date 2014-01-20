using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Tasks;

namespace Coding4Fun.DiceShaker
{
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
        }

        private void Code4FunImageTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var browserTask = new WebBrowserTask {Uri = new Uri("http://www.coding4fun.com")};
            browserTask.Show();
        }
    }
}
