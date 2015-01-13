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
    internal class ExpressionCompiler
    {
        #region Member Variables

        // expr1 op1 expr2 op2 expr3 op3  expr
        // example: 1 + 2 * 3 ^  4

        private IOperator _operator1;
        private IOperator _operator2;
        private IOperator _operator3;

        private Expression _expression1;
        private Expression _expression2;
        private Expression _expression3;

        #endregion

        internal bool SetOperator(IOperator op)
        {
            bool noError = true;

            if (_operator1 == null)
                noError = SetOperator1(op);
            else if (_operator2 == null)
                noError = SetOperator2(op);
            else
                noError = SetOperator3(op);

            return noError;
        }

        internal bool LeftIsEmpty()
        {
            return _expression1 == null;
        }

        internal bool SetExpression(Expression expr)
        {
            bool noError = true;

            if (_expression1 == null)
                SetExpression1(expr);
            else if (_expression2 == null)
                noError = SetExpression2(expr);
            else
                noError = SetExpression3(expr);

            return noError;
        }

        internal Expression GetResult()
        {
            if (_operator2 != null)
            {
                if (_expression3 == null)
                    return null;

                ApplyOperator2(_expression3);
            }

            if (_operator1 != null)
            {
                if (_expression2 == null)
                    return null;

                ApplyOperator1(_expression2);
            }

            return _expression1;
        }

        #region Set Operators

        private bool SetOperator1(IOperator op)
        {
            if (_expression1 == null && !(op is IUnaryOperator))
                return false;

            _operator1 = op;

            return true;
        }

        private bool SetOperator2(IOperator op)
        {
            if (_expression2 == null)
                return false;

            if (op.Prio <= _operator1.Prio)
            {
                ApplyOperator1(_expression2);
                _operator1 = op;
            }
            else
            {
                _operator2 = op;
            }

            return true;
        }

        private bool SetOperator3(IOperator op)
        {
            if (_expression3 == null)
                return false;

            if (op.Prio <= _operator2.Prio)
            {
                ApplyOperator2(_expression3);

                if (op.Prio <= _operator1.Prio)
                {
                    ApplyOperator1(_expression2);
                    _operator1 = op;
                }
                else
                {
                    _operator2 = op;
                }
            }
            else
            {
                _operator3 = op;
            }

            return true;
        }

        #endregion

        #region Set Expressions

        private void SetExpression1(Expression expr)
        {
            if (_operator1 != null && _operator1 is IUnaryOperator)
                _expression2 = expr;

            _expression1 = expr;
        }

        private bool SetExpression2(Expression expr)
        {
            if (_operator1 == null)
                return false;

            if (_operator1.Prio < 3)
                _expression2 = expr;
            else
                ApplyOperator1(expr);

            return true;
        }

        private bool SetExpression3(Expression expr)
        {
            if (_operator2 == null)
                return false;

            if (_expression3 == null)
            {
                if (_operator2.Prio == 2)
                    _expression3 = expr;
                else
                    ApplyOperator2(expr);
            }
            else
            {
                ApplyOperator3(expr);
            }

            return true;
        }

        #endregion

        #region Apply Operators

        private void ApplyOperator1(Expression expr)
        {
            _expression1 = _operator1.Apply(_expression1, expr);
            _operator1 = null;
            _expression2 = null;
        }

        private void ApplyOperator2(Expression expr)
        {
            _expression2 = _operator2.Apply(_expression2, expr);
            _operator2 = null;
            _expression3 = null;
        }

        private void ApplyOperator3(Expression expr)
        {
            _expression3 = _operator3.Apply(_expression3, expr);
            _operator3 = null;
        }

        #endregion
    }
}
