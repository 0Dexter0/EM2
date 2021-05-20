using System;
using System.Collections.ObjectModel;
using EM3.Annotations;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using EM3.Model;

namespace EM3.ViewModel
{
    public class Em3ViewModel : INotifyPropertyChanged
    {
        private static string _out = string.Empty;
        private static string _errors = string.Empty;
        private static string _fileContent;

        private delegate double Sum(double var1, double var2);
        private delegate double Subtr(double var1, double var2);
        private delegate string Output(double value);

        public string FileContent
        {
            get => _fileContent;
            set
            {
                _fileContent = value;
                OnPropertyChanged();
            }
        }
        public string Out
        {
            get => _out;
            set
            {
                _out = value;
                OnPropertyChanged();
            }
        }

        public string Errors
        {
            get => _errors;
            set
            {
                _errors = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Register> Registers { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Em3ViewModel()
        {
            Registers = new();
        }

        private static Parser parser = new();

        private void Run()
        {
            var data = parser.ParseData(FileContent);

            //Runner runner = new(data);

            //var methods = runner.Methods;

            foreach (string[] line in data)
            {
                switch (line[0])
                {
                    case "Sum":

                        Sum sum = Operations.Sum;

                        double var1 = double.Parse(line[2]);
                        double var2 = double.Parse(line[3]);

                        double res = sum(var1, var2);

                        int register = int.Parse(line[1]);

                        Registers.Add(new(res, register));

                        break;

                    case "Out":

                        int numRegister = int.Parse(line[1]);
                        Output output = Operations.Out;

                        var value = Registers.First((r => numRegister == r.Num)).Value;
                        if (value != null)
                            Out = output(value.Value);

                        break;
                }
            }
        }

        public Em3Command RunCommand => new(o =>
        {
            Run();
        });
    }
}
