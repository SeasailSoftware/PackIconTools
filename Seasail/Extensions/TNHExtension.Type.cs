using System;
using System.Collections.Generic;
using System.Text;

namespace Seasail.Extensions
{
    public static partial class TNHExtension
    {

        public static T CreateInstance<T>(this Type type)
        {
            var constructor = type.GetConstructor(new Type[0]);
            return (T)constructor.Invoke(new Type[0]);
        }
    }
}
