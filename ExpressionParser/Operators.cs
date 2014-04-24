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
    internal interface IOperator
    {
        int Prio { get; }

        Expression Apply(Expression l, Expression r);
    }

    internal interface IUnaryOperator { }

    internal class OperatorAdd : IOperator
    {
        public int Prio { get; private set; }

        public OperatorAdd()
        {
            Prio = 1;
        }

        public Expression Apply(Expression l, Expression r)
        {
            return Expression.Add(l, r);
        }
    }

    internal class OperatorSubtract : IOperator
    {
        public int Prio { get; private set; }

        public OperatorSubtract()
        {
            Prio = 1;
        }

        public Expression Apply(Expression l, Expression r)
        {
            return Expression.Subtract(l, r);
        }
    }

    internal class OperatorDivide : IOperator
    {
        public int Prio { get; private set; }

        public OperatorDivide()
        {
            Prio = 2;
        }

        public Expression Apply(Expression l, Expression r)
        {
            return Expression.Divide(l, r);
        }
    }

    internal class OperatorMultiply : IOperator
    {
        public int Prio { get; private set; }

        public OperatorMultiply()
        {
            Prio = 2;
        }

        public Expression Apply(Expression l, Expression r)
        {
            return Expression.Multiply(l, r);
        }
    }

    internal class OperatorPower : IOperator
    {
        public int Prio { get; private set; }

        public OperatorPower()
        {
            Prio = 3;
        }

        public Expression Apply(Expression l, Expression r)
        {
            return Expression.Power(l, r);
        }
    }

    internal class OperatorNegate : IOperator, IUnaryOperator
    {
        public int Prio { get; private set; }

        public OperatorNegate()
        {
            Prio = 1;
        }

        public Expression Apply(Expression l, Expression r)
        {
            return Expression.Negate(r);
        }
    }
}
