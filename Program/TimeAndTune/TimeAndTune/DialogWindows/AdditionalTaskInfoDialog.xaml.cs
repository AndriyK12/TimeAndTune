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

namespace TimeAndTune.DialogWindows
{
    /// <summary>
    /// Interaction logic for AdditionalTaskInfoDialog.xaml
    /// </summary>


    public partial class AdditionalTaskInfoDialog : Window
    {
        private bool isClosedByUser = false;
        public void goBackToHomePage(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Window.GetWindow((DependencyObject)sender);

            if (currentWindow != null)
            {
                isClosedByUser = true;
                currentWindow.Close();
            }
        }
        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            if (!isClosedByUser)
            {
                Close();
            }
        }
        public AdditionalTaskInfoDialog()
        {
            InitializeComponent();

        }

        private void txtTaskName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = txtTaskName.Text;
            Console.WriteLine("Text changed: " + newText);
        }
        private void txtTaskName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtTaskName.Text == "")
            {
                txtTaskName.Text = "";
            }
        }

        private void txtTaskName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskName.Text))
            {
                txtTaskName.Text = "";
            }
        }

        private void txtDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = txtDate.Text;
            Console.WriteLine("Text changed: " + newText);
        }
        private void txtDate_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtDate.Text == "")
            {
                txtDate.Text = "";
            }
        }

        private void txtDate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDate.Text))
            {
                txtDate.Text = "";
            }
        }

        private void txtDate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text[0]) && e.Text[0] != '/' && e.Text[0] != '.' && e.Text[0] != '-')
            {
                e.Handled = true;
            }
            else
            {
                TextBox textBox = (TextBox)sender;
                string currentText = textBox.Text;
                int caretIndex = textBox.CaretIndex;

                if ((caretIndex == 2 || caretIndex == 5) && caretIndex < 10)
                {
                    textBox.Text = currentText + "/";
                    textBox.CaretIndex = caretIndex + 1;
                }
                if (textBox.Text.Length >= 10)
                {
                    e.Handled = true;
                }

                //if (caretIndex == 0 && (e.Text[0] > '1' || (e.Text[0] == '1' && currentText.Length > 0 && currentText[0] > '1')))
                //{
                //    e.Handled = true; // Забороняємо введення значень більше 12 для місяця
                //}
            }
        }

        private void txtDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newText = txtDescription.Text;
            Console.WriteLine("Text changed: " + newText);
        }
        private void txtDescription_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtDescription.Text == "")
            {
                txtDescription.Text = "";
            }
        }

        private void txtDescription_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                txtDescription.Text = "";
            }
        }
    }
}
