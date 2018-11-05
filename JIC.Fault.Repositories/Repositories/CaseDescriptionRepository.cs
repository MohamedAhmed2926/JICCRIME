using AutoMapper;
using JIC.Base.Interfaces;
using JIC.Base.Views;
using JIC.Fault.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Fault.Repositories.Repositories
{
    public class CaseDescriptionRepository : EntityRepositoryBase<Cases_CaseDescription, vw_CaseDescription,long>, ICaseDescriptionRepository
    {
        public override void OnEntityCreate(Cases_CaseDescription entity)
        {
            entity.FromDate = DateTime.Now;
            base.OnEntityCreate(entity);
        }

        public vw_CaseDescription GetCaseDescriptionByCaseID(int CaseID)
        {
            return GetAllQuery().Where(caseDescript => caseDescript.CaseID == CaseID)
                .OrderByDescending(caseDesc => caseDesc.FromDate)
                .FirstOrDefault()
                .MapTo<vw_CaseDescription>();                
        }
    }
}
