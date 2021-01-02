using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DessinObjects
{
    class Noeud : ISupprimable
    {
        #region Implémentation de l'interface 
        public bool Supprimé { get; set; } = false;
        #endregion
        public int id { get; set; } = Outil.mesNoeuds.Count();
        public Point position { get; set; }
        public String texte { get; set; }
        public Size taille { get; set; }
        public Point centre { get { return new Point(position.X +taille.Width / 2, position.Y + taille.Height / 2); } }
        public int epaisseur { get; set; } = 1;
        public Color couleur { get; set; } = Color.Black;
        public void dessine(Graphics g)
        {
            Pen p = new Pen(couleur, epaisseur);
            Rectangle r = new Rectangle(position, taille);
            g.DrawRectangle(p, r);
            g.DrawString(texte, new Font("Times New Roman", 12, FontStyle.Regular), Brushes.Black, new Point(position.X-taille.Width/2, centre.Y + 10));
        }

        public bool Contient(Point p) { 
            Rectangle r = new Rectangle(position, taille);
            return r.Contains(p); 
        }

        public void deplace(Point p)
        {
            this.position = new Point(p.X,p.Y);
        }

        public string VersXML()
        {
            return "<noeud>" 
                + Environment.NewLine+ "    <position x=\"" + position.X+ "\" y=\"" + position.Y + "\"/>" 
                + Environment.NewLine+ "    <taille w=\"" + taille.Width + "\" h=\""+ taille.Height + "\"/>" 
                + Environment.NewLine+ "    <epaisseur>" + epaisseur + "</epaisseur>"
                + Environment.NewLine + "   <texte>" + texte + "</texte>"
                + Environment.NewLine + "   <couleur>" + couleur.ToArgb() + "</couleur>"
                + Environment.NewLine + "   <id>" + id + "</id>"
                + Environment.NewLine + "</noeud>";
        }

        public Noeud()
        {

        }

        public Noeud(XElement el)
        {
            if (el.Name != "noeud") return;
            foreach (XElement xe in el.Nodes())
            {
                switch (xe.Name.ToString())
                {
                    case "id":
                        id = (int.Parse(xe.Value));
                        break; ;
                    case "position":
                       position  = new Point(int.Parse(xe.Attribute("x").Value), int.Parse(xe.Attribute("y").Value));
                        break;
                    case "couleur":
                        couleur = Color.FromArgb(int.Parse(xe.Value));
                        break;
                    case "taille":
                        taille = new Size(int.Parse(xe.Attribute("w").Value), int.Parse(xe.Attribute("h").Value));
                        break;
                    case "epaisseur":
                        epaisseur= (int.Parse(xe.Value));
                        break;
                    case "texte":
                        texte = xe.Value;
                        break;
                }
            }
        }
    }
}
