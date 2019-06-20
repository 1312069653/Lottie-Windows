﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Microsoft.Toolkit.Uwp.UI.Lottie.UIData.CodeGen.AbstractSyntax
{
    /// <summary>
    /// An <see cref="Expression"/> is something that can be evaluated to produce a value.
    /// Evaluating an <see cref="Expression"/> may or may not have a side effect on the environment.
    /// </summary>
    abstract class Expression
    {
        internal Expression()
        {
        }

        public abstract TypeReference ResultType { get; }

        internal abstract ExpressionType ExpressionType { get; }

        /// <summary>
        /// An <see cref="Expression"/> that asserts the type of an object.
        /// </summary>
        public sealed class Cast : Expression
        {
            internal Cast(TypeReference.ImportedTypeReference type)
            {
                ResultType = type;
            }

            public override TypeReference ResultType { get; }

            internal override ExpressionType ExpressionType => ExpressionType.Cast;
        }

        /// <summary>
        /// An <see cref="Expression"/> that returns the result of calling a method.
        /// </summary>
        sealed class MethodCall : Expression
        {
            // TODO - needs a reference to the receiver, and the arguments.
            // TODO - how to handle void methods, and methods where we ignore the result.
            // TODO - how to handle properties.
            internal MethodCall(TypeReference resultType)
            {
                ResultType = resultType;
            }

            public override TypeReference ResultType { get; }

            internal override ExpressionType ExpressionType => ExpressionType.MethodCall;
        }

        /// <summary>
        /// An <see cref="Expression"/> that instantiates an object.
        /// </summary>
        sealed class New : Expression
        {
            internal New(TypeReference.ImportedTypeReference receiverType, IEnumerable<TypeReference> parameterTypes)
            {
                ResultType = receiverType;
                ParameterTypes = parameterTypes;
            }

            public override TypeReference ResultType { get; }

            public IEnumerable<TypeReference> ParameterTypes { get; }

            internal override ExpressionType ExpressionType => ExpressionType.New;
        }
    }
}
