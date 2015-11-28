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

using System;
using System.Collections.Generic;
using Tup.Cobar4Net.Parser.Visitor;

namespace Tup.Cobar4Net.Parser.Ast.Expression
{
    /// <author><a href="mailto:shuo.qius@alibaba-inc.com">QIU Shuo</a></author>
    public abstract class AbstractExpression : Expression
    {
        private bool cacheEvalRst = true;

        private bool evaluated;

        private object evaluationCache;

        public abstract int GetPrecedence();

        public virtual Expression SetCacheEvalRst(bool cacheEvalRst)
        {
            this.cacheEvalRst = cacheEvalRst;
            return this;
        }

        public virtual object Evaluation(IDictionary<object, object> parameters)
        {
            if (cacheEvalRst)
            {
                if (evaluated)
                {
                    return evaluationCache;
                }
                evaluationCache = EvaluationInternal(parameters);
                evaluated = true;
                return evaluationCache;
            }
            return EvaluationInternal(parameters);
        }

        protected abstract object EvaluationInternal(IDictionary<object, object> parameters);

        public abstract void Accept(SQLASTVisitor visitor);
    }
}