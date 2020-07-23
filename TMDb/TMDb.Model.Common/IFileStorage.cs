using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDb.Model.Common
{
    public interface IFileStorage
    {
        Guid FileID { get; set; }
        string ImageName { get; set; }
        string ImagePath { get; set; }
    }
}
