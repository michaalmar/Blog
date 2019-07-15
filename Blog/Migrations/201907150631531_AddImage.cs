namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Post", "Image", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Post", "Image");
        }
    }
}
