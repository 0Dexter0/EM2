using System;
namespace EM3.Model
{
    static class Operations
    {
        public static double Sum(double var1, double var2) => Math.Round(var1 + var2, 5);

        public static double Subtr(double var1, double var2) => Math.Round(var1 - var2, 5);

        public static double Div(double var1, double var2) => Math.Round(var1 / var2, 5);

        public static double Mult(double var1, double var2) => Math.Round(var1 * var2, 5);

        public static Register CrtVarI(int value, int regNum, string name = "") => new(value, regNum, name) {Type = TypeValue.Integer};

        public static Register CrtVarD(double value, int regNum, string name = "") => new(value, regNum, name) { Type = TypeValue.Double };

        public static string Out(double value) => value.ToString();

        public static double Mod(double var1, double var2) => var1 % var2;
    }
}
