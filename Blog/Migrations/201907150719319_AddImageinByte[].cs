namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageinByte : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Post", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Post", "Image", c => c.Byte(nullable: false));
        }
    }
}
