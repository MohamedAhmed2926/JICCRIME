using JIC.Base.Views.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base
{
    #region Defaults

    #region PopoverDiractions

    public enum PopoverDiractions
    {
        Top,
        Left,
        Right,
        Bottom,
    }

    #endregion

    #region PopoverTriggers

    public enum PopoverTriggers
    {
        Hover,
        Focus
    }

    #endregion

    #region CalendarViewModes

    public enum CalendarViewModes
    {
        Days,
        Months,
        Years,

    }

    #endregion

    #region CalendarModes

    public enum CalendarModes
    {
        Date,
        Time,
        DateTime,
    }

    #endregion

    #region DropDownListSelectedValueModes

    public enum DropDownListSelectedValueModes
    {
        ThrowException,
        ClearSelection,
        //InDataBinding,
    }

    #endregion

    #region TextBoxDataTypes --
    public enum TextBoxDefaultData
    {
        empty,
        CurrentDate,
        CurrentYear,
    }
    public enum TextBoxDataTypes
    {
        String,
        Int,
        Double,
        Email,
        Mobile,
        NationalID,
        Year,
        Username,
        Alphapet,
        Phone,
    }
    #endregion

    #region ControlSize

    public enum ControlSizes
    {
        Large,
        Medium,
        Small
    }

    #endregion

    #region ButtonStyles

    public enum ButtonStyles
    {
        Primary,
        Alt,
        Badge,
    }

    #endregion

    #region ButtonTypes

    public enum ButtonTypes
    {
        Button,
        Link,
    }

    #endregion

    #region MessageTypes

    public enum MessageTypes
    {
        Success,
        Error,
        Warning,
        Information,
    }

    #endregion

    #region Modes

    public enum Modes
    {
        Add,
        Update,
        Default,
        View,
        UsersMang,
        Delete,
        UpdateCurrentFormation,
        UpdateNewFormation
    }
    public enum UpdateModes : int
    {
        UpdateWithSaving,
        UpdateWithoutSaving,
        UpdateName,
    }
    #endregion 

    #region  Documents

    public enum FolderFlag
    {
        Folder,
        File,
    }

    public enum DocumentCategory
    {
        File = 0,
        Folder = 1,
    }

    #endregion

    #region FileTypes

    public enum FileTypes
    {
        Images = 2,
        Text = 5,
        Pdf = 15,
        Word = 24,
        PowerPoint = 35,
        OldExcel = 50,
        Excel = 49,
        Sound = 91,
        Video = 100,
        Winrar = 166,
        Zip = 207,
        CSV = 9000,
        AnyType = 1098
    }

    #endregion

    #region EmailFunctions

    public enum EmailFunctions
    {
        Default,
        UserAccountActivation,
        UserPasswordReset,
        Payslip
    }

    #endregion

    #region SMTPProperties

    public enum SMTPProperties
    {
        Host,
        Port,
        From,
        Username,
        Password,
        EnableSSL
    }

    #endregion

    #region AttachmenSources
    public enum AttachmenSources
    {

    }
    #endregion

    #region AddonTypes

    public enum AddonTypes
    {
        RadioButton
    }

    #endregion

    #region SystemReports

    public enum SystemReports
    {
        CaseSessionRepo,
        SessionRoll,
        JudgRoll,

    }
    public enum ReportParamter
    {
        PrintUser,
        CourtId,
        RollID,
        CaseSessionID,
        CaseID
    }

    public enum ReportName
    {
        SessionMenuts,
    }
    #endregion

    #region SelectizeModes
    public enum SelectizeModes
    {
        Single = 0,
        Tags
    }
    #endregion

    #region SelectizeDataSourceModes
    public enum SelectizeDataSourceModes
    {
        Local = 0,
        Remote
    }
    #endregion

    #region Routings
    public enum Routings
    {
        #region General
        Default,
        Error,
        #endregion

        #region Security
        ManageUsers,
        Login,
        Reset,
        CurrentUsers,
        #endregion

        #region Configrations
        MangeProsecuters,
        EditNormalPerson,
        #endregion

        #region CourtConfigrations
        Circuits,
        OpenSessionRoll,
        MoveCases,
        TextPredections,
        SessionRollReview,
        CaseSessionApprove,
        #endregion

        #region Cases
        AddCase,
        RollOrder,
        CaseSearch,
        SessionSearch,
        ViewCase,
        Session,
        AdministrativePostpound,
        AdministrativePostApproval,
        JudgmentOnDefendant,
        RollApprove,
        ExpertPayment,
        EditCase,
        CaseJudgment,
        ElementaryCase,
        UrgeCaseFromSuspend,
        ReJudge,
        GroupJudgment,
        ReExecute,

        #endregion

        #region Test
        DocumentsUpload,
        Test,
        AddNewCase,
        SystemLookups,
        CaseBasicData,
        CaseParties,
        AddNormalPerson,
        AddConsideredPerson,
        TestForm,
        ObjectionBasicData,

        #endregion

        #region Rolls
        JudgRoll,
        SessionRoll,
        Vacations,
        EditObjection,
        EditElementaryCase,
        NotAuthorised,
        EditLegalPerson,
        CaseSessionRoll,
        CaseSessionReportsReview,
        #endregion


    }
    #endregion

    #region SystemErrorMessages
    public enum SystemErrorMessages : byte
    {
        NotAuthorizedAccess = 1,
        WrongValu = 2,
    }

    #endregion

    #endregion

    #region Enums

    #region LookupsCategories
    public enum DecreeType
    {
        Judgement,
        Decision
    }

    public enum LookupsCategories : int
    {
        Nationalities = 1,
        OrganizationsTypes = 2,
        OrganizationsClassification = 3,
        CasesLevels = 4,
        CaseProcedureTypes = 5,
        CourtDefendantsStatuses = 6,
        PoliceStationDefendantsStatuses = 7,
        PresenceStatuses = 8,
        CaseStatuses = 9,
        AttachementTypes = 10,
        JudgLevel = 11,
        Crimes = 12,
        TransferType = 13,
        PunishmentTypes = 14,
        LogonFailure,
        ResumptionStatus = 16,
        ProsecutionResumptionCause = 17,
        JudgePodiumType = 18,
        Citites = 19,
        LawyerLevel = 20
    }

    #region TransferTypes

    public enum TransferTypes : int
    {
        AdminstrativeTransfer = 47
    }

    #endregion

    #region PartyTypes
    public enum PartyTypes : int
    {
        All = 0,
        Victim = 1,
        Defendant = 2,
        VictimAndDefendant = 3,
        DefendantWithObjection = 4,
        PartiesWithPresence = 5
    }
    #endregion

    #region PoliceStationDefendantsStatuses
    public enum PoliceStationDefendantsStatuses : int
    {
        Fugitive = 20, //هارب
        Arrested = 19, //محبوس
        UnWanted = 21, //غير مطلوب
    }
    #endregion

    #region CourtDefendantsStatuses
    public enum CourtDefendantsStatuses : int
    {
        Arrested = 16, //محبوس
        Fugitive = 17, //هارب
        UnWanted = 18, //غير مطلوب

    }
    #endregion

    #endregion

    #region OrganizationsTypes
    public enum OrganizationsTypes : int
    {
        Industiral = 4,
        Commercial = 5,
        Construction = 6
    }
    #endregion

    #region OrganizationsClassification
    public enum OrganizationsClassification : int
    {
        Industiral = 7,
        Commercial = 8,
        Construction = 9
    }
    #endregion

    #region CaseStatuses
    public enum CaseStatuses : int
    {
        New = 26,
        InPrgoress = 27,
        FinalDecision = 28,
        PostDecision = 29,
        ReadyForFinalDecision = 30,
        Suspended = 31,
        decision = 89
    }
    #endregion

    #region CaseLevels
    public enum CaseLevels : int
    {
        Initial = 10,
        Elementary = 11,
        Cassation = 12
    }
    #endregion

    #region RollStatus
    public enum RollStatus : int
    {
        NotStarted = 1,
        InProgress = 2,
        Finished = 3,
        NotSorted = 4,
        HasPostpoundCases = 5,
        HasIncomingPostpoundCases = 6,
        HasNoPostpoundCases = 7,
        EmptyRoll = 8,
        PreviousRollNotClosed = 9,
        OtherCaseTypesOpen = 10,
        OtherRollOpenForSecretary = 11,
        NotOrder = 12,

    }
    #endregion

    #region CaseProcedureTypes
    public enum CaseProcedureTypes : int
    {
        Case = 13,
        Objection = 14,
        ProsecutionJudgment = 15,
    }
    #endregion

    #region DecisionLevels
    public enum DecisionLevels : int
    {
        Final = 11,
        Post = 10,
        Decision = 1,
        ProsecutionJudgment = 4,

    }

    public enum DefendantStatus : int
    {
        NotGuilty = 1,
        Guilty = 2
    }
    #endregion

    #region Crimes

    public enum Crimes : int
    {
        Steal = 1,
        Strike = 2,
    }

    #endregion

    #region DecisionTypes
    public enum DecisionTypes : int
    {
        //Judges//Temporary
        L2_Investigations = 1, //تحقيق
        L2_Experts = 2,//الخبراء
        L2_Forensic = 3, //طب شرعى
        //Judges//Permenant
        L1_NotGuilty = 4,
        L1_Guilty = 5,
        //Decisions
        L3_ReferToTechnicalOffice = 6,// احاله للمكتب الفنى
        L3_Postponed = 7, // تاجيل نظر الدعوى
        
        L3_Suspend_Temporary = 8,// توقف عن النظام - تعليقى
        L3_Suspend_Progress = 9,// توقف عن النظام - سير
        L3_InvalidExpert_Loyal = 10,// عدم اخنصاص ولائى
        L3_InvalidExpert_Specific = 11,//نوعى
        L3_InvalidExpert_Local = 12//محلى

    }
    #endregion

    #region UserTypes
    public enum SystemUserTypes : int
    {
        ElementaryCourtAdministrator = 1,
        Judge = 2,
        Secretary = 3,
        schedualEmployee = 4,
        InitialCourtAdministrator = 5,
        CourtHead = 6,
        InquiriesEmployee = 7,
        JICAdmin = 8,
        ImplementationEmployee = 9,
        CriminalDepManager = 10,
        DataEntry = 11,
    }
    #endregion

    #region DropDownListDataTypes

    public enum DropDownListDataTypes
    {
        Courts,
        ElementaryCourts,
        InitalCourts,
        CourtsUserAccess,
    }

    #endregion

    #region ExecutionModes
    public enum ExecutionModes
    {
        Default,
        ReExecution
    }
    #endregion
    #region SearchCaseModes
    public enum SearchCaseModes
    {
        Default,
        ObjectionRequestMode,
        ResumptionRequestMode,
        MoveCase,
        ExpertPay,
        EditDecision,
        UrgeFromSuspend,
        EditObjectionDecision,
        EditResumptionDecision,
        ShowCasesOnly,
        OpenRollCaseMode
    }
    #endregion
    #region SearchCaseModes
    public enum CaseDecisionModes
    {
        Default,

        EditDecision
    }
    #endregion
    #region SearchSessionMode
    public enum SearchSessionModes
    {
        Default,
        Reports
    }
    #endregion

    #region LogonFailure
    public enum LogonFailure : int
    {
        LogOnSuccess = 52,
        UnknownUsername = 57,
        BadPassword = 51,

        AccountLocked = 53,
        AccountExpired = 54,
        NotAllowedAccess = 55,
        LastFallAndLocked = 58,
        HackTry = 56,

    }
    #endregion

    #region PresenceStatus
    public enum PresenceStatus
    {
        PersonalAttendance = 22,
        ConsederationAttendance = 23,
        AttornyAttendance = 24,
        AbsenceAttendance = 25
    }
    #endregion
    #region ResumptionIdentities

    public enum ResumptionIdentities : int
    {
        Prosecution = 59,
        Defendant = 60,
        VictimCivilRight = 61
    }

    #endregion
    #region AddDefendantModes
    public enum AddDefendantModes : int
    {
        ObjectionMode,
        ResumptionMode,
        ProsecutionResumptionMode

    }
    #endregion

    #region JudgePodiumType

    public enum JudgePodiumType : int
    {
        HeadJudge = 64,
        RightJudge = 65,
        LeftJudge = 66,
        LeftLeftJudge = 67,
        LeftLeftLeftJudge = 68,
        LeftLeftLeftLeftJudge = 69,
        LeftLeftLeftLeftLeftJudge = 70,
        OptionalJudge = 71,

    }
    #endregion

    public enum CaseResultType
    {
        Decision =1,
        judgment = 2
    }
    public enum Enum_CrimeType
    {
        Journalist = 1,
        Normal = 2,
        terrorism = 3
    }
    public enum SaveMinutesOfSessionStatus
    {
        Succeeded,
        Failed,
        SessionApprovedByJudge,
        SessionSentToJudge

    }

    public enum ApproveByJudgeStatus
    {
        SessionApprovedSuccessfully,
        Failed_DefectsPresence,
        Failed_Session,
        Failed_Decision,
        Failed_Attachements,
        Failed,
        SessionSentSuccessfully,
    }
    public enum CaseType
    {
        Fault,
        Crime

    }
    #endregion

    #region Login
    public enum UserStatus
    {
        UserNameExist,
        EmptyMobile,
        AddSuccess,
        Failed,
        LevelMust,
        prsecutionMust,
        CourtMust,
        MobileNotValid
    }
    public enum Deactive
    {
        Deactive,
        CannotDeactive,
        failed

    }

    public enum LoginStatus
    {
        LoginSuccess,
        UserNameNotCorrect,
        PasswordNotCorrect,
        UserBlocked,
        UserNotActive,
        UserNeedsResetPassword,
        EmptyUserName,

    }
    #endregion

    public enum ProsecutorStatus
    {
        Succeeded,
        Failed,
        ProsecuterHasSession,
        NationalNO_Exist_Before
    }

    public enum LawyerStatus
    {
        Succeeded,
        Failed,
        NationalNO_Exist_Before,
        CardNumber_Exist_Before
    }

    public enum AddTextStatus
    {
        AddSuccefull,
        FailedToAdd,
        SameTitle
    }

    public enum EditTextStatus
    {
        EditSuccefull,
        FailedToEdit,
        SameTitle
    }
    public enum WeekDays
    {
        Saturday = 1,
        Monday = 2,

    }
    public enum Cycle
    {
        FirstCycle = 1,
        SecondCycle = 2,
        ThridCycle = 3,
        FourthCycle = 4,
        FirstSeperator = 5,
        SecondSeperator = 6

    }
    public enum SaveCircuitStatus
    {
        Saved_Successfully,
        Failed_To_Save,
        Saved_Before,
        Judge_Used_Twice,
        Failed_To_Save_Judges,
        Secretary_Used_Twice,
        CircuitStartDateBeforeToday
    }
    public enum CaseStatus
    {
        Compelet,
        noncomplet,
        New,
        CaseParties,
        OrderOfAssigment,
        CaseAttachments,
        previousdecisions,
        Edit,
        GoInFlow,
        NotGoInFlow


    }

    public enum StatusCheck
    {

        OrderOFAssignmentExist,
        OrderOFAssignmentNotExist
    }
    public enum PageMode
    {
        New,
        Create,
        Edit,
        Delete
    }
    public enum TestStat
    {
        Pass, Fail
    }
    #region Nationality
    public enum Nationality
    {
        Egyptian = 1
    }
    #endregion

    public enum SaveStatus
    {
        Saved,
        Failed_To_Save,
        Saved_Before,
        WorkingDay
    }

    public enum DeleteStatus
    {
        Deleted = 1,
        NotDeleted = 2

    }

    public enum DeleteCircuitStatus
    {
        Deleted = 1,
        NotDeleted = 2,
        CaseConnectedToCircuit = 3,
        CircuitStartDateBeforeToday = 4

    }
    public enum NotCompleteStatus
    {
        // خصوم القضية - وجود خصم واحد على الأقل من نوع متهم.
        Defendent = 1,

        //وجود مرفقات على القضية : مرفق واحد على الأقل لهذه الانواع (أمر احالة ، أدلة الثبوت)
        //، مرفق واحد على الأقل من نوع (حرز) في حالة وجود حرز على القضية.
       
        // امر الاحالة
        OrderOfAssignment = 2,
         Document = 3,
        OverAllNumber = 4,
        Complete = 5,
       

    }
    public enum AddOverAllStatus
    {
        Defendent = 1,
        Document = 2,
        Description = 4,
        Saved = 5,
        Fail = 6,
        OverAllReserved = 7,
        Error = 8,
            Obtainment= 9
    }
    public enum PersonStatus
    {
        SuccefullSave,
        NationalIDNotValid,
        FailedSave = ErrorCode.Failed,
        NatIDExist,
        ProsecutorSessionExist,
    }
    #region SignIn
    public enum SignInStatus
    {
        //
        // Summary:
        //     Sign in was successful
        Success = 0,
        //
        // Summary:
        //     User is locked out
        LockedOut = 1,
        //
        // Summary:
        //     Sign in requires addition verification (i.e. two factor)
        RequiresVerification = 2,
        //
        // Summary:
        //     Sign in failed
        Failure = 3,
        Blocked = 4,
        PasswordNeedChange = 5,
        NotInCircuit=6,
        ResetFailerAccess,
    }
    #endregion

    public enum SessionSearchMode
    {
        All = 0,
        NotReservedSession = 1,
        LastSessionDate = 2,
        NextSessionDate = 3
    }
    public enum AddOrder
    {
        AddSuccefull,
        FailedToAdd,
    }

    public enum EditAssignment
    {
        EditSuccefull,
        FailedToEdit,
        NoUpdateOverAllNumberExist,
        NoUpdateThereIsASession,
    }
    public enum SaveRollOrderStatus
    {
        SuccessFull,
        Failed,
        RollOpened
    }

    public enum SaveDefectsStatus : int
    {
        Saved,
        Failed_To_Save = ErrorCode.Failed,
        Saved_Before,
        DefendantsPresenceFailed,
        SessionSentToJudge
    }
    public enum SavePartSOrder
    {
        SavedOrder,
        Faild_To_Save,
    }
    public enum DeleteDefectStatus
    {
        Deleted,
        NotDeleted

    }
    public enum SetCasesRollStatus
    {
        Succefull,
        Failed,
    }
    public enum AttachmentTypes : int
    {
        Witness = 1,
        Referral= 81,//أمر احالة
        ProofOfEvidence = 82, //أدلة ثبوت
        Obtainment = 80 //حرز
    }

    public enum UpdatePresenceStatus
    {
        Updated_Successfully,
        Failed_To_Update


    }

    public enum AttachmentsSaveStatus
    {
        Saved,
        Failed,
        Folder_Has_Documents,
        NumberOfDocumentOverFlow,
        UserNotCreate
    }

    public enum CircuitRollsSavestatus
    {
        Saved,
        Failed
    }

    public enum SaveDecisionStatus
    {
        Saved,
        RollNotOpenedYet,
        Failed_To_Save,
        SentToJudge,
        SessionSentToJudge
    }


    public enum CaseSaveStatus
    {
        Saved,
        Failed = Views.Services.ErrorCode.Failed,
        SecondNumberExistBefore,
        Saved_Before
    }
    public enum DecreeTypes
    {
        Judgement =2,
        Decision = 1
    }
    public enum MinutesStatus
    {
        //تم ادخال المحضر 
       Saved,
       NotComplete,
       NotSaved,
    }
    public enum ObtainmentStatus
    {
        All=1,
        HasObtainment=2,
        NotHasObtainment=3

    }
    public enum DeleteUserStatus
    {
Succeeded,
IsSecretary,
IsMember,
        Failed
    }

    public enum FaultCourt : int
    {
        NewCairoCourt = 1
    }

}
