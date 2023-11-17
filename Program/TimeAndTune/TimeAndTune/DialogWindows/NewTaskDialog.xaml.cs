using EFCore;
using EFCore.Service;
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

namespace TimeAndTune
{
    /// <summary>
    /// Interaction logic for NewTaskDialog.xaml
    /// </summary>
    public partial class NewTaskDialog : Window
    {
        private bool noNeedToCloseOnDeactivated = false;
        public void goBackToHomePage(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Window.GetWindow((DependencyObject)sender);

            if (currentWindow != null)
            {
                noNeedToCloseOnDeactivated = true;
                currentWindow.Close();
            }
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
            string taskName = txtTaskName.Text;
            string taskDescription = txtDescription.Text;
            DateOnly taskExpectedTime;
            int taskPriority = 0;
            int userIdRef = MainWindow.ActiveUser.Userid;
            ComboBox priorComboBox = (ComboBox)priorBtn.Template.FindName("priorComboBox", priorBtn);
            if (priorComboBox != null)
            {
                ComboBoxItem priorityComboBoxItem = (ComboBoxItem)priorComboBox.SelectedItem;
                noNeedToCloseOnDeactivated = true;
                if (priorityComboBoxItem.Content.ToString() == "Normal")
                {
                    taskPriority = 1;
                }
                else if(priorityComboBoxItem.Content.ToString() == "Important")
                {
                    taskPriority = 2;
                }
                else
                {
                    taskPriority = 3;
                }
            }
            else{
                taskPriority = 1;
            }
            if (DateOnly.TryParse(txtDate.Text, out DateOnly result))
            {
                taskExpectedTime = result;
                noNeedToCloseOnDeactivated = true;
                DateOnly today = DateOnly.FromDateTime(DateTime.Now);
                if (taskExpectedTime >= today)
                {
                    if (taskName == "") 
                    {
                        MessageBox.Show("Please enter task name!");
                    }
                    else {
                        DatabaseTaskProvider taskService = new DatabaseTaskProvider();
                        taskService.addNewTask(taskName, taskDescription, taskExpectedTime, taskPriority, userIdRef);
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("You can not set expected finish date to past time!");
                }
                
            }
            else{
                MessageBox.Show("Date was set incorrectly! Please enter in format dd/mm/yyyy.");
            }
        }
        public NewTaskDialog()
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
            // Дозволяємо тільки цифри та символи дати ("/", ".", "-", тощо)
            if (!char.IsDigit(e.Text[0]) && e.Text[0] != '/' && e.Text[0] != '.' && e.Text[0] != '-')
            {
                e.Handled = true;
            }
            else
            {
                TextBox textBox = (TextBox)sender;
                string currentText = textBox.Text;
                int caretIndex = textBox.CaretIndex;

                // Вставляємо символи розділення для отримання формату дати
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
