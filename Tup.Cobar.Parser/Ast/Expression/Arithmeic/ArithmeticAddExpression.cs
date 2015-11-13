/*
* Copyright 1999-2012 Alibaba Group.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using Deveel.Math;
using Sharpen;
using Tup.Cobar.Parser.Visitor;

namespace Tup.Cobar.Parser.Ast.Expression.Arithmeic
{
    /// <summary><code>higherExpr '+' higherExpr</code></summary>
    /// <author><a href="mailto:shuo.qius@alibaba-inc.com">QIU Shuo</a></author>
    public class ArithmeticAddExpression : ArithmeticBinaryOperatorExpression
    {
        public ArithmeticAddExpression(Expression leftOprand, Expression rightOprand)
            : base(leftOprand, rightOprand, ExpressionConstants.PrecedenceArithmeticTermOp)
        {
        }

        public override string GetOperator()
        {
            return "+";
        }

        public override Number Calculate(int integer1, int integer2)
        {
            if (integer1 == 0 && integer1 == 0)
            {
                return 0;
            }

            int i1 = integer1;
            int i2 = integer2;
            if (i1 == 0)
            {
                return integer2;
            }
            if (i2 == 0)
            {
                return integer1;
            }
            if (i1 >= 0 && i2 <= 0 || i1 <= 0 && i2 >= 0)
            {
                return i1 + i2;
            }
            int rst = i1 + i2;
            if (i1 > 0 && rst < i1 || i1 < 0 && rst > i1)
            {
                return (long)i1 + (long)i2;
            }
            return rst;
        }

        public override Number Calculate(long long1, long long2)
        {
            if (long1 == 0 && long2 == 0)
            {
                return 0;
            }

            long l1 = long1;
            long l2 = long2;
            if (l1 == 0L)
            {
                return long2;
            }
            if (l2 == 0L)
            {
                return long1;
            }
            if (l1 >= 0L && l2 <= 0L || l1 <= 0L && l2 >= 0L)
            {
                return l1 + l2;
            }
            long rst = l1 + l2;
            if (l1 > 0L && rst < l1 || l1 < 0L && rst > l1)
            {
                BigInteger bi1 = BigInteger.ValueOf(l1);
                BigInteger bi2 = BigInteger.ValueOf(l2);
                return bi1.Add(bi2);
            }
            return rst;
        }

        public override Number Calculate(BigInteger bigint1, BigInteger bigint2)
        {
            if (bigint1 == null || bigint2 == null)
            {
                return 0;
            }
            return bigint1.Add(bigint2);
        }

        public override Number Calculate(BigDecimal bigDecimal1, BigDecimal bigDecimal2)
        {
            if (bigDecimal1 == null || bigDecimal2 == null)
            {
                return 0;
            }
            return bigDecimal1.Add(bigDecimal2);
        }

        public override void Accept(SQLASTVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}