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

namespace WpfAppTaskEZEPEZE
{

    public partial class Window1 : Window
    {
        public User User { get; private set; }
        public Window1()
        {
            InitializeComponent();
            User = new User { RegistrationDate = DateTime.Now }; 
        }

        
        public Window1(User user) : this() 
        {
            User = user; 
            FullNameTextBox.Text = User.FullName; 
            EmailTextBox.Text = User.Email;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text) || string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            User.FullName = FullNameTextBox.Text;
            User.Email = EmailTextBox.Text;

            
            DialogResult = true;
            Close();
        }
    }
}