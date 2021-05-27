using System;
using System.Windows.Data;

namespace EM3.Model
{
    static class Operations
    {
        public static double Sum(double var1, double var2)
        {
            return var1 + var2;
        }

        public static double Subtr(double var1, double var2)
        {
            return var1 - var2;
        }

        public static double Div(double var1, double var2)
        {
            return Math.Round(var1 / var2, 5);
        }

        public static double Mult(double var1, double var2)
        {
            return var1 * var2;
        }

        public static Register CrtVarI(int value, int regNum, string name = "")
        {
            return new(value, regNum, name) {Type = TypeValue.Integer};
        }

        public static Register CrtVarD(double value, int regNum, string name = "")
        {
            return new(value, regNum, name) { Type = TypeValue.Double };
        }

        public static string Out(double value)
        {
            return value.ToString();
        }
    }
}
