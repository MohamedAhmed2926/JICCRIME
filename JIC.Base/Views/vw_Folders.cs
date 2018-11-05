using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_Folders
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int? DocumentsCount { get; set; }
        public int? ComputedCount { get; set; }
        public Guid? ParentFolderID { get; set; }
        public DateTime UploadDate { get; set; }
        public string CurentUserName { get; set; }
    }
}
