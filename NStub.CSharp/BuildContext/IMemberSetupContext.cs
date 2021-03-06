﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMemberSetupContext.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2012, EvePanix. All rights reserved.
//   See the license notes shipped with this source and the GNU GPL.
// </copyright>
// <author>Jedzia</author>
// <email>jed69@gmx.de</email>
// <date>$date$</date>
// --------------------------------------------------------------------------------------------------------------------

namespace NStub.CSharp.BuildContext
{
    using System;
    using System.CodeDom;
    using System.Reflection;
    using NStub.CSharp.ObjectGeneration;

    /// <summary>
    /// Represents the data used to setup new unit tests.
    /// </summary>
    public interface IMemberSetupContext
    {
        #region Properties

        /// <summary>
        /// Gets information about the build members in a dictionary form.
        /// </summary>
        BuildDataDictionary BuildData { get; }

        /// <summary>
        /// Gets the code namespace of the test.
        /// </summary>
        CodeNamespace CodeNamespace { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is a constructor.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is a constructor; otherwise, <c>false</c>.
        /// </value>
        bool IsConstructor { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is an event.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is an event; otherwise, <c>false</c>.
        /// </value>
        bool IsEvent { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is a property.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is a property; otherwise, <c>false</c>.
        /// </value>
        bool IsProperty { get; }

        /// <summary>
        /// Gets the member info about the current test method.
        /// </summary>
        MethodInfo MemberInfo { get; }

        /// <summary>
        /// Gets test class declaration.( early testObject ).
        /// </summary>
        CodeTypeDeclaration TestClassDeclaration { get; }

        /// <summary>
        /// Gets type of the object under test.
        /// </summary>
        Type TestObjectType { get; }

        /// <summary>
        /// Gets a reference to the test SetUp method.
        /// </summary>
        CodeTypeMember TypeMember { get; }

        #endregion
    }
}