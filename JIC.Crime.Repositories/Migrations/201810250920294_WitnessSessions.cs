namespace JIC.Crime.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WitnessSessions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
              "dbo.Cases_WitnessSessionLog",
              c => new
              {
                  WitnessID = c.Long(nullable: false),
                  CaseID = c.Int(nullable: false),
                  SessionID = c.Long(nullable: false),
                  PresenceStatus = c.Int(nullable: false),
                  WitnessTestimony = c.String(nullable: false),
                  TestimonyFileData = c.Binary(nullable: true)
              })
              .PrimaryKey(t => t.WitnessID)
              .ForeignKey("dbo.Cases_CaseWitnesses", t => t.WitnessID, cascadeDelete: true)
              .ForeignKey("dbo.Cases_CaseSessions", t => t.SessionID, cascadeDelete: true)
              .ForeignKey("dbo.Cases_Cases", t => t.CaseID, cascadeDelete: true)
              .Index(t => t.WitnessID, unique: true);

               
        }
        
        public override void Down()
        {
        }
    }
}
