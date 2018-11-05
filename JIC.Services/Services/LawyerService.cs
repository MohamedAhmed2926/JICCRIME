using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Services.Services;
using JIC.Services.ServicesInterfaces;
using JIC.Base.views;
using JIC.Components.Components;
using JIC.Base.Views;
using JIC.Components.Components;
using System.Data.Entity.Validation;

namespace JIC.Services.Services
{
    public class LawyerService : ServiceBase, ILawyerService
    {
        public LawyerService(CaseType caseType) : base(caseType)
        {
        }
        public LawyerComponent LawyerComponent { get { return GetComponent<LawyerComponent>(); } }
        public PersonComponent PersonComponent { get { return GetComponent<PersonComponent>(); } }
        public LawyerStatus AddLawyer(vw_LawyerData lawyerData, out int LawyerID)
        {
            try
            {


                //LawyerID = 1;
                //return LawyerStatus.LawyerHasSession;
                if (lawyerData.NationalID != null)
                {
                    if (PersonComponent.IsNationalNoExist(lawyerData.NationalID, null))
                    {
                        List<vw_PersonData> vw_s = new List<vw_PersonData>();
                        vw_s = PersonComponent.GetPersons(lawyerData.NationalID);
                        vw_PersonData vps = new vw_PersonData();
                        vps = vw_s.FirstOrDefault();

                        lawyerData.PersonID = vps.ID;

                        //LawyerComponent.SearchBYPerson(lawyerData.PersonID)
                        return LawyerComponent.AddLawyer(lawyerData, out LawyerID);
                    }
                    else
                    {
                        long PerID;
                        vw_PersonData vw_PersonData = new vw_PersonData();
                        vw_Address vw_Address = new vw_Address();
                        vw_PersonData.Name = lawyerData.LawyerName;
                        vw_PersonData.CleanFullName = lawyerData.LawyerName;
                        vw_PersonData.BirthDate = lawyerData.DateOfBirth;
                        vw_PersonData.NatNo = lawyerData.NationalID;
                        vw_Address.CityID = 0;
                        vw_Address.PoliceStationID = 0;
                        vw_Address.address = lawyerData.Address;
                        vw_PersonData.address = vw_Address;
                        PersonComponent.AddPerson(vw_PersonData, out PerID);
                        lawyerData.PersonID = PerID;
                        return LawyerComponent.AddLawyer(lawyerData, out LawyerID);

                    }
                }
                else
                {
                    vw_Address vw_Address = new vw_Address();
                    vw_PersonData vw_PersonData = new vw_PersonData();
                    vw_PersonData.Name = lawyerData.LawyerName;
                    vw_PersonData.CleanFullName = lawyerData.LawyerName;
                    vw_PersonData.BirthDate = lawyerData.DateOfBirth;
                    vw_PersonData.NatNo = lawyerData.NationalID;
                    vw_Address.CityID = 0;
                    vw_Address.PoliceStationID = 0;
                    vw_Address.address = lawyerData.Address;
                    vw_PersonData.address = vw_Address;
                    vw_PersonData.NationalityID = 0;
                    long PerID;
                    PersonComponent.AddPerson(vw_PersonData, out PerID);
                    lawyerData.PersonID = PerID;
                    return LawyerComponent.AddLawyer(lawyerData, out LawyerID);

                }
            }
            catch (DbEntityValidationException e)
            {
                LawyerID = 0;
                HandleException(e);
                return LawyerStatus.Failed;
                //  throw newException;
            }

        }

        public vw_LawyerData GetLawyerByID(int? LawyerID)
        {
            vw_LawyerData L = new vw_LawyerData();
            //return L;
            L= LawyerComponent.GetLawyerByID(LawyerID);
            L.Address = L.Address.Remove(L.Address.LastIndexOf ('/'));
            L.Address = L.Address.Remove(L.Address.LastIndexOf('/'));
            return L;
        }

        public List<vw_LawyerData> GetLawyers()
        {
            //    List<vw_LawyerData> list1 = new List<vw_LawyerData>()
            //{


            //};

            //    return list1;
            List<vw_LawyerData> L = new List<vw_LawyerData>();
            L = LawyerComponent.GetLawyers();
            if (L != null)
            {
                for (int i = 0; i < L.Count; i++)
                {

                    L[i].Address = L[i].Address.Remove(L[i].Address.LastIndexOf('/'));
                    L[i].Address = L[i].Address.Remove(L[i].Address.LastIndexOf('/'));
                }
            }

            return L;
            //return LawyerComponent.GetLawyers();
        }

        public LawyerStatus EditLawyer(vw_LawyerData lawyerData)
        {
            //vw_LawyerData vw_LawyerData = new vw_LawyerData();

            //vw_LawyerData = LawyerComponent.GetLawyerByID(lawyerData.ID);
            vw_Address vw_Address = new vw_Address();

            vw_PersonData vw_PersonData = new vw_PersonData();
            vw_PersonData.ID = lawyerData.PersonID;
            vw_PersonData.Name = lawyerData.LawyerName;
            vw_PersonData.BirthDate = lawyerData.DateOfBirth;
            vw_PersonData.NatNo = lawyerData.NationalID;
            vw_Address.CityID = 0;
            vw_Address.PoliceStationID = 0;
            vw_Address.address = lawyerData.Address;
            vw_PersonData.address = vw_Address;

            LawyerStatus ls = LawyerComponent.EditLawyer(lawyerData);
            if (ls== LawyerStatus.Succeeded)
            {
                PersonComponent.EditPerson(vw_PersonData);
            }
            return ls;




        }
    }
}
