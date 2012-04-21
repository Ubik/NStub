﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuilderSerializer.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2012, EvePanix. All rights reserved.
//   See the license notes shipped with this source and the GNU GPL.
// </copyright>
// <author>Jedzia</author>
// <email>jed69@gmx.de</email>
// <date>$date$</date>
// --------------------------------------------------------------------------------------------------------------------

namespace NStub.CSharp.ObjectGeneration.Builders
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using NStub.Core;

    /// <summary>
    /// Helper class to serialize <see cref="BuildParametersBase{T}"/> data classes of the <see cref="IMemberBuilder"/> family.
    /// </summary>
    internal sealed class BuilderSerializer : IBuilderSerializer
    {
        /// <summary>
        /// Set the parameters in the properties storage from a specified xml representation of the data.
        /// </summary>
        /// <param name="xml">The xml representation of the data.</param>
        /// <param name="properties">The global properties storage.</param>
        /// <param name="handlers">The list of existing handlers to investigate.</param>
        /// <returns>
        /// A new instance of a matching parameter data set for the specified builder.
        /// </returns>
        /// <exception cref="InvalidCastException"><c>InvalidCastException</c> Problem building from serialization data.</exception>
        public IEnumerable<IMemberBuildParameters> DeserializeAllSetupData(
            string xml, IBuildDataDictionary properties, IEnumerable<IBuildHandler> handlers)
        {
            // <NStub.CSharp.ObjectGeneration.Builders.PropertyBuilder>
            var plist = new List<IMemberBuildParameters>();
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            foreach(XmlElement item in doc[BuilderConstants.BuildParametersXmlId])
            {
                IBuildHandler handler = this.DetermineIMemberBuilderFromXmlFragment(item.OuterXml, handlers);
                var para = this.SetParameters(item.OuterXml, properties, handler);

                plist.Add(para);

                // yield return para;
            }

            return plist;
        }

        /// <summary>
        /// Determines the matching member builder by the first childs name from a XML fragment.
        /// </summary>
        /// <param name="xml">The xml fragment.</param>
        /// <param name="handlers">The list of existing handlers to investigate.</param>
        /// <returns>The matching handler or <c>null</c> if none is found.</returns>
        public IBuildHandler DetermineIMemberBuilderFromXmlFragment(string xml, IEnumerable<IBuildHandler> handlers)
        {
            // <NStub.CSharp.ObjectGeneration.Builders.PropertyBuilder>
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var firstChild = doc.FirstChild;

            IBuildHandler handler = null;
            foreach(var item in handlers.Where(item => item.Type.FullName == firstChild.Name))
            {
                handler = item;
            }

            return handler;
        }

        /// <summary>
        /// Get the parameters for the specified builder type, possibly creating it, if there
        /// is not yet one in the build data collection.
        /// </summary>
        /// <param name="builderType">Type of the builder to request a set of sample data for.</param>
        /// <param name="paraType">Type of the parameter class.</param>
        /// <param name="properties">The global properties storage.</param>
        /// <returns>
        /// A new instance of a matching parameter data set for the specified builder.
        /// </returns>
        /// <exception cref="KeyNotFoundException">The given <paramref name="builderType"/> was not present in the lookup.</exception>
        public IMemberBuildParameters GetParameters(Type builderType, Type paraType, IBuildDataDictionary properties)
        {
            Guard.NotNull(() => properties, properties);
            IBuilderData result;
            var found = properties.TryGetValue(string.Empty + builderType.FullName, out result);
            if (found)
            {
                return result as IMemberBuildParameters;
            }

            var paraInstance = Activator.CreateInstance(paraType);
            var setupPara = (IMemberBuildParameters)paraInstance;

            properties.AddDataItem(string.Empty + builderType.FullName, setupPara);
            return setupPara;
        }

        /// <summary>
        /// Gets the sample setup xml data for a specified <see cref="IMemberBuilder"/>.
        /// </summary>
        /// <param name="builderType">Type of the builder to request a set of sample data for.</param>
        /// <param name="paraType">Type of the parameter class.</param>
        /// <returns>
        /// A string containing the sample data for the specified <paramref name="builderType"/> as xml string.
        /// </returns>
        public string GetSampleSetupData(Type builderType, Type paraType)
        {
            var paraInstance = Activator.CreateInstance(paraType);
            var setupPara = (IMemberBuildParameters)paraInstance;

            return SerializeParametersForBuilderType(builderType, setupPara.GetType().Name, setupPara.SampleXml);
        }

        /// <summary>
        /// Gets the xml data representation of all registered <see cref="IBuildHandler"/>s.
        /// </summary>
        /// <param name="properties">The properties storage which stores the <see cref="IMemberBuildParameters"/> data to serialize.</param>
        /// <param name="handlers">The handlers with the links to the <see cref="IMemberBuilder"/> types.</param>
        /// <returns>
        /// A new instance of a matching parameter data set for the specified builder.
        /// </returns>
        public string SerializeAllHandlers(IBuildDataDictionary properties, IEnumerable<IBuildHandler> handlers)
        {
            var xmlDoc = new XmlDocument();
            var root = xmlDoc.CreateElement(BuilderConstants.BuildParametersXmlId);
            xmlDoc.AppendChild(root);
            foreach(var handler in handlers)
            {
                if (handler.Type == null || handler.Type.FullName == null)
                {
                    continue;
                }

                var setupPara = this.GetParameters(handler.Type, handler.ParameterDataType, properties);

                var setupParaType = handler.ParameterDataType;
                var setupParaXml = setupPara.Serialize();

                var ele = xmlDoc.CreateElement(handler.Type.FullName);
                var ele2 = xmlDoc.CreateElement(setupParaType.Name);
                root.AppendChild(ele);
                ele.AppendChild(ele2);
                var innerDoc = new XmlDocument();
                var xml = setupParaXml;
                innerDoc.LoadXml(xml);
                ele2.InnerXml = innerDoc[setupParaType.Name].InnerXml;
            }

            return PrettyPrintXml(xmlDoc.OuterXml);
        }

        /// <summary>
        /// Gets the xml data representation of a single registered <see cref="IMemberBuilder"/>s parameters.
        /// </summary>
        /// <param name="builderType">Type of the builder to request data for.</param>
        /// <param name="parameters">The parameters with the data to serialize.</param>
        /// <returns>
        /// The serialized data of the specified <paramref name="builderType"/>.
        /// </returns>
        public string SerializeParametersForBuilderType(Type builderType, IMemberBuildParameters parameters)
        {
            return SerializeParametersForBuilderType(builderType, parameters.GetType().Name, parameters.Serialize());
        }

        /// <summary>
        /// Set the parameters in the properties storage from a specified xml representation of the data.
        /// </summary>
        /// <param name="xml">The xml representation of the data.</param>
        /// <param name="properties">The global properties storage.</param>
        /// <param name="handler">The handler that holds the builder- to parameter-type relation.</param>
        /// <returns>
        /// A new instance of a matching parameter data set for the specified builder.
        /// </returns>
        /// <exception cref="InvalidCastException"><c>InvalidCastException</c> Problem building from serialization data.</exception>
        public IMemberBuildParameters SetParameters(string xml, IBuildDataDictionary properties, IBuildHandler handler)
        {
            Guard.NotNull(() => properties, properties);
            if (handler == null)
            {
                // Todo: or throw?
                return MemberBuilder.EmptyParameters;
            }

            // <NStub.CSharp.ObjectGeneration.Builders.PropertyBuilder>
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var firstChild = doc.FirstChild;

            var paraType = handler.ParameterDataType;

            // null check paraType
            var xxxx = paraType.BaseType.GetGenericTypeDefinition();

            // null check xxxx
            var serializer2 = xxxx.BaseType.GetGenericTypeDefinition();

            // Todo: This strange thingy ... very good checking and logging! :)
            // null check serializer2
            var paraInstance = serializer2
                .MakeGenericType(paraType)
                .GetMethod("Deserialize", new[] { typeof(string) })
                .Invoke(null, new object[] { firstChild.InnerXml });

            var setupPara = (IMemberBuildParameters)paraInstance;
            try
            {
                var propertyKey = string.Empty + handler.Type.FullName;

                // IBuilderData property;
                // var found = properties.TryGetValue(propertyKey, out property);
                // if (found)
                // {
                properties.AddDataItem(propertyKey, setupPara, true);

                // return setupPara;
                // }
                // properties.AddDataItem(propertyKey, setupPara);
            }
            catch (Exception ex)
            {
                var message = string.Format(
                    "Problem building {0} from serialization data.{1}{2}{3}", 
                    handler.Type.FullName, 
                    Environment.NewLine, 
                    firstChild.InnerXml, 
                    Environment.NewLine);
                throw new InvalidCastException(message, ex);
            }

            return setupPara;
        }

        /// <summary>
        /// Pretty print XML data.
        /// </summary>
        /// <param name="xml">The string containing valid XML data.</param>
        /// <returns>The xml data in indented and justified form.</returns>
        private static string PrettyPrintXml(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            doc.Normalize();

            TextWriter wr = new StringWriter();
            doc.Save(wr);
            var str = wr.ToString();
            return str;
        }

        /// <summary>
        /// Gets the xml data representation of a single registered <see cref="IMemberBuilder"/>s parameters.
        /// </summary>
        /// <param name="builderType">Type of the builder to request data for.</param>
        /// <param name="parameterTypeShortName">Short name of the parameter type.</param>
        /// <param name="parameterTypeXmlData">The data of the parameter type as XML representation.</param>
        /// <returns>
        /// The serialized data of the specified <paramref name="builderType"/>.
        /// </returns>
        private static string SerializeParametersForBuilderType(
            Type builderType, string parameterTypeShortName, string parameterTypeXmlData)
        {
            var xmlDoc = new XmlDocument();
            if (builderType != null && builderType.FullName != null)
            {
                var element = xmlDoc.CreateElement(builderType.FullName);
                var innerElement = xmlDoc.CreateElement(parameterTypeShortName);
                xmlDoc.AppendChild(element);
                element.AppendChild(innerElement);
                var innerDoc = new XmlDocument();
                innerDoc.LoadXml(parameterTypeXmlData);
                if (parameterTypeShortName != null)
                {
                    // null check
                    innerElement.InnerXml = innerDoc[parameterTypeShortName].InnerXml;
                }
            }

            return PrettyPrintXml(xmlDoc.OuterXml);
        }
    }
}