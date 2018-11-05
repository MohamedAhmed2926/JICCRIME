using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface ITextPredictionsRepository
    {
        AddTextStatus AddTextPredections(vw_TextPredections TextPredections);
        vw_TextPredections GetTextByID(int TextID);
        bool DeleteTextByID(int TextID);
        EditTextStatus EditText(vw_TextPredections TextPredections);
        List<vw_TextPredections> GetTextPredections(List<vw_KeyValue> CircuitID);
        List<vw_KeyValue> GetCrimeTypes(int UserId);
       
    }
}
