namespace EntidadesKor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.emergencia", "usuario_usuario_id1", c => c.Int());
            CreateIndex("dbo.emergencia", "usuario_usuario_id1");
            AddForeignKey("dbo.emergencia", "usuario_usuario_id1", "dbo.usuario", "usuario_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.emergencia", "usuario_usuario_id1", "dbo.usuario");
            DropIndex("dbo.emergencia", new[] { "usuario_usuario_id1" });
            DropColumn("dbo.emergencia", "usuario_usuario_id1");
        }
    }
}
