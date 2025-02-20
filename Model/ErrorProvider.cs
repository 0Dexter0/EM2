﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM3.Model
{
    class ErrorProvider
    {
        private List<Error> _errors;

        public ErrorProvider()
        {
            _errors = new();
        }

        public void CreateError(int numLine, string value, ErrorType errorType)
        {
            switch (errorType)
            {
                case ErrorType.Command:

                    _errors.Add(new($"{errorType} error: in line {++numLine} unknown command \"{value}\""));

                    break;

                case ErrorType.RegOut:

                    _errors.Add(new($"{errorType} error: in line {++numLine} unknown out register \"{value}\""));

                    break;

                case ErrorType.RegOrVal:

                    _errors.Add(new($"{errorType} error: in line {++numLine} unknown \"{value}\""));

                    break;
            }
        }

        public List<Error> GetErrors() => _errors;
        public void ClearError() => _errors.Clear();
    }
}
