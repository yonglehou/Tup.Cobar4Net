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

using Sharpen;
using Tup.Cobar4Net.Parser.Ast.Expression.Primary;
using Tup.Cobar4Net.Parser.Ast.Expression.Primary.Literal;
using Tup.Cobar4Net.Parser.Visitor;

namespace Tup.Cobar4Net.Parser.Ast.Fragment.Tableref
{
    /// <author>
    ///     <a href="mailto:shuo.qius@alibaba-inc.com">QIU Shuo</a>
    /// </author>
    public abstract class AliasableTableReference : TableReference
    {
        protected readonly string alias;

        protected string aliasUpEscape;

        protected AliasableTableReference(string alias)
        {
            this.alias = alias;
        }

        public virtual string Alias
        {
            get { return alias; }
        }

        public abstract override int Precedence { get; }

        public abstract override bool IsSingleTable { get; }

        /// <returns>upper-case, empty is possible</returns>
        public virtual string GetAliasUnescapeUppercase()
        {
            if (alias == null || alias.Length <= 0)
            {
                return alias;
            }
            if (aliasUpEscape != null)
            {
                return aliasUpEscape;
            }
            switch (alias[0])
            {
                case '`':
                {
                    return aliasUpEscape = Identifier.UnescapeName(alias, true);
                }

                case '\'':
                {
                    return
                        aliasUpEscape =
                            LiteralString.GetUnescapedString(Runtime.Substring(alias, 1, alias.Length - 1), true);
                }

                case '_':
                {
                    var ind = -1;
                    for (var i = 1; i < alias.Length; ++i)
                    {
                        if (alias[i] == '\'')
                        {
                            ind = i;
                            break;
                        }
                    }
                    if (ind >= 0)
                    {
                        var st = new LiteralString(Runtime.Substring(alias, 0, ind),
                            Runtime.Substring(alias, ind + 1, alias.Length - 1), false);
                        return aliasUpEscape = st.GetUnescapedString(true);
                    }
                    goto default;
                }

                default:
                {
                    return aliasUpEscape = alias.ToUpper();
                }
            }
        }

        public abstract override void Accept(ISqlAstVisitor visitor);

        public abstract override object RemoveLastConditionElement();
    }
}