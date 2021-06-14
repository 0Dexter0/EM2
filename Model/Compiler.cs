using System;
using System.Linq;

namespace EM3.Model
{
    class Compiler
    {
        private string[] _commands;
        private bool[] _isCommands;
        public ErrorProvider ErrorProvider { get; }

        public Compiler()
        {
            ErrorProvider = new();
        }

        public bool Compile(string[] commands, string[] regOut, 
                            string[] regA, string[] regB)
        {
            _commands = commands;
            _isCommands = new bool[commands.Length];
            CheckCommands(commands);
            CheckRegOut(regOut);
            CheckRegAOrVal(regA);
            CheckRegBOrVal(regB);

            return ErrorProvider.GetErrors().Count == 0;
        }

        private void CheckCommands(string[] commands)
        {
            bool isOk = true;

            for (int i = 0; i < commands.Length; i++)
            {
                foreach (string oprName in Enum.GetNames(typeof(OpertationEnum)))
                {
                    if (commands[i].Equals(string.Empty))
                    {
                        _isCommands[i] = true;
                        continue;
                    }
                    else if (commands[i].Equals(oprName))
                    {
                        _isCommands[i] = true;
                        isOk = true;
                        break;
                    }
                    else
                    {
                        _isCommands[i] = false;
                        isOk = false;
                    }
                }

                if (!isOk)
                {
                    ErrorProvider.CreateError(i, commands[i], ErrorType.Command);
                }
            }
        }

        private void CheckRegOut(string[] regOut)
        {
            for (int i = 0; i < regOut.Length; i++)
            {
                try
                {
                    if (regOut[i].Equals(string.Empty))
                    {
                        if (_commands[i].Equals(string.Empty) ||
                            _commands[i].Equals(OpertationEnum.End.ToString())) continue;
                    }
                    else if (!_isCommands[i] && regOut[i].Equals(string.Empty)) continue;
                    else
                    {
                        int tmp;

                        if (!int.TryParse(regOut[i], out tmp))
                        {
                            ErrorProvider.CreateError(i, regOut[i], ErrorType.RegOut);
                        }
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    continue;
                }

                
            }
        }

        private void CheckRegAOrVal(string[] regA)
        {
            for (int i = 0; i < regA.Length - 1; i++)
            {
                try
                {
                    if (_commands[i].Equals(string.Empty) ||
                        _commands[i].Equals(OpertationEnum.End.ToString()) ||
                        _commands[i].Equals(OpertationEnum.Out.ToString())) continue;
                    else if (!_isCommands[i] && regA[i].Equals(string.Empty)) continue;
                    else
                    {
                        double tmp;

                        if (!double.TryParse(regA[i], out tmp))
                        {
                            if (!double.TryParse(regA[i].Substring(0, regA[i].Length - 1), out tmp))
                            {
                                ErrorProvider.CreateError(i, regA[i], ErrorType.RegOrVal);
                            }
                        }
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    continue;
                }
            }
        }

        private void CheckRegBOrVal(string[] regB)
        {
            for (int i = 0; i < regB.Length - 1; i++)
            {
                try
                {
                    if (_commands[i].Equals(string.Empty) ||
                        _commands[i].Equals(OpertationEnum.End.ToString())) continue;
                    else if (_commands[i].Equals(OpertationEnum.CrtVarD.ToString()) ||
                             _commands[i].Equals(OpertationEnum.Jmp.ToString()) ||
                             _commands[i].Equals(OpertationEnum.NextElem.ToString()) ||
                             _commands[i].Equals(OpertationEnum.End.ToString()) ||
                             _commands[i].Equals(OpertationEnum.Out.ToString()))
                    {
                        if (regB[i].Equals(string.Empty)) continue;
                    }
                    else if (!_isCommands[i] && regB[i].Equals(string.Empty)) continue;
                    else
                    {
                        double tmp;

                        if (!double.TryParse(regB[i], out tmp))
                        {
                            if (!double.TryParse(regB[i].Substring(0, regB[i].Length - 1), out tmp))
                            {
                                ErrorProvider.CreateError(i, regB[i], ErrorType.RegOrVal);
                            }
                        }
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    continue;
                }
            }

        }
    }
}
