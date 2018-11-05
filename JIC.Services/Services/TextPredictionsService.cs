using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Components.Components;

namespace JIC.Services.Services
{
    public class TextPredictionsService : ServiceBase , ITextPredectionsService
    {
        public TextPredictionsService(CaseType caseType) : base(caseType)
        {
        }

        private TextPredictionsComponent TextPredictionsComponent { get { return GetComponent<TextPredictionsComponent>(); } }

        public AddTextStatus AddTextPredections(vw_TextPredections TextPredections)
        {
            return TextPredictionsComponent.AddTextPredections(TextPredections);
        }

        public bool DeleteTextByID(int TextID)
        {
            return TextPredictionsComponent.DeleteTextByID(TextID);
        }

        public EditTextStatus EditText(vw_TextPredections TextPredections)
        {
            return TextPredictionsComponent.EditText(TextPredections);
        }

        public vw_TextPredections GetTextByID(int TextID)
        {
            return TextPredictionsComponent.GetTextByID(TextID);
        }

        public List<vw_TextPredections> GetTextPredections(List<vw_KeyValue> CircuitID)
        {
            return TextPredictionsComponent.GetTextPredections(CircuitID);
        }

        //public List<vw_TextPredections> GetTextPredections(int? userID, int CrimeTypeID)
        //{
        //    return TextPredictionsComponent.GetTextPredections(null,CrimeTypeID);
        //}
    }
}
