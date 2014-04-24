/*
Formula.ExpressionParser - Parsing of mathematical formula from a string to an Expression
  
Written in 2014 by <Olga Miller> <olga.rgb@googlemail.com>
To the extent possible under law, the author(s) have dedicated all copyright and related and neighboring rights to this software to the public domain worldwide.
This software is distributed without any warranty.
You should have received a copy of the CC0 Public Domain Dedication along with this software. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.
*/

using System.Linq.Expressions;

namespace Formula.ExpressionParser
{
    public delegate double FormulaFunc(double x, double y, double z, double t);

    public class FormulaExpression
    {
        private Parameters _params;
        private Expression _expr;

        public FormulaExpression(Parameters parameters, Expression expression)
        {
            _params = parameters;
            _expr = expression;
        }

        public Expression GetExpression()
        {
            return _expr;
        }

        public FormulaFunc Compile()
        {
            return Expression.Lambda<FormulaFunc>(_expr, _params.X, _params.Y, _params.Z, _params.T).Compile();
        }
    }
}
