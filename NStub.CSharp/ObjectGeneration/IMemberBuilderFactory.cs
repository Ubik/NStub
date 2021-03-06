﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMemberBuilderFactory.cs" company="EvePanix">
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
    using System.Collections.Generic;
    using System.ComponentModel;
    using NStub.CSharp.BuildContext;

    /// <summary>
    /// Provides access to builders used in test method generation.
    /// </summary>
    public interface IMemberBuilderFactory
    {
        #region Properties

        /// <summary>
        /// Gets the name of the builders.
        /// </summary>
        IEnumerable<string> BuilderNames { get; }

        /// <summary>
        /// Gets the type of each builder.
        /// </summary>
        IEnumerable<Type> BuilderTypes { get; }

        #endregion

        /// <summary>
        /// Set the parameters in the properties storage from a specified xml representation of the data.
        /// </summary>
        /// <param name="xml">The xml representation of the data.</param>
        /// <param name="properties">The global properties storage.</param>
        /// <returns>
        /// A new instance of a matching parameter data set for the specified builder.
        /// </returns>
        /// <exception cref="InvalidCastException"><c>InvalidCastException</c> Problem building from serialization data.</exception>
        IEnumerable<IMemberBuildParameters> DeserializeAllSetupData(string xml, IBuildDataDictionary properties);

        /// <summary>
        /// Tries to get the builder for the specified context.
        /// </summary>
        /// <param name="context">The context of the current test object.</param>
        /// <returns>A list of member builders that can handle the request or an <c>empty</c> list if no one can be found.</returns>
        IEnumerable<IMemberBuilder> GetBuilder(IMemberBuildContext context);

        /// <summary>
        /// Tries to get the builder for the specified context dependent on user property activation.
        /// </summary>
        /// <param name="context">The context of the current test object.</param>
        /// <param name="useUserActivation">if set to <c>true</c> use user activation in context.BuildData stored values.</param>
        /// <returns>
        /// A list of member builders that can handle the request or an <c>empty</c> list if no one can be found.
        /// </returns>
        IEnumerable<IMemberBuilder> GetBuilder(IMemberBuildContext context, bool useUserActivation);

        /// <summary>
        /// Gets the description for the specified builder type.
        /// </summary>
        /// <param name="builderType">Type of the builder.</param>
        /// <returns>A text with the description of the builder and builder parameters, specified by 
        /// <see cref="DescriptionAttribute"/>'s.</returns>
        string GetBuilderDescription(Type builderType);

        /// <summary>
        /// Get the parameters for the specified multi builder type, possibly creating it, if there
        /// is not yet one in the build data collection.
        /// </summary>
        /// <param name="key">The unique identifier for the set of parameter data.</param>
        /// <param name="builderType">Type of the builder to request a set of sample data for.</param>
        /// <param name="properties">The global properties storage.</param>
        /// <returns>
        /// A new instance of a matching parameter data set for the specified builder.
        /// </returns>
        /// <exception cref="KeyNotFoundException">The given <paramref name="builderType"/> was not present in the lookup.</exception>
        IMultiBuildParameters GetMultiParameter(Guid key, Type builderType, IBuildDataDictionary properties);

        /// <summary>
        /// Get the parameters for the specified static builder type, possibly creating it, if there
        /// is not yet one in the build data collection.
        /// </summary>
        /// <param name="builderType">Type of the builder to request a set of sample data for.</param>
        /// <param name="properties">The global properties storage.</param>
        /// <returns>
        /// A new instance of a matching parameter data set for the specified builder.
        /// </returns>
        /// <exception cref="KeyNotFoundException">The given <paramref name="builderType"/> was not present in the lookup.</exception>
        IMemberBuildParameters GetParameters(Type builderType, IBuildDataDictionary properties);

        /// <summary>
        /// Gets the xml data representation of all registered <see cref="IMemberBuilder"/>s parameters.
        /// </summary>
        /// <param name="properties">The properties storage which stores the <see cref="IMemberBuildParameters"/> data to serialize.</param>
        /// <returns>
        /// A new instance of a matching parameter data set for the specified builder.
        /// </returns>
        string SerializeAllSetupData(IBuildDataDictionary properties);

        /// <summary>
        /// Gets the xml data representation of a single registered <see cref="IMemberBuilder"/>s parameters.
        /// </summary>
        /// <param name="builderType">Type of the builder to request a set of sample data for.</param>
        /// <param name="properties">The properties storage which stores the <see cref="IMemberBuildParameters"/> data to serialize.</param>
        /// <returns>
        /// The serialized data of the specified <paramref name="builderType"/>.
        /// </returns>
        string SerializeSetupData(Type builderType, BuildDataDictionary properties);

        /// <summary>
        /// Gets the xml data representation of multiple registered <see cref="IMultiBuilder"/>s parameters by key.
        /// </summary>
        /// <param name="key">The unique id of the multi parameter item.</param>
        /// <param name="builderType">Type of the builder to request a set of sample data for.</param>
        /// <param name="properties">The properties storage which stores the <see cref="IMultiBuildParameters"/> data to serialize.</param>
        /// <returns>
        /// The serialized data of the specified <paramref name="builderType"/>.
        /// </returns>
        string SerializeSetupData(Guid key, Type builderType, BuildDataDictionary properties);

        /// <summary>
        /// Set the parameters in the properties storage from a specified xml representation of the data.
        /// </summary>
        /// <param name="xml">The xml representation of the data.</param>
        /// <param name="properties">The global properties storage.</param>
        /// <returns>
        /// A new instance of a matching parameter data set for the specified builder.
        /// </returns>
        /// <exception cref="InvalidCastException"><c>InvalidCastException</c> Problem building from serialization data.</exception>
        IMemberBuildParameters SetParameters(string xml, IBuildDataDictionary properties);
    }
}