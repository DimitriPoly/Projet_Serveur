namespace Projet_Serveur.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Produits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NomProduit = c.String(),
                        Description = c.String(),
                        QuantiteDispo = c.Int(nullable: false),
                        Categorie = c.String(),
                        Cout = c.Single(nullable: false),

                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Produits");
        }
    }
}
