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
using System.Windows.Shapes;

namespace TimeAndTune
{
    /// <summary>
    /// Interaction logic for UserInfoWindow.xaml
    /// </summary>
    public partial class UserInfoWindow : Window
    {
        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            Close();
        }
        public UserInfoWindow()
        {
            InitializeComponent();
            Left = SystemParameters.PrimaryScreenWidth - Width;
            Top = 23;
        }
    }
}
