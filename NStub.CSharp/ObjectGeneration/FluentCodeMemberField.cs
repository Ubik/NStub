﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;

namespace NStub.CSharp.ObjectGeneration
{
    public static class FluentCodeMemberField
    {
        public static CodeMemberField Create(string memberFieldName, string memberFieldType)
        {
            var memberField = new CodeMemberField(memberFieldType, memberFieldName)
            {
                Attributes = MemberAttributes.Private
            };
            return memberField;
        }

        public static CodeMemberField Create(string memberFieldName, Type memberFieldType)
        {
            var memberField = new CodeMemberField(memberFieldType, memberFieldName)
            {
                Attributes = MemberAttributes.Private
            };
            return memberField;
        }

        public static CodeMemberField Create<T>(string memberFieldName)
        {
            return Create(memberFieldName, typeof(T));
        }

    }

    /// <summary>
    /// Build a reference to a code field from fluent parameters.
    /// </summary>
    public class CodeFieldReferenceBinder
    {
        private static void TestThis()
        {
            var ctdecl = new CodeTypeDeclaration("MyClass");
            var cm = new CodeMemberMethod();
            cm.Assign("myField").AndCreateIn<IAsyncResult>(ctdecl).With(123);
        }

        private readonly CodeMemberMethod method;
        private readonly CodeFieldReferenceExpression fieldReference;
        private CodeAssignStatement fieldAssignment;
        private CodeMemberField memberField;

        public CodeAssignStatement FieldAssignment
        {
            get { return fieldAssignment; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeTypeReferenceBinder"/> class.
        /// </summary>
        /// <param name="method">The method to add a CodeTypeReference to.</param>
        /// <param name="fieldReference">The field reference to add.</param>
        internal CodeFieldReferenceBinder(CodeMemberMethod method, CodeFieldReferenceExpression fieldReference)
        {
            Guard.NotNull(() => method, method);
            Guard.NotNull(() => fieldReference, fieldReference);
            this.method = method;
            this.fieldReference = fieldReference;
        }

        /// <summary>
        /// Create the field in the specified class.
        /// </summary>
        /// <param name="owningClass">The owning class.</param>
        /// <param name="fieldType">CLR-Type of the field.</param>
        /// <returns>
        /// A fluent interface to build up member field types.
        /// </returns>
        public CodeFieldReferenceBinder AndCreateIn(CodeTypeDeclaration owningClass, Type fieldType)
        {
            this.memberField = new CodeMemberField(fieldType, fieldReference.FieldName);
            owningClass.Members.Add(memberField);
            return this;
        }

        /// <summary>
        /// Create the field in the specified class.
        /// </summary>
        /// <param name="owningClass">The owning class.</param>
        /// <param name="fieldType">CLR-Type of the field.</param>
        /// <returns>
        /// A fluent interface to build up member field types.
        /// </returns>
        public CodeFieldReferenceBinder AndCreateIn<T>(CodeTypeDeclaration owningClass)
        {
            if (this.memberField != null)
            {
                throw new CodeFieldReferenceException(this, "Cannot create a memberfield twice.");
            }
            this.memberField = new CodeMemberField(typeof(T), fieldReference.FieldName);
            //this.fieldReference = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldReference.FieldName);
            owningClass.Members.Add(memberField);
            return this;
        }

        /// <summary>
        /// Add a primitive parameter to the method invocation.
        /// </summary>
        /// <param name="value">The object to represent by the primitive expression.</param>
        /// <returns>
        /// A fluent interface to build up reference types.
        /// </returns>
        public CodeFieldReferenceBinder With(object value)
        {
            var primitive = new CodePrimitiveExpression(value);
            fieldAssignment = new CodeAssignStatement(fieldReference, primitive);
            return this;
        }

        /// <summary>
        /// Completes the creation of the field assign statement.
        /// </summary>
        /// <returns>
        /// A fluent interface to build up methods.
        /// </returns>
        public CodeMemberMethod Commit()
        {
            if (this.fieldAssignment == null)
            {
                throw new CodeFieldReferenceException(this, "There are no assignments to the field. Nothing to commit"+
                    ", use With(...) to assign values to the field.");
            }
            method.Statements.Add(fieldAssignment);
            return method;
        }

    }
    

}
