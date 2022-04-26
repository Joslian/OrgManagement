using System;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using OrgManagement.ViewModels;

namespace OrgManagement.Views
{
    public partial class AuthView : MetroWindow
    {
        public AuthView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is not PasswordBox passwordBox)
                return;

            if (DataContext is not AuthViewModel viewModel)
                return;

            viewModel.Password = passwordBox.SecurePassword;
        }
    }
}
