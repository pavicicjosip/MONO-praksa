﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Model.Common
{
    public interface IAccount
    {
        Guid AccountID { get; set; }
        string Email { get; set; }
        string Username { get; set; }
        Guid FileID { get; set; }
    }
}
