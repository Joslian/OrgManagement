using System;
using System.Linq;
using System.Windows;
using MahApps.Metro.Controls;
using Microsoft.EntityFrameworkCore;
using OrgManagement.Database.Models;

namespace OrgManagement.Views
{
    public partial class MainView : MetroWindow
    {
        private readonly Database.OrgContext _orgContext;

        public MainView()
        {
            InitializeComponent();

            _orgContext = new Database.OrgContext();

            _orgContext.Employees.Load();

            DataGridEmployees.ItemsSource = _orgContext.Employees.Local.ToBindingList();
        }

        private void ButtonAddClick(object sender, RoutedEventArgs e)
        {
            _orgContext.Employees.Add(new Employee("Иванов", "Иван", "Иванович"));

            _orgContext.SaveChanges();

            //DataGridEmployees.ItemsSource = orgContext.Employees.Local.ToBindingList();
        }

        private void ButtonUpdateClick(object sender, RoutedEventArgs e)
        {
            _orgContext.SaveChanges();
        }

        private void ButtonDeleteClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтвердить удаление", "Подтверждение действия", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }

            var collection = DataGridEmployees.SelectedItems.OfType<Employee>().ToList();

            foreach (var item in collection)
            {
                if (item is Employee employee)
                {
                    _orgContext.Employees.Remove(employee);
                }
            }
        }
    }
}
