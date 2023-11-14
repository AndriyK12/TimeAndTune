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
    /// Interaction logic for NewTaskDialog.xaml
    /// </summary>
    public partial class NewTaskDialog : Window
    {
        public void goBackToHomePage(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Window.GetWindow((DependencyObject)sender);

            if (currentWindow != null)
            {
                currentWindow.Close();
            }
        }

        public NewTaskDialog()
        {
            InitializeComponent();

        }
    }
}
