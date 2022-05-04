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
            _orgContext.Departments.Load();
            _orgContext.Posts.Load();

            DataGridEmployees.ItemsSource = _orgContext.Employees.Local.ToBindingList();
            DataGridDepartments.ItemsSource = _orgContext.Departments.Local.ToBindingList();
            DataGridPosts.ItemsSource = _orgContext.Posts.Local.ToBindingList();
        }

        private void ButtonUpdateClick(object sender, RoutedEventArgs e)
        {
            _orgContext.SaveChanges();
        }

        private void AddEmployeeClick(object sender, RoutedEventArgs e)
        {
            var department = DataGridDepartments.SelectedItem as Department;
            var post = DataGridPosts.SelectedItem as Post;

            _orgContext.Employees.Add(new Employee("Иванов", "Иван", "Иванович", 10_000)
            {
                DepartmentId = department?.DepartmentId,
                PostId = post?.PostId,
            });

            _orgContext.SaveChanges();
        }

        private void DelEmployeeClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтвердить удаление", "Подтверждение действия",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
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

        private void AddDepartmentClick(object sender, RoutedEventArgs e)
        {
            _orgContext.Departments.Add(new Department
            {
                Name = "Сотрудник",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            });

            _orgContext.SaveChanges();
        }

        private void DelDepartmentClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтвердить удаление", "Подтверждение действия",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }

            var collection = DataGridDepartments.SelectedItems.OfType<Department>().ToList();

            foreach (var item in collection)
            {
                if (item is Department department)
                {
                    _orgContext.Departments.Remove(department);
                }
            }
        }

        private void AddPostClick(object sender, RoutedEventArgs e)
        {
            _orgContext.Posts.Add(new Post
            {
                Name = "Должность",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            });

            _orgContext.SaveChanges();
        }

        private void DelPostClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Подтвердить удаление", "Подтверждение действия",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }

            var collection = DataGridPosts.SelectedItems.OfType<Post>().ToList();

            foreach (var item in collection)
            {
                if (item is Post post)
                {
                    _orgContext.Posts.Remove(post);
                }
            }
        }
    }
}
