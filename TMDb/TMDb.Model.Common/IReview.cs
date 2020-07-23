﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Model.Common
{
    public interface IReview
    {
        Guid ReviewID { get; set; }
        int NumberOfStars { get; set; }
        string Comment { get; set; }
        Guid AccountID { get; set; }
        Guid MovieID { get; set; }
    }
}
