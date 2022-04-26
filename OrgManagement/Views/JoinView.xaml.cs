using System;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using OrgManagement.ViewModels;

namespace OrgManagement.Views
{
    public partial class JoinView : MetroWindow
    {
        public JoinView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is not PasswordBox passwordBox)
                return;

            if (DataContext is not JoinViewModel viewModel)
                return;

            viewModel.Password = passwordBox.SecurePassword;
        }

        private void PasswordBox_RepasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is not PasswordBox passwordBox)
                return;

            if (DataContext is not JoinViewModel viewModel)
                return;

            viewModel.Repassword = passwordBox.SecurePassword;
        }
    }
}
