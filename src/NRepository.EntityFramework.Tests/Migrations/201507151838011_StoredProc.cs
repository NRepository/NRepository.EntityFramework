namespace NRepository.EntityFramework.Tests
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoredProc : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("GetCounter", "select 99");
        }
        
        public override void Down()
        {
        }
    }
}
