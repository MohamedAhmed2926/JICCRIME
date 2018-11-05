using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_Documents
    {
        public Guid ID { get; set; }
        public Guid? FolderID { get; set; }
        public string DocumentTitle { get; set; }
        public string FileName { get; set; }
        public int TypeID { get; set; }
        public DateTime UploadDate { get; set; }
        public int UploadBy { get; set; }
        public byte[] FileData { get; set; }

        public string CurentUserName { get; set; }
    }
}
