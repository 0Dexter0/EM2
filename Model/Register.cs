using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EM3.Annotations;

namespace EM3.Model
{
    class Register : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static int _count = 0;
        private int? _intValue;
        private double? _doubleValue;
        private string _name;

        public TypeValue Type { get; set; }
        public int Num { get; }
        public int? IntValue
        {
            get => _intValue;
            set
            {
                _intValue = value;
                OnPropertyChanged();
            }
        }

        public double? DoubleValue
        {
            get => _doubleValue;
            set
            {
                _doubleValue = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public Register(int value, string name = "")
        {
            Type = TypeValue.Integer;
            IntValue = value;
            Name = name;
            Num = ++_count;
        }

        public Register(double value, string name = "")
        {
            Type = TypeValue.Double;
            DoubleValue = value;
            Name = name;
            Num = ++_count;
        }

        public override string ToString()
        {
            if (IntValue == null)
            {
                return $"{Num}) {Name} {DoubleValue}";
            }
            else
            {
                return $"{Num}) {Name} {IntValue}";
            }
        }
    }
}
