using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;

namespace JIC.Components.Components
{
    public class TextPredictionsComponent
    {
        private ITextPredictionsRepository ITextPredictionsRepository;

        public TextPredictionsComponent( ITextPredictionsRepository ITextPredictionsRepository)
        {
            this.ITextPredictionsRepository = ITextPredictionsRepository;
        }


        public AddTextStatus AddTextPredections(vw_TextPredections TextPredections)
        {
            return ITextPredictionsRepository.AddTextPredections(TextPredections);
        }

        public bool DeleteTextByID(int TextID)
        {
            return ITextPredictionsRepository.DeleteTextByID(TextID);
        }

        public EditTextStatus EditText(vw_TextPredections TextPredections)
        {
            return ITextPredictionsRepository.EditText(TextPredections);
        }

        public vw_TextPredections GetTextByID(int TextID)
        {
            return ITextPredictionsRepository.GetTextByID(TextID);
        }

        public List<vw_TextPredections> GetTextPredections(List<vw_KeyValue> CircuitID)
        {
            return ITextPredictionsRepository.GetTextPredections(CircuitID);
        }

        public List<vw_KeyValue> GetCrimeTypes(int UserId)
        {
            return ITextPredictionsRepository.GetCrimeTypes(UserId);
        }

    }
}
