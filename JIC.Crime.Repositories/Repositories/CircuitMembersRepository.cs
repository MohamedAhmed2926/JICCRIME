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
    public class CircuitMembersRepository : EntityRepositoryBase<CourtConfigurations_CircuitMembers>, ICircuitMembersRepository
    {


        public bool IsCircuitMember(int userID)
        {
            return DataContext.CourtConfigurations_CircuitMembers.Where(user => user.UserID == userID).Count() > 0;
        }

        public bool IsPersonIsCircuitMember(int PersonID, int CaseID)
        {
          return  (from cases in DataContext.Cases_Cases
            join Circ in DataContext.CourtConfigurations_Circuits on cases.CircuitID equals Circ.ID
            join Member in DataContext.CourtConfigurations_CircuitMembers on Circ.ID equals Member.CircuitID
            join user in DataContext.Users on Member.UserID equals user.Id
            join person in DataContext.Configurations_Persons on user.PersonsId equals PersonID
            where (cases.ID == CaseID && Member.ToDate == null)
            select Member.ID).Count()>0;

            }

        public SaveCircuitStatus AddCircuitJudges(List<vw_CircuitsJudges> JudgesList, int CircuitID, DateTime CircuitStartDate)
        {
            foreach (var CircuitJudge in JudgesList)
            {
                Add(new CourtConfigurations_CircuitMembers
                {
                    CircuitID = CircuitID,
                    UserID = CircuitJudge.JudgeID,
                    FromDate = CircuitStartDate,
                    JudgeType = (int)CircuitJudge.JudgePodiumType

                });

            }
            Save();
            return SaveCircuitStatus.Saved_Successfully;



        }

        public SaveCircuitStatus EditCircuitJudges(List<vw_CircuitsJudges> JudgesList, int CircuitID, DateTime CircuitStartDate)
        {
          
            
            var CircuitMembers = (from circuitMember in DataContext.CourtConfigurations_CircuitMembers where circuitMember.CircuitID == CircuitID & (circuitMember.ToDate == null || circuitMember.ToDate > DateTime.Today) && circuitMember.Security_Users.UserTypeID == (int)SystemUserTypes.Judge select circuitMember).ToList();

          
                foreach (var member in CircuitMembers)
                {
                    var circuitJudge = JudgesList.Where(judge => (int)judge.JudgePodiumType == member.JudgeType).First();
                    if (circuitJudge.JudgeID != member.UserID)
                    {
                        Add(new CourtConfigurations_CircuitMembers()
                        {
                            JudgeType = (int)circuitJudge.JudgePodiumType,
                            FromDate = DateTime.Today,
                            CircuitID = CircuitID,
                            UserID = circuitJudge.JudgeID,
                        });
                        member.ToDate = DateTime.Today;
                        Update(member);
                        Save();

                    }
                 

                }
            CircuitMembers = (from circuitMember in DataContext.CourtConfigurations_CircuitMembers where circuitMember.CircuitID == CircuitID & (circuitMember.ToDate == null || circuitMember.ToDate > DateTime.Today) && circuitMember.Security_Users.UserTypeID == (int)SystemUserTypes.Judge select circuitMember).ToList();
            bool Found=false;
            if (CircuitMembers.Count != JudgesList.Count)
            {

                if (JudgesList.Count > CircuitMembers.Count)
                {
                    foreach (var j in JudgesList)
                    {
                        foreach (var k in CircuitMembers)
                        {
                            if (j.JudgeID == k.UserID)
                            {
                                Found = true;
                                break;
                            }
                        }
                        if (Found == false)
                        {
                            Add(new CourtConfigurations_CircuitMembers()
                            {
                                JudgeType = j.JudgePodiumType,
                                FromDate = DateTime.Today,
                                CircuitID = CircuitID,
                                UserID = j.JudgeID,
                            });

                            Save();
                        }
                        Found = false;
                    }
                }
                else if (JudgesList.Count < CircuitMembers.Count)
                {
                 
                    foreach (var j in CircuitMembers)
                    {
                        foreach (var k in JudgesList)
                        {
                            if (j.UserID  == k.JudgeID )
                            {
                                Found = true;
                                break;
                            }
                        }
                        if (Found == false)
                        {

                            j.ToDate = DateTime.Today;
                            Update(j);
                            Save();

                        }
                        Found = false;
                    }
                }

                //IEnumerable<vw_CircuitsJudges> DiffernceList;
              
                //List<vw_CircuitsJudges> CircuitMemebersList = new List<vw_CircuitsJudges>();
                //foreach (var member in CircuitMembers)
                //{
                //    CircuitMemebersList.Add(new vw_CircuitsJudges {
                //        JudgeID = member.UserID ,
                //        JudgePodiumType =(int)member.JudgeType 
                //    });
                //}

                //// check if there is judges saved in the database that are removed from the retrieved list
                //    DiffernceList = CircuitMemebersList.Except(JudgesList);
                //if (DiffernceList.Count() != CircuitMemebersList.Count())
                //{
                //     CourtConfigurations_CircuitMembers obj;
                //    foreach (var k in DiffernceList)
                //    {
                //        obj = GetAll().Where(z => z.UserID == k.UserID && z.CircuitID == CircuitID && z.JudgeType == k.JudgePodiumType).FirstOrDefault();
                //        obj.ToDate = DateTime.Today;
                //        Update(obj);
                //        Save();
                //    }
                //}
                

                //// check iff there is judges added to the judges list not saved in the database
                //DiffernceList = JudgesList.Except(CircuitMemebersList);
                //if (DiffernceList.Count() != JudgesList.Count())
                //{
                //    foreach (var k in DiffernceList)
                //    {
                //        Add(new CourtConfigurations_CircuitMembers()
                //        {
                //            JudgeType = k.JudgePodiumType,
                //            FromDate = DateTime.Today,
                //            CircuitID = CircuitID,
                //            UserID = k.JudgeID,
                //        });
                       
                //        Save();
                //    }

                //}
            }
                return SaveCircuitStatus.Saved_Successfully;
            

        }

        public DeleteStatus DeleteMembersByCircuitID(int CircuitID)
        {
            var CircuitMembers = (from circuitMember in DataContext.CourtConfigurations_CircuitMembers where circuitMember.CircuitID == CircuitID select circuitMember);
            DataContext.CourtConfigurations_CircuitMembers.RemoveRange(CircuitMembers);
            return DeleteStatus.Deleted;
        }

        public List<vw_CircuitsJudges> GetCircuitMembersByCircuitID(int CircuitID)
        {
            return (from Member in DataContext.CourtConfigurations_CircuitMembers
                    join user in DataContext.Users on Member.UserID equals user.Id
                    join person in DataContext.Configurations_Persons on user.PersonsId equals person.ID
                    where (Member.CircuitID == CircuitID && Member.ToDate ==null)
                    select new vw_CircuitsJudges
                    {
                        JudgeID = Member.ID,
                        JudgeName=person.FullName ,
                        JudgePodiumType = (int)Member.JudgeType,
                        UserID=Member.UserID 
                    }).ToList();

        }
    }
}
