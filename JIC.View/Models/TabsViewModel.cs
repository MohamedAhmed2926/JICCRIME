using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class TabsViewModel
    {
        public string Name { get; set; }
        public string Url { get; set; } = null;
        public bool IsDisabled { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public bool IsDone { get; set; } = false;
    }
}