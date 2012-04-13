﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace NStub.CSharp.ObjectGeneration
{

    /// <summary>
    /// List of <see cref="IBuilderData"/>.
    /// </summary>
    public class BuildDataCollection : IEnumerable<IBuilderData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:BuildDataCollection"/> class.
        /// </summary>
        public BuildDataCollection()
        {
            this.generalData = new Dictionary<string, IBuilderData>();
            data.Add("General", this.generalData);
        }

        #region Fields

        private readonly Dictionary<string, IBuilderData> generalData;
        private readonly Dictionary<string, Dictionary<string, IBuilderData>> data = new Dictionary<string, Dictionary<string, IBuilderData>>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of data items.
        /// </summary>
        public int Count
        {
            get
            {
                return this.data.Count;
            }
        }

        /*/// <summary>
        /// Gets the number of parameters with standard arguments. ( Currently all but interfaces ).
        /// </summary>
        public int CountOfStandardTypes
        {
            get
            {
                return this.parameters.Count(e => !e.ParameterType.IsInterface);
            }
        }*/

        /*/// <summary>
        /// Gets a value indicating whether this instance has parameters of a interface type stored.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has interfaces in the parameter types; otherwise, <c>false</c>.
        /// </value>
        public bool HasInterfaces { get; private set; }*/

        /*/// <summary>
        /// Gets the parameters where the parameter-type isn't an interface.
        /// </summary>
        public IEnumerable<ParameterInfo> StandardTypes
        {
            get
            {
                return this.parameters.Where(e => !e.ParameterType.IsInterface);
            }
        }*/

        #endregion

        /// <summary>
        /// Adds the specified data item to the "General" topic of this list.
        /// </summary>
        /// <param name="key">The key of the item.</param>
        /// <param name="item">The data item to add.</param>
        public void AddDataItem(string key, IBuilderData item)
        {
            this.generalData.Add(key, item);
        }

        /// <summary>
        /// Adds the specified data item to the specified category of this list.
        /// </summary>
        /// <param name="category">The category of the item.</param>
        /// <param name="key">The key of the item.</param>
        /// <param name="item">The data item to add.</param>
        public void AddDataItem(string category, string key, IBuilderData item)
        {
            Dictionary<string, IBuilderData> catLookup;
            var found = this.TryGetCategory(category, out catLookup);
            if (!found)
            {
                catLookup = new Dictionary<string, IBuilderData>();
                this.data.Add(category, catLookup);
            }
            catLookup.Add(key, item);
        }

        /// <summary>
        /// Gets the <see cref="System.Collections.Generic.Dictionary&lt;System.String,NStub.CSharp.ObjectGeneration.IBuilderData&gt;"/> with the specified category.
        /// </summary>
        /// <param name="category">The category of the requested data items.</param>
        public Dictionary<string, IBuilderData> this[string category]
        {
            get
            {
                return this.data[category];
            }

            /*set
            {
                this.data[category] = value;
            }*/
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">When this method returns, contains the value associated with the specified
        /// key, if the key is found; otherwise, the default value for the type of the
        /// value parameter. This parameter is passed uninitialized.</param>
        /// <returns><c>true</c> if the this instance contains an
        /// element with the specified key; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">key is null.</exception>
        public bool TryGetValue(string key, out IBuilderData value)
        {
            return this.generalData.TryGetValue(key, out value);
        }

        public bool TryGetValue(string category, string key, out IBuilderData value)
        {
            Dictionary<string, IBuilderData> catLookup;
            var found = this.TryGetCategory(category, out catLookup);
            if (found)
            {
                return catLookup.TryGetValue(key, out value);
            }
            value = null;
            return false;
        }

        /// <summary>
        /// Gets the category associated with the specified key.
        /// </summary>
        /// <param name="category">The key of the value to get.</param>
        /// <param name="value">When this method returns, contains the value associated with the specified
        /// key, if the key is found; otherwise, the default value for the type of the
        /// value parameter. This parameter is passed uninitialized.</param>
        /// <returns><c>true</c> if the this instance contains an
        /// element with the specified key; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">key is null.</exception>
        public bool TryGetCategory(string category, out Dictionary<string, IBuilderData> value)
        {
            return this.data.TryGetValue(category, out value);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<IBuilderData> GetEnumerator()
        {
            return this.generalData.Values.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.data.Values.GetEnumerator();
        }
    }

}
