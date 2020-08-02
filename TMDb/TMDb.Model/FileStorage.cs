using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDb.Model.Common;


namespace TMDb.Model
{
    public class FileStorage : IFileStorage
    {
        public Guid FileID { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public FileStorage() { }
        public FileStorage(Guid fileID, string imageName, string imagePath)
        {
            this.FileID = fileID;
            this.ImageName = imageName;
            this.ImagePath = imagePath;
        }
    }
}
