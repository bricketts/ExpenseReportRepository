using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ExpenseReportRepo
{
    public static class MVCExtensions
    {
        public static void CopyData<T>(this T destination, T newData)
        {
            var type = newData.GetType();
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                propertyInfo.SetValue(destination, propertyInfo.GetValue(newData));
            }
        }
    }
}
