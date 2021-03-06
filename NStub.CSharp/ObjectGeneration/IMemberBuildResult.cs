﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMemberBuildResult.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2012, EvePanix. All rights reserved.
//   See the license notes shipped with this source and the GNU GPL.
// </copyright>
// <author>Jedzia</author>
// <email>jed69@gmx.de</email>
// <date>$date$</date>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.CodeDom;
namespace NStub.CSharp.ObjectGeneration
{
    /// <summary>
    /// Provides feedback in the build phase of test object generation.
    /// </summary>
    public interface IMemberBuildResult : IMemberPreBuildResult
    {
        /// <summary>
        /// Gets the class methods to add to the test class under Build-Phase.
        /// </summary>
        ICollection<CodeMemberMethod> ClassMethodsToAdd { get; }
    }
}