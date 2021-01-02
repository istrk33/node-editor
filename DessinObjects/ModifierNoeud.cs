using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DessinObjects
{
    public partial class ModifierNoeud : Form
    {
        public Color couleurParDefaut { get { return couleur.BackColor; } }
        public int epaisseurParDefaut { get { return (int)epaisseur.Value; } }
        public string texteDuNoeud{ get {return texteNoeud.Text;} }
        public ModifierNoeud(Color couleur,int epaisseur,string texteDuNoeud)
        {
            InitializeComponent();
            this.couleur.BackColor = couleur;
            this.epaisseur.Value = epaisseur;
            this.texteNoeud.Text= texteDuNoeud;
        }
        private void couleur_Click(object sender, EventArgs e)
        {
            ColorDialog col = new ColorDialog { Color = Outil.couleurParDefaut };
            if (col.ShowDialog() == DialogResult.OK)
            {
                couleur.BackColor = col.Color;
            }
        }

       
    }
}
