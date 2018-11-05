namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CaseAction_UserType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserTypeID = c.Int(nullable: false),
                        CaseActionID = c.Byte(nullable: false),
                        ActionTypeID = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CaseActions", t => t.CaseActionID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.ActionTypeID)
                .ForeignKey("dbo.Security_UserTypes", t => t.UserTypeID)
                .Index(t => t.UserTypeID)
                .Index(t => t.CaseActionID)
                .Index(t => t.ActionTypeID);
            
            CreateTable(
                "dbo.CaseActions",
                c => new
                    {
                        ID = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        TypeID = c.Byte(),
                        description = c.String(),
                        Title = c.String(),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Cases_MasterCase",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        FirstLevelNumber = c.String(nullable: false, maxLength: 100),
                        FirstLevelYear = c.String(nullable: false, maxLength: 4),
                        PoliceStationID = c.Int(nullable: false),
                        SecondLevelNumber = c.String(maxLength: 100),
                        SecondLevelYear = c.String(maxLength: 4),
                        ProsecutionID = c.Int(),
                        NationalID = c.String(nullable: false, maxLength: 50, unicode: false),
                        CrimeID = c.Int(nullable: false),
                        OverallID = c.Int(nullable: false),
                        HasObtainments = c.Boolean(nullable: false),
                        CrimeType = c.Int(nullable: false),
                        NotificationActionID = c.Byte(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configurations_Prosecutions", t => t.ProsecutionID)
                .ForeignKey("dbo.Configurations_OverallNumbers", t => t.OverallID)
                .ForeignKey("dbo.Configurations_PoliceStations", t => t.PoliceStationID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.CrimeID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.CrimeType)
                .ForeignKey("dbo.CaseActions", t => t.NotificationActionID)
                .Index(t => t.PoliceStationID)
                .Index(t => t.ProsecutionID)
                .Index(t => t.CrimeID)
                .Index(t => t.OverallID)
                .Index(t => t.CrimeType)
                .Index(t => t.NotificationActionID);
            
            CreateTable(
                "dbo.Cases_CaseObtainments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ObtainmentName = c.String(nullable: false, maxLength: 50),
                        ObtainmentNumber = c.Int(nullable: false),
                        ObtainmentYear = c.Int(nullable: false),
                        ProsecutionID = c.Int(nullable: false),
                        ObtainmentPerson = c.String(nullable: false, maxLength: 75),
                        ObtainmentPersonTitle = c.String(nullable: false, maxLength: 50),
                        SafeNumber = c.Int(nullable: false),
                        SafeYear = c.Int(nullable: false),
                        MasterCaseID = c.Int(nullable: false),
                        CaseSessionID = c.Long(),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cases_CaseSessions", t => t.CaseSessionID)
                .ForeignKey("dbo.Configurations_Prosecutions", t => t.ProsecutionID)
                .ForeignKey("dbo.Cases_MasterCase", t => t.MasterCaseID)
                .Index(t => t.ProsecutionID)
                .Index(t => t.MasterCaseID)
                .Index(t => t.CaseSessionID);
            
            CreateTable(
                "dbo.Cases_CaseDocuments",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        AttachmentTypeID = c.Int(nullable: false),
                        DocumentTitle = c.String(maxLength: 100),
                        UploadDate = c.DateTime(nullable: false),
                        UploadedBy = c.Int(nullable: false),
                        FileName = c.String(nullable: false, maxLength: 500),
                        FileData = c.Binary(nullable: false),
                        FolderID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cases_CaseDocumentFolders", t => t.FolderID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.AttachmentTypeID)
                .Index(t => t.AttachmentTypeID)
                .Index(t => t.FolderID);
            
            CreateTable(
                "dbo.Cases_CaseDocumentFolders",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        CaseID = c.Int(nullable: false),
                        SessionID = c.Long(),
                        ParentFolderID = c.Guid(),
                        Date = c.DateTime(nullable: false),
                        DocumentsCount = c.Int(),
                        ComputedDocumentsCount = c.Int(),
                        ComputedFoldersCount = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cases_CaseSessions", t => t.SessionID)
                .ForeignKey("dbo.Cases_Cases", t => t.CaseID)
                .Index(t => t.CaseID)
                .Index(t => t.SessionID);
            
            CreateTable(
                "dbo.Cases_Cases",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CircuitID = c.Int(nullable: false),
                        MasterCaseID = c.Int(nullable: false),
                        CaseLevelID = c.Int(nullable: false),
                        ProcedureTypeID = c.Int(nullable: false),
                        CaseStatusID = c.Int(nullable: false),
                        NewCaseStatusID = c.Int(),
                        NoteStatusID = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.CaseStatusID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.NoteStatusID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.CaseLevelID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.ProcedureTypeID)
                .ForeignKey("dbo.CourtConfigurations_Circuits", t => t.CircuitID)
                .ForeignKey("dbo.Cases_MasterCase", t => t.MasterCaseID)
                .Index(t => t.CircuitID)
                .Index(t => t.MasterCaseID)
                .Index(t => t.CaseLevelID)
                .Index(t => t.ProcedureTypeID)
                .Index(t => t.CaseStatusID)
                .Index(t => t.NoteStatusID);
            
            CreateTable(
                "dbo.Cases_CaseDefendants",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        PersonID = c.Long(nullable: false),
                        CaseID = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsCivilRightProsecutor = c.Boolean(nullable: false),
                        DefendantStatusID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.DefendantStatusID)
                .ForeignKey("dbo.Configurations_Persons", t => t.PersonID)
                .ForeignKey("dbo.Cases_Cases", t => t.CaseID)
                .Index(t => t.PersonID)
                .Index(t => t.CaseID)
                .Index(t => t.DefendantStatusID);
            
            CreateTable(
                "dbo.Cases_DefendantsDecision",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CaseDefendantId = c.Long(nullable: false),
                        SessionDessionId = c.Long(nullable: false),
                        IsGuilty = c.Boolean(nullable: false),
                        PunishmentDetails = c.String(),
                        RestrictionNo = c.Int(),
                        RestrictionYear = c.Int(),
                        PunishmentType = c.Int(),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.PunishmentType)
                .ForeignKey("dbo.Cases_SessionDecision", t => t.SessionDessionId)
                .ForeignKey("dbo.Cases_CaseDefendants", t => t.CaseDefendantId)
                .Index(t => t.CaseDefendantId)
                .Index(t => t.SessionDessionId)
                .Index(t => t.PunishmentType);
            
            CreateTable(
                "dbo.Cases_SessionDecision",
                c => new
                    {
                        CaseSessionID = c.Long(nullable: false),
                        DecisionText = c.String(nullable: false),
                        DecisionTypeID = c.Short(nullable: false),
                        FirstRollID = c.Long(),
                        SecondRollID = c.Long(),
                        PaymentStatus = c.Boolean(),
                        PaymentDate = c.DateTime(storeType: "date"),
                        OldCircuitID = c.Int(),
                        NewCircuitID = c.Int(),
                    })
                .PrimaryKey(t => t.CaseSessionID)
                .ForeignKey("dbo.CourtConfigurations_CircuitRolls", t => t.FirstRollID)
                .ForeignKey("dbo.CourtConfigurations_CircuitRolls", t => t.SecondRollID)
                .ForeignKey("dbo.Cases_CaseSessions", t => t.CaseSessionID)
                .ForeignKey("dbo.Configurations_DecisionTypes", t => t.DecisionTypeID)
                .Index(t => t.CaseSessionID)
                .Index(t => t.DecisionTypeID)
                .Index(t => t.FirstRollID)
                .Index(t => t.SecondRollID);
            
            CreateTable(
                "dbo.Cases_CaseSessions",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CaseID = c.Int(nullable: false),
                        RollID = c.Long(nullable: false),
                        DoneByDefaultCircuit = c.Boolean(nullable: false),
                        RollIndex = c.Int(nullable: false),
                        ProsecuterID = c.Int(),
                        MunitesOfSession = c.String(),
                        ApprovedByJudge = c.Boolean(nullable: false),
                        IsTransferedFromSession = c.Boolean(),
                        IsPendingOnTransfer = c.Boolean(),
                        IsTransferedApproved = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CourtConfigurations_CircuitRolls", t => t.RollID)
                .ForeignKey("dbo.Configurations_Prosecuters", t => t.ProsecuterID)
                .ForeignKey("dbo.Cases_Cases", t => t.CaseID)
                .Index(t => t.CaseID)
                .Index(t => t.RollID)
                .Index(t => t.ProsecuterID);
            
            CreateTable(
                "dbo.Cases_CaseDescription",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CaseID = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        LawItems = c.String(nullable: false),
                        FromDate = c.DateTime(nullable: false, storeType: "date"),
                        ToDate = c.DateTime(storeType: "date"),
                        SessionID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cases_CaseSessions", t => t.SessionID)
                .ForeignKey("dbo.Cases_Cases", t => t.CaseID)
                .Index(t => t.CaseID)
                .Index(t => t.SessionID);
            
            CreateTable(
                "dbo.Cases_DefendatnsSessionsLog",
                c => new
                    {
                        DefendantID = c.Long(nullable: false),
                        SessionID = c.Long(nullable: false),
                        PresenceStatusID = c.Int(nullable: false),
                        CourtStatusID = c.Int(),
                    })
                .PrimaryKey(t => new { t.DefendantID, t.SessionID })
                .ForeignKey("dbo.Configurations_Lookups", t => t.CourtStatusID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.PresenceStatusID)
                .ForeignKey("dbo.Cases_CaseSessions", t => t.SessionID)
                .ForeignKey("dbo.Cases_CaseDefendants", t => t.DefendantID)
                .Index(t => t.DefendantID)
                .Index(t => t.SessionID)
                .Index(t => t.PresenceStatusID)
                .Index(t => t.CourtStatusID);
            
            CreateTable(
                "dbo.Configurations_Lookups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        CategoryID = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configurations_LookupCategories", t => t.CategoryID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Cases_CaseTransfer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CaseID = c.Int(nullable: false),
                        OldRollID = c.Long(nullable: false),
                        NewRollID = c.Long(nullable: false),
                        TransferTypeID = c.Int(nullable: false),
                        Approved = c.Boolean(),
                        TransferedBy = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CourtConfigurations_CircuitRolls", t => t.OldRollID)
                .ForeignKey("dbo.CourtConfigurations_CircuitRolls", t => t.NewRollID)
                .ForeignKey("dbo.Security_Users", t => t.TransferedBy)
                .ForeignKey("dbo.Configurations_Lookups", t => t.TransferTypeID)
                .ForeignKey("dbo.Cases_Cases", t => t.CaseID)
                .Index(t => t.CaseID)
                .Index(t => t.OldRollID)
                .Index(t => t.NewRollID)
                .Index(t => t.TransferTypeID)
                .Index(t => t.TransferedBy);
            
            CreateTable(
                "dbo.CourtConfigurations_CircuitRolls",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CircuitID = c.Int(nullable: false),
                        SessionDate = c.DateTime(nullable: false, storeType: "date"),
                        RollStatusID = c.Byte(nullable: false),
                        ApprovedByJudge = c.Boolean(),
                        SecretaryID = c.Int(),
                        GeneratedBySystem = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Security_Users", t => t.SecretaryID)
                .ForeignKey("dbo.CourtConfigurations_Circuits", t => t.CircuitID)
                .Index(t => t.CircuitID)
                .Index(t => t.SecretaryID);
            
            CreateTable(
                "dbo.CourtConfigurations_Circuits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        CourtID = c.Int(nullable: false),
                        SecretaryID = c.Int(nullable: false),
                        AssistantSecretaryID = c.Int(),
                        CrimeType = c.Int(nullable: false),
                        CircuitStartDate = c.DateTime(nullable: false, storeType: "date"),
                        CycleID = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsFutureCircuit = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Security_Users", t => t.SecretaryID)
                .ForeignKey("dbo.Security_Users", t => t.AssistantSecretaryID)
                .ForeignKey("dbo.Configurations_Courts", t => t.CourtID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.CrimeType)
                .ForeignKey("dbo.Configurations_Lookups", t => t.CycleID)
                .Index(t => t.CourtID)
                .Index(t => t.SecretaryID)
                .Index(t => t.AssistantSecretaryID)
                .Index(t => t.CrimeType)
                .Index(t => t.CycleID);
            
            CreateTable(
                "dbo.Configurations_Courts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ParentID = c.Int(),
                        CourtLevelID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configurations_Courts", t => t.ParentID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.CourtLevelID)
                .Index(t => t.ParentID)
                .Index(t => t.CourtLevelID);
            
            CreateTable(
                "dbo.Cases_CaseRequests",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CaseID = c.Int(nullable: false),
                        RequestType = c.Int(nullable: false),
                        RequestNumber = c.Int(nullable: false),
                        RequestYear = c.Int(nullable: false),
                        CourtID = c.Int(nullable: false),
                        RequestDate = c.DateTime(storeType: "date"),
                        ProsecutionNoteDate = c.DateTime(nullable: false, storeType: "date"),
                        RevocationDate = c.DateTime(storeType: "date"),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configurations_Courts", t => t.CourtID)
                .ForeignKey("dbo.Cases_Cases", t => t.CaseID)
                .Index(t => t.CaseID)
                .Index(t => t.CourtID);
            
            CreateTable(
                "dbo.Configurations_Courts_Crimes",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CourtID = c.Int(nullable: false),
                        CrimeTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cases_CrimeTypes", t => t.CrimeTypeID)
                .ForeignKey("dbo.Configurations_Courts", t => t.CourtID)
                .Index(t => t.CourtID)
                .Index(t => t.CrimeTypeID);
            
            CreateTable(
                "dbo.Cases_CrimeTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20, fixedLength: true),
                        Code = c.String(nullable: false, maxLength: 2, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Configurations_Prosecutions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ParentID = c.Int(),
                        CourtID = c.Int(nullable: false),
                        Code = c.String(nullable: false, maxLength: 2, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configurations_Prosecutions", t => t.ParentID)
                .ForeignKey("dbo.Configurations_Courts", t => t.CourtID)
                .Index(t => t.ParentID)
                .Index(t => t.CourtID);
            
            CreateTable(
                "dbo.Configurations_OverallNumbers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InclosiveSierial = c.Long(nullable: false),
                        Year = c.Int(nullable: false),
                        ProsecutionID = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configurations_Prosecutions", t => t.ProsecutionID)
                .Index(t => t.ProsecutionID);
            
            CreateTable(
                "dbo.Configurations_PoliceStations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ProsecutionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configurations_Prosecutions", t => t.ProsecutionID)
                .Index(t => t.ProsecutionID);
            
            CreateTable(
                "dbo.CourtConfigurations_CircuitsPoliceStation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CircuitID = c.Int(nullable: false),
                        PoliceStationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configurations_PoliceStations", t => t.PoliceStationID)
                .ForeignKey("dbo.CourtConfigurations_Circuits", t => t.CircuitID)
                .Index(t => t.CircuitID)
                .Index(t => t.PoliceStationID);
            
            CreateTable(
                "dbo.Configurations_Prosecuters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProsecutionID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        PersonID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configurations_Persons", t => t.PersonID)
                .ForeignKey("dbo.Configurations_Prosecutions", t => t.ProsecutionID)
                .Index(t => t.ProsecutionID)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.Configurations_Persons",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 255),
                        NationalID = c.String(maxLength: 14),
                        PassportNumber = c.String(maxLength: 10, fixedLength: true),
                        IsLegalPerson = c.Boolean(nullable: false),
                        Address = c.String(maxLength: 1000),
                        Birthdate = c.DateTime(storeType: "date"),
                        JobTitle = c.String(maxLength: 1000),
                        IsEgyption = c.Boolean(nullable: false),
                        NationalityID = c.Int(),
                        ImprisonStatusID = c.Int(),
                        CleanFullName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.NationalityID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.ImprisonStatusID)
                .Index(t => t.NationalityID)
                .Index(t => t.ImprisonStatusID);
            
            CreateTable(
                "dbo.Cases_CaseVictims",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        PersonID = c.Long(nullable: false),
                        CaseID = c.Int(nullable: false),
                        IsCivilRightProsecutor = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configurations_Persons", t => t.PersonID)
                .ForeignKey("dbo.Cases_Cases", t => t.CaseID)
                .Index(t => t.PersonID)
                .Index(t => t.CaseID);
            
            CreateTable(
                "dbo.Cases_VictimsCaseLog",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        VictimID = c.Long(nullable: false),
                        IsCivilRightProsecutor = c.Boolean(nullable: false),
                        FromDate = c.DateTime(nullable: false, storeType: "date"),
                        ToDate = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cases_CaseVictims", t => t.VictimID)
                .Index(t => t.VictimID);
            
            CreateTable(
                "dbo.Cases_VictimsSessionsLog",
                c => new
                    {
                        VictimID = c.Long(nullable: false),
                        SessionID = c.Long(nullable: false),
                        PresenceStatusID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VictimID, t.SessionID })
                .ForeignKey("dbo.Cases_CaseVictims", t => t.VictimID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.PresenceStatusID)
                .ForeignKey("dbo.Cases_CaseSessions", t => t.SessionID)
                .Index(t => t.VictimID)
                .Index(t => t.SessionID)
                .Index(t => t.PresenceStatusID);
            
            CreateTable(
                "dbo.Cases_CaseWitnesses",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        PersonID = c.Long(nullable: false),
                        CaseID = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configurations_Persons", t => t.PersonID)
                .ForeignKey("dbo.Cases_Cases", t => t.CaseID)
                .Index(t => t.PersonID)
                .Index(t => t.CaseID);
            
            CreateTable(
                "dbo.Cases_WitnessesCaseLog",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        WitnessID = c.Long(nullable: false),
                        FromDate = c.DateTime(nullable: false, storeType: "date"),
                        ToDate = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cases_CaseWitnesses", t => t.WitnessID)
                .Index(t => t.WitnessID);
            
            CreateTable(
                "dbo.Configurations_OrganizationDetails",
                c => new
                    {
                        ID = c.Long(nullable: false),
                        OrganizationTypeID = c.Int(nullable: false),
                        OrganizationCategoryID = c.Int(),
                        RegistrationNumber = c.String(maxLength: 50),
                        PhoneNumber = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configurations_Persons", t => t.ID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.OrganizationTypeID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.OrganizationCategoryID)
                .Index(t => t.ID)
                .Index(t => t.OrganizationTypeID)
                .Index(t => t.OrganizationCategoryID);
            
            CreateTable(
                "dbo.Configurations_OrganizationRepresentatives",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrganizationID = c.Long(nullable: false),
                        PersonID = c.Long(nullable: false),
                        FromDate = c.DateTime(nullable: false, storeType: "date"),
                        ToDate = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configurations_Persons", t => t.OrganizationID)
                .ForeignKey("dbo.Configurations_Persons", t => t.PersonID)
                .Index(t => t.OrganizationID)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.Security_Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 100),
                        UserTypeID = c.Int(nullable: false),
                        CourtID = c.Int(),
                        TitleID = c.Int(),
                        Active = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                        FailedPWCount = c.Int(),
                        MobileNo = c.String(maxLength: 16),
                        PersonsId = c.Long(),
                        Locked = c.Boolean(),
                        ActiveDateFrom = c.DateTime(),
                        ActiveDateTo = c.DateTime(),
                        ProsecutionID = c.Int(),
                        LevelID = c.Int(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Security_UserTypes", t => t.UserTypeID)
                .ForeignKey("dbo.Configurations_Persons", t => t.PersonsId)
                .ForeignKey("dbo.Configurations_Courts", t => t.CourtID)
                .Index(t => t.UserTypeID)
                .Index(t => t.CourtID)
                .Index(t => t.PersonsId);
            
            CreateTable(
                "dbo.CustomUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        Security_Users_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Security_Users", t => t.Security_Users_Id)
                .Index(t => t.Security_Users_Id);
            
            CreateTable(
                "dbo.CourtConfigurations_CircuitMembers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        CircuitID = c.Int(nullable: false),
                        FromDate = c.DateTime(nullable: false, storeType: "date"),
                        ToDate = c.DateTime(storeType: "date"),
                        JudgeType = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Security_Users", t => t.UserID)
                .ForeignKey("dbo.CourtConfigurations_Circuits", t => t.CircuitID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.JudgeType)
                .Index(t => t.UserID)
                .Index(t => t.CircuitID)
                .Index(t => t.JudgeType);
            
            CreateTable(
                "dbo.CustomUserLogins",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        Security_Users_Id = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Security_Users", t => t.Security_Users_Id)
                .Index(t => t.Security_Users_Id);
            
            CreateTable(
                "dbo.CustomUserRoles",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Security_Users_Id = c.Int(),
                        CustomRole_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.Security_Users", t => t.Security_Users_Id)
                .ForeignKey("dbo.CustomRoles", t => t.CustomRole_Id)
                .Index(t => t.Security_Users_Id)
                .Index(t => t.CustomRole_Id);
            
            CreateTable(
                "dbo.Security_UsersLoginFailure",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        FallID = c.Int(),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Security_Users", t => t.UserID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.FallID)
                .Index(t => t.UserID)
                .Index(t => t.FallID);
            
            CreateTable(
                "dbo.Security_UserTypes",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Security_UserTypeActions",
                c => new
                    {
                        UserTypeID = c.Int(nullable: false),
                        ActionID = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.UserTypeID, t.ActionID })
                .ForeignKey("dbo.Security_Actions", t => t.ActionID)
                .ForeignKey("dbo.Security_UserTypes", t => t.UserTypeID, cascadeDelete: true)
                .Index(t => t.UserTypeID)
                .Index(t => t.ActionID);
            
            CreateTable(
                "dbo.Security_Actions",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        PageID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Security_Pages", t => t.PageID, cascadeDelete: true)
                .Index(t => t.PageID);
            
            CreateTable(
                "dbo.Security_Pages",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Path = c.String(nullable: false),
                        ModuleID = c.Int(nullable: false),
                        ShowInMenu = c.Boolean(nullable: false),
                        OrderIndex = c.Int(nullable: false),
                        RoutingName = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Security_Modules", t => t.ModuleID, cascadeDelete: true)
                .Index(t => t.ModuleID);
            
            CreateTable(
                "dbo.Security_Modules",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CourtConfigurations_CycleRolls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CycleID = c.Int(nullable: false),
                        RollDate = c.DateTime(nullable: false, storeType: "date"),
                        CourtID = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CourtConfigurations_Cycles", t => t.CycleID)
                .ForeignKey("dbo.Configurations_Courts", t => t.CourtID)
                .Index(t => t.CycleID)
                .Index(t => t.CourtID);
            
            CreateTable(
                "dbo.CourtConfigurations_Cycles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Cases_DefendantsCharges",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ChargeID = c.Int(nullable: false),
                        DefendantCaseLogID = c.Long(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cases_DefendatnsCaseLog", t => t.DefendantCaseLogID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.ChargeID)
                .Index(t => t.ChargeID)
                .Index(t => t.DefendantCaseLogID);
            
            CreateTable(
                "dbo.Cases_DefendatnsCaseLog",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        DefendantID = c.Long(nullable: false),
                        PoliceStationStatusID = c.Int(nullable: false),
                        FromDate = c.DateTime(nullable: false, storeType: "date"),
                        ToDate = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Configurations_Lookups", t => t.PoliceStationStatusID)
                .ForeignKey("dbo.Cases_CaseDefendants", t => t.DefendantID)
                .Index(t => t.DefendantID)
                .Index(t => t.PoliceStationStatusID);
            
            CreateTable(
                "dbo.Configurations_LookupCategories",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        ManagedByUser = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Configurations_DecisionTypes",
                c => new
                    {
                        ID = c.Short(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        DecisionLevel = c.Byte(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Cases_CaseNotes",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CaseID = c.Int(nullable: false),
                        Note = c.String(nullable: false, unicode: false, storeType: "text"),
                        StepID = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cases_Cases", t => t.CaseID)
                .Index(t => t.CaseID);
            
            CreateTable(
                "dbo.Cases_CasesLog",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CaseID = c.Int(nullable: false),
                        StepID = c.Int(nullable: false),
                        CircuitID = c.Int(nullable: false),
                        MasterCaseID = c.Int(nullable: false),
                        CaseLevel = c.Int(nullable: false),
                        ProcedureTypeID = c.Int(nullable: false),
                        CaseStatusID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cases_Cases", t => t.CaseID)
                .Index(t => t.CaseID);
            
            CreateTable(
                "dbo.Cases_CaseTransmission",
                c => new
                    {
                        TransmissionID = c.Int(nullable: false),
                        CaseID = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.TransmissionID, t.CaseID, t.IsActive })
                .ForeignKey("dbo.Cases_CasesTransmissionRequests", t => t.TransmissionID)
                .ForeignKey("dbo.Cases_Cases", t => t.CaseID)
                .Index(t => t.TransmissionID)
                .Index(t => t.CaseID);
            
            CreateTable(
                "dbo.Cases_CasesTransmissionRequests",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        RequestDate = c.DateTime(nullable: false, storeType: "date"),
                        RequestType = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Cases_MasterCasesLog",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        MasterCaseID = c.Int(nullable: false),
                        Title = c.String(maxLength: 100),
                        FirstLevelNumber = c.String(nullable: false, maxLength: 100),
                        FirstLevelYear = c.String(nullable: false, maxLength: 4),
                        PoliceStationID = c.Int(nullable: false),
                        SecondLevelNumber = c.String(nullable: false, maxLength: 100),
                        SecondLevelYear = c.String(nullable: false, maxLength: 100),
                        ProsecutionID = c.Int(nullable: false),
                        CrimeID = c.Int(nullable: false),
                        CrimeType = c.Int(nullable: false),
                        OverallID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cases_MasterCase", t => t.MasterCaseID)
                .Index(t => t.MasterCaseID);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Body = c.String(nullable: false, maxLength: 200),
                        NotificationDate = c.DateTime(nullable: false),
                        NotificationActionID = c.Byte(nullable: false),
                        CaseID = c.Long(),
                        CaseSessionID = c.Long(),
                        NotificationActionBy = c.Long(nullable: false),
                        NotifierID = c.Long(nullable: false),
                        IsViewer = c.Boolean(nullable: false),
                        BeenRead = c.Boolean(nullable: false),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CaseActions", t => t.NotificationActionID)
                .Index(t => t.NotificationActionID);
            
            CreateTable(
                "dbo.Cases_DocumentsLog",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SessionFolderID = c.Guid(nullable: false),
                        CreateBy = c.String(nullable: false, maxLength: 50, unicode: false),
                        Date = c.DateTime(nullable: false),
                        ActionType = c.String(nullable: false, maxLength: 50, unicode: false),
                        ActionDetails = c.String(nullable: false),
                        FolderID = c.Guid(),
                        FileID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Cases_JudgmentReasons",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        SessionDecisionID = c.Long(nullable: false),
                        JudgemntReason = c.String(nullable: false, unicode: false, storeType: "text"),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Configurations_CaseTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 10, fixedLength: true),
                        Code = c.String(nullable: false, maxLength: 2, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CourtConfigurations_Vacations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        FromDate = c.DateTime(nullable: false, storeType: "date"),
                        EndDate = c.DateTime(nullable: false, storeType: "date"),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CourtConfigurations_WorkDays",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        DayOfWeek = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ID, t.DayOfWeek });
            
            CreateTable(
                "dbo.DeletedOverallNum",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OverallNumberId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50),
                        LastModifiedAt = c.DateTime(),
                        LastModifiedBy = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.NotificationItemTypes",
                c => new
                    {
                        ID = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CustomRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cases_CaseObtainmentDocuments",
                c => new
                    {
                        CaseDocumentID = c.Guid(nullable: false),
                        CaseObtainmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CaseDocumentID, t.CaseObtainmentID })
                .ForeignKey("dbo.Cases_CaseDocuments", t => t.CaseDocumentID, cascadeDelete: true)
                .ForeignKey("dbo.Cases_CaseObtainments", t => t.CaseObtainmentID, cascadeDelete: true)
                .Index(t => t.CaseDocumentID)
                .Index(t => t.CaseObtainmentID);
            
            CreateTable(
                "dbo.Cases_ObtainmentDocument",
                c => new
                    {
                        CaseDocumentID = c.Guid(nullable: false),
                        CaseObtainmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CaseDocumentID, t.CaseObtainmentID })
                .ForeignKey("dbo.Cases_CaseDocuments", t => t.CaseDocumentID, cascadeDelete: true)
                .ForeignKey("dbo.Cases_CaseObtainments", t => t.CaseObtainmentID, cascadeDelete: true)
                .Index(t => t.CaseDocumentID)
                .Index(t => t.CaseObtainmentID);
            
            CreateTable(
                "dbo.Cases_CaseRequestDocuments",
                c => new
                    {
                        CaseDocumentID = c.Guid(nullable: false),
                        CaseRequestID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CaseDocumentID, t.CaseRequestID })
                .ForeignKey("dbo.Cases_CaseDocuments", t => t.CaseDocumentID, cascadeDelete: true)
                .ForeignKey("dbo.Cases_CaseRequests", t => t.CaseRequestID, cascadeDelete: true)
                .Index(t => t.CaseDocumentID)
                .Index(t => t.CaseRequestID);
            
            CreateTable(
                "dbo.Cases_CaseTransmissionDocuments",
                c => new
                    {
                        CaseDocumentID = c.Guid(nullable: false),
                        CaseTransmissionRequestID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CaseDocumentID, t.CaseTransmissionRequestID })
                .ForeignKey("dbo.Cases_CaseDocuments", t => t.CaseDocumentID, cascadeDelete: true)
                .ForeignKey("dbo.Cases_CasesTransmissionRequests", t => t.CaseTransmissionRequestID, cascadeDelete: true)
                .Index(t => t.CaseDocumentID)
                .Index(t => t.CaseTransmissionRequestID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomUserRoles", "CustomRole_Id", "dbo.CustomRoles");
            DropForeignKey("dbo.Notifications", "NotificationActionID", "dbo.CaseActions");
            DropForeignKey("dbo.Cases_MasterCase", "NotificationActionID", "dbo.CaseActions");
            DropForeignKey("dbo.Cases_MasterCasesLog", "MasterCaseID", "dbo.Cases_MasterCase");
            DropForeignKey("dbo.Cases_Cases", "MasterCaseID", "dbo.Cases_MasterCase");
            DropForeignKey("dbo.Cases_CaseObtainments", "MasterCaseID", "dbo.Cases_MasterCase");
            DropForeignKey("dbo.Cases_CaseTransmissionDocuments", "CaseTransmissionRequestID", "dbo.Cases_CasesTransmissionRequests");
            DropForeignKey("dbo.Cases_CaseTransmissionDocuments", "CaseDocumentID", "dbo.Cases_CaseDocuments");
            DropForeignKey("dbo.Cases_CaseRequestDocuments", "CaseRequestID", "dbo.Cases_CaseRequests");
            DropForeignKey("dbo.Cases_CaseRequestDocuments", "CaseDocumentID", "dbo.Cases_CaseDocuments");
            DropForeignKey("dbo.Cases_ObtainmentDocument", "CaseObtainmentID", "dbo.Cases_CaseObtainments");
            DropForeignKey("dbo.Cases_ObtainmentDocument", "CaseDocumentID", "dbo.Cases_CaseDocuments");
            DropForeignKey("dbo.Cases_CaseObtainmentDocuments", "CaseObtainmentID", "dbo.Cases_CaseObtainments");
            DropForeignKey("dbo.Cases_CaseObtainmentDocuments", "CaseDocumentID", "dbo.Cases_CaseDocuments");
            DropForeignKey("dbo.Cases_CaseWitnesses", "CaseID", "dbo.Cases_Cases");
            DropForeignKey("dbo.Cases_CaseVictims", "CaseID", "dbo.Cases_Cases");
            DropForeignKey("dbo.Cases_CaseTransmission", "CaseID", "dbo.Cases_Cases");
            DropForeignKey("dbo.Cases_CaseTransmission", "TransmissionID", "dbo.Cases_CasesTransmissionRequests");
            DropForeignKey("dbo.Cases_CaseTransfer", "CaseID", "dbo.Cases_Cases");
            DropForeignKey("dbo.Cases_CasesLog", "CaseID", "dbo.Cases_Cases");
            DropForeignKey("dbo.Cases_CaseSessions", "CaseID", "dbo.Cases_Cases");
            DropForeignKey("dbo.Cases_CaseRequests", "CaseID", "dbo.Cases_Cases");
            DropForeignKey("dbo.Cases_CaseNotes", "CaseID", "dbo.Cases_Cases");
            DropForeignKey("dbo.Cases_CaseDocumentFolders", "CaseID", "dbo.Cases_Cases");
            DropForeignKey("dbo.Cases_CaseDescription", "CaseID", "dbo.Cases_Cases");
            DropForeignKey("dbo.Cases_CaseDefendants", "CaseID", "dbo.Cases_Cases");
            DropForeignKey("dbo.Cases_DefendatnsSessionsLog", "DefendantID", "dbo.Cases_CaseDefendants");
            DropForeignKey("dbo.Cases_DefendatnsCaseLog", "DefendantID", "dbo.Cases_CaseDefendants");
            DropForeignKey("dbo.Cases_DefendantsDecision", "CaseDefendantId", "dbo.Cases_CaseDefendants");
            DropForeignKey("dbo.Cases_SessionDecision", "DecisionTypeID", "dbo.Configurations_DecisionTypes");
            DropForeignKey("dbo.Cases_DefendantsDecision", "SessionDessionId", "dbo.Cases_SessionDecision");
            DropForeignKey("dbo.Cases_VictimsSessionsLog", "SessionID", "dbo.Cases_CaseSessions");
            DropForeignKey("dbo.Cases_SessionDecision", "CaseSessionID", "dbo.Cases_CaseSessions");
            DropForeignKey("dbo.Cases_DefendatnsSessionsLog", "SessionID", "dbo.Cases_CaseSessions");
            DropForeignKey("dbo.Security_UsersLoginFailure", "FallID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.CourtConfigurations_Circuits", "CycleID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.CourtConfigurations_Circuits", "CrimeType", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.CourtConfigurations_CircuitMembers", "JudgeType", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.Configurations_Persons", "ImprisonStatusID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.Configurations_Persons", "NationalityID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.Configurations_OrganizationDetails", "OrganizationCategoryID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.Configurations_OrganizationDetails", "OrganizationTypeID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.Configurations_Lookups", "CategoryID", "dbo.Configurations_LookupCategories");
            DropForeignKey("dbo.Configurations_Courts", "CourtLevelID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.Cases_VictimsSessionsLog", "PresenceStatusID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.Cases_MasterCase", "CrimeType", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.Cases_MasterCase", "CrimeID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.Cases_DefendatnsSessionsLog", "PresenceStatusID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.Cases_DefendatnsSessionsLog", "CourtStatusID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.Cases_DefendatnsCaseLog", "PoliceStationStatusID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.Cases_DefendantsDecision", "PunishmentType", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.Cases_DefendantsCharges", "ChargeID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.Cases_DefendantsCharges", "DefendantCaseLogID", "dbo.Cases_DefendatnsCaseLog");
            DropForeignKey("dbo.Cases_CaseTransfer", "TransferTypeID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.CourtConfigurations_CircuitsPoliceStation", "CircuitID", "dbo.CourtConfigurations_Circuits");
            DropForeignKey("dbo.CourtConfigurations_CircuitRolls", "CircuitID", "dbo.CourtConfigurations_Circuits");
            DropForeignKey("dbo.CourtConfigurations_CircuitMembers", "CircuitID", "dbo.CourtConfigurations_Circuits");
            DropForeignKey("dbo.Security_Users", "CourtID", "dbo.Configurations_Courts");
            DropForeignKey("dbo.CourtConfigurations_CycleRolls", "CourtID", "dbo.Configurations_Courts");
            DropForeignKey("dbo.CourtConfigurations_CycleRolls", "CycleID", "dbo.CourtConfigurations_Cycles");
            DropForeignKey("dbo.CourtConfigurations_Circuits", "CourtID", "dbo.Configurations_Courts");
            DropForeignKey("dbo.Configurations_Prosecutions", "CourtID", "dbo.Configurations_Courts");
            DropForeignKey("dbo.Configurations_Prosecutions", "ParentID", "dbo.Configurations_Prosecutions");
            DropForeignKey("dbo.Configurations_Prosecuters", "ProsecutionID", "dbo.Configurations_Prosecutions");
            DropForeignKey("dbo.Security_Users", "PersonsId", "dbo.Configurations_Persons");
            DropForeignKey("dbo.Security_UserTypeActions", "UserTypeID", "dbo.Security_UserTypes");
            DropForeignKey("dbo.Security_UserTypeActions", "ActionID", "dbo.Security_Actions");
            DropForeignKey("dbo.Security_Pages", "ModuleID", "dbo.Security_Modules");
            DropForeignKey("dbo.Security_Actions", "PageID", "dbo.Security_Pages");
            DropForeignKey("dbo.Security_Users", "UserTypeID", "dbo.Security_UserTypes");
            DropForeignKey("dbo.CaseAction_UserType", "UserTypeID", "dbo.Security_UserTypes");
            DropForeignKey("dbo.Security_UsersLoginFailure", "UserID", "dbo.Security_Users");
            DropForeignKey("dbo.CustomUserRoles", "Security_Users_Id", "dbo.Security_Users");
            DropForeignKey("dbo.CustomUserLogins", "Security_Users_Id", "dbo.Security_Users");
            DropForeignKey("dbo.CourtConfigurations_Circuits", "AssistantSecretaryID", "dbo.Security_Users");
            DropForeignKey("dbo.CourtConfigurations_Circuits", "SecretaryID", "dbo.Security_Users");
            DropForeignKey("dbo.CourtConfigurations_CircuitRolls", "SecretaryID", "dbo.Security_Users");
            DropForeignKey("dbo.CourtConfigurations_CircuitMembers", "UserID", "dbo.Security_Users");
            DropForeignKey("dbo.CustomUserClaims", "Security_Users_Id", "dbo.Security_Users");
            DropForeignKey("dbo.Cases_CaseTransfer", "TransferedBy", "dbo.Security_Users");
            DropForeignKey("dbo.Configurations_Prosecuters", "PersonID", "dbo.Configurations_Persons");
            DropForeignKey("dbo.Configurations_OrganizationRepresentatives", "PersonID", "dbo.Configurations_Persons");
            DropForeignKey("dbo.Configurations_OrganizationRepresentatives", "OrganizationID", "dbo.Configurations_Persons");
            DropForeignKey("dbo.Configurations_OrganizationDetails", "ID", "dbo.Configurations_Persons");
            DropForeignKey("dbo.Cases_CaseWitnesses", "PersonID", "dbo.Configurations_Persons");
            DropForeignKey("dbo.Cases_WitnessesCaseLog", "WitnessID", "dbo.Cases_CaseWitnesses");
            DropForeignKey("dbo.Cases_CaseVictims", "PersonID", "dbo.Configurations_Persons");
            DropForeignKey("dbo.Cases_VictimsSessionsLog", "VictimID", "dbo.Cases_CaseVictims");
            DropForeignKey("dbo.Cases_VictimsCaseLog", "VictimID", "dbo.Cases_CaseVictims");
            DropForeignKey("dbo.Cases_CaseDefendants", "PersonID", "dbo.Configurations_Persons");
            DropForeignKey("dbo.Cases_CaseSessions", "ProsecuterID", "dbo.Configurations_Prosecuters");
            DropForeignKey("dbo.Configurations_PoliceStations", "ProsecutionID", "dbo.Configurations_Prosecutions");
            DropForeignKey("dbo.CourtConfigurations_CircuitsPoliceStation", "PoliceStationID", "dbo.Configurations_PoliceStations");
            DropForeignKey("dbo.Cases_MasterCase", "PoliceStationID", "dbo.Configurations_PoliceStations");
            DropForeignKey("dbo.Configurations_OverallNumbers", "ProsecutionID", "dbo.Configurations_Prosecutions");
            DropForeignKey("dbo.Cases_MasterCase", "OverallID", "dbo.Configurations_OverallNumbers");
            DropForeignKey("dbo.Cases_MasterCase", "ProsecutionID", "dbo.Configurations_Prosecutions");
            DropForeignKey("dbo.Cases_CaseObtainments", "ProsecutionID", "dbo.Configurations_Prosecutions");
            DropForeignKey("dbo.Configurations_Courts", "ParentID", "dbo.Configurations_Courts");
            DropForeignKey("dbo.Configurations_Courts_Crimes", "CourtID", "dbo.Configurations_Courts");
            DropForeignKey("dbo.Configurations_Courts_Crimes", "CrimeTypeID", "dbo.Cases_CrimeTypes");
            DropForeignKey("dbo.Cases_CaseRequests", "CourtID", "dbo.Configurations_Courts");
            DropForeignKey("dbo.Cases_Cases", "CircuitID", "dbo.CourtConfigurations_Circuits");
            DropForeignKey("dbo.Cases_SessionDecision", "SecondRollID", "dbo.CourtConfigurations_CircuitRolls");
            DropForeignKey("dbo.Cases_SessionDecision", "FirstRollID", "dbo.CourtConfigurations_CircuitRolls");
            DropForeignKey("dbo.Cases_CaseTransfer", "NewRollID", "dbo.CourtConfigurations_CircuitRolls");
            DropForeignKey("dbo.Cases_CaseTransfer", "OldRollID", "dbo.CourtConfigurations_CircuitRolls");
            DropForeignKey("dbo.Cases_CaseSessions", "RollID", "dbo.CourtConfigurations_CircuitRolls");
            DropForeignKey("dbo.Cases_Cases", "ProcedureTypeID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.Cases_Cases", "CaseLevelID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.Cases_Cases", "NoteStatusID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.Cases_Cases", "CaseStatusID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.Cases_CaseDocuments", "AttachmentTypeID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.Cases_CaseDefendants", "DefendantStatusID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.CaseAction_UserType", "ActionTypeID", "dbo.Configurations_Lookups");
            DropForeignKey("dbo.Cases_CaseObtainments", "CaseSessionID", "dbo.Cases_CaseSessions");
            DropForeignKey("dbo.Cases_CaseDocumentFolders", "SessionID", "dbo.Cases_CaseSessions");
            DropForeignKey("dbo.Cases_CaseDescription", "SessionID", "dbo.Cases_CaseSessions");
            DropForeignKey("dbo.Cases_CaseDocuments", "FolderID", "dbo.Cases_CaseDocumentFolders");
            DropForeignKey("dbo.CaseAction_UserType", "CaseActionID", "dbo.CaseActions");
            DropIndex("dbo.Cases_CaseTransmissionDocuments", new[] { "CaseTransmissionRequestID" });
            DropIndex("dbo.Cases_CaseTransmissionDocuments", new[] { "CaseDocumentID" });
            DropIndex("dbo.Cases_CaseRequestDocuments", new[] { "CaseRequestID" });
            DropIndex("dbo.Cases_CaseRequestDocuments", new[] { "CaseDocumentID" });
            DropIndex("dbo.Cases_ObtainmentDocument", new[] { "CaseObtainmentID" });
            DropIndex("dbo.Cases_ObtainmentDocument", new[] { "CaseDocumentID" });
            DropIndex("dbo.Cases_CaseObtainmentDocuments", new[] { "CaseObtainmentID" });
            DropIndex("dbo.Cases_CaseObtainmentDocuments", new[] { "CaseDocumentID" });
            DropIndex("dbo.Notifications", new[] { "NotificationActionID" });
            DropIndex("dbo.Cases_MasterCasesLog", new[] { "MasterCaseID" });
            DropIndex("dbo.Cases_CaseTransmission", new[] { "CaseID" });
            DropIndex("dbo.Cases_CaseTransmission", new[] { "TransmissionID" });
            DropIndex("dbo.Cases_CasesLog", new[] { "CaseID" });
            DropIndex("dbo.Cases_CaseNotes", new[] { "CaseID" });
            DropIndex("dbo.Cases_DefendatnsCaseLog", new[] { "PoliceStationStatusID" });
            DropIndex("dbo.Cases_DefendatnsCaseLog", new[] { "DefendantID" });
            DropIndex("dbo.Cases_DefendantsCharges", new[] { "DefendantCaseLogID" });
            DropIndex("dbo.Cases_DefendantsCharges", new[] { "ChargeID" });
            DropIndex("dbo.CourtConfigurations_CycleRolls", new[] { "CourtID" });
            DropIndex("dbo.CourtConfigurations_CycleRolls", new[] { "CycleID" });
            DropIndex("dbo.Security_Pages", new[] { "ModuleID" });
            DropIndex("dbo.Security_Actions", new[] { "PageID" });
            DropIndex("dbo.Security_UserTypeActions", new[] { "ActionID" });
            DropIndex("dbo.Security_UserTypeActions", new[] { "UserTypeID" });
            DropIndex("dbo.Security_UsersLoginFailure", new[] { "FallID" });
            DropIndex("dbo.Security_UsersLoginFailure", new[] { "UserID" });
            DropIndex("dbo.CustomUserRoles", new[] { "CustomRole_Id" });
            DropIndex("dbo.CustomUserRoles", new[] { "Security_Users_Id" });
            DropIndex("dbo.CustomUserLogins", new[] { "Security_Users_Id" });
            DropIndex("dbo.CourtConfigurations_CircuitMembers", new[] { "JudgeType" });
            DropIndex("dbo.CourtConfigurations_CircuitMembers", new[] { "CircuitID" });
            DropIndex("dbo.CourtConfigurations_CircuitMembers", new[] { "UserID" });
            DropIndex("dbo.CustomUserClaims", new[] { "Security_Users_Id" });
            DropIndex("dbo.Security_Users", new[] { "PersonsId" });
            DropIndex("dbo.Security_Users", new[] { "CourtID" });
            DropIndex("dbo.Security_Users", new[] { "UserTypeID" });
            DropIndex("dbo.Configurations_OrganizationRepresentatives", new[] { "PersonID" });
            DropIndex("dbo.Configurations_OrganizationRepresentatives", new[] { "OrganizationID" });
            DropIndex("dbo.Configurations_OrganizationDetails", new[] { "OrganizationCategoryID" });
            DropIndex("dbo.Configurations_OrganizationDetails", new[] { "OrganizationTypeID" });
            DropIndex("dbo.Configurations_OrganizationDetails", new[] { "ID" });
            DropIndex("dbo.Cases_WitnessesCaseLog", new[] { "WitnessID" });
            DropIndex("dbo.Cases_CaseWitnesses", new[] { "CaseID" });
            DropIndex("dbo.Cases_CaseWitnesses", new[] { "PersonID" });
            DropIndex("dbo.Cases_VictimsSessionsLog", new[] { "PresenceStatusID" });
            DropIndex("dbo.Cases_VictimsSessionsLog", new[] { "SessionID" });
            DropIndex("dbo.Cases_VictimsSessionsLog", new[] { "VictimID" });
            DropIndex("dbo.Cases_VictimsCaseLog", new[] { "VictimID" });
            DropIndex("dbo.Cases_CaseVictims", new[] { "CaseID" });
            DropIndex("dbo.Cases_CaseVictims", new[] { "PersonID" });
            DropIndex("dbo.Configurations_Persons", new[] { "ImprisonStatusID" });
            DropIndex("dbo.Configurations_Persons", new[] { "NationalityID" });
            DropIndex("dbo.Configurations_Prosecuters", new[] { "PersonID" });
            DropIndex("dbo.Configurations_Prosecuters", new[] { "ProsecutionID" });
            DropIndex("dbo.CourtConfigurations_CircuitsPoliceStation", new[] { "PoliceStationID" });
            DropIndex("dbo.CourtConfigurations_CircuitsPoliceStation", new[] { "CircuitID" });
            DropIndex("dbo.Configurations_PoliceStations", new[] { "ProsecutionID" });
            DropIndex("dbo.Configurations_OverallNumbers", new[] { "ProsecutionID" });
            DropIndex("dbo.Configurations_Prosecutions", new[] { "CourtID" });
            DropIndex("dbo.Configurations_Prosecutions", new[] { "ParentID" });
            DropIndex("dbo.Configurations_Courts_Crimes", new[] { "CrimeTypeID" });
            DropIndex("dbo.Configurations_Courts_Crimes", new[] { "CourtID" });
            DropIndex("dbo.Cases_CaseRequests", new[] { "CourtID" });
            DropIndex("dbo.Cases_CaseRequests", new[] { "CaseID" });
            DropIndex("dbo.Configurations_Courts", new[] { "CourtLevelID" });
            DropIndex("dbo.Configurations_Courts", new[] { "ParentID" });
            DropIndex("dbo.CourtConfigurations_Circuits", new[] { "CycleID" });
            DropIndex("dbo.CourtConfigurations_Circuits", new[] { "CrimeType" });
            DropIndex("dbo.CourtConfigurations_Circuits", new[] { "AssistantSecretaryID" });
            DropIndex("dbo.CourtConfigurations_Circuits", new[] { "SecretaryID" });
            DropIndex("dbo.CourtConfigurations_Circuits", new[] { "CourtID" });
            DropIndex("dbo.CourtConfigurations_CircuitRolls", new[] { "SecretaryID" });
            DropIndex("dbo.CourtConfigurations_CircuitRolls", new[] { "CircuitID" });
            DropIndex("dbo.Cases_CaseTransfer", new[] { "TransferedBy" });
            DropIndex("dbo.Cases_CaseTransfer", new[] { "TransferTypeID" });
            DropIndex("dbo.Cases_CaseTransfer", new[] { "NewRollID" });
            DropIndex("dbo.Cases_CaseTransfer", new[] { "OldRollID" });
            DropIndex("dbo.Cases_CaseTransfer", new[] { "CaseID" });
            DropIndex("dbo.Configurations_Lookups", new[] { "CategoryID" });
            DropIndex("dbo.Cases_DefendatnsSessionsLog", new[] { "CourtStatusID" });
            DropIndex("dbo.Cases_DefendatnsSessionsLog", new[] { "PresenceStatusID" });
            DropIndex("dbo.Cases_DefendatnsSessionsLog", new[] { "SessionID" });
            DropIndex("dbo.Cases_DefendatnsSessionsLog", new[] { "DefendantID" });
            DropIndex("dbo.Cases_CaseDescription", new[] { "SessionID" });
            DropIndex("dbo.Cases_CaseDescription", new[] { "CaseID" });
            DropIndex("dbo.Cases_CaseSessions", new[] { "ProsecuterID" });
            DropIndex("dbo.Cases_CaseSessions", new[] { "RollID" });
            DropIndex("dbo.Cases_CaseSessions", new[] { "CaseID" });
            DropIndex("dbo.Cases_SessionDecision", new[] { "SecondRollID" });
            DropIndex("dbo.Cases_SessionDecision", new[] { "FirstRollID" });
            DropIndex("dbo.Cases_SessionDecision", new[] { "DecisionTypeID" });
            DropIndex("dbo.Cases_SessionDecision", new[] { "CaseSessionID" });
            DropIndex("dbo.Cases_DefendantsDecision", new[] { "PunishmentType" });
            DropIndex("dbo.Cases_DefendantsDecision", new[] { "SessionDessionId" });
            DropIndex("dbo.Cases_DefendantsDecision", new[] { "CaseDefendantId" });
            DropIndex("dbo.Cases_CaseDefendants", new[] { "DefendantStatusID" });
            DropIndex("dbo.Cases_CaseDefendants", new[] { "CaseID" });
            DropIndex("dbo.Cases_CaseDefendants", new[] { "PersonID" });
            DropIndex("dbo.Cases_Cases", new[] { "NoteStatusID" });
            DropIndex("dbo.Cases_Cases", new[] { "CaseStatusID" });
            DropIndex("dbo.Cases_Cases", new[] { "ProcedureTypeID" });
            DropIndex("dbo.Cases_Cases", new[] { "CaseLevelID" });
            DropIndex("dbo.Cases_Cases", new[] { "MasterCaseID" });
            DropIndex("dbo.Cases_Cases", new[] { "CircuitID" });
            DropIndex("dbo.Cases_CaseDocumentFolders", new[] { "SessionID" });
            DropIndex("dbo.Cases_CaseDocumentFolders", new[] { "CaseID" });
            DropIndex("dbo.Cases_CaseDocuments", new[] { "FolderID" });
            DropIndex("dbo.Cases_CaseDocuments", new[] { "AttachmentTypeID" });
            DropIndex("dbo.Cases_CaseObtainments", new[] { "CaseSessionID" });
            DropIndex("dbo.Cases_CaseObtainments", new[] { "MasterCaseID" });
            DropIndex("dbo.Cases_CaseObtainments", new[] { "ProsecutionID" });
            DropIndex("dbo.Cases_MasterCase", new[] { "NotificationActionID" });
            DropIndex("dbo.Cases_MasterCase", new[] { "CrimeType" });
            DropIndex("dbo.Cases_MasterCase", new[] { "OverallID" });
            DropIndex("dbo.Cases_MasterCase", new[] { "CrimeID" });
            DropIndex("dbo.Cases_MasterCase", new[] { "ProsecutionID" });
            DropIndex("dbo.Cases_MasterCase", new[] { "PoliceStationID" });
            DropIndex("dbo.CaseAction_UserType", new[] { "ActionTypeID" });
            DropIndex("dbo.CaseAction_UserType", new[] { "CaseActionID" });
            DropIndex("dbo.CaseAction_UserType", new[] { "UserTypeID" });
            DropTable("dbo.Cases_CaseTransmissionDocuments");
            DropTable("dbo.Cases_CaseRequestDocuments");
            DropTable("dbo.Cases_ObtainmentDocument");
            DropTable("dbo.Cases_CaseObtainmentDocuments");
            DropTable("dbo.CustomRoles");
            DropTable("dbo.NotificationItemTypes");
            DropTable("dbo.DeletedOverallNum");
            DropTable("dbo.CourtConfigurations_WorkDays");
            DropTable("dbo.CourtConfigurations_Vacations");
            DropTable("dbo.Configurations_CaseTypes");
            DropTable("dbo.Cases_JudgmentReasons");
            DropTable("dbo.Cases_DocumentsLog");
            DropTable("dbo.Notifications");
            DropTable("dbo.Cases_MasterCasesLog");
            DropTable("dbo.Cases_CasesTransmissionRequests");
            DropTable("dbo.Cases_CaseTransmission");
            DropTable("dbo.Cases_CasesLog");
            DropTable("dbo.Cases_CaseNotes");
            DropTable("dbo.Configurations_DecisionTypes");
            DropTable("dbo.Configurations_LookupCategories");
            DropTable("dbo.Cases_DefendatnsCaseLog");
            DropTable("dbo.Cases_DefendantsCharges");
            DropTable("dbo.CourtConfigurations_Cycles");
            DropTable("dbo.CourtConfigurations_CycleRolls");
            DropTable("dbo.Security_Modules");
            DropTable("dbo.Security_Pages");
            DropTable("dbo.Security_Actions");
            DropTable("dbo.Security_UserTypeActions");
            DropTable("dbo.Security_UserTypes");
            DropTable("dbo.Security_UsersLoginFailure");
            DropTable("dbo.CustomUserRoles");
            DropTable("dbo.CustomUserLogins");
            DropTable("dbo.CourtConfigurations_CircuitMembers");
            DropTable("dbo.CustomUserClaims");
            DropTable("dbo.Security_Users");
            DropTable("dbo.Configurations_OrganizationRepresentatives");
            DropTable("dbo.Configurations_OrganizationDetails");
            DropTable("dbo.Cases_WitnessesCaseLog");
            DropTable("dbo.Cases_CaseWitnesses");
            DropTable("dbo.Cases_VictimsSessionsLog");
            DropTable("dbo.Cases_VictimsCaseLog");
            DropTable("dbo.Cases_CaseVictims");
            DropTable("dbo.Configurations_Persons");
            DropTable("dbo.Configurations_Prosecuters");
            DropTable("dbo.CourtConfigurations_CircuitsPoliceStation");
            DropTable("dbo.Configurations_PoliceStations");
            DropTable("dbo.Configurations_OverallNumbers");
            DropTable("dbo.Configurations_Prosecutions");
            DropTable("dbo.Cases_CrimeTypes");
            DropTable("dbo.Configurations_Courts_Crimes");
            DropTable("dbo.Cases_CaseRequests");
            DropTable("dbo.Configurations_Courts");
            DropTable("dbo.CourtConfigurations_Circuits");
            DropTable("dbo.CourtConfigurations_CircuitRolls");
            DropTable("dbo.Cases_CaseTransfer");
            DropTable("dbo.Configurations_Lookups");
            DropTable("dbo.Cases_DefendatnsSessionsLog");
            DropTable("dbo.Cases_CaseDescription");
            DropTable("dbo.Cases_CaseSessions");
            DropTable("dbo.Cases_SessionDecision");
            DropTable("dbo.Cases_DefendantsDecision");
            DropTable("dbo.Cases_CaseDefendants");
            DropTable("dbo.Cases_Cases");
            DropTable("dbo.Cases_CaseDocumentFolders");
            DropTable("dbo.Cases_CaseDocuments");
            DropTable("dbo.Cases_CaseObtainments");
            DropTable("dbo.Cases_MasterCase");
            DropTable("dbo.CaseActions");
            DropTable("dbo.CaseAction_UserType");
        }
    }
}
