using JIC.Base.Interfaces;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;

namespace JIC.Crime.Repositories.Repositories
{
    public class MasterCaseRepository : EntityRepositoryBase<Cases_MasterCase>, IOrderOfAssignmentRepository
    {
        public AddOrder AddOrder(vw_OrderOfAssignment OrderOfAssignment)
        {
            try
            {
                int masterID = (from master in DataContext.Cases_MasterCase
                                join cases in DataContext.Cases_Cases on master.ID equals cases.MasterCaseID
                                where cases.ID == OrderOfAssignment.CaseID && cases.IsDeleted != true
                                select master.ID).FirstOrDefault();

                var masterElement = this.GetByID(masterID);
                masterElement.OrderOfAssignment = OrderOfAssignment.Description;

                this.Update(masterElement);
                this.Save();
                return Base.AddOrder.AddSuccefull;
            }
            catch (Exception)
            {
                return Base.AddOrder.FailedToAdd;
            }
        }

        public EditAssignment EditOrder(vw_OrderOfAssignment OrderOfAssignment)
        {
            try
            {
                int masterID = (from master in DataContext.Cases_MasterCase
                                join cases in DataContext.Cases_Cases on master.ID equals cases.MasterCaseID
                                where cases.ID == OrderOfAssignment.CaseID && cases.IsDeleted != true
                                select master.ID).FirstOrDefault();
                bool HasSession = (from sessions in DataContext.Cases_CaseSessions
                                   where sessions.CaseID == OrderOfAssignment.CaseID
                                   select sessions.ID).Any();
                if (HasSession)
                {
                    return EditAssignment.NoUpdateThereIsASession;
                }
                else
                {
                    var masterElement = this.GetByID(masterID);
                    masterElement.OrderOfAssignment = OrderOfAssignment.Description;

                    this.Update(masterElement);
                    this.Save();
                    return EditAssignment.EditSuccefull;

                }
            }
            catch (Exception)
            {
                return EditAssignment.FailedToEdit;
            }
        }

        public vw_OrderOfAssignment GetOrderByID(int CaseID)
        {
            try
            {
                return new vw_OrderOfAssignment
                {
                    CaseID = CaseID,
                    Description = (from master in DataContext.Cases_MasterCase
                                   join cases in DataContext.Cases_Cases on master.ID equals cases.MasterCaseID
                                   where cases.ID == CaseID && cases.IsDeleted != true
                                   select master.OrderOfAssignment).FirstOrDefault()
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
