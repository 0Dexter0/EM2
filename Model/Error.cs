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
    public class Error : INotifyPropertyChanged
    {
        private string _error;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Err
        {
            get => _error;
            set
            {
                _error = value;
                OnPropertyChanged();
            }
        }

        public Error(string error)
        {
            Err = error;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
