namespace EntidadesKor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teste3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.caixa_email",
                c => new
                    {
                        caixa_email_id = c.Int(nullable: false, identity: true),
                        usuario_id = c.Int(),
                        destinatario = c.String(),
                        remetente = c.String(),
                        assunto = c.String(),
                        texto = c.String(),
                        tipo = c.String(),
                        ativo = c.Int(),
                        enviado = c.Int(),
                        baixado = c.Int(),
                        datacriado = c.DateTime(),
                        deleted = c.DateTime(),
                    })
                .PrimaryKey(t => t.caixa_email_id)
                .ForeignKey("dbo.usuario", t => t.usuario_id)
                .Index(t => t.usuario_id);
            
            CreateTable(
                "dbo.usuario",
                c => new
                    {
                        usuario_id = c.Int(nullable: false, identity: true),
                        grupo_id = c.Int(nullable: false),
                        nome = c.String(),
                        identificador = c.String(),
                        login = c.String(),
                        email = c.String(),
                        imap_server = c.String(),
                        email_senha = c.String(),
                        senha = c.String(),
                        coordenada = c.String(),
                        data_posicao = c.DateTime(nullable: false),
                        data_criado = c.DateTime(nullable: false),
                        ultimo_acesso = c.DateTime(nullable: false),
                        qtd_acessos = c.Int(nullable: false),
                        ativo = c.Int(nullable: false),
                        deleted = c.Int(nullable: false),
                        emergencia_emergencia_id = c.Int(),
                    })
                .PrimaryKey(t => t.usuario_id)
                .ForeignKey("dbo.emergencia", t => t.emergencia_emergencia_id)
                .ForeignKey("dbo.grupo", t => t.grupo_id, cascadeDelete: true)
                .Index(t => t.grupo_id)
                .Index(t => t.emergencia_emergencia_id);
            
            CreateTable(
                "dbo.emergencia",
                c => new
                    {
                        emergencia_id = c.Int(nullable: false, identity: true),
                        usuario_id = c.Int(),
                        usuario_atendeu_id = c.Int(),
                        coordenada = c.String(),
                        datacriado = c.DateTime(),
                        deleted = c.Int(),
                        ativo = c.Int(),
                        usuario_usuario_id = c.Int(),
                        usuario_usuario_id1 = c.Int(),
                    })
                .PrimaryKey(t => t.emergencia_id)
                .ForeignKey("dbo.usuario", t => t.usuario_usuario_id)
                .ForeignKey("dbo.usuario", t => t.usuario_usuario_id1)
                .Index(t => t.usuario_usuario_id)
                .Index(t => t.usuario_usuario_id1);
            
            CreateTable(
                "dbo.grupo",
                c => new
                    {
                        grupo_id = c.Int(nullable: false, identity: true),
                        nome = c.String(),
                        datacriado = c.DateTime(),
                        ativo = c.Int(),
                        datamodificado = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.grupo_id);
            
            CreateTable(
                "dbo.imagem",
                c => new
                    {
                        imagem_id = c.Int(nullable: false, identity: true),
                        nome = c.String(),
                        usuario_id = c.Int(),
                        id_fiscalizacao = c.Int(),
                        datacriado = c.DateTime(),
                        ativo = c.Int(),
                    })
                .PrimaryKey(t => t.imagem_id)
                .ForeignKey("dbo.fiscalizacao", t => t.id_fiscalizacao)
                .ForeignKey("dbo.usuario", t => t.usuario_id)
                .Index(t => t.usuario_id)
                .Index(t => t.id_fiscalizacao);
            
            CreateTable(
                "dbo.fiscalizacao",
                c => new
                    {
                        id_fiscalizacao = c.Int(nullable: false, identity: true),
                        data_fiscalizacao = c.DateTime(),
                        posto = c.String(),
                        numero_guia = c.String(),
                        qtd_verificada = c.Int(),
                        placa_registro = c.String(),
                        nota_fiscal = c.String(),
                        validade_documento = c.String(),
                        id_status_fiscalizacao = c.Int(),
                        id_operacao = c.Int(),
                        ativo = c.Int(),
                    })
                .PrimaryKey(t => t.id_fiscalizacao)
                .ForeignKey("dbo.operacao", t => t.id_operacao)
                .ForeignKey("dbo.status_fiscalizacao", t => t.id_status_fiscalizacao)
                .Index(t => t.id_status_fiscalizacao)
                .Index(t => t.id_operacao);
            
            CreateTable(
                "dbo.operacao",
                c => new
                    {
                        id_operacao = c.Int(nullable: false, identity: true),
                        desc_operacao = c.String(),
                        data_criado_operacao = c.DateTime(),
                        data_final_operacao = c.DateTime(),
                        ativo = c.Int(),
                        usuario_id = c.Int(),
                    })
                .PrimaryKey(t => t.id_operacao)
                .ForeignKey("dbo.usuario", t => t.usuario_id)
                .Index(t => t.usuario_id);
            
            CreateTable(
                "dbo.status_fiscalizacao",
                c => new
                    {
                        id_status_fiscalizacao = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.id_status_fiscalizacao);
            
            CreateTable(
                "dbo.mensagem",
                c => new
                    {
                        mensagem_id = c.Int(nullable: false, identity: true),
                        usuario_id = c.Int(),
                        usuario_destino_id = c.Int(),
                        texto = c.String(),
                        visualizado = c.Int(),
                        baixado = c.Int(),
                        datacriado = c.DateTime(),
                        datamodificado = c.DateTime(),
                        deleted = c.Int(),
                    })
                .PrimaryKey(t => t.mensagem_id)
                .ForeignKey("dbo.usuario", t => t.usuario_id)
                .Index(t => t.usuario_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.mensagem", "usuario_id", "dbo.usuario");
            DropForeignKey("dbo.imagem", "usuario_id", "dbo.usuario");
            DropForeignKey("dbo.fiscalizacao", "id_status_fiscalizacao", "dbo.status_fiscalizacao");
            DropForeignKey("dbo.operacao", "usuario_id", "dbo.usuario");
            DropForeignKey("dbo.fiscalizacao", "id_operacao", "dbo.operacao");
            DropForeignKey("dbo.imagem", "id_fiscalizacao", "dbo.fiscalizacao");
            DropForeignKey("dbo.usuario", "grupo_id", "dbo.grupo");
            DropForeignKey("dbo.emergencia", "usuario_usuario_id1", "dbo.usuario");
            DropForeignKey("dbo.usuario", "emergencia_emergencia_id", "dbo.emergencia");
            DropForeignKey("dbo.emergencia", "usuario_usuario_id", "dbo.usuario");
            DropForeignKey("dbo.caixa_email", "usuario_id", "dbo.usuario");
            DropIndex("dbo.mensagem", new[] { "usuario_id" });
            DropIndex("dbo.operacao", new[] { "usuario_id" });
            DropIndex("dbo.fiscalizacao", new[] { "id_operacao" });
            DropIndex("dbo.fiscalizacao", new[] { "id_status_fiscalizacao" });
            DropIndex("dbo.imagem", new[] { "id_fiscalizacao" });
            DropIndex("dbo.imagem", new[] { "usuario_id" });
            DropIndex("dbo.emergencia", new[] { "usuario_usuario_id1" });
            DropIndex("dbo.emergencia", new[] { "usuario_usuario_id" });
            DropIndex("dbo.usuario", new[] { "emergencia_emergencia_id" });
            DropIndex("dbo.usuario", new[] { "grupo_id" });
            DropIndex("dbo.caixa_email", new[] { "usuario_id" });
            DropTable("dbo.mensagem");
            DropTable("dbo.status_fiscalizacao");
            DropTable("dbo.operacao");
            DropTable("dbo.fiscalizacao");
            DropTable("dbo.imagem");
            DropTable("dbo.grupo");
            DropTable("dbo.emergencia");
            DropTable("dbo.usuario");
            DropTable("dbo.caixa_email");
        }
    }
}
