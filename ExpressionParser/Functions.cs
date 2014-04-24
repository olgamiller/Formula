/*
Formula.ExpressionParser - Parsing of mathematical formula from a string to an Expression

Written in 2014 by <Olga Miller> <olga.rgb@googlemail.com>
To the extent possible under law, the author(s) have dedicated all copyright and related and neighboring rights to this software to the public domain worldwide.
This software is distributed without any warranty.
You should have received a copy of the CC0 Public Domain Dedication along with this software. If not, see <http://creativecommons.org/publicdomain/zero/1.0/>.
*/

using System;
using System.Reflection;

namespace Formula.ExpressionParser
{
    class Functions
    {
        private delegate double MathFunc(double d);

        internal static MethodInfo Sin { get { return ((MathFunc)Math.Sin).Method; } }
        internal static MethodInfo Cos { get { return ((MathFunc)Math.Cos).Method; } }
        internal static MethodInfo Abs { get { return ((MathFunc)Math.Abs).Method; } }
        internal static MethodInfo Sqrt { get { return ((MathFunc)Math.Sqrt).Method; } }
    }
}
