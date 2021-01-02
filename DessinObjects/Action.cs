using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DessinObjects
{
    public enum Type_Action { Créer, Détruire, Déplacer, Modifier }
    class Action
    {
        public Type_Action TypeAction { get; set; }
        public ISupprimable Objet { get; set; }

        public void undo()
        {
            switch (TypeAction)
            {
                case Type_Action.Créer:
                    Objet.Supprimé = true;
                    break;
                case Type_Action.Détruire:
                    Objet.Supprimé = false; 
                    break;
                /*case Type_Action.Déplacer :
                    Objet.Supprimé = false;
                    break;
                case Type_Action.Modifier:
                    Objet.Supprimé = false;
                    break;*/
            }
    }
    public void redo()
    {
            switch (TypeAction)
            {
                case Type_Action.Créer:
                    Objet.Supprimé = false;
                    break;
                case Type_Action.Détruire:
                    Objet.Supprimé = true;
                    break;
                    /*case Type_Action.Déplacer :
                        Objet.Supprimé = false;
                        break;
                    case Type_Action.Modifier:
                        Objet.Supprimé = false;
                        break;*/
            }
        }
}
}
