using JIC.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;
using JIC.Base.Interfaces;

namespace JIC.Components.Components
{
    public class SearchCaseComponent
    {
        private ISearchCaseRepository SearchCaseRepository;
        public SearchCaseComponent( ISearchCaseRepository SearchCaseRepository)
        {
            this.SearchCaseRepository = SearchCaseRepository;
        }

        public List<SearchResult> Search(vw_SearchData searchData)
        {
            return SearchCaseRepository.SearchCase(searchData);
        }
        public List<vw_AddSessionSearchResult> AddSessionsSearch(vw_AddSessionsSearchData searchData)
        {
            return SearchCaseRepository.AddSessionsSearch(searchData);
        }

        public List<vw_AddSessionSearchResult> EditSessionSearch(vw_AddSessionsSearchData searchData)
        {
            return SearchCaseRepository.EditSessionSearch(searchData);
        }
    }
}
