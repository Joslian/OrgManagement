using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace OrgManagement.Helpers
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NotifyPropertyChanged<TProperty>(Expression<Func<TProperty>> property)
        {
            if (property is LambdaExpression lambda)
            {
                if (lambda.Body is UnaryExpression unaryExpression)
                {
                    if (unaryExpression.Operand is MemberExpression unaryMemberExpression)
                    {
                        NotifyPropertyChanged(unaryMemberExpression.Member.Name);
                    }
                }
                else if (lambda.Body is MemberExpression memberExpression)
                {
                    NotifyPropertyChanged(memberExpression.Member.Name);
                }
            }
        }
    }
}
