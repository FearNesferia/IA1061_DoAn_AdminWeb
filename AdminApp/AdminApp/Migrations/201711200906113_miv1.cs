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
                        Id = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Path = c.String(),
                        QueryString = c.String(),
                        Payload = c.String(),
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
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TrafficPackages");
        }
    }
}
