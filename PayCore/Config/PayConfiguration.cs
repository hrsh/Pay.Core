// ***********************************************************************
// Assembly         : PayCore
// Author           : Mazdak Shojaie
// Created          : 09-24-2019
//
// Last Modified By : Mazdak Shojaie
// Last Modified On : 09-24-2019
// ***********************************************************************
// <copyright file="PayConfiguration.cs" company="PayCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PayCore.Config {
    /// <summary>
    /// Class PayConfiguration.
    /// </summary>
    public class PayConfiguration {
        /// <summary>
        /// Gets or sets the API. Provide this value from appsettings.json file, section [PayConfig]
        /// </summary>
        /// <value>The API access code.</value>
        public string Api { get; set; }
    }
}