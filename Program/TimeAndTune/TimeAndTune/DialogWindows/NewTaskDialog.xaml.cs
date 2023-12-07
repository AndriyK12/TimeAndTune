namespace TimeAndTune
{
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
    using System.Xml.Linq;
    using EFCore;
    using EFCore.Service;
    using TimeAndTune.BLL;

    public partial class NewTaskDialog : Window
    {
        public bool noNeedToCloseOnDeactivated = false;
        public HomePage homePage;

        public NewTaskDialog(HomePage homePage)
        {
            InitializeComponent();
            this.homePage = homePage;
        }

        public void goBackToHomePage(object sender, RoutedEventArgs e)
        {
            NewTaskDialogBack.goBackToHomePage(sender, e, this);
        }

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            if (!noNeedToCloseOnDeactivated)
            {
                Close();
            }
        }

        private void addNewTask_Click(object sender, RoutedEventArgs e)
        {
            NewTaskDialogBack.addNewTask_Click(sender, e, this);
        }

        private void txtTaskName_TextChanged(object sender, TextChangedEventArgs e)
        {
            NewTaskDialogBack.txtTaskName_TextChanged(sender, e, this);
        }

        private void txtTaskName_GotFocus(object sender, RoutedEventArgs e)
        {
            NewTaskDialogBack.txtTaskName_GotFocus(sender, e, this);
        }

        private void txtTaskName_LostFocus(object sender, RoutedEventArgs e)
        {
            NewTaskDialogBack.txtTaskName_LostFocus(sender, e, this);
        }

        private void txtDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            NewTaskDialogBack.txtDate_TextChanged(sender, e, this);
        }

        private void txtDate_GotFocus(object sender, RoutedEventArgs e)
        {
            NewTaskDialogBack.txtDate_GotFocus(sender, e, this);
        }

        private void txtDate_LostFocus(object sender, RoutedEventArgs e)
        {
            NewTaskDialogBack.txtDate_LostFocus(sender, e, this);
        }

        private void txtDate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            NewTaskDialogBack.txtDate_PreviewTextInput(sender, e, this);
        }

        private void txtDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            NewTaskDialogBack.txtDescription_TextChanged(sender, e, this);
        }

        private void txtDescription_GotFocus(object sender, RoutedEventArgs e)
        {
            NewTaskDialogBack.txtDescription_GotFocus(sender, e, this);
        }

        private void txtDescription_LostFocus(object sender, RoutedEventArgs e)
        {
            NewTaskDialogBack.txtDescription_LostFocus(sender, e, this);
        }
    }
}
