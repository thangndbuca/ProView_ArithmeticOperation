/***********************************************************************
 * <copyright file="Domain.cs" company="m365 JSC">
 * -->    Copyright (C) statement. All right reserved
 * </copyright>
 * 
 * Created:   ThangND
 * Email:    thangnd@m365.vn
 * Website:
 * Create Date: 16 November 2015
 * Usage: 
 * 
 * RevisionHistory: 
 * Date         Author               Description 
 * 
 * ************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProView.Domain
{
    /// <summary>
    ///    Arithmetic
    /// </summary>
    public partial class Arithmetic : Entity<Arithmetic>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Arithmetic"/> class.
        /// </summary>
        public Arithmetic() { }
      
        /// <summary>
        /// Initializes a new instance of the <see cref="Arithmetic"/> class.
        /// </summary>
        /// <param name="defaults">if set to <c>true</c> [defaults].</param>
        public Arithmetic(bool defaults) : base(defaults) { }

        /// <summary>
        /// Gets or sets the row identifier.
        /// </summary>
        /// <value>
        /// The row identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the arithmetic operation.
        /// </summary>
        /// <value>
        /// The arithmetic operation.
        /// </value>
        public string ArithmeticOperation { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public double Result { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>
        /// The create date.
        /// </value>
        public DateTime CreateDate { get; set; }
    }
}
