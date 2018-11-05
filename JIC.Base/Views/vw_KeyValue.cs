using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    [Serializable]
    public class vw_KeyValue
    {
        public vw_KeyValue()
        { }
        public vw_KeyValue(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
        public int ID { get; set; }
        public string Name { get; set; }
    }


    public class vw_KeyValue_Nullable
    {
        public vw_KeyValue_Nullable()
        { }
        public vw_KeyValue_Nullable(int? ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
        public int? ID { get; set; }
        public string Name { get; set; }
    }
    public class vw_KeyValueLongID
    {
        public vw_KeyValueLongID()
        { }
        public vw_KeyValueLongID(long ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
        public long ID { get; set; }
        public string Name { get; set; }
    }
    public class vw_KeyValueStringID
    {
        public vw_KeyValueStringID()
        { }
        public vw_KeyValueStringID(string ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
