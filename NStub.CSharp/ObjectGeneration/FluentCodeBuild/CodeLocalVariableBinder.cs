﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodeLocalVariableBinder.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2012, EvePanix. All rights reserved.
//   See the license notes shipped with this source and the GNU GPL.
// </copyright>
// <author>Jedzia</author>
// <email>jed69@gmx.de</email>
// <date>$date$</date>
// --------------------------------------------------------------------------------------------------------------------

namespace NStub.CSharp.ObjectGeneration.FluentCodeBuild
{
    using System;
    using System.CodeDom;
    using NStub.Core;

    /// <summary>
    /// Build a local variable from fluent parameters.
    /// </summary>
    public class CodeLocalVariableBinder
    {
        /*private static void TestThis()
        {
            var cm = new CodeMemberMethod();
            cm.StaticClass("Assert").Invoke("Inconclusive").With("Thisone").Commit();
        }*/
        #region Fields

        private readonly CodeMemberMethod method;
        private readonly CodeVariableReferenceExpression reference;
        private readonly CodeVariableDeclarationStatement variableDeclaration;

        // private CodeMethodInvokeExpression invoker;
        private CodeStatement assignStatement;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeLocalVariableBinder"/> class
        /// with a local variable declaration.
        /// </summary>
        /// <param name="method">The method to add a <see cref="CodeTypeReference"/> to.</param>
        /// <param name="variableDeclaration">The variable declaration to add.</param>
        internal CodeLocalVariableBinder(CodeMemberMethod method, CodeVariableDeclarationStatement variableDeclaration)
        {
            Guard.NotNull(() => method, method);
            Guard.NotNull(() => variableDeclaration, variableDeclaration);
            this.method = method;
            this.variableDeclaration = variableDeclaration;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeLocalVariableBinder"/> class
        /// with a reference to a variable.
        /// </summary>
        /// <param name="method">The method to add a <see cref="CodeTypeReference"/> to.</param>
        /// <param name="reference">The reference to a local variable.</param>
        internal CodeLocalVariableBinder(CodeMemberMethod method, CodeVariableReferenceExpression reference)
        {
            Guard.NotNull(() => method, method);
            Guard.NotNull(() => reference, reference);
            this.method = method;
            this.reference = reference;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the expression to the referenced type.
        /// </summary>
        public CodeVariableDeclarationStatement LocalVariableDeclaration
        {
            get
            {
                return this.variableDeclaration;
            }
        }

        /// <summary>
        /// Gets the assign statement of the variable.
        /// </summary>
        internal CodeStatement AssignStatement
        {
            get
            {
                return this.assignStatement;
            }
        }

        /// <summary>
        /// Gets the expression to the referenced type.
        /// </summary>
        internal CodeVariableReferenceExpression LocalVariableReference
        {
            get
            {
                return this.reference;
            }
        }

        #endregion

        /*/// <summary>
        /// Add a primitive parameter to the method invocation.
        /// </summary>
        /// <param name="text">The content of the primitive expression.</param>
        /// <returns>
        /// A fluent interface to build up reference types.
        /// </returns>
        public CodeTypeReferenceBinder With(string text)
        {
            if (invoker == null)
            {
                throw new CodeTypeReferenceException(this, "Cannot add parameter to a method that is not defined." +
                                                           "Use Invoke(...) to specify the method.");
            }
            var primitive = new CodePrimitiveExpression(text);
            invoker.Parameters.Add(primitive);
            return this;
        }*/

        /*/// <summary>
        /// Add a parameter with a reference to a local variable of the specified name to the method invocation.
        /// </summary>
        /// <param name="variableName">The name of the referenced local variable.</param>
        /// <returns>
        /// A fluent interface to build up reference types.
        /// </returns>
        public CodeTypeReferenceBinder WithReference(string variableName)
        {
            // Todo: add WithThisReference
            if (invoker == null)
            {
                throw new CodeTypeReferenceException(this, "Cannot add parameter to a method that is not defined." +
                                                           "Use Invoke(...) to specify the method.");
            }
            var varRef = new CodeVariableReferenceExpression(variableName);
            invoker.Parameters.Add(varRef);
            return this;
        }*/

        /*/// <summary>
        /// Specify the name of the method to invoke.
        /// </summary>
        /// <param name="methodname">The name of the method.</param>
        /// <returns>A fluent interface to build up reference types.</returns>
        public CodeLocalVariableReferenceBinder Invoke(string methodname)
        {
            invoker = new CodeMethodInvokeExpression();
            //invoker.Method = new CodeMethodReferenceExpression(reference, methodname);
            return this;
        }*/

        /*/// <summary>
        /// Completes the creation of the reference type with an assignment of a local variable.
        /// </summary>
        /// <param name="variableName">Name of the local variable that gets assigned.</param>
        /// <param name="createVariable">if set to <c>true</c> a local variable is created with the assignment.</param>
        /// <returns>
        /// A fluent interface to build up methods.
        /// </returns>
        public CodeMemberMethod AssignLocal(string variableName, bool createVariable)
        {
            // Todo: member checking.
            if (createVariable)
            {
                var localDecl = new CodeVariableDeclarationStatement("var", variableName, invoker);
                method.Statements.Add(localDecl);
            }
            else
            {
                var localRef = new CodeVariableReferenceExpression(variableName);
                var as1 = new CodeAssignStatement(localRef, invoker);
                method.Statements.Add(as1);
            }
            return method;
        }*/

        /// <summary>
        /// Completes the creation of the reference type with an assignment of a member field.
        /// </summary>
        /// <returns>
        /// A fluent interface to build up methods.
        /// </returns>
        public CodeLocalVariableBinder Assign()
        {
            // Todo: member checking.
            // var fieldRef1 = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName);
            // var as1 = new CodeAssignStatement(fieldRef1, invoker);
            // method.Statements.Add(as1);
            return this;
        }

        /// <summary>
        /// Completes the creation of the reference type.
        /// </summary>
        /// <returns>
        /// A fluent interface to build up methods.
        /// </returns>
        /// <exception cref="InvalidOperationException">Cannot add not assigned local variable to a method.Use <see cref="StaticClass"/>
        /// (...) or <see cref="With"/>(...) to specify a initialization expression.</exception>
        public CodeMemberMethod Commit()
        {
            // Todo: member checking.
            if (this.assignStatement == null)
            {
                // CodeLocalVariableException
                throw new InvalidOperationException(
                    "Cannot add not assigned local variable to a method." +
                    "Use StaticClass(...) or With(...) to specify a initialization expression.");
            }

            this.method.Statements.Add(this.assignStatement);
            return this.method;
        }

        /// <summary>
        /// Add a reference to a static class to the method body. Like '<c>Assert</c>' or '<c>DateTime</c>'.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        /// <returns>A fluent interface to build up reference types.</returns>
        public CodeTypeReferenceBinder StaticClass(string className)
        {
            // var localRef = new CodeVariableReferenceExpression(this.reference.Name);
            // var as1 = new CodeAssignStatement(localRef, invoker);
            var staticexpr = new CodeTypeReferenceExpression(className);
            var result = new CodeTypeReferenceBinder(this.method, staticexpr) { LocalVar = this };
            return result;
        }

        /// <summary>
        /// Add a primitive parameter to the method invocation.
        /// </summary>
        /// <param name="value">The object to represent by the primitive expression.</param>
        /// <returns>
        /// A fluent interface to build up reference types.
        /// </returns>
        public CodeLocalVariableBinder With(object value)
        {
            var primitive = new CodePrimitiveExpression(value);
            if (this.variableDeclaration == null)
            {
                // add 'localVar = "primitive expression";'
                this.assignStatement = new CodeAssignStatement(this.reference, primitive);
            }
            else
            {
                // add 'var localVar = "primitive expression";'
                this.variableDeclaration.InitExpression = primitive;
                this.assignStatement = this.variableDeclaration;
            }

            return this;
        }

        /// <summary>
        /// Completes the creation of the reference type from a <see cref="CodeTypeReferenceBinder"/> build reference.
        /// </summary>
        /// <param name="binder">The calling binder.</param>
        /// <returns>
        /// A fluent interface to build up methods.
        /// </returns>
        internal CodeMemberMethod Commit(CodeTypeReferenceBinder binder)
        {
            if (this.variableDeclaration == null)
            {
                // add 'localVar = DateTime.Now;'
                var assign = new CodeAssignStatement(this.reference, binder.Invoker);
                this.method.Statements.Add(assign);
            }
            else
            {
                // add 'var localVar = DateTime.Now;'
                this.variableDeclaration.InitExpression = binder.Invoker;
                this.method.Statements.Add(this.variableDeclaration);
            }

            return this.method;
        }
    }
}