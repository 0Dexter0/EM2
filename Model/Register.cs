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
    public class Register : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static int _count = 0;
        private double? _value;
        private string _name;

        public TypeValue Type { get; set; }
        public int Num { get; }

        public double? Value
        {
            get => _value;
            set
            {
                _value = value;
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

        public string Info => ToString();

        public Register(double value, int num = -1 , string name = "")
        {
            Type = TypeValue.Double;
            Value = value;
            Name = name;
            
            if (num == -1)
                Num = ++_count;
            else
                Num = num;
        }

        public override string ToString()
        {
            return $"{Num} {Value}";

        }
    }
}
