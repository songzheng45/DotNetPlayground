namespace Users.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNicknameProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Nickname", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Nickname");
        }
    }
}
