using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DessinObjects
{
    class Outil
    {
        public static List<Noeud> mesNoeuds;
        public static List<Trait> mesTraits;
        public static Noeud selection;
        public static bool enMouvement;
        public static bool enDessinTrait;
        public static int EpaisseurParDefaut;
        public static Color couleurParDefaut;
        public static bool enSuppression;
        public static string texteNoeud;
    }
}
