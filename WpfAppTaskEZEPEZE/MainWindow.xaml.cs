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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppTaskEZEPEZE
{

    public partial class MainWindow : Window
    {
        private AndrrevD223Entities db;
        public MainWindow()
        {
            InitializeComponent();
            db = new AndrrevD223Entities(); 
            LoadUsers(); 
        }

        private void LoadUsers()
        {
            UsersDataGrid.ItemsSource = db.Users.ToList(); 
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchText = SearchTextBox.Text;
            var users = db.Users
                .Where(u => u.FullName.Contains(searchText) || u.Email.Contains(searchText))
                .ToList();
            UsersDataGrid.ItemsSource = users;
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            var addUserWindow = new Window1();
            if (addUserWindow.ShowDialog() == true)
            {
                db.Users.Add(addUserWindow.User); 
                db.SaveChanges(); 
                LoadUsers(); 
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = UsersDataGrid.SelectedItem as User;
            if (selectedUser != null)
            {
                var editUserWindow = new Window1(selectedUser); 
                if (editUserWindow.ShowDialog() == true)
                {
                    db.SaveChanges(); 
                    LoadUsers(); 
                }
            }
            else
            {
                MessageBox.Show("Please select a user to edit.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = UsersDataGrid.SelectedItem as User;
            if (selectedUser != null)
            {
                db.Users.Remove(selectedUser); 
                db.SaveChanges(); 
                LoadUsers(); 
            }
            else
            {
                MessageBox.Show("Please select a user to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            var startDate = DateTime.Now.AddDays(-30);

            var filteredUsers = db.Users
                .Where(u => u.RegistrationDate >= startDate) 
                .ToList();

            UsersDataGrid.ItemsSource = filteredUsers;
        }
    }
    }