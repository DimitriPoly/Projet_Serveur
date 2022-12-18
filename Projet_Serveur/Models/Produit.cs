using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Projet_Serveur.Models
{
/*On définit ici tous les attributs d'un Produit*/
    public class Produit
    {
        public int ID { get; set; }
        public string NomProduit { get; set; }
        public string Description { get; set; }
        public int QuantiteDispo { get; set; }
        public string Categorie { get; set; }
        public float Cout { get; set; }
        public byte[] Image { get; set; }

    }
/*Création du dbContext*/
    public class ProduitDBContext : DbContext
    {
        public DbSet<Produit> Produits { get; set; }
    }
}