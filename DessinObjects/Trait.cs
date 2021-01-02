using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DessinObjects
{
    class Trait : ISupprimable
    {
        #region Implémentation de l'interface 
        public bool Supprimé { get; set; } = false;
        #endregion
        public Noeud source { get; set; }
        public Noeud destination { get; set; }
        public Pen couleur { get; set; } = Pens.Black;
        public void dessine(Graphics g)
        {
            g.DrawLine(couleur, source.centre,
            destination.centre);
        }
        public string VersXML()
        {
            return "<trait>"
                + Environment.NewLine + "   <source>"+source.id+"</source>"
                + Environment.NewLine + "   <destination>"+destination.id+"</destination>"
                + Environment.NewLine + "   <couleur>" + couleur.Color.ToArgb() + "</couleur>"
                + Environment.NewLine + "</trait>";
        }

        public Trait()
        {

        }

        public Trait(XElement el)
        {
            if (el.Name != "trait") return;
            foreach (XElement xe in el.Nodes())
            {
                switch (xe.Name.ToString())
                {
                    case "source":
                        source = Outil.mesNoeuds.Find(s => s.id== int.Parse(xe.Value));
                        break;
                    case "destination":
                        destination = Outil.mesNoeuds.Find(s => s.id == int.Parse(xe.Value));
                        break;
                    case "couleur":
                         couleur = new Pen(Color.FromArgb(int.Parse(xe.Value))) ;
                        break;
                }
            }
        }
    }
}
