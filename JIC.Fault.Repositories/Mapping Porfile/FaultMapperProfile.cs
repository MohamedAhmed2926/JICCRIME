using AutoMapper;
using JIC.Base.Views;
using JIC.Fault.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Fault.Repositories.Mapping_Porfile
{
    public class FaultMapperProfile : Profile
    {
        public FaultMapperProfile()
        {
            //Map only CaseDescription defind in view  
            CreateMap<vw_CaseDescription, Cases_CaseDescription>(MemberList.Source);
            CreateMap<Cases_CaseDescription,vw_CaseDescription>(MemberList.Destination);

            CreateMap<vw_FaultCaseBasicData, Cases_Cases>(MemberList.Source);
            CreateMap<Cases_Cases, vw_FaultCaseBasicData>(MemberList.Destination)
                .ForMember(dest=>dest.CaseID,resolver=>resolver.MapFrom(source=>source.ID));

            CreateMap<vw_DefendantsDecisionData, Cases_DefendantsDecision>(MemberList.Source);
            CreateMap<Cases_DefendantsDecision, vw_DefendantsDecisionData>(MemberList.Destination);

            CreateMap<vw_CaseProsecution, Service_CaseProsecution>(MemberList.Source);
            CreateMap<Service_CaseProsecution, vw_CaseProsecution>(MemberList.Destination);


            CreateMap<vw_CasePartyProsecution, Service_CaseDefendantProsecution>(MemberList.Source)
                .ForMember(dest => dest.CaseDefendantID, resolver => resolver.MapFrom(source => source.CasePartyID));
            CreateMap<Service_CaseDefendantProsecution, vw_CasePartyProsecution>(MemberList.Destination)
                .ForMember(dest => dest.CasePartyID, resolver => resolver.MapFrom(source => source.CaseDefendantID));

            CreateMap<vw_CasePartyProsecution, Service_CaseVictimProsecution>(MemberList.Source)
                .ForMember(dest => dest.CaseVictimID, resolver => resolver.MapFrom(source => source.CasePartyID));
            CreateMap<Service_CaseVictimProsecution, vw_CasePartyProsecution>(MemberList.Destination)
                .ForMember(dest => dest.CasePartyID, resolver => resolver.MapFrom(source => source.CaseVictimID));

        }
    }
}
