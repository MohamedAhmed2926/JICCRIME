using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.ServicesInterfaces
{
    public interface ISearchCasesService
    {
        List<SearchResult> Search(vw_SearchData searchData);
        List<vw_AddSessionSearchResult> AddSessionsSearch(vw_AddSessionsSearchData searchData);
        List<vw_AddSessionSearchResult> EditSessionSearch(vw_AddSessionsSearchData searchData);
    }
}
