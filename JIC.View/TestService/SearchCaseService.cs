using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;
using JIC.Crime.View.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.TestService
{
    public class SearchCaseService : ISearchCasesService
    {
        public List<vw_AddSessionSearchResult> AddSessionsSearch(vw_AddSessionsSearchData searchData)
        {
            throw new NotImplementedException();
        }

        public List<vw_AddSessionSearchResult> EditSessionSearch(vw_AddSessionsSearchData searchData)
        {
            throw new NotImplementedException();
        }

        public List<SearchResult> Search (vw_SearchData SearchObj)
            {
            List<SearchResult> VList = new List<SearchResult>();

            VList.Add(new SearchResult { FirstLevelNumber   = "1", SecondLevelNumber  = "2", OverAllNumber  = "3", CrimeType="1",LastSessionDate=  new DateTime(2017, 3, 3), LastDecision   = "abc" });
            VList.Add(new SearchResult { FirstLevelNumber = "1", SecondLevelNumber = "4", OverAllNumber = "3", CrimeType = "2", LastSessionDate = new DateTime(2017, 3, 3), LastDecision  = "abc" });
           // VList.Add(new vw_SearchData { FirstNumber = 1, SecondNumber = 2, OverAllNumber = 3, CrimeType = 1, SessionDate = new DateTime(2017, 3, 3), JudgeType = 3 });
            return VList;
        }



    }
}