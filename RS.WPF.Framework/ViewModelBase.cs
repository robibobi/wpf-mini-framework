using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace RS.WPF.Framework
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName]string propertyName = default(string))
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> expression)
        {

            MemberExpression expressionBody = (MemberExpression)expression.Body;

            if (expressionBody == null)
            {
                throw new ArgumentException("Invalid parameter", "expression");
            }

            PropertyInfo property = (PropertyInfo)expressionBody.Member;

            if (property == null)
            {
                throw new ArgumentException("Parameter must be a property", "expression");
            }

            RaisePropertyChanged(property.Name);
        }

    }

       
}
