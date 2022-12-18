using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projet_Serveur.Models;

namespace Projet_Serveur.Controllers
{
    public class ProduitsController : Controller
    {
        private ProduitDBContext db = new ProduitDBContext();

        
        public ActionResult Index(string searchString)
        {
            /*Récupérer les produits*/
            var produits = from s in db.Produits
                           select s;
            /*Récuperer seulement une partie des produits*/
            if (!String.IsNullOrEmpty(searchString))
            {
                produits = produits.Where(s => (s.Categorie.Contains(searchString) || s.NomProduit.Contains(searchString)));

                produits = produits.OrderByDescending(s => s.NomProduit);
            }
            return View(produits.ToList());
        }

        public ActionResult IndexAdmin(string searchString)
        {

            var produits = from s in db.Produits
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                produits = produits.Where(s => (s.Categorie.Contains(searchString) || s.NomProduit.Contains(searchString)));

                produits = produits.OrderByDescending(s => s.NomProduit);
            }
            return View(produits.ToList());
        }

        // Récupèrer le détail d'un produit passé en parametre
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produit produit = db.Produits.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            return View(produit);
        }

        // Créer un produit
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produits/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NomProduit,Description,QuantiteDispo,Categorie,Cout,Image")] Produit produit)
        {
            if (ModelState.IsValid)
            {
                db.Produits.Add(produit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(produit);

        }

        // Modifier un produit passé en paramètre
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produit produit = db.Produits.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            return View(produit);
        }

        // Modifier un produit avec la méthode post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NomProduit,Description,QuantiteDispo,Categorie,Cout,Image")] Produit produit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexAdmin");
            }
            return View(produit);
        }

        // Supprimer un produit
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produit produit = db.Produits.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            return View(produit);
        }

            /*Ajouter un produit au panier*/
        public ActionResult AjouterAuPanier(Produit produit)
        {
            if (produit == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
            

                // Créer le panier s'il n'existe pas encore
                if (Session["panier"] == null)
                {
                    List<Produit>panier = new List<Produit>();
                    panier.Add((Produit)produit);
                    Session["panier"] = panier;
                }
                /*Sinon on récupère la liste et on y ajouter le produit*/
                else
                {
                    List<Produit> panier = (List<Produit>)Session["panier"];
                    panier.Add((Produit)produit);
                }
                /*Pour gérer l'affichage du feedback*/
                TempData["message"] = "L'article a bien été ajouté au panier";
                return RedirectToAction("Index");
            }
            
}
            /*Retirer un produit du panier*/
        public ActionResult RetirerDuPanier(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else {
                /*On parcours la liste jusqu'au produit souhaité et on le retire*/
                List<Produit> panier = (List<Produit>)Session["panier"];
                int i = 0;
                foreach (Produit produit in panier)
                {
                    if(produit.ID == id)
                    {
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
                panier.RemoveAt(i);
                Session.Remove("panier");
                if(panier.Count > 0)
                {
                    Session["panier"] = panier;
                }
            }
                /*Pour gérer le feedback*/
            TempData["message"] = "L'article a bien été retiré du panier";
            return RedirectToAction("Index");
        }


        // Supprimer un produit avec la méthode post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produit produit = db.Produits.Find(id);
            db.Produits.Remove(produit);
            db.SaveChanges();
            return RedirectToAction("IndexAdmin");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
