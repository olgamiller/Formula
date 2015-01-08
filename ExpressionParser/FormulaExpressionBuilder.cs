/*
Formula.ExpressionParser - Parsing of mathematical formula from a string to an Expression
  
Written in 2014 by <Olga Miller> <olga.rgb@googlemail.com>
To the extent possible under law, the author(s) have dedicated all copyright and related and neighboring rights to this software to the public domain worldwide.
This software is distributed without any warranty.
You should have received a copy of the CC0 Public Domain Dedication along with this software. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.
*/

using Formula.Properties;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Formula.ExpressionParser
{
    public class FormulaExpressionBuilder
    {
        #region Member Variables

        private int _pos;
        private string _input;

        private Parameters _params;

        #endregion

        /// <exception cref="SyntaxException"/>
        public FormulaExpression Build(string input)
        {
            _pos = 0;
            _input = input;
            _params = new Parameters();

            return new FormulaExpression(_params, Parse(false));
        }

        private Expression Parse(bool bParentheses)
        {
            ExpressionCompiler ec = new ExpressionCompiler();

            while (_pos < _input.Length)
            {
                switch (_input[_pos])
                {
                    case ' ':
                        break;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '.':
                        HandleNumber(ec);
                        break;
                    case 'x':
                    case 'X':
                        SetExpression(ec, _params.X);
                        break;
                    case 'y':
                    case 'Y':
                        SetExpression(ec, _params.Y);
                        break;
                    case 'z':
                    case 'Z':
                        SetExpression(ec, _params.Z);
                        break;
                    case 't':
                    case 'T':
                        SetExpression(ec, _params.T);
                        break;
                    case '+':
                        SetOperator(ec, new OperatorAdd());
                        break;
                    case '-':
                        HandleMinus(ec);
                        break;
                    case '/':
                        SetOperator(ec, new OperatorDivide());
                        break;
                    case '*':
                        SetOperator(ec, new OperatorMultiply());
                        break;
                    case '^':
                        SetOperator(ec, new OperatorPower());
                        break;
                    case '(':
                        _pos++;
                        SetExpression(ec, Parse(true));
                        break;
                    case ')':
                        if (!bParentheses)
                            throw new ParenthesisException(_input, _pos, Resources.Opening);
                        return GetResultExpression(ec);
                    case 'a':
                    case 'A':
                        if (ConsumeWord("abs"))
                            SetMethod(ec, Functions.Abs);
                        else
                            throw new SyntaxException(_input, _pos);
                        break;
                    case 'c':
                    case 'C':
                        if (ConsumeWord("cos"))
                            SetMethod(ec, Functions.Cos);
                        else
                            throw new SyntaxException(_input, _pos);
                        break;
                    case 'p':
                    case 'P':
                        if (ConsumeWord("pi"))
                            SetExpression(ec, Expression.Constant(Math.PI));
                        else
                            throw new SyntaxException(_input, _pos);
                        break;
                    case 's':
                    case 'S':
                        if (ConsumeWord("sin"))
                            SetMethod(ec, Functions.Sin);
                        else if (ConsumeWord("sqrt"))
                            SetMethod(ec, Functions.Sqrt);
                        else
                            throw new SyntaxException(_input, _pos);
                        break;
                    default:
                        throw new UnknownTokenException(_input, _pos);
                }

                _pos++;
            }

            if (bParentheses)
                throw new ParenthesisException(_input, _pos, Resources.Closing);

            return GetResultExpression(ec);
        }

        private void HandleNumber(ExpressionCompiler ec)
        {
            bool point = _input[_pos] == '.';
            int begin = _pos++;

            while (_pos < _input.Length)
            {
                if (_input[_pos] == '.')
                {
                    if (point)
                        throw new SyntaxException(_input, _pos);

                    point = true;
                }
                else if (!char.IsDigit(_input[_pos]))
                {
                    break;
                }

                _pos++;
            }

            _pos--;

            if (_input[_pos] == '.')
                throw new SyntaxException(_input, _pos);

            double d = double.Parse(_input.Substring(begin, _pos - begin + 1));
            SetExpression(ec, Expression.Constant(d));
        }

        private void SetExpression(ExpressionCompiler ec, Expression expr)
        {
            if (!ec.SetExpression(expr))
                throw new OperatorException(_input, _pos);
        }

        private void HandleMinus(ExpressionCompiler ec)
        {
            if (ec.LeftIsEmpty())
                SetOperator(ec, new OperatorNegate());
            else
                SetOperator(ec, new OperatorSubtract());
        }

        private void SetOperator(ExpressionCompiler ec, IOperator op)
        {
            if (!ec.SetOperator(op))
                throw new EmptyExpressionException(_input, _pos);
        }

        private bool ConsumeWord(string name)
        {
            if ((_pos + name.Length) <= _input.Length && _input.Substring(_pos, name.Length).ToLower() == name)
            {
                _pos += name.Length - 1;
                return true;
            }

            return false;
        }

        private void SetMethod(ExpressionCompiler ec, MethodInfo methodInfo)
        {
            _pos++;

            while (_pos < _input.Length && _input[_pos] == ' ')
                _pos++;

            if (_pos >= _input.Length || _input[_pos] != '(')
                throw new ParenthesisException(_input, _pos, Resources.Opening);

            _pos++;
            SetExpression(ec, Expression.Call(methodInfo, Parse(true)));
        }

        private Expression GetResultExpression(ExpressionCompiler ec)
        {
            Expression result = ec.GetResult();

            if (result == null)
                throw new EmptyExpressionException(_input, _pos);

            return result;
        }
    }
}

