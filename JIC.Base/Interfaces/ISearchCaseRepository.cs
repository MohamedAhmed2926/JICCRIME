using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base.Views;

namespace JIC.Base.Interfaces
{
    public interface ISearchCaseRepository
    {
        List<SearchResult> SearchCase(vw_SearchData searchData);
        List<vw_AddSessionSearchResult> AddSessionsSearch(vw_AddSessionsSearchData searchData);
        List<vw_AddSessionSearchResult> EditSessionSearch(vw_AddSessionsSearchData searchData);

    }
}
