using System;
using System.Collections.Generic;
using System.Reflection;

namespace EifelMono.Core
{
    public class Check
    {
        public bool IsChanged { get; set; } = false;
        public void ResetChanged() => IsChanged = false;

        public static IEnumerable<Check> ThisChecks(object thisObject)
        {
            var list = new List<Check>();
            if (thisObject == null)
                return list;
            foreach (var propertyInfo in thisObject.GetType().GetTypeInfo().GetProperties()) 
            {
                if (propertyInfo.PropertyType.GetTypeInfo().IsSubclassOf(typeof(Check)))
                    list.Add((Check)propertyInfo.GetValue(thisObject));
            }
            return list;
        }

        public static bool ThisIsChanged(object thisObject)
        {
            bool result = false;
            foreach (var item in ThisChecks(thisObject))
                result = result || item.IsChanged;
            return result;
        }

        public static void ThisResetChanged(object thisObject)
        {
            foreach (var item in ThisChecks(thisObject))
                item.ResetChanged();
        }
    }

    public class Check<T> : Check where T : IComparable
    {
        public Check()
        {
        }

        public Check(T value)
        {
            Value = value;
        }

        T _Value = default(T);

        public T Value
        {
            get
            {
                return _Value;
            }
            set
            {

                if ((object)_Value == null)
                    IsChanged = false;
                else
                    IsChanged = _Value.CompareTo(value) != 0;
                _Value = value;
            }
        }
    }
}
