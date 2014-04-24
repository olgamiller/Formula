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
    public class Parameters
    {
        #region Properties

        public ParameterExpression X { get; private set; }
        public ParameterExpression Y { get; private set; }
        public ParameterExpression Z { get; private set; }
        public ParameterExpression T { get; private set; }

        #endregion

        public Parameters()
        {
            X = Expression.Parameter(typeof(double), "x");
            Y = Expression.Parameter(typeof(double), "y");
            Z = Expression.Parameter(typeof(double), "z");
            T = Expression.Parameter(typeof(double), "t");
        }
    }
}
