﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodeTypeReferenceException.cs" company="EvePanix">
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
    using System.Runtime.Serialization;
    using NStub.Core;

    /// <summary>
    /// Represents errors that occur during code generation with a <see cref="CodeTypeReferenceBinder"/>.
    /// </summary>
    [Serializable]
    public class CodeTypeReferenceException : Exception
    {
        #region Fields

        private CodeTypeReferenceBinder binder;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeTypeReferenceException"/> class
        /// </summary>
        /// <param name="binder">The binder associated with this exception.</param>
        public CodeTypeReferenceException(CodeTypeReferenceBinder binder)
        {
            Guard.NotNull(() => binder, binder);
            this.binder = binder;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeTypeReferenceException"/> class
        /// </summary>
        /// <param name="binder">The binder associated with this exception.</param>
        /// <param name="message">A <see cref="T:System.String"/> that describes the error. The content of message is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
        public CodeTypeReferenceException(CodeTypeReferenceBinder binder, string message)
            : base(message)
        {
            Guard.NotNull(() => binder, binder);
            this.binder = binder;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeTypeReferenceException"/> class
        /// </summary>
        /// <param name="binder">The binder associated with this exception.</param>
        /// <param name="message">A <see cref="T:System.String"/> that describes the error. The content of message is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
        /// <param name="inner">The exception that is the cause of the current exception. If the innerException parameter is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
        public CodeTypeReferenceException(CodeTypeReferenceBinder binder, string message, Exception inner)
            : base(message, inner)
        {
            Guard.NotNull(() => binder, binder);
            this.binder = binder;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeTypeReferenceException"/> class
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected CodeTypeReferenceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}