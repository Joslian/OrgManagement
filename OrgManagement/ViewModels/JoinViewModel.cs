using System;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Input;
using OrgManagement.Helpers;

namespace OrgManagement.ViewModels
{
    public class JoinViewModel : ViewModelBase
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

        private SecureString? _repassword;
        public SecureString? Repassword
        {
            get => _repassword;
            set
            {
                _repassword = value;
                NotifyPropertyChanged(() => Repassword);
            }
        }

        private ICommand _joinCommand;
        public ICommand JoinCommand
        {
            get => _joinCommand;
            set
            {
                _joinCommand = value;
                NotifyPropertyChanged(() => JoinCommand);
            }
        }

        public JoinViewModel()
        {
            _joinCommand = new RelayCommand(JoinExecute, CanExecuteJoinCommand);
        }

        private bool CanExecuteJoinCommand(object? parameter)
        {
            if (string.IsNullOrEmpty(Login))
                return false;

            if (Password is null || Password.Length == 0)
                return false;

            if (Repassword is null || Repassword.Length == 0)
                return false;

            return true;
        }

        public void JoinExecute(object? parameter)
        {
            if (string.IsNullOrEmpty(Login))
                return;

            if (Password is null)
                return;

            var password = Password.GetString();
            if (string.IsNullOrEmpty(password))
                return;

            if (Repassword is null)
                return;

            var repassword = Repassword.GetString();
            if (string.IsNullOrEmpty(repassword))
                return;

            if (!password.Equals(repassword))
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }

            using var orgContext = new Database.OrgContext();

            if (orgContext.Admins.Any(x => x.Login == Login))
            {
                MessageBox.Show("Пользователь существует");
                return;
            }

            orgContext.Admins.Add(new Database.Models.Admin(Login, password));

            orgContext.SaveChanges();

            MessageBox.Show("Успешная регистрация!");
        }
    }
}
