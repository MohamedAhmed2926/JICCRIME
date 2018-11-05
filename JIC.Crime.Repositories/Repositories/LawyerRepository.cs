using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.views;
using JIC.Base.Views;
using System;
using JIC.Crime.Entities.Models;
using JIC.Repositories.DBInteractions;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;


namespace JIC.Crime.Repositories.Repositories
{
   public class LawyerRepository : EntityRepositoryBase<Case_Lawyer> , ILawyerRepository
    {
        public LawyerStatus AddLawyer(vw_LawyerData lawyerData, out int LawyerID)
        {
            
                if (checkCN(lawyerData))
                {
                    LawyerID = lawyerData.ID;
                    return LawyerStatus.CardNumber_Exist_Before;
                }
            else if (checkPersonid(lawyerData))
            {
                LawyerID = lawyerData.ID;
                return LawyerStatus.NationalNO_Exist_Before;
            }
            else
                {
                Case_Lawyer LawyerObj = new Case_Lawyer();
                
                    LawyerObj.PersonID = lawyerData.PersonID;
                    LawyerObj.CardNumber = lawyerData.LawyerCardNumber;
                    LawyerObj.LevelID = lawyerData.LawyerLevelID;
                LawyerObj.LawyerFileData = lawyerData.LawyerFileData;
                LawyerObj.ID = 1;
                    this.Add(LawyerObj);
                    this.Save();
                    LawyerID = LawyerObj.ID;
                    return LawyerStatus.Succeeded;
                }
            
           
          
            


        }

        public LawyerStatus EditLawyer(vw_LawyerData Lawyer)
        {
          

            if (checkCNandid(Lawyer))
            {
                return LawyerStatus.CardNumber_Exist_Before;
            }
            else if (checkPersonid(Lawyer))
            {
               
                return LawyerStatus.NationalNO_Exist_Before;
            }
            else
            {
                var caseLawyers = this.GetByID(Lawyer.ID);
                
                caseLawyers.LevelID = Lawyer.LawyerLevelID;
                caseLawyers.CardNumber = Lawyer.LawyerCardNumber;
                caseLawyers.LawyerFileData = Lawyer.LawyerFileData;
                this.Update(caseLawyers);
                this.Save();
                return LawyerStatus.Succeeded;
            }
        }

        public vw_LawyerData GetLawyerByID(int? LawyerID)
        {
            
            return (from Case_Lawyer in DataContext.Case_Lawyer
                    join Lookup in DataContext.Configurations_Lookups on Case_Lawyer.LevelID equals Lookup.ID
                    join Configurations_Persons in DataContext.Configurations_Persons on Case_Lawyer.PersonID equals Configurations_Persons.ID
                    where Case_Lawyer.ID == LawyerID
                    select new vw_LawyerData
                    {

                        ID = Case_Lawyer.ID,
                        LawyerLevelID = Lookup.ID,
                        LawyerCardNumber = Case_Lawyer.CardNumber,
                        LawyerName = Configurations_Persons.FullName,
                        Address = Configurations_Persons.Address,
                        DateOfBirth =Configurations_Persons.Birthdate.Value,
                        NationalID =Configurations_Persons.NationalID,
                        LawyerLevelName = Lookup.Name,
                        PersonID =Case_Lawyer.PersonID,
                        LawyerFileData = Case_Lawyer.LawyerFileData,






                    }).FirstOrDefault();
            

        }

        public List<vw_LawyerData> GetLawyers()
        {
            return (from Case_Lawyer in DataContext.Case_Lawyer
                    join Lookup in DataContext.Configurations_Lookups on Case_Lawyer.LevelID equals Lookup.ID
                    join Configurations_Persons in DataContext.Configurations_Persons on Case_Lawyer.PersonID equals Configurations_Persons.ID
                    select new vw_LawyerData
                    {
                        ID = Case_Lawyer.ID,
                        LawyerLevelID = Lookup.ID,
                        LawyerCardNumber = Case_Lawyer.CardNumber,
                        LawyerName = Configurations_Persons.FullName,
                        Address = Configurations_Persons.Address,
                        DateOfBirth = Configurations_Persons.Birthdate.Value,
                        NationalID = Configurations_Persons.NationalID,
                        LawyerLevelName = Lookup.Name,
                    }).ToList();
        }
        public bool checkCN(vw_LawyerData Lawyer)
    {
       return     GetAll().Where(z => z.CardNumber == Lawyer.LawyerCardNumber).Count() > 0;
            //var CardNumberIDExistBefore = 0;
            //if (!string.IsNullOrEmpty(Lawyer.LawyerCardNumber))
            //{
            //    CardNumberIDExistBefore = (from CaseLawyers in DataContext.CaseLawyers
            //                               where CaseLawyers.CardNumber == Lawyer.LawyerCardNumber
            //                               select CaseLawyers.ID).FirstOrDefault();
            //}

            //if (CardNumberIDExistBefore != 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}


        }

        public bool checkCNandid(vw_LawyerData Lawyer)
        {
            return GetAll().Where(m => m.CardNumber == Lawyer.LawyerCardNumber && m.ID != Lawyer.ID).Count() > 0;
        }

        public bool checkPersonid(vw_LawyerData Lawyer)
        {
            return GetAll().Where(m => m.PersonID == Lawyer.PersonID ).Count() > 0;
        }
    }

  
}
