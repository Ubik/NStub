﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MemberBuildContextBase.cs" company="EvePanix">
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
    using NStub.Core;
    using NStub.CSharp.ObjectGeneration;

    /// <summary>
    /// Abstract base class for data used to create new unit tests.
    /// </summary>
    public abstract class MemberBuildContextBase : IMemberBuildContext, IMemberPreBuildContext
    {
        #region Fields

        private MethodInfo memberInfo;
        private Type testObjectType;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberBuildContextBase"/> class.
        /// </summary>
        /// <param name="codeNamespace">The code namespace.</param>
        /// <param name="testClassDeclaration">The test class declaration.( early testObject ).</param>
        /// <param name="typeMember">The current type to create a test method for.</param>
        /// <param name="buildData">The additional build data lookup.</param>
        /// <param name="setUpTearDownContext">Contains data specific to SetUp and TearDown test-methods.</param>
        /// <param name="baseKey">The base string of the <see cref="TestKey"/>. Is amended by the
        /// <paramref name="codeNamespace"/> identifier, normalized and fixed by a <see cref="KeynameFixer"/>.</param>
        protected MemberBuildContextBase(
            CodeNamespace codeNamespace,
            CodeTypeDeclaration testClassDeclaration,
            CodeTypeMember typeMember,
            BuildDataDictionary buildData,
            ISetupAndTearDownContext setUpTearDownContext,
            string baseKey)
        {
            Guard.NotNull(() => codeNamespace, codeNamespace);
            Guard.NotNull(() => testClassDeclaration, testClassDeclaration);
            Guard.NotNull(() => typeMember, typeMember);
            Guard.NotNull(() => buildData, buildData);
            Guard.NotNull(() => setUpTearDownContext, setUpTearDownContext);
            Guard.NotNullOrEmpty(() => baseKey, baseKey);

            this.CodeNamespace = codeNamespace;
            this.TestClassDeclaration = testClassDeclaration;
            this.TypeMember = typeMember;
            this.BuildData = buildData;
            this.SetUpTearDownContext = setUpTearDownContext;

            this.PreBuildResult = new MemberBuildResult();
            this.BuildResult = new MemberBuildResult();

            //this.TestKey = GetTestKey(codeNamespace, testClassDeclaration, typeMember);
            var fixer = new KeynameFixer(codeNamespace, testClassDeclaration, typeMember);
            var fixedName = fixer.Fix( typeMember.Name);
            this.TestKey = baseKey + "." + fixedName;
        }

        /*private static string GetTestKey(CodeNamespace codeNamespace, CodeTypeDeclaration testClassDeclaration, CodeTypeMember typeMember)
        {
            return "";
        }*/

        #endregion

        #region Properties

        /// <summary>
        /// Gets information about the build members in a dictionary form.
        /// </summary>
        public BuildDataDictionary BuildData { get; private set; }

        /// <summary>
        /// Gets the code namespace.
        /// </summary>
        public CodeNamespace CodeNamespace { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is a constructor.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is a constructor; otherwise, <c>false</c>.
        /// </value>
        public bool IsConstructor
        {
            get
            {
                /*if (this.MemberInfo == null)
                {
                    return false;
                }*/
                /*if (this.TypeMember is CodeConstructor)
                {
                    
                }*/
                return this.TypeMember is CodeConstructor;

                // return this.MemberInfo.Name.StartsWith("add_") || this.MemberInfo.Name.StartsWith("remove_");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is an event.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is an event; otherwise, <c>false</c>.
        /// </value>
        public bool IsEvent
        {
            get
            {
                if (this.MemberInfo == null)
                {
                    return false;
                }

                return this.MemberInfo.Name.StartsWith("add_") || this.MemberInfo.Name.StartsWith("remove_");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a property.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is a property; otherwise, <c>false</c>.
        /// </value>
        public bool IsProperty
        {
            get
            {
                if (this.MemberInfo == null)
                {
                    return false;
                }

                return this.MemberInfo.Name.StartsWith("get_") || this.MemberInfo.Name.StartsWith("set_");
            }
        }

        /// <summary>
        /// Gets the member info about the current test method.
        /// </summary>
        public MethodInfo MemberInfo
        {
            get
            {
                if (this.memberInfo == null)
                {
                    this.memberInfo = this.GetTestMethodInfo(this.TypeMember);
                }

                return this.memberInfo;
            }
        }

        /// <summary>
        /// Gets the data specific to SetUp and TearDown test-methods.
        /// </summary>
        public ISetupAndTearDownContext SetUpTearDownContext { get; private set; }

        /// <summary>
        /// Gets test class declaration.( early testObject ).
        /// </summary>
        public CodeTypeDeclaration TestClassDeclaration { get; private set; }

        /// <summary>
        /// Gets the key associated with the test.
        /// </summary>
        /// <value>
        /// The key associated with the test.
        /// </value>
        public string TestKey { get; private set; }

        /// <summary>
        /// Gets type of the object under test.
        /// </summary>
        public Type TestObjectType
        {
            get
            {
                return this.testObjectType ??
                       (this.testObjectType = this.GetTestObjectClassType(this.TestClassDeclaration));
            }
        }

        /// <summary>
        /// Gets the current type to create a test method for.
        /// </summary>
        public CodeTypeMember TypeMember { get; private set; }

        /// <summary>
        /// Gets the build result feedback object in the pre-build phase of test object generation.
        /// </summary>
        public IMemberPreBuildResult PreBuildResult { get; private set; }

        /// <summary>
        /// Gets the build result feedback object in the build phase of test object generation.
        /// </summary>
        public IMemberBuildResult BuildResult { get; private set; }

        #endregion

        /// <summary>
        /// Gets the builder data specific to this builders key. <see cref="TestKey"/>.
        /// </summary>
        /// <param name="category">Name of the category to request.</param>
        /// <returns>
        /// The builder data with the <see cref="TestKey"/> or <c>null</c> if nothing is found.
        /// </returns>
        /// <exception cref="InvalidOperationException">Can't lookup category builder data without a correct <see cref="TestKey"/> property.</exception>
        public IBuilderData GetBuilderData(string category)
        {
            if (string.IsNullOrEmpty(this.TestKey))
            {
                throw new InvalidOperationException(
                    "Can't lookup category builder data without a correct TestKey propery.");
            }

            IBuilderData result;
            this.BuildData.TryGetValue(category, this.TestKey, out result);
            return result;
        }

        /// <summary>
        /// Gets the builder data specific to this builders key.
        /// </summary>
        /// <typeparam name="T">Type of the <see cref="IMemberBuilder"/></typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The builder data with the <see cref="TestKey"/> or <c>null</c> if nothing is found.
        /// </returns>
        public T GetBuilderData<T>(IMemberBuilder builder) where T : class, IBuilderData
        {
            var dic = this.BuildData.General;
            IBuilderData userData;
            dic.TryGetValue(builder.GetType().FullName, out userData);

            // var userData = this.BuildData.General[builder.GetType().FullName];
            // as PropertyBuilderUserParameters;
            return userData as T;
        }

        /// <summary>
        /// Gets the test method info for this test object member.
        /// </summary>
        /// <param name="typeMember">The type member that hold the User data of the current test object member.</param>
        /// <returns>The test method info for this test object member.</returns>
        protected abstract MethodInfo GetTestMethodInfo(CodeTypeMember typeMember);

        /// <summary>
        /// Gets the type of the object under test.
        /// </summary>
        /// <param name="testClassDeclaration">The test class declaration.</param>
        /// <returns>The type of the object under test.</returns>
        protected abstract Type GetTestObjectClassType(CodeTypeDeclaration testClassDeclaration);
    }
}