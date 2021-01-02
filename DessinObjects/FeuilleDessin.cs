using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DessinObjects
{
    public partial class FeuilleDessin : Form
    {
        bool textChanged;
        Color couleurSelection;
        int epaisseur;
        Stack<Action> actionsEffectuées;
        Stack<Action> actionsAnnulées;
        public FeuilleDessin()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Outil.mesNoeuds = new List<Noeud>();
            Outil.mesTraits = new List<Trait>();
            Outil.enMouvement = false;
            Outil.enDessinTrait = false;
            Outil.EpaisseurParDefaut = 1;
            Outil.couleurParDefaut = Color.Black;
            couleur.BackColor = Outil.couleurParDefaut;
            Outil.enSuppression = false;
            Outil.texteNoeud = Outil.mesNoeuds.Count.ToString();
            texteNoeud.Text = Outil.texteNoeud;
            textChanged = false;
            actionsEffectuées = new Stack<Action>(50);
            actionsAnnulées= new Stack<Action>(50);
        }

        private void FeuilleDessin_Paint(object sender, PaintEventArgs e)
        {
            Outil.mesNoeuds.FindAll(n=>!n.Supprimé).ForEach(n => n.dessine(e.Graphics));
            Outil.mesTraits.FindAll(t=> !t.Supprimé).ForEach(t => t.dessine(e.Graphics));
        }

        private void FeuilleDessin_MouseDown(object sender, MouseEventArgs e)
        {
            Outil.selection = Selection(e.Location);
            if (deplacer.Checked)
            {
                Outil.enMouvement = Outil.selection != null;
                if (Outil.selection != null)
                {
                    epaisseur = Outil.selection.epaisseur;
                    couleurSelection = Outil.selection.couleur;
                }
            }
            else if (supprimer.Checked)
            {
                Outil.enSuppression = Outil.selection != null;
            }
            else
            {
                //SI ON CLIQUE GAUCHE
                if (e.Button == MouseButtons.Left)
                {
                    if (Outil.selection == null)
                    {
                        Noeud noeud =
                        new Noeud
                        {
                            position = e.Location,
                            taille = new Size(20, 20),
                            epaisseur = Outil.EpaisseurParDefaut,
                            couleur = Outil.couleurParDefaut,
                            texte = Outil.texteNoeud
                        };
                        Outil.mesNoeuds.Add(noeud);

                        Action action = new Action{ TypeAction = Type_Action.Créer, Objet = noeud}; 
                        actionsEffectuées.Push(action);

                        Console.WriteLine(!textChanged);
                        if (!textChanged)
                        {
                            Outil.texteNoeud = Outil.mesNoeuds.Count.ToString(); ;
                        }
                    }
                    else Outil.enDessinTrait = true;
                }
                //CLIC GAUCHE AVEC MENU
                else if (Outil.selection != null)
                {
                    ContextMenuStrip cm = new ContextMenuStrip();
                    foreach (string libel in new string[] { "Supprimer", "Modifier" })
                    {
                        ToolStripMenuItem element = new ToolStripMenuItem(libel);
                        element.Click += elementClick;
                        cm.Items.Add(element);
                    }
                    cm.Show(this, e.Location);
                }
            }
            Refresh();
        }

        private void elementClick(object sender, EventArgs e)
        {
            ToolStripMenuItem tm = (ToolStripMenuItem)sender;
            switch (tm.Text)
            {
                //MODIFIER LE NOEUD EN APPEL LA DIALOGBOX
                case "Modifier":
                    ModifierNoeud mod = new ModifierNoeud(Outil.selection.couleur, Outil.selection.epaisseur, Outil.selection.texte);
                    if (mod.ShowDialog() == DialogResult.OK)
                    {
                        Outil.selection.epaisseur = mod.epaisseurParDefaut;
                        Outil.selection.couleur = mod.couleurParDefaut;
                        Outil.selection.texte = mod.texteDuNoeud;
                        Outil.selection = null;
                    }
                    break;
                //SUPPRIMER LE NOEUD
                case "Supprimer":
                    Outil.mesTraits.FindAll(t=> t.source==Outil.selection|| t.destination== Outil.selection).ForEach(t=>t.Supprimé=true);
                    Outil.selection.Supprimé = true;
                    Action a = new Action
                    {
                        TypeAction = Type_Action.Détruire,
                        Objet = Outil.selection
                    };
                    actionsEffectuées.Push(a);
                    Outil.mesTraits.FindAll(t => t.source.Supprimé||t.destination.Supprimé).ForEach(t => actionsEffectuées.Push(new Action { TypeAction=Type_Action.Détruire, Objet=t }));
                    break;
            }
            Refresh();
        }

        private void FeuilleDessin_MouseMove(object sender, MouseEventArgs e)
        {
            //DEPLACEMENT DE NOEUD
            if (Outil.enMouvement)
            {
                Outil.selection.couleur = Color.Red;
                Outil.selection.epaisseur = 8;
                Outil.selection.deplace(e.Location);
                Refresh();
            }
            //DESSIN DE TRAIT
            else if (Outil.enDessinTrait)
            {
                Noeud noeudTraitTmp = new Noeud { position = e.Location, taille = new Size(20, 20), epaisseur = 8, couleur = Color.Red };
                Trait t = new Trait { source = Outil.selection, destination = noeudTraitTmp, couleur = Pens.Red };
                Outil.mesTraits.Add(t);
                Outil.mesNoeuds.Add(noeudTraitTmp);
                Refresh();
                Outil.mesTraits.Remove(t);
                Outil.mesNoeuds.Remove(noeudTraitTmp);
            }
        }

        private void FeuilleDessin_MouseUp(object sender, MouseEventArgs e)
        {
            //GESTION TRAIT DESSIN
            if (Outil.enDessinTrait)
            {
                //SUR NOEUD EXISTANT
                Noeud fin = Selection(e.Location);
                if (fin != null && !fin.Equals(Outil.selection))
                {
                    Trait intermediaire;
                    Outil.mesTraits.Add(intermediaire = new Trait
                    {
                        source = Outil.selection,
                        destination = fin
                    });
                    Action action = new Action { TypeAction = Type_Action.Créer, Objet = intermediaire };
                    actionsEffectuées.Push(action);
                }
                //SUR SUR AUCUN NOEUD
                else if (fin == null)
                {
                    Noeud noeudTraitTmp;
                    Outil.mesNoeuds.Add(noeudTraitTmp = new Noeud { position = e.Location, taille = new Size(20, 20), epaisseur = Outil.EpaisseurParDefaut, couleur = Outil.couleurParDefaut, texte = Outil.texteNoeud });

                    Action action = new Action { TypeAction = Type_Action.Créer, Objet = noeudTraitTmp };
                    actionsEffectuées.Push(action);

                    Trait intermediaire;
                    Outil.mesTraits.Add(intermediaire = new Trait { source = Outil.selection, destination = noeudTraitTmp });

                    Action action2 = new Action { TypeAction = Type_Action.Créer, Objet = intermediaire };
                    actionsEffectuées.Push(action2);

                    if (!textChanged)
                    {
                        Outil.texteNoeud = Outil.mesNoeuds.Count.ToString(); ;
                    }
                }
                Outil.enDessinTrait = false;
            }
            //DEPLACEMENT A LA NOUVELLE POSITION
            else if (Outil.enMouvement)
            {
                Outil.enMouvement = false;
                Outil.selection.couleur = couleurSelection;
                Outil.selection.epaisseur = epaisseur;

                Outil.selection = null;
            }
            //SUPPRESSION DE NOEUD
            else if (Outil.enSuppression)
            {
                List<Trait> traitsDuNoeud = new List<Trait>();
                foreach (Trait t in Outil.mesTraits)
                {
                    if (t.source.Equals(Outil.selection) || t.destination.Equals(Outil.selection))
                    {
                        traitsDuNoeud.Add(t);
                    }
                }
                traitsDuNoeud.ForEach(t => Outil.mesTraits.Remove(t));
                Outil.mesNoeuds.Remove(Outil.selection);
            }
            Refresh();
        }
        private Noeud Selection(Point p)
        {
            return Outil.mesNoeuds.FirstOrDefault(c => c.Contient(p));
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Outil.EpaisseurParDefaut++;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Outil.EpaisseurParDefaut--;
        }

        private void supprimerDessin_Click(object sender, EventArgs e)
        {
            Outil.EpaisseurParDefaut = 1;
            Outil.couleurParDefaut = Color.Black;
            Outil.mesNoeuds.Clear();
            Outil.mesTraits.Clear();
            Refresh();
        }
        private void parametreNoeud_Click(object sender, EventArgs e)
        {
            ModifierNoeud mod = new ModifierNoeud(Outil.couleurParDefaut, Outil.EpaisseurParDefaut, Outil.texteNoeud);
            if (mod.ShowDialog() == DialogResult.OK)
            {
                Outil.EpaisseurParDefaut = mod.epaisseurParDefaut;
                Outil.couleurParDefaut = mod.couleurParDefaut;
                Outil.texteNoeud = mod.texteDuNoeud;
            }
        }
        /*
         * COULEUR PAR DEFAUT
         */
        private void couleur_Click_1(object sender, EventArgs e)
        {
            ColorDialog col = new ColorDialog { Color = Outil.couleurParDefaut };
            if (col.ShowDialog() == DialogResult.OK)
            {
                Outil.couleurParDefaut = col.Color;
                couleur.BackColor = col.Color;
            }
        }

        private void texteNoeud_TextChanged(object sender, EventArgs e)
        {
            Outil.texteNoeud = texteNoeud.Text;
            textChanged = true;
        }

        private void texteNoeud_Click(object sender, EventArgs e)
        {
            texteNoeud.SelectAll();
        }

        private void enregistrerToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog svd = new SaveFileDialog
            {
                Filter = "Fichier xml|.xml",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (svd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(svd.FileName);
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?> ");
                sw.WriteLine("<dessin>");
                foreach (Noeud n in Outil.mesNoeuds)
                {
                    sw.WriteLine(n.VersXML());
                }
                foreach (Trait t in Outil.mesTraits)
                {
                    sw.WriteLine(t.VersXML());
                }
                sw.WriteLine("</dessin>");
                sw.Close();
            }
        }

        private void ouvrirFichierButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog opfd = new OpenFileDialog
            {
                Filter = "Fichier xml|*.xml",
                Title = "Choisir le fichier",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (opfd.ShowDialog() == DialogResult.OK)
            { // Code de relecture    
                XDocument doc = XDocument.Load(opfd.FileName);
                Outil.mesNoeuds.Clear();
                Outil.mesTraits.Clear();
                foreach (XElement el in doc.Root.Nodes())
                {
                    if (el.Name == "noeud")
                    {
                        Noeud redessiner = new Noeud(el);
                        Outil.mesNoeuds.Add(redessiner);
                    }
                    if (el.Name == "trait")
                    {
                        Trait redessinerTrait = new Trait(el);
                        Outil.mesTraits.Add(redessinerTrait);
                    }
                }
            }
        }

        private void annulerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (actionsEffectuées.Count > 0) {
                Action a = actionsEffectuées.Pop();
                actionsAnnulées.Push(a);
                a.undo();
                Refresh();
            }
        }

        private void rétablirToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if (actionsAnnulées.Count > 0) {
                Action a = actionsAnnulées.Pop();
                actionsEffectuées.Push(a);
                a.redo(); 
                Refresh(); 
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (actionsEffectuées.Count > 0)
            {
                Action a = actionsEffectuées.Pop();
                actionsAnnulées.Push(a);
                a.undo();
                Refresh();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (actionsAnnulées.Count > 0)
            {
                Action a = actionsAnnulées.Pop();
                actionsEffectuées.Push(a);
                a.redo();
                Refresh();
            }
        }
    }
}
