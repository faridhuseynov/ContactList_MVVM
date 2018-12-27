namespace ContactList_MVVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredRuleAdd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contacts", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Contacts", new[] { "CategoryId" });
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Contacts", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Contacts", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Contacts", "CategoryId", c => c.Int());
            CreateIndex("dbo.Contacts", "CategoryId");
            AddForeignKey("dbo.Contacts", "CategoryId", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contacts", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Contacts", new[] { "CategoryId" });
            AlterColumn("dbo.Contacts", "CategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Contacts", "Phone", c => c.String());
            AlterColumn("dbo.Contacts", "Name", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String());
            CreateIndex("dbo.Contacts", "CategoryId");
            AddForeignKey("dbo.Contacts", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
