/*
Formula - Parsing of mathematical formula from a string
  
Written in 2014 by <Olga Miller> <olga.rgb@googlemail.com>
To the extent possible under law, the author(s) have dedicated all copyright and related and neighboring rights to this software to the public domain worldwide.
This software is distributed without any warranty.
You should have received a copy of the CC0 Public Domain Dedication along with this software. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.
*/

using Formula.ExpressionParser;
using System;

namespace Formula
{
    class Program
    {
        static void Main(string[] args)
        {
            //BuildFormulaExpression("-x + y * abs(1.0002345 + z) ^ t - .6 + 2 + sqrt(cos(2 * pi) - sin(x))");
            
            Console.WriteLine("Enter \"quit\" to exit.");
            Console.WriteLine();

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();

                if (input == "quit")
                    return;

                BuildFormulaExpression(input);
            }
        }

        private static void BuildFormulaExpression(string input)
        {
            FormulaExpressionBuilder builder = new FormulaExpressionBuilder();

            try
            {
                FormulaExpression formula = builder.Build(input);
                Console.WriteLine("Expression: " + formula.GetExpression().ToString());

                Console.WriteLine();
                Console.WriteLine("f(x, y, z, a) = " + input);

                FormulaFunc f = formula.Compile();
                double x = 1, y = 2, z = 3, t = 4;
                double result = f(x, y, z, t);

                Console.WriteLine();
                Console.WriteLine(string.Format("f({0}, {1}, {2}, {3}) = {4}", x, y, z, t, result));
            }
            catch (SyntaxException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Internal parser error: " + ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
