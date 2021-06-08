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
        #region Props

        private static string _out = string.Empty;
        private static string _errors = string.Empty;
        private static string _fileContent;
        private static string _commands;
        private static string _outReg;
        private static string _regA;
        private static string _regB;
        private static string _other;
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
        public string Commands
        {
            get => _commands;
            set
            {
                _commands = value;
                OnPropertyChanged();
            }
        }
        public string OutReg
        {
            get => _outReg;
            set
            {
                _outReg = value;
                OnPropertyChanged();
            }
        }
        public string RegA
        {
            get => _regA;
            set
            {
                _regA = value;
                OnPropertyChanged();
            }
        }
        public string RegB
        {
            get => _regB;
            set
            {
                _regB = value;
                OnPropertyChanged();
            }
        }
        public string Other
        {
            get => _other;
            set
            {
                _other = value;
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

        private static Parser parser = new();

        #endregion


        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Em3ViewModel()
        {
            Registers = new();
        }

        

        private void Run()
        {

            /*for (int i = 0; i < data.Count && !_isBreak; i++)
            {

                if (data[i][0] == OpertationEnum.Sum.ToString())
                {

                    double var1 = 0;
                    if (!double.TryParse(data[i][2], out var1))
                    {
                        int _reg = int.Parse(data[i][2].Substring(0, data[i][2].Length - 1));
                        var1 = (double) Registers.FirstOrDefault((r) => r.Num == _reg).Value;
                    }

                    // sum1 = double.Parse(data[i][2]);
                    double var2 = 0; //double.Parse(data[i][3]);
                    if (!double.TryParse(data[i][3], out var2))
                    {
                        int _reg = int.Parse(data[i][3].Substring( 0, data[i][3].Length - 2));
                        var2 = (double)Registers.FirstOrDefault((r) => r.Num == _reg).Value;
                    }

                    double res = Operations.Sum(var1, var2);

                    int register = int.Parse(data[i][1]);

                    Register reg = Registers.FirstOrDefault((r) => r.Num == register);

                    if (reg != null)
                        reg.Value = res;
                    else
                        Registers.Add(new(res, register));
                }
                else if (data[i][0] == OpertationEnum.Subtr.ToString())
                {
                    double var1 = 0;
                    if (!double.TryParse(data[i][2], out var1))
                    {
                        int _reg = int.Parse(data[i][2].Substring(0, data[i][2].Length - 1));
                        var1 = (double)Registers.FirstOrDefault((r) => r.Num == _reg).Value;
                    }

                    // sum1 = double.Parse(data[i][2]);
                    double var2 = 0; //double.Parse(data[i][3]);
                    if (!double.TryParse(data[i][3], out var2))
                    {
                        int _reg = int.Parse(data[i][3].Substring(0, data[i][3].Length - 2));
                        var2 = (double)Registers.FirstOrDefault((r) => r.Num == _reg).Value;
                    }

                    double res = Operations.Subtr(var1, var2);

                    int register = int.Parse(data[i][1]);

                    Register reg = Registers.FirstOrDefault((r) => r.Num == register);

                    if (reg != null)
                        reg.Value = res;
                    else
                        Registers.Add(new(res, register));
                }
                else if (data[i][0] == OpertationEnum.Div.ToString())
                {
                    double var1 = 0;
                    if (!double.TryParse(data[i][2], out var1))
                    {
                        int _reg = int.Parse(data[i][2].Substring(0, data[i][2].Length - 1));
                        var1 = (double)Registers.FirstOrDefault((r) => r.Num == _reg).Value;
                    }

                    // sum1 = double.Parse(data[i][2]);
                    double var2 = 0; //double.Parse(data[i][3]);
                    if (!double.TryParse(data[i][3], out var2))
                    {
                        int _reg = int.Parse(data[i][3].Substring(0, data[i][3].Length - 2));
                        var2 = (double)Registers.FirstOrDefault((r) => r.Num == _reg).Value;
                    }

                    double res = Operations.Div(var1, var2);

                    int register = int.Parse(data[i][1]);

                    Register reg = Registers.FirstOrDefault((r) => r.Num == register);

                    if (reg != null)
                        reg.Value = res;
                    else
                        Registers.Add(new(res, register));
                }
                else if (data[i][0] == OpertationEnum.Mult.ToString())
                {
                    double var1 = 0;
                    if (!double.TryParse(data[i][2], out var1))
                    {
                        int _reg = int.Parse(data[i][2].Substring(0, data[i][2].Length - 1));
                        var1 = (double)Registers.FirstOrDefault((r) => r.Num == _reg).Value;
                    }

                    // sum1 = double.Parse(data[i][2]);
                    double var2 = 0; //double.Parse(data[i][3]);
                    if (!double.TryParse(data[i][3], out var2))
                    {
                        int _reg = int.Parse(data[i][3].Substring(0, data[i][3].Length - 2));
                        var2 = (double)Registers.FirstOrDefault((r) => r.Num == _reg).Value;
                    }

                    double res = Operations.Mult(var1, var2);

                    int register = int.Parse(data[i][1]);

                    Register reg = Registers.FirstOrDefault((r) => r.Num == register);

                    if (reg != null)
                        reg.Value = res;
                    else
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
                    else
                    {
                        _jmpCount = 0;
                    }
                }
                else if (data[i][0].Trim() == OpertationEnum.Out.ToString())
                {
                    int numRegister = int.Parse(data[i][1]);

                    var value = Registers.FirstOrDefault((r => numRegister == r.Num)).Value;
                    if (value != null)
                        Out = Operations.Out(value.Value);
                }
                else if (data[i][0] == "End\r") _isBreak = true;




                if (_isBreak) break;
                
            }*/
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

        public Em3Command ParseCommand => new(o =>
        {
            var data = parser.ParseData(FileContent);
            Commands = data[0];
            OutReg = data[1];
            RegA = data[2];
            RegB = data[3];
            Other = data[4];
        });
    }
}
