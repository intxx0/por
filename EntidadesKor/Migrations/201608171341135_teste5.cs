namespace EntidadesKor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tipo_materia_prima", "desc_tipo_materia_prima", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tipo_materia_prima", "desc_tipo_materia_prima");
        }
    }
}
