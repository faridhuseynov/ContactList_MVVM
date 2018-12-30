namespace ContactList_MVVM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAsPerBucketList : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "Name", c => c.String(nullable: false));
        }
    }
}
