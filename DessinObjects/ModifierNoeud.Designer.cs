namespace DessinObjects
{
    partial class ModifierNoeud
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.epaisseur = new System.Windows.Forms.NumericUpDown();
            this.couleur = new System.Windows.Forms.Label();
            this.ok = new System.Windows.Forms.Button();
            this.annuler = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.texteNoeud = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.epaisseur)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Epaisseur";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Couleur";
            // 
            // epaisseur
            // 
            this.epaisseur.Location = new System.Drawing.Point(87, 26);
            this.epaisseur.Name = "epaisseur";
            this.epaisseur.Size = new System.Drawing.Size(80, 20);
            this.epaisseur.TabIndex = 2;
            // 
            // couleur
            // 
            this.couleur.AutoSize = true;
            this.couleur.Location = new System.Drawing.Point(84, 71);
            this.couleur.Name = "couleur";
            this.couleur.Size = new System.Drawing.Size(82, 13);
            this.couleur.TabIndex = 3;
            this.couleur.Text = "                         ";
            this.couleur.Click += new System.EventHandler(this.couleur_Click);
            // 
            // ok
            // 
            this.ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ok.Location = new System.Drawing.Point(186, 23);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 4;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = true;
            // 
            // annuler
            // 
            this.annuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.annuler.Location = new System.Drawing.Point(186, 61);
            this.annuler.Name = "annuler";
            this.annuler.Size = new System.Drawing.Size(75, 23);
            this.annuler.TabIndex = 5;
            this.annuler.Text = "Annuler";
            this.annuler.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Texte";
            // 
            // texteNoeud
            // 
            this.texteNoeud.Location = new System.Drawing.Point(68, 98);
            this.texteNoeud.Name = "texteNoeud";
            this.texteNoeud.Size = new System.Drawing.Size(119, 20);
            this.texteNoeud.TabIndex = 7;
            // 
            // ModifierNoeud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 130);
            this.Controls.Add(this.texteNoeud);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.annuler);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.couleur);
            this.Controls.Add(this.epaisseur);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ModifierNoeud";
            this.Text = "ModifierNoeud";
            ((System.ComponentModel.ISupportInitialize)(this.epaisseur)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown epaisseur;
        private System.Windows.Forms.Label couleur;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button annuler;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox texteNoeud;
    }
}