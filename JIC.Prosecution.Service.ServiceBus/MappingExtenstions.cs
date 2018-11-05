using JIC.Base.Views.ProsecutionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Prosecution.Service.ServiceBus
{
    public static class MappingExtenstions
    {

        public static PublicProsecutionService.party[] toPPOParty(this party[] parties)
        {
            return AutoMapper.Mapper.Map<PublicProsecutionService.party[]>(parties);
        }

        public static PublicProsecutionService.parParties toPPoParParties(this parParties parParties)
        {
            return AutoMapper.Mapper.Map<PublicProsecutionService.parParties>(parParties);
        }
    }
}
