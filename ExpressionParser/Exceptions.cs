/*
Formula.ExpressionParser - Parsing of mathematical formula from a string to an Expression
  
Written in 2014 by <Olga Miller> <olga.rgb@googlemail.com>
To the extent possible under law, the author(s) have dedicated all copyright and related and neighboring rights to this software to the public domain worldwide.
This software is distributed without any warranty.
You should have received a copy of the CC0 Public Domain Dedication along with this software. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.
*/

using Formula.Properties;
using System;

namespace Formula.ExpressionParser
{
    public class SyntaxException : Exception
    {
        public SyntaxException(string message)
            : base(message)
        {
        }

        public SyntaxException(string input, int i)
            : base(string.Format(Resources.SyntaxError, input, GetArrow(i)))
        {
        }

        public static string GetArrow(int i)
        {
            return new string(' ', i) + "^";
        }
    }

    public class EmptyExpressionException : SyntaxException
    {
        public EmptyExpressionException(string input, int i)
            : base(string.Format(Resources.EmptyExpressionError, input, GetArrow(i)))
        {
        }
    }

    public class OperatorException : SyntaxException
    {
        public OperatorException(string input, int i)
            : base(string.Format(Resources.OperatorError, input, GetArrow(i)))
        {
        }
    }

    public class UnknownTokenException : SyntaxException
    {
        public UnknownTokenException(string input, int i)
            : base(string.Format(Resources.UnknownTokenError, input, GetArrow(i), input[i]))
        {
        }
    }

    public class ParenthesisException : SyntaxException
    {
        public ParenthesisException(string input, int i, string missedParenthesis)
            : base(string.Format(Resources.ParenthesisError, input, GetArrow(i), missedParenthesis))
        {
        }
    }
}
