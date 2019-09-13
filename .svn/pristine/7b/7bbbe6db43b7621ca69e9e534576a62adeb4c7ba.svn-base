using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesKor.Entidades
{
    public class ModeloBanco : DbContext
    {

        public ModeloBanco()
            : base("ModeloBanco")
        {
           
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

      
            /*Desabilitamos o delete em cascata em relacionamentos 1:N evitando
             ter registros filhos     sem registros pai*/
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Basicamente a mesma configuração, porém em relacionamenos N:N
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            /*Toda propriedade do tipo string na entidade POCO
          seja configurado como VARCHAR no SQL Server*/
            modelBuilder.Properties<string>()
                      .Configure(p => p.HasColumnType("varchar"));

            /*Toda propriedade do tipo string na entidade POCO seja configurado como VARCHAR (150) no banco de dados */
            modelBuilder.Properties<string>()
                   .Configure(p => p.HasMaxLength(100));

        }

        public virtual DbSet<caixa_email> caixa_emails { get; set; }
        public virtual DbSet<emergencia> emergencias { get; set; }
        public virtual DbSet<fiscalizacao> fiscalizacaos { get; set; }
        public virtual DbSet<grupo> grupos { get; set; }
        public virtual DbSet<guia> guias { get; set; }
        public virtual DbSet<imagem> imagems { get; set; }
        public virtual DbSet<item_guia> item_guias { get; set; }
        public virtual DbSet<mensagem> mensagems { get; set; }
        public virtual DbSet<operacao> operacaos { get; set; }
        public virtual DbSet<status_fiscalizacao> status_fiscalizacaos { get; set; }
        public virtual DbSet<tipo_materia_prima> tipo_materia_primas { get; set; }
        public virtual DbSet<usuario> usuarios { get; set; }
        

    }
}
