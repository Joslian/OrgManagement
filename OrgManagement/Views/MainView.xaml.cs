using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using MahApps.Metro.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Newtonsoft.Json;
using OfficeOpenXml;
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
            if (DataGridDepartments.SelectedItem is not Department department)
            {
                MessageBox.Show("Отдел не выбран");
                return;
            }

            if (DataGridPosts.SelectedItem is not Post post)
            {
                MessageBox.Show("Должность не выбрана");
                return;
            }

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

        private void ExportExcelClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                FileName = "export",
                DefaultExt = ".xlsx",
            };

            if (dialog.ShowDialog() == false || string.IsNullOrEmpty(dialog.FileName))
                return;

            using ExcelPackage pck = new(new FileInfo(dialog.FileName));

            //DataTable dt = new();

            //dt.Columns.AddRange(new DataColumn[]
            //{
            //    new DataColumn { ColumnName = "Фамилия" },
            //    new DataColumn { ColumnName = "Имя" },
            //    new DataColumn { ColumnName = "Отчество" },
            //    new DataColumn { ColumnName = "З/П" },
            //    new DataColumn { ColumnName = "Отдел" },
            //    new DataColumn { ColumnName = "Должность" },
            //    new DataColumn { ColumnName = "Отпуск" },
            //    new DataColumn { ColumnName = "Принят" },
            //});

            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Employees");

            // Columns
            {
                ws.SetValue(1, 1, "Фамилия");
                ws.SetValue(1, 2, "Имя");
                ws.SetValue(1, 3, "Отчество");
                ws.SetValue(1, 4, "З/П");
                ws.SetValue(1, 5, "Отдел");
                ws.SetValue(1, 6, "Должность");
                ws.SetValue(1, 7, "Отпуск");
                ws.SetValue(1, 8, "Принят");
            }

            var employees = _orgContext.Employees.Local.ToList();
            var departments = _orgContext.Departments.Local.ToBindingList();
            var posts = _orgContext.Posts.Local.ToBindingList();

            for (int rowIndex = 0; rowIndex < employees.Count; rowIndex++)
            {
                var row = employees[rowIndex];

                var departmentName = departments.FirstOrDefault(x => x.DepartmentId == row.DepartmentId)?.Name ?? $"{row.DepartmentId}";
                var postName = posts.FirstOrDefault(x => x.PostId == row.PostId)?.Name ?? $"{row.PostId}";

                int rIndex = rowIndex + 2;
                ws.SetValue(rIndex, 1, row.LastName);
                ws.SetValue(rIndex, 2, row.FirstName);
                ws.SetValue(rIndex, 3, row.MiddleName);
                ws.SetValue(rIndex, 4, $"{row.Salary} RUB");
                ws.SetValue(rIndex, 5, departmentName);
                ws.SetValue(rIndex, 6, postName);
                ws.SetValue(rIndex, 7, Helpers.DateTimeConverter.CalcVacationDays(row.AcceptedAt));
                ws.SetValue(rIndex, 8, $"{row.CreatedAt}");
            }

            //ws.Cells["A1"].LoadFromDataTable(dt, true);

            pck.Save();
        }
    }
}
