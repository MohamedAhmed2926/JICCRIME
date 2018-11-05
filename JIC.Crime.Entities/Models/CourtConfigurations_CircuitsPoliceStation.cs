namespace JIC.Crime.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CourtConfigurations_CircuitsPoliceStation
    {
        public int ID { get; set; }

        public int CircuitID { get; set; }

        public int PoliceStationID { get; set; }

        public virtual Configurations_PoliceStations Configurations_PoliceStations { get; set; }

        public virtual CourtConfigurations_Circuits CourtConfigurations_Circuits { get; set; }
    }
}
