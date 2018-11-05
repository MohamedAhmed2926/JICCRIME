using System.Data.Entity;
using JIC.Crime.Entities.Models;
using JIC.Base;
using JIC.Crime.Entities;
using JIC.Base.Entities.Models;

namespace JIC.Crime.Repositories
{
    public partial class JICCrimeContext : DBContext<Security_Users>
    {
        #region Constractor
        public JICCrimeContext():base()
        {
           
        }
        #endregion
        #region Properties
        #region DbSets
        #endregion
        public virtual DbSet<CaseAction_UserType> CaseAction_UserType { get; set; }
        public virtual DbSet<CaseAction> CaseActions { get; set; }
        public virtual DbSet<Cases_CaseDefendants> Cases_CaseDefendants { get; set; }
        public virtual DbSet<Cases_CaseDescription> Cases_CaseDescription { get; set; }
        public virtual DbSet<Cases_CaseDocumentFolders> Cases_CaseDocumentFolders { get; set; }
        public virtual DbSet<Cases_CaseDocuments> Cases_CaseDocuments { get; set; }
        public virtual DbSet<Cases_CaseNotes> Cases_CaseNotes { get; set; }
        public virtual DbSet<Cases_CaseObtainments> Cases_CaseObtainments { get; set; }
        public virtual DbSet<Cases_CaseRequests> Cases_CaseRequests { get; set; }
        public virtual DbSet<Cases_Cases> Cases_Cases { get; set; }
        public virtual DbSet<Cases_CaseSessions> Cases_CaseSessions { get; set; }
        public virtual DbSet<Cases_CasesLog> Cases_CasesLog { get; set; }
        public virtual DbSet<Cases_CasesTransmissionRequests> Cases_CasesTransmissionRequests { get; set; }
        public virtual DbSet<Cases_CaseTransfer> Cases_CaseTransfer { get; set; }
        public virtual DbSet<Cases_CaseVictims> Cases_CaseVictims { get; set; }
        public virtual DbSet<Cases_CaseWitnesses> Cases_CaseWitnesses { get; set; }
        public virtual DbSet<Cases_CrimeTypes> Cases_CrimeTypes { get; set; }
        public virtual DbSet<Cases_DefendantsCharges> Cases_DefendantsCharges { get; set; }
        public virtual DbSet<Cases_DefendantsDecision> Cases_DefendantsDecision { get; set; }
        public virtual DbSet<Cases_DefendatnsCaseLog> Cases_DefendatnsCaseLog { get; set; }
        public virtual DbSet<Cases_DefendatnsSessionsLog> Cases_DefendatnsSessionsLog { get; set; }
        public virtual DbSet<Cases_DocumentsLog> Cases_DocumentsLog { get; set; }
        public virtual DbSet<Cases_JudgmentReasons> Cases_JudgmentReasons { get; set; }
        public virtual DbSet<Cases_MasterCase> Cases_MasterCase { get; set; }
        public virtual DbSet<Cases_MasterCasesLog> Cases_MasterCasesLog { get; set; }
        public virtual DbSet<Cases_SessionDecision> Cases_SessionDecision { get; set; }
        public virtual DbSet<Cases_VictimsCaseLog> Cases_VictimsCaseLog { get; set; }
        public virtual DbSet<Cases_VictimsSessionsLog> Cases_VictimsSessionsLog { get; set; }
        public virtual DbSet<Cases_WitnessesCaseLog> Cases_WitnessesCaseLog { get; set; }
        public virtual DbSet<Configurations_CaseTypes> Configurations_CaseTypes { get; set; }
        public virtual DbSet<Configurations_Courts> Configurations_Courts { get; set; }
        public virtual DbSet<Configurations_Courts_Crimes> Configurations_Courts_Crimes { get; set; }
        public virtual DbSet<Configurations_DecisionTypes> Configurations_DecisionTypes { get; set; }
        public virtual DbSet<Configurations_LookupCategories> Configurations_LookupCategories { get; set; }
        public virtual DbSet<Configurations_Lookups> Configurations_Lookups { get; set; }
        public virtual DbSet<Configurations_OrganizationDetails> Configurations_OrganizationDetails { get; set; }
        public virtual DbSet<Configurations_OrganizationRepresentatives> Configurations_OrganizationRepresentatives { get; set; }
        public virtual DbSet<Configurations_OverallNumbers> Configurations_OverallNumbers { get; set; }
        public virtual DbSet<Configurations_Persons> Configurations_Persons { get; set; }
        public virtual DbSet<Configurations_PoliceStations> Configurations_PoliceStations { get; set; }
        public virtual DbSet<Configurations_Prosecuters> Configurations_Prosecuters { get; set; }
        public virtual DbSet<Configurations_Prosecutions> Configurations_Prosecutions { get; set; }
        public virtual DbSet<CourtConfigurations_Vacations> CourtConfigurations_Vacations { get; set; }
        public virtual DbSet<CourtConfigurations_CircuitMembers> CourtConfigurations_CircuitMembers { get; set; }
        public virtual DbSet<CourtConfigurations_CircuitRolls> CourtConfigurations_CircuitRolls { get; set; }
        public virtual DbSet<CourtConfigurations_Circuits> CourtConfigurations_Circuits { get; set; }
        public virtual DbSet<CourtConfigurations_CircuitsPoliceStation> CourtConfigurations_CircuitsPoliceStation { get; set; }
        public virtual DbSet<CourtConfigurations_CycleRolls> CourtConfigurations_CycleRolls { get; set; }
        public virtual DbSet<CourtConfigurations_Cycles> CourtConfigurations_Cycles { get; set; }
        public virtual DbSet<DeletedOverallNum> DeletedOverallNums { get; set; }
        public virtual DbSet<NotificationItemType> NotificationItemTypes { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Security_Actions> Security_Actions { get; set; }
        public virtual DbSet<Security_Modules> Security_Modules { get; set; }
        public virtual DbSet<Security_Pages> Security_Pages { get; set; }
        //public virtual IDbSet<Security_Users> Security_Users { get { return Users; } set { Users = value; } }
        public virtual DbSet<Security_UsersLoginFailure> Security_UsersLoginFailure { get; set; }
        public virtual DbSet<Security_UserTypeActions> Security_UserTypeActions { get; set; }
        public virtual DbSet<Security_UserTypes> Security_UserTypes { get; set; }
        public virtual DbSet<Cases_CaseTransmission> Cases_CaseTransmission { get; set; }
        public virtual DbSet<CourtConfigurations_WorkDays> CourtConfigurations_WorkDays { get; set; }
        public virtual DbSet<CourtConfigurations_TextPredictions> CourtConfigurations_TextPredictions { get; set; }
        public virtual DbSet<CourtConfigurations_CourtHalls> CourtConfigurations_CourtHalls { get; set; }
        public virtual DbSet<Cases_WitnessSessionLog> Cases_WitnessSessionLog { get; set; }
        #endregion

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Database.SetInitializer<JICCrimeContext>(null);

            modelBuilder.Entity<CaseAction>()
                .HasMany(e => e.CaseAction_UserType)
                .WithRequired(e => e.CaseAction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CaseAction>()
                .HasMany(e => e.Cases_MasterCase)
                .WithOptional(e => e.CaseAction)
                .HasForeignKey(e => e.NotificationActionID);

            modelBuilder.Entity<CaseAction>()
                .HasMany(e => e.Notifications)
                .WithRequired(e => e.CaseAction)
                .HasForeignKey(e => e.NotificationActionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_CaseDefendants>()
                .HasMany(e => e.Cases_DefendantsDecision)
                .WithRequired(e => e.Cases_CaseDefendants)
                .HasForeignKey(e => e.CaseDefendantId)
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

            modelBuilder.Entity<Cases_CaseDocuments>()
                .HasMany(e => e.Cases_CaseObtainments)
                .WithMany(e => e.Cases_CaseDocuments)
                .Map(m => m.ToTable("Cases_CaseObtainmentDocuments").MapLeftKey("CaseDocumentID").MapRightKey("CaseObtainmentID"));

            modelBuilder.Entity<Cases_CaseDocuments>()
                .HasMany(e => e.Cases_CaseRequests)
                .WithMany(e => e.Cases_CaseDocuments)
                .Map(m => m.ToTable("Cases_CaseRequestDocuments").MapLeftKey("CaseDocumentID").MapRightKey("CaseRequestID"));

            modelBuilder.Entity<Cases_CaseDocuments>()
                .HasMany(e => e.Cases_CasesTransmissionRequests)
                .WithMany(e => e.Cases_CaseDocuments)
                .Map(m => m.ToTable("Cases_CaseTransmissionDocuments").MapLeftKey("CaseDocumentID").MapRightKey("CaseTransmissionRequestID"));

            modelBuilder.Entity<Cases_CaseDocuments>()
                .HasMany(e => e.Cases_CaseObtainments1)
                .WithMany(e => e.Cases_CaseDocuments1)
                .Map(m => m.ToTable("Cases_ObtainmentDocument").MapLeftKey("CaseDocumentID").MapRightKey("CaseObtainmentID"));

            modelBuilder.Entity<Cases_CaseNotes>()
                .Property(e => e.Note)
                .IsUnicode(false);

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
                .HasMany(e => e.Cases_CaseNotes)
                .WithRequired(e => e.Cases_Cases)
                .HasForeignKey(e => e.CaseID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_Cases>()
                .HasMany(e => e.Cases_CaseRequests)
                .WithRequired(e => e.Cases_Cases)
                .HasForeignKey(e => e.CaseID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_Cases>()
                .HasMany(e => e.Cases_CasesLog)
                .WithRequired(e => e.Cases_Cases)
                .HasForeignKey(e => e.CaseID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_Cases>()
                .HasMany(e => e.Cases_CaseTransfer)
                .WithRequired(e => e.Cases_Cases)
                .HasForeignKey(e => e.CaseID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_Cases>()
                .HasMany(e => e.Cases_CaseTransmission)
                .WithRequired(e => e.Cases_Cases)
                .HasForeignKey(e => e.CaseID)
                .WillCascadeOnDelete(false);

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

            modelBuilder.Entity<Cases_Cases>()
                .HasMany(e => e.Cases_CaseWitnesses)
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
                .HasMany(e => e.Cases_CaseObtainments)
                .WithOptional(e => e.Cases_CaseSessions)
                .HasForeignKey(e => e.CaseSessionID);

            modelBuilder.Entity<Cases_CaseSessions>()
                .HasMany(e => e.Cases_DefendatnsSessionsLog)
                .WithRequired(e => e.Cases_CaseSessions)
                .HasForeignKey(e => e.SessionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_CaseSessions>()
                .HasOptional(e => e.Cases_SessionDecision)
                .WithRequired(e => e.Cases_CaseSessions);

            modelBuilder.Entity<Cases_CaseSessions>()
                .HasMany(e => e.Cases_VictimsSessionsLog)
                .WithRequired(e => e.Cases_CaseSessions)
                .HasForeignKey(e => e.SessionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_CasesTransmissionRequests>()
                .HasMany(e => e.Cases_CaseTransmission)
                .WithRequired(e => e.Cases_CasesTransmissionRequests)
                .HasForeignKey(e => e.TransmissionID)
                .WillCascadeOnDelete(false);

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

            modelBuilder.Entity<Cases_CaseWitnesses>()
                .HasMany(e => e.Cases_WitnessesCaseLog)
                .WithRequired(e => e.Cases_CaseWitnesses)
                .HasForeignKey(e => e.WitnessID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_CrimeTypes>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Cases_CrimeTypes>()
                .Property(e => e.Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Cases_CrimeTypes>()
                .HasMany(e => e.Configurations_Courts_Crimes)
                .WithRequired(e => e.Cases_CrimeTypes)
                .HasForeignKey(e => e.CrimeTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_CaseDefendants>()
                .HasMany(e => e.Cases_DefendantsCharges)
                .WithRequired(e => e.Cases_CaseDefendants)
                .HasForeignKey(e => e.DefendantID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_DocumentsLog>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Cases_DocumentsLog>()
                .Property(e => e.ActionType)
                .IsUnicode(false);

            modelBuilder.Entity<Cases_JudgmentReasons>()
                .Property(e => e.JudgemntReason)
                .IsUnicode(false);

            modelBuilder.Entity<Cases_MasterCase>()
                .Property(e => e.NationalID)
                .IsUnicode(false);

            modelBuilder.Entity<Cases_MasterCase>()
                .HasMany(e => e.Cases_CaseObtainments)
                .WithRequired(e => e.Cases_MasterCase)
                .HasForeignKey(e => e.MasterCaseID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_MasterCase>()
                .HasMany(e => e.Cases_Cases)
                .WithRequired(e => e.Cases_MasterCase)
                .HasForeignKey(e => e.MasterCaseID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_MasterCase>()
                .HasMany(e => e.Cases_MasterCasesLog)
                .WithRequired(e => e.Cases_MasterCase)
                .HasForeignKey(e => e.MasterCaseID)
                .WillCascadeOnDelete(false);

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

            modelBuilder.Entity<Configurations_Courts>()
                .HasMany(e => e.Cases_CaseRequests)
                .WithRequired(e => e.Configurations_Courts)
                .HasForeignKey(e => e.CourtID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Courts>()
                .HasMany(e => e.CourtConfigurations_Circuits)
                .WithRequired(e => e.Configurations_Courts)
                .HasForeignKey(e => e.CourtID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Courts>()
                .HasMany(e => e.Configurations_Courts_Crimes)
                .WithRequired(e => e.Configurations_Courts)
                .HasForeignKey(e => e.CourtID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Courts>()
                .HasMany(e => e.Configurations_Courts1)
                .WithOptional(e => e.Configurations_Courts2)
                .HasForeignKey(e => e.ParentID);

            modelBuilder.Entity<Configurations_Courts>()
                .HasMany(e => e.Configurations_OverallNumbers)
                .WithRequired(e => e.Configurations_Courts)
                .HasForeignKey(e => e.CourtID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Courts>()
                .HasMany(e => e.CourtConfigurations_CycleRolls)
                .WithRequired(e => e.Configurations_Courts)
                .HasForeignKey(e => e.CourtID)
                .WillCascadeOnDelete(false);

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
                .HasMany(e => e.CaseAction_UserType)
                .WithRequired(e => e.Configurations_Lookups)
                .HasForeignKey(e => e.ActionTypeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Cases_CaseDefendants)
                .WithOptional(e => e.Configurations_Lookups)
                .HasForeignKey(e => e.DefendantStatusID);

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
                .HasForeignKey(e => e.NoteStatusID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Cases_Cases2)
                .WithRequired(e => e.Configurations_Lookups2)
                .HasForeignKey(e => e.CaseLevelID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.Cases_Cases3)
                .WithRequired(e => e.Configurations_Lookups3)
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
                .WithRequired(e => e.MainCrimeLookup)
                .HasForeignKey(e => e.CrimeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_CrimeTypes>()
                .HasMany(e => e.Cases_MasterCases)
                .WithRequired(e => e.CrimeTypeLookup)
                .HasForeignKey(e => e.CrimeType)
                .WillCascadeOnDelete(false);

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

            modelBuilder.Entity<Cases_CrimeTypes>()
                .HasMany(e => e.CourtConfigurations_Circuits)
                .WithRequired(e => e.Cases_CrimeTypes)
                .HasForeignKey(e => e.CrimeType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Lookups>()
                .HasMany(e => e.CourtConfigurations_Circuits1)
                .WithRequired(e => e.Configurations_Lookups1)
                .HasForeignKey(e => e.CycleID)
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

            modelBuilder.Entity<Configurations_OverallNumbers>()
                .HasMany(e => e.Cases_MasterCase)
                .WithOptional(e => e.Configurations_OverallNumbers)
                .HasForeignKey(e => e.OverallID)
                .WillCascadeOnDelete(false);

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
                .HasMany(e => e.Cases_CaseWitnesses)
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
                .HasMany(e => e.Security_Users)
                .WithOptional(e => e.Configurations_Persons)
                .HasForeignKey(e => e.PersonsId);

            modelBuilder.Entity<Configurations_PoliceStations>()
                .HasMany(e => e.Cases_MasterCase)
                .WithRequired(e => e.Configurations_PoliceStations)
                .HasForeignKey(e => e.PoliceStationID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_PoliceStations>()
                .HasMany(e => e.CourtConfigurations_CircuitsPoliceStation)
                .WithRequired(e => e.Configurations_PoliceStations)
                .HasForeignKey(e => e.PoliceStationID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Prosecuters>()
                .HasMany(e => e.CourtConfigurations_CircuitRolls)
                .WithOptional(e => e.Configurations_Prosecuters)
                .HasForeignKey(e => e.ProsecuterID);

            modelBuilder.Entity<Configurations_Prosecuters>()
             .HasMany(e => e.Cases_CaseSessions )
             .WithOptional(e => e.Configurations_Prosecuters)
             .HasForeignKey(e => e.ProsecuterID);

            modelBuilder.Entity<Configurations_Prosecutions>()
                .Property(e => e.Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Configurations_Prosecutions>()
                .HasMany(e => e.Cases_CaseObtainments)
                .WithRequired(e => e.Configurations_Prosecutions)
                .HasForeignKey(e => e.ProsecutionID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Prosecutions>()
                .HasMany(e => e.Cases_MasterCase)
                .WithOptional(e => e.Configurations_Prosecutions)
                .HasForeignKey(e => e.ProsecutionID);

            modelBuilder.Entity<Configurations_Prosecutions>()
                .HasMany(e => e.Configurations_OverallNumbers)
                .WithRequired(e => e.Configurations_Prosecutions)
                .HasForeignKey(e => e.ProsecutionID)
                .WillCascadeOnDelete(false);

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
                .HasMany(e => e.CourtConfigurations_CircuitsPoliceStation)
                .WithRequired(e => e.CourtConfigurations_Circuits)
                .HasForeignKey(e => e.CircuitID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourtConfigurations_Cycles>()
                .HasMany(e => e.CourtConfigurations_CycleRolls)
                .WithRequired(e => e.CourtConfigurations_Cycles)
                .HasForeignKey(e => e.CycleID)
                .WillCascadeOnDelete(false);
            

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
                .HasMany(e => e.CourtConfigurations_CircuitMembers)
                .WithRequired(e => e.Security_Users)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Security_Users>()
                .HasMany(e => e.CourtConfigurations_CircuitRolls)
                .WithOptional(e => e.Security_Users)
                .HasForeignKey(e => e.SecretaryID);

            modelBuilder.Entity<Security_Users>()
                  .HasMany(e => e.Cases_CaseSessions )
                  .WithOptional(e => e.Security_Users )
                  .HasForeignKey(e => e.SecretaryID);

            modelBuilder.Entity<Security_Users>()
                .HasMany(e => e.CourtConfigurations_Circuits)
                .WithRequired(e => e.Secretary_Users)
                .HasForeignKey(e => e.SecretaryID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Security_Users>()
                .HasMany(e => e.CourtConfigurations_Circuits1)
                .WithOptional(e => e.Assistant_Secretary_Users)
                .HasForeignKey(e => e.AssistantSecretaryID);

            modelBuilder.Entity<Security_Users>()
                .HasMany(e => e.Security_UsersLoginFailure)
                .WithRequired(e => e.Security_Users)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Security_UserTypes>()
                .HasMany(e => e.CaseAction_UserType)
                .WithRequired(e => e.Security_UserTypes)
                .HasForeignKey(e => e.UserTypeID)
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

            //modelBuilder.Entity<Cases_CrimeTypes>()
            //   .HasMany(e => e.CourtConfigurations_TextPredictions)
            //   .WithRequired(e => e.Cases_CrimeTypes)
            //   .HasForeignKey(e => e.CrimeTypeID)
            //   .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Security_Users>()
            //  .HasMany(e => e.CourtConfigurations_TextPredictions)
            //  .WithRequired(e => e.Security_Users)
            //  .HasForeignKey(e => e.UserID)
            //  .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourtConfigurations_Circuits>()
               .HasMany(e => e.CourtConfigurations_TextPredictions)
               .WithRequired(e => e.CourtConfigurations_Circuits)
               .HasForeignKey(e => e.CircuitID)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Configurations_Courts>()
               .HasMany(e => e.Cases_Cases)
               .WithRequired(e => e.Configurations_Courts)
               .HasForeignKey(e => e.CourtID)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourtConfigurations_CourtHalls>()
              .HasMany(e => e.CourtConfigurations_CircuitRolls)
              .WithOptional(e => e.CourtConfigurations_CourtHalls)
              .HasForeignKey(e => e.HallID);


            modelBuilder.Entity<Cases_Cases>()
               .HasMany(e => e.Cases_WitnessSessionLog)
               .WithRequired(e => e.Cases_Cases)
               .HasForeignKey(e => e.CaseID)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_CaseSessions>()
              .HasMany(e => e.Cases_WitnessSessionLog)
              .WithRequired(e => e.Cases_CaseSessions)
              .HasForeignKey(e => e.SessionID)
             .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cases_CaseWitnesses>()
            .HasMany(e => e.Cases_WitnessSessionLog)
            .WithRequired(e => e.Cases_CaseWitnesses)
            .HasForeignKey(e => e.WitnessID)
           .WillCascadeOnDelete(false);
        }

        #endregion
    }
}
