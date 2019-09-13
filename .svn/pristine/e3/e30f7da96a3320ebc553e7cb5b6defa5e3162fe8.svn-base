namespace EntidadesKor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.status_fiscalizacao", "desc_status_fiscalizacao", c => c.String(maxLength: 100, unicode: false));
            DropColumn("dbo.status_fiscalizacao", "fiscalizacao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.status_fiscalizacao", "fiscalizacao", c => c.String(maxLength: 100, unicode: false));
            DropColumn("dbo.status_fiscalizacao", "desc_status_fiscalizacao");
        }
    }
}
