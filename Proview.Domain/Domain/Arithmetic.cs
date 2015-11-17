/***********************************************************************
 * <copyright file="Arithmetic.cs" company="m365 JSC">
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
        /// Validates this instance.
        /// </summary>
        protected override void Validate()
        {
            // simple validation rules

            if (string.IsNullOrEmpty(ArithmeticOperation)) Errors.Add("0001", "ArithmeticOperation is required");

            if (ArithmeticOperation.Length > 500) Errors.Add("0002", "ArithmeticOperation cannot be longer than 500 characters");
        }
    }
}
