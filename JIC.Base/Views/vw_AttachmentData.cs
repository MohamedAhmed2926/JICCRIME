using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_AttachmentData
    {
        public Guid? ID { get; set; }
        public string AttachmentPath { get; set; }
        public AttachmentTypes AttachmentType { get; set; }
        public string Name { get; set; }
        public int? FolderID { get; set; }
    }
}
