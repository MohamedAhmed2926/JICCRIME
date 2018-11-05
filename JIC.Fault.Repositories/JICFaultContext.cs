namespace JIC.Fault.Repositories
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using JIC.Fault.Entities.Models;
    using JIC.Base;

    public partial class JICFaultContext : DbContext
    {
        public JICFaultContext()
            : base(SystemConfigurations.Settings_ConnectionString)
        {
        }

        public virtual DbSet<Cases_CaseDefendants> Cases_CaseDefendants { get; set; }
        public virtual DbSet<Cases_CaseDescription> Cases_CaseDescription { get; set; }
        public virtual DbSet<Cases_CaseDocumentFolders> Cases_CaseDocumentFolders { get; set; }
        public virtual DbSet<Cases_CaseDocuments> Cases_CaseDocuments { get; set; }
        public virtual DbSet<Cases_Cases> Cases_Cases { get; set; }
        public virtual DbSet<Cases_CaseSessions> Cases_CaseSessions { get; set; }
        public virtual DbSet<Cases_CaseTransfer> Cases_CaseTransfer { get; set; }
        public virtual DbSet<Cases_CaseVictims> Cases_CaseVictims { get; set; }
        public virtual DbSet<Cases_DefendantsCharges> Cases_DefendantsCharges { get; set; }
        public virtual DbSet<Cases_DefendantsDecision> Cases_DefendantsDecision { get; set; }
        public virtual DbSet<Cases_DefendatnsCaseLog> Cases_DefendatnsCaseLog { get; set; }
        public virtual DbSet<Cases_DefendatnsSessionsLog> Cases_DefendatnsSessionsLog { get; set; }
        public virtual DbSet<Cases_DocumentsLog> Cases_DocumentsLog { get; set; }
        public virtual DbSet<Cases_MasterCase> Cases_MasterCase { get; set; }
        public virtual DbSet<Cases_Resumption_Detail> Cases_Resumption_Detail { get; set; }
        public virtual DbSet<Cases_SessionDecision> Cases_SessionDecision { get; set; }
        public virtual DbSet<Cases_VictimsCaseLog> Cases_VictimsCaseLog { get; set; }
        public virtual DbSet<Cases_VictimsSessionsLog> Cases_VictimsSessionsLog { get; set; }
        public virtual DbSet<Configurations_CaseTypes> Configurations_CaseTypes { get; set; }
        public virtual DbSet<Configurations_Courts> Configurations_Courts { get; set; }
        public virtual DbSet<Configurations_DecisionTypes> Configurations_DecisionTypes { get; set; }
        public virtual DbSet<Configurations_LookupCategories> Configurations_LookupCategories { get; set; }
        public virtual DbSet<Configurations_Lookups> Configurations_Lookups { get; set; }
        public virtual DbSet<Configurations_OrganizationDetails> Configurations_OrganizationDetails { get; set; }
        public virtual DbSet<Configurations_OrganizationRepresentatives> Configurations_OrganizationRepresentatives { get; set; }
        public virtual DbSet<Configurations_Persons> Configurations_Persons { get; set; }
        public virtual DbSet<Configurations_PoliceStations> Configurations_PoliceStations { get; set; }
        public virtual DbSet<Configurations_Prosecuters> Configurations_Prosecuters { get; set; }
        public virtual DbSet<Configurations_Prosecutions> Configurations_Prosecutions { get; set; }
        public virtual DbSet<CounrtConfigurations_Vacations> CounrtConfigurations_Vacations { get; set; }
        public virtual DbSet<CourtConfigurations_CasesDistributionRules> CourtConfigurations_CasesDistributionRules { get; set; }
        public virtual DbSet<CourtConfigurations_CircuitDaysSetup> CourtConfigurations_CircuitDaysSetup { get; set; }
        public virtual DbSet<CourtConfigurations_CircuitMembers> CourtConfigurations_CircuitMembers { get; set; }
        public virtual DbSet<CourtConfigurations_CircuitRolls> CourtConfigurations_CircuitRolls { get; set; }
        public virtual DbSet<CourtConfigurations_Circuits> CourtConfigurations_Circuits { get; set; }
        public virtual DbSet<CourtConfigurations_DistributionDates> CourtConfigurations_DistributionDates { get; set; }
        public virtual DbSet<CourtConfigurations_DistributionNumbers> CourtConfigurations_DistributionNumbers { get; set; }
        public virtual DbSet<CourtConfigurations_TextPredections> CourtConfigurations_TextPredections { get; set; }
        public virtual DbSet<Security_Actions> Security_Actions { get; set; }
        public virtual DbSet<Security_Modules> Security_Modules { get; set; }
        public virtual DbSet<Security_Pages> Security_Pages { get; set; }
        public virtual DbSet<Security_Users> Security_Users { get; set; }
        public virtual DbSet<Security_UsersLoginFailure> Security_UsersLoginFailure { get; set; }
        public virtual DbSet<Security_UserTypeActions> Security_UserTypeActions { get; set; }
        public virtual DbSet<Security_UserTypes> Security_UserTypes { get; set; }
        public virtual DbSet<Z_DefendantsSessionLawyers> Z_DefendantsSessionLawyers { get; set; }
        public virtual DbSet<Z_LawyerAttorneys> Z_LawyerAttorneys { get; set; }
        public virtual DbSet<Z_Lawyers> Z_Lawyers { get; set; }
        public virtual DbSet<Z_SessionWitnesses> Z_SessionWitnesses { get; set; }
        public virtual DbSet<CourtConfigurations_WorkDays> CourtConfigurations_WorkDays { get; set; }

        public virtual DbSet<Service_CaseVictimProsecution> Service_CaseVictimProsecution { get; set; }
        public virtual DbSet<Service_CaseProsecution> Service_CaseProsecution { get; set; }
        public virtual DbSet<Service_CaseDefendantProsecution> Service_CaseDefendantProsecution { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cases_CaseDefendants>()
                .HasMany(e => e.Cases_DefendantsDecision)
                .WithRequired(e => e.Cases_CaseDefendants)
                .HasForeignKey(e => e.CaseDefendantId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_CaseDefendants>()
                .HasMany(e => e.Z_DefendantsSessionLawyers)
                .WithRequired(e => e.Cases_CaseDefendants)
                .HasForeignKey(e => e.DefendantID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_CaseDefendants>()
                .HasMany(e => e.Cases_DefendatnsCaseLog)
                .WithRequired(e => e.Cases_CaseDefendants)
                .HasForeignKey(e => e.DefendantID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_CaseDefendants>()
                .HasMany(e => e.Cases_DefendatnsSessionsLog)
                .WithRequired(e => e.Cases_CaseDefendants)
                .HasForeignKey(e => e.DefendantID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_CaseDocumentFolders>()
                .HasMany(e => e.Cases_CaseDocuments)
                .WithOptional(e => e.Cases_CaseDocumentFolders)
                .HasForeignKey(e => e.FolderID);

            modelBuilder.Entity<Cases_Cases>()
                .HasMany(e => e.Cases_CaseDefendants)
                .WithRequired(e => e.Cases_Cases)
                .HasForeignKey(e => e.CaseID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_Cases>()
                .HasMany(e => e.Cases_CaseDescription)
                .WithRequired(e => e.Cases_Cases)
                .HasForeignKey(e => e.CaseID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_Cases>()
                .HasMany(e => e.Cases_CaseDocumentFolders)
                .WithRequired(e => e.Cases_Cases)
                .HasForeignKey(e => e.CaseID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_Cases>()
                .HasMany(e => e.Cases_CaseTransfer)
                .WithRequired(e => e.Cases_Cases)
                .HasForeignKey(e => e.CaseID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_Cases>()
                .HasOptional(e => e.Cases_Resumption_Detail)
                .WithRequired(e => e.Cases_Cases);

            modelBuilder.Entity<Cases_Cases>()
                .HasMany(e => e.Cases_CaseSessions)
                .WithRequired(e => e.Cases_Cases)
                .HasForeignKey(e => e.CaseID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_Cases>()
                .HasMany(e => e.Cases_CaseVictims)
                .WithRequired(e => e.Cases_Cases)
                .HasForeignKey(e => e.CaseID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_CaseSessions>()
                .HasMany(e => e.Cases_CaseDescription)
                .WithOptional(e => e.Cases_CaseSessions)
                .HasForeignKey(e => e.SessionID);

            modelBuilder.Entity<Cases_CaseSessions>()
                .HasMany(e => e.Cases_CaseDocumentFolders)
                .WithOptional(e => e.Cases_CaseSessions)
                .HasForeignKey(e => e.SessionID);

            modelBuilder.Entity<Cases_CaseSessions>()
                .HasMany(e => e.Z_DefendantsSessionLawyers)
                .WithRequired(e => e.Cases_CaseSessions)
                .HasForeignKey(e => e.SessionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_CaseSessions>()
                .HasMany(e => e.Cases_DefendatnsSessionsLog)
                .WithRequired(e => e.Cases_CaseSessions)
                .HasForeignKey(e => e.SessionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_CaseSessions>()
                .HasOptional(e => e.Cases_SessionDecision)
                .WithRequired(e => e.Cases_CaseSessions);

            modelBuilder.Entity<Cases_CaseSessions>()
                .HasMany(e => e.Z_SessionWitnesses)
                .WithRequired(e => e.Cases_CaseSessions)
                .HasForeignKey(e => e.SessionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_CaseSessions>()
                .HasMany(e => e.Cases_VictimsSessionsLog)
                .WithRequired(e => e.Cases_CaseSessions)
                .HasForeignKey(e => e.SessionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_CaseSessions>()
                .HasMany(e => e.Security_Users)
                .WithMany(e => e.Cases_CaseSessions)
                .Map(m => m.ToTable("Cases_SessionCircuitMembers").MapLeftKey("SessionID").MapRightKey("UserID"));

            modelBuilder.Entity<Cases_CaseVictims>()
                .HasMany(e => e.Cases_VictimsCaseLog)
                .WithRequired(e => e.Cases_CaseVictims)
                .HasForeignKey(e => e.VictimID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_CaseVictims>()
                .HasMany(e => e.Cases_VictimsSessionsLog)
                .WithRequired(e => e.Cases_CaseVictims)
                .HasForeignKey(e => e.VictimID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_DefendatnsCaseLog>()
                .HasMany(e => e.Cases_DefendantsCharges)
                .WithRequired(e => e.Cases_DefendatnsCaseLog)
                .HasForeignKey(e => e.DefendantCaseLogID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_DocumentsLog>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Cases_DocumentsLog>()
                .Property(e => e.ActionType)
                .IsUnicode(false);

            modelBuilder.Entity<Cases_MasterCase>()
                .Property(e => e.NationalID)
                .IsUnicode(false);

            modelBuilder.Entity<Cases_MasterCase>()
                .HasMany(e => e.Cases_Cases)
                .WithRequired(e => e.Cases_MasterCase)
                .HasForeignKey(e => e.MasterCaseID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_Resumption_Detail>()
                .Property(e => e.ProsecutionReport)
                .IsUnicode(false);

            modelBuilder.Entity<Cases_Resumption_Detail>()
                .HasMany(e => e.Configurations_Lookups1)
                .WithMany(e => e.Cases_Resumption_Detail1)
                .Map(m => m.ToTable("Cases_ResumptionIdentities").MapLeftKey("CaseID").MapRightKey("ResumptionIdentitiyID"));

            modelBuilder.Entity<Cases_SessionDecision>()
                .HasMany(e => e.Cases_DefendantsDecision)
                .WithRequired(e => e.Cases_SessionDecision)
                .HasForeignKey(e => e.SessionDessionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_CaseTypes>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Configurations_CaseTypes>()
                .Property(e => e.Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Configurations_CaseTypes>()
                .HasMany(e => e.Cases_MasterCase)
                .WithRequired(e => e.Configurations_CaseTypes)
                .HasForeignKey(e => e.CaseTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_CaseTypes>()
                .HasMany(e => e.CourtConfigurations_CircuitDaysSetup)
                .WithRequired(e => e.Configurations_CaseTypes)
                .HasForeignKey(e => e.CaseTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_CaseTypes>()
                .HasMany(e => e.CourtConfigurations_CircuitRolls)
                .WithRequired(e => e.Configurations_CaseTypes)
                .HasForeignKey(e => e.CaseTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_CaseTypes>()
                .HasMany(e => e.CourtConfigurations_TextPredections)
                .WithRequired(e => e.Configurations_CaseTypes)
                .HasForeignKey(e => e.CaseTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_CaseTypes>()
                .HasMany(e => e.Configurations_Courts)
                .WithMany(e => e.Configurations_CaseTypes)
                .Map(m => m.ToTable("CourtConfigurations_CourtCaseTypes").MapLeftKey("CaseTypeID").MapRightKey("CourtID"));

            modelBuilder.Entity<Configurations_Courts>()
                .HasMany(e => e.CourtConfigurations_Circuits)
                .WithRequired(e => e.Configurations_Courts)
                .HasForeignKey(e => e.CourtID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Courts>()
                .HasMany(e => e.Configurations_Courts1)
                .WithOptional(e => e.Configurations_Courts2)
                .HasForeignKey(e => e.ParentID);

            modelBuilder.Entity<Configurations_Courts>()
                .HasMany(e => e.Configurations_Prosecutions)
                .WithRequired(e => e.Configurations_Courts)
                .HasForeignKey(e => e.CourtID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Courts>()
                .HasMany(e => e.Security_Users)
                .WithOptional(e => e.Configurations_Courts)
                .HasForeignKey(e => e.CourtID);

            modelBuilder.Entity<Configurations_DecisionTypes>()
                .HasMany(e => e.Cases_SessionDecision)
                .WithRequired(e => e.Configurations_DecisionTypes)
                .HasForeignKey(e => e.DecisionTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_LookupCategories>()
                .HasMany(e => e.Configurations_Lookups)
                .WithRequired(e => e.Configurations_LookupCategories)
                .HasForeignKey(e => e.CategoryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Cases_CaseDocuments)
                .WithRequired(e => e.Configurations_Lookups)
                .HasForeignKey(e => e.AttachmentTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Cases_Cases)
                .WithRequired(e => e.Configurations_Lookups)
                .HasForeignKey(e => e.CaseStatusID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Cases_Cases1)
                .WithRequired(e => e.Configurations_Lookups1)
                .HasForeignKey(e => e.CaseLevelID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Cases_Cases2)
                .WithRequired(e => e.Configurations_Lookups2)
                .HasForeignKey(e => e.ProcedureTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Cases_CaseTransfer)
                .WithRequired(e => e.Configurations_Lookups)
                .HasForeignKey(e => e.TransferTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Cases_DefendantsCharges)
                .WithRequired(e => e.Configurations_Lookups)
                .HasForeignKey(e => e.ChargeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Cases_DefendantsDecision)
                .WithOptional(e => e.Configurations_Lookups)
                .HasForeignKey(e => e.PunishmentType);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Cases_DefendatnsCaseLog)
                .WithRequired(e => e.Configurations_Lookups)
                .HasForeignKey(e => e.PoliceStationStatusID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Cases_DefendatnsSessionsLog)
                .WithOptional(e => e.Configurations_Lookups)
                .HasForeignKey(e => e.CourtStatusID);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Cases_DefendatnsSessionsLog1)
                .WithRequired(e => e.Configurations_Lookups1)
                .HasForeignKey(e => e.PresenceStatusID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Cases_MasterCase)
                .WithRequired(e => e.Configurations_Lookups)
                .HasForeignKey(e => e.CrimeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Cases_Resumption_Detail)
                .WithOptional(e => e.Configurations_Lookups)
                .HasForeignKey(e => e.ProsecutionResumptionCause);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Cases_VictimsSessionsLog)
                .WithRequired(e => e.Configurations_Lookups)
                .HasForeignKey(e => e.PresenceStatusID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Configurations_Courts)
                .WithRequired(e => e.Configurations_Lookups)
                .HasForeignKey(e => e.CourtLevelID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Configurations_OrganizationDetails)
                .WithRequired(e => e.Configurations_Lookups)
                .HasForeignKey(e => e.OrganizationTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.CourtConfigurations_CircuitMembers)
                .WithOptional(e => e.Configurations_Lookups)
                .HasForeignKey(e => e.JudgeType);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Z_LawyerAttorneys)
                .WithRequired(e => e.Configurations_Lookups)
                .HasForeignKey(e => e.TypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Z_Lawyers)
                .WithRequired(e => e.Configurations_Lookups)
                .HasForeignKey(e => e.DegreeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Configurations_OrganizationDetails1)
                .WithOptional(e => e.Configurations_Lookups1)
                .HasForeignKey(e => e.OrganizationCategoryID);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Configurations_Persons)
                .WithOptional(e => e.Configurations_Lookups)
                .HasForeignKey(e => e.NationalityID);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Configurations_Persons1)
                .WithOptional(e => e.Configurations_Lookups1)
                .HasForeignKey(e => e.ImprisonStatusID);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Security_UsersLoginFailure)
                .WithOptional(e => e.Configurations_Lookups)
                .HasForeignKey(e => e.FallID);

            modelBuilder.Entity<Configurations_Persons>()
                .Property(e => e.PassportNumber)
                .IsFixedLength();

            modelBuilder.Entity<Configurations_Persons>()
                .HasMany(e => e.Cases_CaseDefendants)
                .WithRequired(e => e.Configurations_Persons)
                .HasForeignKey(e => e.PersonID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Persons>()
                .HasMany(e => e.Cases_CaseVictims)
                .WithRequired(e => e.Configurations_Persons)
                .HasForeignKey(e => e.PersonID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Persons>()
                .HasOptional(e => e.Configurations_OrganizationDetails)
                .WithRequired(e => e.Configurations_Persons);

            modelBuilder.Entity<Configurations_Persons>()
                .HasMany(e => e.Configurations_OrganizationRepresentatives)
                .WithRequired(e => e.Configurations_Persons)
                .HasForeignKey(e => e.OrganizationID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Persons>()
                .HasMany(e => e.Configurations_OrganizationRepresentatives1)
                .WithRequired(e => e.Configurations_Persons1)
                .HasForeignKey(e => e.PersonID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Persons>()
                .HasMany(e => e.Z_Lawyers)
                .WithRequired(e => e.Configurations_Persons)
                .HasForeignKey(e => e.PersonID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Persons>()
                .HasMany(e => e.Configurations_Prosecuters)
                .WithRequired(e => e.Configurations_Persons)
                .HasForeignKey(e => e.PersonID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Persons>()
                .HasMany(e => e.Z_SessionWitnesses)
                .WithRequired(e => e.Configurations_Persons)
                .HasForeignKey(e => e.PersonID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Persons>()
                .HasMany(e => e.Security_Users)
                .WithOptional(e => e.Configurations_Persons)
                .HasForeignKey(e => e.PersonsId);

            modelBuilder.Entity<Configurations_PoliceStations>()
                .HasMany(e => e.Cases_MasterCase)
                .WithRequired(e => e.Configurations_PoliceStations)
                .HasForeignKey(e => e.PoliceStationID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Prosecuters>()
                .HasMany(e => e.Cases_CaseSessions)
                .WithOptional(e => e.Configurations_Prosecuters)
                .HasForeignKey(e => e.ProsecuterID);

            modelBuilder.Entity<Configurations_Prosecutions>()
                .Property(e => e.Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Configurations_Prosecutions>()
                .HasMany(e => e.Cases_MasterCase)
                .WithOptional(e => e.Configurations_Prosecutions)
                .HasForeignKey(e => e.ProsecutionID);

            modelBuilder.Entity<Configurations_Prosecutions>()
                .HasMany(e => e.Configurations_PoliceStations)
                .WithRequired(e => e.Configurations_Prosecutions)
                .HasForeignKey(e => e.ProsecutionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Prosecutions>()
                .HasMany(e => e.Configurations_Prosecuters)
                .WithRequired(e => e.Configurations_Prosecutions)
                .HasForeignKey(e => e.ProsecutionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Prosecutions>()
                .HasMany(e => e.Configurations_Prosecutions1)
                .WithOptional(e => e.Configurations_Prosecutions2)
                .HasForeignKey(e => e.ParentID);

            modelBuilder.Entity<CourtConfigurations_CasesDistributionRules>()
                .HasMany(e => e.CourtConfigurations_DistributionDates)
                .WithRequired(e => e.CourtConfigurations_CasesDistributionRules)
                .HasForeignKey(e => e.RuleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourtConfigurations_CasesDistributionRules>()
                .HasMany(e => e.CourtConfigurations_DistributionNumbers)
                .WithRequired(e => e.CourtConfigurations_CasesDistributionRules)
                .HasForeignKey(e => e.RuleID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourtConfigurations_CircuitRolls>()
                .HasMany(e => e.Cases_CaseSessions)
                .WithRequired(e => e.CourtConfigurations_CircuitRolls)
                .HasForeignKey(e => e.RollID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourtConfigurations_CircuitRolls>()
                .HasMany(e => e.Cases_CaseTransfer)
                .WithRequired(e => e.CourtConfigurations_CircuitRolls)
                .HasForeignKey(e => e.OldRollID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourtConfigurations_CircuitRolls>()
                .HasMany(e => e.Cases_CaseTransfer1)
                .WithRequired(e => e.CourtConfigurations_CircuitRolls1)
                .HasForeignKey(e => e.NewRollID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourtConfigurations_CircuitRolls>()
                .HasMany(e => e.Cases_SessionDecision)
                .WithOptional(e => e.CourtConfigurations_CircuitRolls)
                .HasForeignKey(e => e.FirstRollID);

            modelBuilder.Entity<CourtConfigurations_CircuitRolls>()
                .HasMany(e => e.Cases_SessionDecision1)
                .WithOptional(e => e.CourtConfigurations_CircuitRolls1)
                .HasForeignKey(e => e.SecondRollID);

            modelBuilder.Entity<CourtConfigurations_Circuits>()
                .HasMany(e => e.Cases_Cases)
                .WithRequired(e => e.CourtConfigurations_Circuits)
                .HasForeignKey(e => e.CircuitID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourtConfigurations_Circuits>()
                .HasMany(e => e.CourtConfigurations_CasesDistributionRules)
                .WithRequired(e => e.CourtConfigurations_Circuits)
                .HasForeignKey(e => e.DestinationCircuitID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourtConfigurations_Circuits>()
                .HasMany(e => e.CourtConfigurations_CircuitDaysSetup)
                .WithRequired(e => e.CourtConfigurations_Circuits)
                .HasForeignKey(e => e.CircuitID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourtConfigurations_Circuits>()
                .HasMany(e => e.CourtConfigurations_CircuitMembers)
                .WithRequired(e => e.CourtConfigurations_Circuits)
                .HasForeignKey(e => e.CircuitID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourtConfigurations_Circuits>()
                .HasMany(e => e.CourtConfigurations_CircuitRolls)
                .WithRequired(e => e.CourtConfigurations_Circuits)
                .HasForeignKey(e => e.CircuitID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourtConfigurations_Circuits>()
                .HasMany(e => e.CourtConfigurations_CasesDistributionRules1)
                .WithMany(e => e.CourtConfigurations_Circuits1)
                .Map(m => m.ToTable("CourtConfigurations_DistributionTargetCircuits").MapLeftKey("TargetCircutiID").MapRightKey("RuleID"));

            modelBuilder.Entity<Security_Actions>()
                .HasMany(e => e.Security_UserTypeActions)
                .WithRequired(e => e.Security_Actions)
                .HasForeignKey(e => e.ActionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Security_Modules>()
                .HasMany(e => e.Security_Pages)
                .WithRequired(e => e.Security_Modules)
                .HasForeignKey(e => e.ModuleID);

            modelBuilder.Entity<Security_Pages>()
                .HasMany(e => e.Security_Actions)
                .WithRequired(e => e.Security_Pages)
                .HasForeignKey(e => e.PageID);

            modelBuilder.Entity<Security_Users>()
                .HasMany(e => e.Cases_CaseTransfer)
                .WithRequired(e => e.Security_Users)
                .HasForeignKey(e => e.TransferedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Security_Users>()
                .HasMany(e => e.CourtConfigurations_CircuitDaysSetup)
                .WithRequired(e => e.Security_Users)
                .HasForeignKey(e => e.SecretaryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Security_Users>()
                .HasMany(e => e.CourtConfigurations_CircuitMembers)
                .WithRequired(e => e.Security_Users)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Security_Users>()
                .HasMany(e => e.CourtConfigurations_CircuitRolls)
                .WithOptional(e => e.Security_Users)
                .HasForeignKey(e => e.SecretaryID);

            modelBuilder.Entity<Security_Users>()
                .HasMany(e => e.CourtConfigurations_TextPredections)
                .WithRequired(e => e.Security_Users)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Security_Users>()
                .HasMany(e => e.Security_UsersLoginFailure)
                .WithRequired(e => e.Security_Users)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Security_UserTypes>()
                .HasMany(e => e.Security_Users)
                .WithRequired(e => e.Security_UserTypes)
                .HasForeignKey(e => e.UserTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Security_UserTypes>()
                .HasMany(e => e.Security_Users1)
                .WithRequired(e => e.Security_UserTypes1)
                .HasForeignKey(e => e.UserTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Security_UserTypes>()
                .HasMany(e => e.Security_UserTypeActions)
                .WithRequired(e => e.Security_UserTypes)
                .HasForeignKey(e => e.UserTypeID);

            modelBuilder.Entity<Z_DefendantsSessionLawyers>()
                .HasMany(e => e.Z_LawyerAttorneys)
                .WithMany(e => e.Z_DefendantsSessionLawyers)
                .Map(m => m.ToTable("Z_DefendantsSessionAttornies").MapLeftKey("LogEntryID").MapRightKey("AttornyCopyID"));

            modelBuilder.Entity<Z_Lawyers>()
                .HasMany(e => e.Z_DefendantsSessionLawyers)
                .WithRequired(e => e.Z_Lawyers)
                .HasForeignKey(e => e.LawyerID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Z_Lawyers>()
                .HasMany(e => e.Z_LawyerAttorneys)
                .WithRequired(e => e.Z_Lawyers)
                .HasForeignKey(e => e.LawyerID)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Cases_Cases>()
                .HasOptional(e => e.Service_CaseProsecution)
                .WithRequired(e => e.Cases_Case);

            modelBuilder.Entity<Cases_CaseDefendants>()
                .HasOptional(e => e.Service_CaseDefendantProsecution)
                .WithRequired(e => e.Cases_CaseDefendant);

            modelBuilder.Entity<Cases_CaseVictims>()
                .HasOptional(e => e.Service_CaseVictimProsecution)
                .WithRequired(e => e.Cases_CaseVictim);

        }
    }
}
