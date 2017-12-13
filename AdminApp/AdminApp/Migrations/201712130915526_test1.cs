namespace AdminApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Websites", "NumberOfPackageInTree", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Websites", "NumberOfPackageInTree");
        }
    }
}
