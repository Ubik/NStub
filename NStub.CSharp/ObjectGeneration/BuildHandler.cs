﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildHandler.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2012, EvePanix. All rights reserved.
//   See the license notes shipped with this source and the GNU GPL.
// </copyright>
// <author>Jedzia</author>
// <email>jed69@gmx.de</email>
// <date>$date$</date>
// --------------------------------------------------------------------------------------------------------------------

namespace NStub.CSharp.ObjectGeneration
{
    using System;
    using NStub.Core;
    using NStub.CSharp.BuildContext;
    using NStub.CSharp.ObjectGeneration.Builders;

    /// <summary>
    /// Checks if a registered <see cref="IMemberBuilder"/> can handle a <see cref="IMemberBuildContext"/> request.
    /// </summary>
    public class BuildHandler : IBuildHandler
    {
        #region Fields

        private readonly Func<IMemberBuildContext, bool> handler;
        private readonly Type type;
        private readonly Type parameterDataType;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildHandler"/> class.
        /// </summary>
        /// <param name="type">The type of the <see cref="IMemberBuilder"/> worker.</param>
        /// <param name="handler">The handler that can determine if the <paramref name="type"/> can 
        /// accept an <see cref="IMemberBuildContext"/> order.</param>
        public BuildHandler(Type type, Func<IMemberBuildContext, bool> handler, Type parameterDataType)
        {
            Guard.CanBeAssigned(() => type, type, typeof(IMemberBuilder));
            Guard.NotNull(() => handler, handler);
            //Guard.NotNull(() => parameterDataType, parameterDataType);
            Guard.CanBeAssigned(() => parameterDataType, parameterDataType, typeof(IBuilderSetupParameters));

            this.type = type;
            this.handler = handler;
            this.parameterDataType = parameterDataType;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the handler, that checks, if the associated <see cref="Type"/> can process an <see cref="IMemberBuildContext"/> assignment.
        /// </summary>
        public Func<IMemberBuildContext, bool> CanHandle
        {
            get
            {
                return this.handler;
            }
        }

        /// <summary>
        /// Gets the associated <see cref="IMemberBuilder"/> type that can handle the request specified in the <see cref="CanHandle"/> method.
        /// </summary>
        public Type Type
        {
            get
            {
                return this.type;
            }
        }

        public Type ParameterDataType
        {
            get
            {
                return this.parameterDataType;
            }
        }

        #endregion

        /// <summary>
        /// Creates a new instance of the <see cref="IMemberBuilder"/> specified in the <see cref="Type"/> property with 
        /// the specified context data.
        /// </summary>
        /// <param name="context">The context of the current test object.</param>
        /// <returns>
        /// A test member builder that can handle the request specified in the <see cref="CanHandle"/> method.
        /// </returns>
        public IMemberBuilder CreateInstance(IMemberBuildContext context)
        {
            var parameters = new object[] { context };
            var memberBuilder = (IMemberBuilder)Activator.CreateInstance(this.Type, parameters);
            return memberBuilder;
        }
    }
}