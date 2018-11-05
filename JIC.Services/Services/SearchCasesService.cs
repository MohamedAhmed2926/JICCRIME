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
    public class SearchCasesService : ServiceBase , ISearchCasesService
    {
        public SearchCasesService(CaseType caseType) : base(caseType)
        {
        }

        public SearchCaseComponent SearchCaseComponent { get { return GetComponent<SearchCaseComponent>(); } }

        public List<vw_AddSessionSearchResult> AddSessionsSearch(vw_AddSessionsSearchData searchData)
        {
            return SearchCaseComponent.AddSessionsSearch(searchData);
        }

        public List<vw_AddSessionSearchResult> EditSessionSearch(vw_AddSessionsSearchData searchData)
        {
            return SearchCaseComponent.EditSessionSearch(searchData);
        }

        public List<SearchResult> Search(vw_SearchData searchData)
        {
            return SearchCaseComponent.Search(searchData);
        }
    }
}
