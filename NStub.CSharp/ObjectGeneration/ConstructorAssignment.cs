﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstructorAssignment.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2012, EvePanix. All rights reserved.
//   See the license notes shipped with this source and the GNU GPL.
// </copyright>
// <author>Jedzia</author>
// <email>jed69@gmx.de</email>
// <date>$date$</date>
// --------------------------------------------------------------------------------------------------------------------

namespace NStub.CSharp.ObjectGeneration
{
    using System.CodeDom;
    using NStub.Core;
    using System.Collections.Generic;
using System;

    /// <summary>
    /// Holds a mapping from parameter name to code creation expressions.
    /// </summary>
    public class ConstructorAssignment
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorAssignment"/> class.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="assignStatement">The assign statement for the parameter.</param>
        /// <param name="memberField">The related member field of the parameter.</param>
        public ConstructorAssignment(
            string parameterName, CodeAssignStatement assignStatement, CodeMemberField memberField, Type type)
        {
            Guard.NotNullOrEmpty(() => parameterName, parameterName);
            Guard.NotNull(() => assignStatement, assignStatement);
            Guard.NotNull(() => memberField, memberField);
            Guard.NotNull(() => type, type);

            this.ParameterName = parameterName;
            this.AssignStatement = assignStatement;
            this.MemberField = memberField;
            this.MemberType = type;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the assign statement for the parameter.
        /// </summary>
        public CodeAssignStatement AssignStatement { get; private set; }

        /// <summary>
        /// Gets the related member field of the parameter.
        /// </summary>
        public CodeMemberField MemberField { get; private set; }
        public Type MemberType { get; private set; }

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        /// <value>
        /// The name of the parameter.
        /// </value>
        public string ParameterName { get; private set; }
        
        private ICollection<ConstructorAssignment> createAssignments;

        /// <summary>
        /// Gets the additional assignments used to create this constructor assignment.
        /// </summary>
        public ICollection<ConstructorAssignment> CreateAssignments
        {
            get
            {
                if (this.createAssignments == null)
                {
                    this.createAssignments = new List<ConstructorAssignment>();
                }
                return this.createAssignments;
            }

            /*set
            {
                this.createAssignments = value;
            }*/
        }

        /// <summary>
        /// Gets a value indicating whether this instance has creation assignments.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has creation assignments; otherwise, <c>false</c>.
        /// </value>
        public bool HasCreationAssignments
        {
            get
            {
                return this.createAssignments != null && this.createAssignments.Count > 0;
            }
        }

        #endregion
    }
}