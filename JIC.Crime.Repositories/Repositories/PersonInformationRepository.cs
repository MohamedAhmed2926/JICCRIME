using JIC.Base.Interfaces;
using JIC.Base.Views;
using JIC.Crime.Entities.Models;
using JIC.Crime.Repositories.DBInteractions;
using JIC.Repositories.DBInteractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JIC.Crime.Repositories.Repositories
{

    public class PersonInformationRepository : RepositoryBase, IPersonInformationRepository
    {
        

        public vw_InformationPerson GetInformationPerson(string NatNo, string Name)
        {


            var result = (from p in this.DataContext.Configurations_Persons
                                           join lookups in DataContext.Configurations_Lookups on p.NationalityID equals lookups.ID
                                          // join u in DataContext.Users on p.ID equals u.PersonsId
                                           //join lu in DataContext.Configurations_Lookups on u.UserTypeID equals lu.ID 
                                           // join defend in DataContext.Cases_CaseDefendants  on p.ID  equals defend.PersonID
                                           // join victim in DataContext.Cases_CaseVictims  on p.ID equals victim.PersonID
                                           //join cases1 in DataContext.Cases_Cases  on defend.CaseID  equals cases1.ID 
                                           //join cases2 in DataContext.Cases_Cases on victim.CaseID equals cases2.ID
                                           where ((p.NationalID == NatNo && p.FullName == Name))
                                           select new vw_InformationPerson
                                           {
                                              // UserTypes  = lu.Name ,
                                               BirthDate = p.Birthdate.Value.ToString(),
                                               Job = p.JobTitle,
                                               Name = p.FullName,
                                               // CleanFullName = p.CleanFullName,
                                               Nationalities = lookups.Name ,
                                               NatNo = p.NationalID,
                                               PassportNo = p.PassportNumber,
                                               address = p.Address 
                                           }
                                               ).FirstOrDefault();



            if (result != null)
            {
                //List<string> Add= result.address.Split()
                      string[] li = Regex.Split(result.address.ToString(), "/");
                if (li[0] != null)
                    result.address  = li[0];
                if (li[1] != null)
                { int City = int.Parse(li[1]);
                    result.Cities = (from l in DataContext.Configurations_Lookups 
                                              where l.ID == City
                                              select l.Name).FirstOrDefault();
                }
                if (li[2] != null)
                { int PS = int.Parse(li[2]);
                    result.PoliceStations = (from ps in DataContext.Configurations_PoliceStations
                                              where ps.ID == PS
                                              select ps.Name).FirstOrDefault();
                }
                var userresult  = (from p in this.DataContext.Configurations_Persons
                              join u in DataContext.Users on p.ID equals u.PersonsId
                              join lu in DataContext.Security_UserTypes  on u.UserTypeID equals lu.ID
                              where ((p.NationalID == NatNo && p.FullName == Name))
                              select new
                              {
                    usertypename = lu.Name,
                    userphone = u.MobileNo 
                } ).FirstOrDefault();


                if (userresult != null)
                {
                    result.UserTypes = userresult.usertypename;
                    result.PhoneNo = userresult.userphone;
                }




                var resultD = (from p in this.DataContext.Configurations_Persons
                               join defend in DataContext.Cases_CaseDefendants on p.ID equals defend.PersonID

                               join cases1 in DataContext.Cases_Cases on defend.CaseID equals cases1.ID
                               join Mc in DataContext.Cases_MasterCase on cases1.MasterCaseID equals Mc.ID

                               where ((p.NationalID == NatNo && p.FullName == Name))
                               select new vw_cases
                               {
                                   Status = "متهم",
                                   CaseName = Mc.Title,
                                   OverallNumber = Mc.Configurations_OverallNumbers.InclosiveSierial
                                   + "/" + Mc.Configurations_OverallNumbers.Year
                                   + "/" + Mc.Configurations_OverallNumbers.Configurations_Prosecutions.Name
                                 



                               }).ToList();



                var resultV = (from p in this.DataContext.Configurations_Persons

                               join victim in DataContext.Cases_CaseVictims on p.ID equals victim.PersonID

                               join cases2 in DataContext.Cases_Cases on victim.CaseID equals cases2.ID
                               join Mc in DataContext.Cases_MasterCase on cases2.MasterCaseID equals Mc.ID
                               where ((p.NationalID == NatNo && p.FullName == Name))
                               select new vw_cases
                               {
                                   Status = "متهم",
                                   CaseName = Mc.Title,
                                   OverallNumber = Mc.Configurations_OverallNumbers.InclosiveSierial
                                   + "/" + Mc.Configurations_OverallNumbers.Configurations_Prosecutions.Name
                                   + "/" + Mc.Configurations_OverallNumbers.Year


                               }).ToList();


                result.CasesList.AddRange(resultD);
                result.CasesList.AddRange(resultV);
            }
            return result;
        }

        
        //vw_InformationPerson IRepositoryBase<vw_InformationPerson, long>.GetByID(long ID)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
