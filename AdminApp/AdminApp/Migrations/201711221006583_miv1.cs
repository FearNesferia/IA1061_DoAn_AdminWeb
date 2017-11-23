namespace AdminApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class miv1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrafficPackages",
                c => new
                    {
                        TrafficPackageId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Path = c.String(),
                        QueryString = c.String(),
                        Payload = c.String(),
                        IsChecked = c.Boolean(nullable: false),
                        IsAttack = c.Boolean(nullable: false),
                        LengthOfArguments = c.Int(nullable: false),
                        NumberOfArguments = c.Int(nullable: false),
                        NumberOfDigitsInArguments = c.Int(nullable: false),
                        NumberOfOtherCharInArguments = c.Int(nullable: false),
                        NumberOfDigitsInPath = c.Int(nullable: false),
                        NumberOfSpecialCharInArguments = c.Int(nullable: false),
                        LengthOfPath = c.Int(nullable: false),
                        LengthOfRequest = c.Int(nullable: false),
                        NumberOfLettersInArguments = c.Int(nullable: false),
                        NumberOfLettersCharInPath = c.Int(nullable: false),
                        NumberOfSepicalCharInPath = c.Int(nullable: false),
                        WebsiteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrafficPackageId)
                .ForeignKey("dbo.Websites", t => t.WebsiteId, cascadeDelete: true)
                .Index(t => t.WebsiteId);
            
            CreateTable(
                "dbo.Websites",
                c => new
                    {
                        WebsiteId = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false),
                        IsDetecMode = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.WebsiteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrafficPackages", "WebsiteId", "dbo.Websites");
            DropIndex("dbo.TrafficPackages", new[] { "WebsiteId" });
            DropTable("dbo.Websites");
            DropTable("dbo.TrafficPackages");
        }
    }
}
