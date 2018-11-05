using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Linq;
using System.Collections.Generic;

namespace JIC.Crime.Repositories.Repositories
{
    public class VacationsRepository : EntityRepositoryBase<CourtConfigurations_Vacations>, IVacationsRepository
    {
        public SaveStatus AddVacation(vw_VacationData VacationsObj, out int vacationID)
        {
            try
            {
                var vacation = (from vacations in DataContext.CourtConfigurations_Vacations
                                where vacations.Name == VacationsObj.VacationName
                                && vacations.FromDate.Year == VacationsObj.VacationFrom.Year
                                || vacations.FromDate == VacationsObj.VacationFrom
                                || vacations.EndDate == VacationsObj.VacationTo
                                select vacations.ID).FirstOrDefault();

                if (vacation != 0)
                {
                    vacationID = vacation;
                    return SaveStatus.Saved_Before;
                }
                else
                {
                    CourtConfigurations_Vacations VacationObjNew = new CourtConfigurations_Vacations();

                    VacationObjNew.FromDate = VacationsObj.VacationFrom;
                    VacationObjNew.EndDate = VacationsObj.VacationTo;
                    VacationObjNew.Name = VacationsObj.VacationName;

                    this.Add(VacationObjNew);
                    this.Save();
                    vacationID = VacationObjNew.ID;
                    return SaveStatus.Saved;
                }
            }
            catch (Exception)
            {
                vacationID = 0;
                return SaveStatus.Failed_To_Save;
            }

        }

        public SaveStatus EditVacation(vw_VacationData VacationsObj)
        {
            try
            {
                var vacation = (from vacations in DataContext.CourtConfigurations_Vacations
                                where vacations.Name == VacationsObj.VacationName
                                select vacations.ID).FirstOrDefault();
                // if (!string.IsNullOrEmpty(vacation.ToString()))
                if (vacation != 0)
                {
                    return SaveStatus.Saved_Before;
                }
                else
                {
                    var VacationView = this.GetByID(VacationsObj.ID);

                    VacationView.ID = (int)VacationsObj.ID;
                    VacationView.FromDate = VacationsObj.VacationFrom;
                    VacationView.EndDate = VacationsObj.VacationTo;
                    VacationView.Name = VacationsObj.VacationName;

                    this.Update(VacationView);
                    this.Save();
                    return SaveStatus.Saved;
                }
            }
            catch (Exception)
            {
                return SaveStatus.Failed_To_Save;
            }

        }

        public DeleteStatus DeleteVacation(int VacationsID)
        {
            var VacationView = this.GetByID(VacationsID);

            this.Delete(VacationView);
            this.Save();
            return DeleteStatus.Deleted;
        }

        public List<vw_VacationData> GetVacations()
        {
            return (from Vacations in DataContext.CourtConfigurations_Vacations
                    select new vw_VacationData
                    {
                        ID = Vacations.ID,
                        VacationName = Vacations.Name,
                        VacationFrom = Vacations.FromDate,
                        VacationTo = Vacations.EndDate,

                    }).ToList();
        }
    }
}
