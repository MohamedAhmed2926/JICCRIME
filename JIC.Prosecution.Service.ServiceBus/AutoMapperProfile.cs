using AutoMapper;
using JIC.Base.Views.ProsecutionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Prosecution.Service.ServiceBus
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<parParties, PublicProsecutionService.parParties>()

                .ForMember(dest => dest.businessCaseDetailsId,resolver => resolver.MapFrom(source => source.businessCaseDetailsId.GetValueOrDefault()))
                .ForMember(dest => dest.businessCaseDetailsIdNull, resolver => resolver.MapFrom(source => !source.businessCaseDetailsId.HasValue))
                
                .ForMember(dest => dest.businessCaseId, resolver => resolver.MapFrom(source => source.businessCaseId.GetValueOrDefault()))
                .ForMember(dest => dest.businessCaseIdNull, resolver => resolver.MapFrom(source => !source.businessCaseId.HasValue))

                .ForMember(dest => dest.city, resolver => resolver.MapFrom(source => source.city.GetValueOrDefault()))
                .ForMember(dest => dest.cityNull, resolver => resolver.MapFrom(source => !source.city.HasValue))

                .ForMember(dest => dest.createdByBusinessCaseId, resolver => resolver.MapFrom(source => source.createdByBusinessCaseId.GetValueOrDefault()))
                .ForMember(dest => dest.createdByBusinessCaseIdNull, resolver => resolver.MapFrom(source => !source.createdByBusinessCaseId.HasValue))

                .ForMember(dest => dest.createdDate, resolver => resolver.MapFrom(source => source.createdDate.GetValueOrDefault()))
                .ForMember(dest => dest.createdDateSpecified, resolver => resolver.MapFrom(source => source.createdDate.HasValue))

                .ForMember(dest => dest.dateOfBirth, resolver => resolver.MapFrom(source => source.dateOfBirth.GetValueOrDefault()))
                .ForMember(dest => dest.dateOfBirthSpecified, resolver => resolver.MapFrom(source => source.dateOfBirth.HasValue))

                .ForMember(dest => dest.educationalEntityId, resolver => resolver.MapFrom(source => source.educationalEntityId.GetValueOrDefault()))
                .ForMember(dest => dest.educationalEntityIdNull, resolver => resolver.MapFrom(source => !source.educationalEntityId.HasValue))

                .ForMember(dest => dest.educationalLevelId, resolver => resolver.MapFrom(source => source.educationalLevelId.GetValueOrDefault()))
                .ForMember(dest => dest.educationalLevelIdNull, resolver => resolver.MapFrom(source => !source.educationalLevelId.HasValue))

                .ForMember(dest => dest.govermentDegreeId, resolver => resolver.MapFrom(source => source.govermentDegreeId.GetValueOrDefault()))
                .ForMember(dest => dest.govermentDegreeIdNull, resolver => resolver.MapFrom(source => !source.govermentDegreeId.HasValue))

                .ForMember(dest => dest.govermentJoiningDate, resolver => resolver.MapFrom(source => source.govermentJoiningDate.GetValueOrDefault()))
                .ForMember(dest => dest.govermentJoiningDateSpecified, resolver => resolver.MapFrom(source => source.govermentJoiningDate.HasValue))

                .ForMember(dest => dest.guardianNationalityId, resolver => resolver.MapFrom(source => source.guardianNationalityId.GetValueOrDefault()))
                .ForMember(dest => dest.guardianNationalityIdNull, resolver => resolver.MapFrom(source => !source.guardianNationalityId.HasValue))

                .ForMember(dest => dest.idType, resolver => resolver.MapFrom(source => source.idType.GetValueOrDefault()))
                .ForMember(dest => dest.idTypeNull, resolver => resolver.MapFrom(source => !source.idType.HasValue))

                .ForMember(dest => dest.issueDate, resolver => resolver.MapFrom(source => source.issueDate.GetValueOrDefault()))
                .ForMember(dest => dest.issueDateSpecified, resolver => resolver.MapFrom(source => source.issueDate.HasValue))

                .ForMember(dest => dest.militaryRankId, resolver => resolver.MapFrom(source => source.militaryRankId.GetValueOrDefault()))
                .ForMember(dest => dest.militaryRankIdNull, resolver => resolver.MapFrom(source => !source.militaryRankId.HasValue))

                .ForMember(dest => dest.militaryServiceCorpsId, resolver => resolver.MapFrom(source => source.militaryServiceCorpsId.GetValueOrDefault()))
                .ForMember(dest => dest.militaryServiceCorpsIdNull, resolver => resolver.MapFrom(source => !source.militaryServiceCorpsId.HasValue))

                .ForMember(dest => dest.moiPrisonId, resolver => resolver.MapFrom(source => source.moiPrisonId.GetValueOrDefault()))
                .ForMember(dest => dest.moiPrisonIdNull, resolver => resolver.MapFrom(source => !source.moiPrisonId.HasValue))

                .ForMember(dest => dest.nearlyGuardianId, resolver => resolver.MapFrom(source => source.nearlyGuardianId.GetValueOrDefault()))
                .ForMember(dest => dest.nearlyGuardianIdNull, resolver => resolver.MapFrom(source => !source.nearlyGuardianId.HasValue))

                .ForMember(dest => dest.parId, resolver => resolver.MapFrom(source => source.parId.GetValueOrDefault()))
                .ForMember(dest => dest.parIdNull, resolver => resolver.MapFrom(source => !source.parId.HasValue))

                .ForMember(dest => dest.partyStatus, resolver => resolver.MapFrom(source => source.partyStatus.GetValueOrDefault()))
                .ForMember(dest => dest.partyStatusNull, resolver => resolver.MapFrom(source => !source.partyStatus.HasValue))

                .ForMember(dest => dest.requestId, resolver => resolver.MapFrom(source => source.requestId.GetValueOrDefault()))
                .ForMember(dest => dest.requestIdNull, resolver => resolver.MapFrom(source => !source.requestId.HasValue))

                .ForMember(dest => dest.updatedDate, resolver => resolver.MapFrom(source => source.updatedDate.GetValueOrDefault()))
                .ForMember(dest => dest.updatedDateSpecified, resolver => resolver.MapFrom(source => !source.updatedDate.HasValue));
        }
    }
}