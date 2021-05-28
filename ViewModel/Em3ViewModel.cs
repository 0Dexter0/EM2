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
        private bool _isBreak = false;
        private int _jmpCount = 0;

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

            for (int i = 0; i < data.Count && !_isBreak; i++)
            {

                if (data[i][0] == OpertationEnum.Sum.ToString())
                {

                    double sum1 = 0;
                    if (!double.TryParse(data[i][2], out sum1))
                    {
                        int _reg = int.Parse(data[i][2].Substring(0, data[i][2].Length - 1));
                        sum1 = (double) Registers.FirstOrDefault((r) => r.Num == _reg).Value;
                    }

                    // sum1 = double.Parse(data[i][2]);
                    double sum2 = double.Parse(data[i][3]);

                    double res = Operations.Sum(sum1, sum2);

                    int register = int.Parse(data[i][1]);

                    Register reg = Registers.FirstOrDefault((r) => r.Num == register);

                    if (reg != null)
                        reg.Value = res;
                    else
                        Registers.Add(new(res, register));
                }
                else if (data[i][0] == OpertationEnum.Div.ToString())
                {
                    double div1 = double.Parse(data[i][2]);
                    double div2 = double.Parse(data[i][3]);

                    double resDiv = Operations.Div(div1, div2);

                    int register = int.Parse(data[i][1]);

                    Registers.Add(new(resDiv, register));
                }
                else if (data[i][0] == OpertationEnum.Mult.ToString())
                {
                    double var1 = double.Parse(data[i][2]);
                    double var2 = double.Parse(data[i][3]);

                    double res = Operations.Mult(var1, var2);

                    int register = int.Parse(data[i][1]);

                    Registers.Add(new(res, register));
                }
                else if (data[i][0] == OpertationEnum.CrtVarD.ToString())
                {
                    int register = int.Parse(data[i][2]);
                    double value = double.Parse(data[i][3]);
                    string name = data[i][1];


                    Registers.Add(Operations.CrtVarD(value, register, name));
                }
                else if (data[i][0] == OpertationEnum.Jmp.ToString())
                {
                    int line = int.Parse(data[i][1]);
                    int to = int.Parse(data[i][2]);

                    if (_jmpCount < to - 1)
                    {
                        i = line - 2;
                        _jmpCount++;
                    }
                }
                else if (data[i][0].Trim() == OpertationEnum.Out.ToString())
                {
                    int numRegister = int.Parse(data[i][1]);

                    var value = Registers.First((r => numRegister == r.Num)).Value;
                    if (value != null)
                        Out = Operations.Out(value.Value);
                }
                else if (data[i][0] == "End") _isBreak = true;




                if (_isBreak) break;
                
            }
        }

        private void Reset()
        {
            Registers.Clear();
            _jmpCount = 0;
            _isBreak = false;
        }

        public Em3Command RunCommand => new(o =>
        {
            Reset();
            Run();
        });
    }
}
