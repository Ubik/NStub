﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterDescriptionAttribute.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2012, EvePanix. All rights reserved.
//   See the license notes shipped with this source and the GNU GPL.
// </copyright>
// <author>Jedzia</author>
// <email>jed69@gmx.de</email>
// <date>$date$</date>
// --------------------------------------------------------------------------------------------------------------------

namespace NStub.CSharp
{
    using System;

    /// <summary>
    /// Describes a parameter class.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class ParameterDescriptionAttribute : Attribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterDescriptionAttribute"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        public ParameterDescriptionAttribute(string description)
        {
            this.Description = description;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        #endregion
    }
}