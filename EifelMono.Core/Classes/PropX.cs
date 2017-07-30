﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace EifelMono.Core
{
    public class PropX<T> : PropXCore where T : IComparable
    {

        public PropX(IOnPropertyChanged parent, [CallerMemberName] string propertyName = "")
        {
            Parent = parent;
            PropertyName = propertyName;
        }

        public IOnPropertyChanged Parent { get; set; }
        public string PropertyName { get; set; }

        protected void OnPropertyChanged(string name= null)
        {
            if (name == null)
                name = PropertyName;
            Parent?.OnPropertyChanged(name);
        }

        public new T Value { get { return GetValue(); } set { SetValue(value); } }

        private void SetValue(T value)
        {
            var tValue = (T)base.Value;
            if (!tValue.Equals(null))
            {
                if (tValue.CompareTo(value)!= 0)
                {
                    OnPropertyChanged();
                    tValue = value;
                }
            }
        }

        private T GetValue()
        {
            return (T)base.Value;
        }
    }
}