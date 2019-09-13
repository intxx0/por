namespace EntidadesKor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimeiroMigrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.guia",
                c => new
                    {
                        id_guia = c.Int(nullable: false, identity: true),
                        numero_guia = c.String(),
                        nome_emissor = c.String(),
                        cnpj_emissor = c.String(),
                        municipio_emissor = c.String(),
                        nome_origem = c.String(),
                        municipio_origem = c.String(),
                        nome_destino = c.String(),
                        municipio_destino = c.String(),
                        placa_registro = c.String(),
                        numero_notafiscal = c.String(),
                        data_emissao_guia = c.DateTime(),
                        data_validade_inicio = c.DateTime(),
                        data_validade_fim = c.DateTime(),
                        ativo = c.Int(),
                    })
                .PrimaryKey(t => t.id_guia);
            
            CreateTable(
                "dbo.item_guia",
                c => new
                    {
                        id_item_guia = c.Int(nullable: false, identity: true),
                        id_tipo_materia_prima = c.Int(),
                        descricao_item = c.String(),
                        id_guia = c.Int(),
                        qtd_item = c.Int(),
                        unidade_item = c.String(),
                        valor_item = c.Decimal(precision: 18, scale: 2),
                        ativo = c.Int(),
                    })
                .PrimaryKey(t => t.id_item_guia)
                .ForeignKey("dbo.guia", t => t.id_guia)
                .ForeignKey("dbo.tipo_materia_prima", t => t.id_tipo_materia_prima)
                .Index(t => t.id_tipo_materia_prima)
                .Index(t => t.id_guia);
            
            CreateTable(
                "dbo.tipo_materia_prima",
                c => new
                    {
                        id_tipo_materia_prima = c.Int(nullable: false, identity: true),
                        ativo = c.Int(),
                    })
                .PrimaryKey(t => t.id_tipo_materia_prima);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.item_guia", "id_tipo_materia_prima", "dbo.tipo_materia_prima");
            DropForeignKey("dbo.item_guia", "id_guia", "dbo.guia");
            DropIndex("dbo.item_guia", new[] { "id_guia" });
            DropIndex("dbo.item_guia", new[] { "id_tipo_materia_prima" });
            DropTable("dbo.tipo_materia_prima");
            DropTable("dbo.item_guia");
            DropTable("dbo.guia");
        }
    }
}
