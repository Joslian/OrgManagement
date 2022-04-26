using System;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Input;
using OrgManagement.Helpers;

namespace OrgManagement.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        private string? _login;
        public string? Login
        {
            get => _login;
            set
            {
                _login = value;
                NotifyPropertyChanged(() => Login);
            }
        }

        private SecureString? _password;
        public SecureString? Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyPropertyChanged(() => Password);
            }
        }

        private ICommand _authCommand;
        public ICommand AuthCommand
        {
            get => _authCommand;
            set
            {
                _authCommand = value;
                NotifyPropertyChanged(() => AuthCommand);
            }
        }

        private ICommand _showJoinCommand;
        public ICommand ShowJoinCommand
        {
            get => _showJoinCommand;
            set
            {
                _showJoinCommand = value;
                NotifyPropertyChanged(() => ShowJoinCommand);
            }
        }

        public AuthViewModel()
        {
            _authCommand = new RelayCommand(AuthExecute, CanExecuteAuthCommand);
            _showJoinCommand = new RelayCommand(ShowJoinExecute, (object? p) => true);
        }

        private bool CanExecuteAuthCommand(object? parameter)
        {
            if (string.IsNullOrEmpty(Login))
                return false;

            if (Password is null || Password.Length == 0)
                return false;

            return true;
        }

        public void AuthExecute(object? parameter)
        {
            if (string.IsNullOrEmpty(Login))
                return;

            if (Password is null)
                return;

            var password = Password.GetString();
            if (string.IsNullOrEmpty(password))
                return;

            using var orgContext = new Database.OrgContext();

            var result = orgContext.Admins.FirstOrDefault(x => x.Login == Login);
            if (result is null)
            {
                MessageBox.Show("Пользователь не существует");
                return;
            }

            if (result.Password != password)
            {
                MessageBox.Show("Неверный пароль");
                return;
            }

            var authView = Application.Current.MainWindow;
            Application.Current.MainWindow = new Views.MainView();
            authView.Close();
            Application.Current.MainWindow.Show();
        }

        public void ShowJoinExecute(object? parameter)
        {
            var joinView = new Views.JoinView();
            joinView.ShowDialog();
        }
    }
}
