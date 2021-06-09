using System;
using System.Linq;

namespace EM3.Model
{
    class Compiler
    {
        public ErrorProvider ErrorProvider { get; }

        public Compiler()
        {
            ErrorProvider = new();
        }

        public bool Compile(string[] commands, string[] regOut, 
                            string[] regA, string[] regB)
        {
            CheckCommands(commands);
            CheckRegOut(regOut);
            CheckRegOrVal(regA);
            CheckRegOrVal(regB);

            return ErrorProvider.GetErrors().Count == 0;
        }

        private void CheckCommands(string[] commands)
        {
            bool isOk = true;

            for (int i = 0; i < commands.Length; i++)
            {
                foreach (string oprName in Enum.GetNames(typeof(OpertationEnum)))
                {
                    if (commands[i].Equals(oprName))
                    {
                        isOk = true;
                        break;
                    }
                    else
                    {
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
                int tmp;

                if (!int.TryParse(regOut[i], out tmp))
                {
                    ErrorProvider.CreateError(i, regOut[i], ErrorType.RegOut);
                }
            }
        }

        private void CheckRegOrVal(string[] reg)
        {
            for (int i = 0; i < reg.Length - 1; i++)
            {
                double tmp;

                if (!double.TryParse(reg[i], out tmp))
                {
                    if (!double.TryParse(reg[i].Substring(0, reg[i].Length - 1), out tmp))
                    {
                        ErrorProvider.CreateError(i, reg[i], ErrorType.RegOrVal);
                    }
                }
            }
        }
    }
}
