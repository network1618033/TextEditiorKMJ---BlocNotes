/*
    Programmeurs:   ZOGONA Jonathan

    Date:           29 Oct. 2019
 
    Solution:       TextEditiorKMJ.sln
    Projet:		    TextEditiorKMJ.csproj
    Classe:         rechercherForm.cs
 
    Buts:           Créer une application de similaire au Bloc Notes
*/



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TextEditorKMJ
{
    public partial class rechercherForm : Form
    {
        #region Propriétés

        public String Mot
        {
            get { return motARechercheTextBox.Text.Trim(); }
            set { motARechercheTextBox.Text = value; }
        }

        #endregion

        public rechercherForm()
        {
            InitializeComponent();
        }

        #region Recherche du mot du début à la fin du document (Circulaire)

        private void suivantButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(this.Owner.ActiveMdiChild != null)
                {
                    RichTextBox noteActiveRichTextBox = (this.Owner.ActiveMdiChild as TextEditorKMJ).textEditorRichTextBox;

                    int positionDepartInteger = noteActiveRichTextBox.SelectionStart;										

                    if (noteActiveRichTextBox.SelectionLength == 0) 									
                    {
                        if (noteActiveRichTextBox.Find(Mot, positionDepartInteger, RichTextBoxFinds.None) == -1)
                        {										
                            noteActiveRichTextBox.Find(Mot, 0, RichTextBoxFinds.None);	
                        }
                    }
                    else 										
                    {
                        if (noteActiveRichTextBox.Find(Mot, positionDepartInteger + 1, RichTextBoxFinds.None) == -1)									
                        {
                            noteActiveRichTextBox.Find(Mot, 0, RichTextBoxFinds.None);								
                        }
                    }

                }
            }
            catch(Exception rechercheException)
            {
                MessageBox.Show("Erreur inattendue : " + Environment.NewLine + rechercheException.ToString(), "Rechercher", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Fermer la boite à outils de recherche

        private void annulerButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
