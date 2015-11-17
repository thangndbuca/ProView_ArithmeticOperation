/***********************************************************************
 * <copyright file="UnitOfWork.cs" company="m365 JSC">
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
    ///   ProViewUnitOfWork
    /// </summary>
    public partial class ProViewUnitOfWork : UnitOfWork
    {
        public ProViewUnitOfWork() : base(new ProViewDb()) { }
    }
}
