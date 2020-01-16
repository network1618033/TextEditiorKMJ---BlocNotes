/*
    Programmeurs:   ZOGONA Jonathan

    Date:           29 Oct. 2019
 
    Solution:       TextEditiorKMJ.sln
    Projet:		    TextEditiorKMJ.csproj
    Classe:         TextEditorKMJEnfantForm.cs
 
    But:           Créer une application de similaire au Bloc Notes
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using g = TextEditorKMJ.TextEditorKMJGenerale;
using ce = TextEditorKMJ.TextEditorKMJGenerale.CEMessages;

namespace TextEditorKMJ
{
    public partial class TextEditorKMJ : Form
    { 

        #region Déclaration des variables

        public static int numero;
        private static string filtreString;
        private static string initialDirectory;
        private Boolean enregistrerBool;
        private Boolean modeInsertion;

        #endregion

        #region Propriété Enregistrer

        public Boolean Enregistre
        {
            get
            {
                return enregistrerBool;
            }
            set
            {
                enregistrerBool = value;

            }
        }

        #endregion

        #region Propriété ModeInseree

        public Boolean ModeInseree
        {
            get 
            { 
                return modeInsertion; 
            }
            set 
            {
                modeInsertion = value; 
            }
        }

        #endregion

        #region Initialisation

        public TextEditorKMJ()
        {
            try
            {
                InitializeComponent();
                numero++;
                Enregistre = false;
                filtreString = "Fichiers rtf (*.rtf)|*.rtf|Tous les fichiers (*.*)|*.*";
                initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            catch
            {
                throw new Exception(g.tableauMessages[(int)ce.erreurIndeterminee]);
            }
        }

        #endregion

        #region Méthodes pour l'enregistrement

        #region Méthode pour enregistrer

        public void Enregitrer()
        {
            try
            {
                if (textEditorRichTextBox.Modified && textEditorRichTextBox.Text.Length >= 1)
                {
                    if (!Enregistre)
                        EnregitrerSous();
                    else
                    {
                        textEditorRichTextBox.SaveFile(this.Text);
                        textEditorRichTextBox.Modified = false;
                    }
                }
            }
            catch
            {
                throw new Exception(g.tableauMessages[(int)ce.enregistrementErreur]);
            }
        }

        #endregion

        #region Méthode pour enregistrer sous

        public void EnregitrerSous()
        {
            try
            {

                SaveFileDialog textEditorKMJSaveFileDialog = new SaveFileDialog();

                textEditorKMJSaveFileDialog.DefaultExt = "rtf";
                textEditorKMJSaveFileDialog.FilterIndex = 0;
                textEditorKMJSaveFileDialog.CheckPathExists = true;
                textEditorKMJSaveFileDialog.OverwritePrompt = true;
                textEditorKMJSaveFileDialog.AddExtension = true;
                textEditorKMJSaveFileDialog.Title = "Enregistrer le texte";
                textEditorKMJSaveFileDialog.InitialDirectory = initialDirectory;
                textEditorKMJSaveFileDialog.Filter = filtreString;
                textEditorKMJSaveFileDialog.FileName = this.Text;

                if (textEditorKMJSaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.textEditorRichTextBox.SaveFile(textEditorKMJSaveFileDialog.FileName);
                    this.Text = textEditorKMJSaveFileDialog.FileName;
                    Enregistre = true;
                    textEditorRichTextBox.Modified = false;

                }

                textEditorKMJSaveFileDialog.Dispose();
            }
            catch(ArgumentNullException)
            {
                MessageBox.Show(g.tableauMessages[(int)ce.erreurArgumentNullExeption]);
            }
            catch(System.Runtime.InteropServices.ExternalException)
            {
                MessageBox.Show(g.tableauMessages[(int)ce.erreurExternalException]);
            }
            catch (Exception ex)
            {
                throw new Exception(g.tableauMessages[(int)ce.enregistrementErreur] + Environment.NewLine + ex.ToString());
            }
        }

        #endregion

        #endregion

        #region Fermeture du TextEditor

        private void TextEditorKMJ_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult enregistre;

            try
            {
                if (textEditorRichTextBox.Modified && textEditorRichTextBox.Text.Length >= 1)
                {
                    enregistre = MessageBox.Show(g.tableauMessages[(int)ce.enregistrerErreur], "Fermer le document"
                                  , MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                    switch (enregistre)
                    {
                        case DialogResult.Yes:
                            Enregitrer();
                            this.Dispose();
                            break;
                        case DialogResult.No:
                            this.Dispose();
                            break;
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(g.tableauMessages[(int)ce.erreurIndeterminee] + Environment.NewLine + ex.ToString());
            }
        }

        #endregion

        #region SelectionChanged du RichTextBox

        private void textEditorRichTextBox_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                TextEditorKMJParentForm oForm = this.MdiParent as TextEditorKMJParentForm;
                    
                #region phase F

                if (this.textEditorRichTextBox.SelectionFont != null)
                {
                    // Cocher / décocher les boutons Gras, Italique et Souligné si la sélection n’est pas null
                    oForm.grasToolStripButton.Checked = textEditorRichTextBox.SelectionFont.Bold;
                    oForm.italiqueToolStripButton.Checked = textEditorRichTextBox.SelectionFont.Italic;
                    oForm.soulignementToolStripButton.Checked = textEditorRichTextBox.SelectionFont.Underline;

                    oForm.fontsToolStripComboBox.Text = textEditorRichTextBox.SelectionFont.Name;
                    
                    if (textEditorRichTextBox.SelectionFont.Size == 13) //Deux Sizes selectionneées
                        oForm.sizeToolStripComboBox.Text = "13";
                    else
                        oForm.sizeToolStripComboBox.Text = Convert.ToInt32(textEditorRichTextBox.SelectionFont.Size).ToString();
                }
                else
                {
                    oForm.fontsToolStripComboBox.Text = string.Empty;
                    oForm.sizeToolStripComboBox.Text = string.Empty;
                }

                #endregion
                
                // Activer les options du menu et les boutons Coller

                if (Clipboard.ContainsText() || Clipboard.ContainsImage())
                    oForm.collerToolStripButton.Checked = true;
                else
                    oForm.collerToolStripMenuItem.Checked = oForm.collerToolStripButton.Checked;

                oForm.collerToolStripMenuItem.Enabled = Clipboard.GetDataObject().GetDataPresent("System.String") || Clipboard.GetDataObject().GetDataPresent(DataFormats.Bitmap);

                oForm.copierToolStripMenuItem.Enabled = textEditorRichTextBox.SelectionLength > 0;
                oForm.copierToolStripButton.Enabled = oForm.copierToolStripMenuItem.Enabled;

                oForm.couperToolStripMenuItem.Enabled = textEditorRichTextBox.SelectionLength > 0;
                oForm.couperToolStripButton.Enabled = oForm.couperToolStripMenuItem.Enabled;


                // Activer les options du menu et les boutons Copier ou Coper

                if (textEditorRichTextBox.SelectionLength > 0)
                {
                    oForm.copierToolStripButton.Enabled = true;
                    oForm.couperToolStripButton.Enabled = true;
                }
                else
                {
                    oForm.copierToolStripButton.Enabled = false;
                    oForm.couperToolStripButton.Enabled = false;
                }

                // Selon l’alignement choisi (Gauche, Centré, Droit), cocher / décocher les boutons appropriés

                if (textEditorRichTextBox.SelectionAlignment == HorizontalAlignment.Left)
                {
                    oForm.alignementGaucheToolStripButton.Checked = true;
                    oForm.alignementDroiteToolStripButton.Checked = false;
                    oForm.alignementCentrerToolStripButton.Checked = false;

                }
                else if (textEditorRichTextBox.SelectionAlignment == HorizontalAlignment.Center)
                {
                    oForm.alignementGaucheToolStripButton.Checked = false;
                    oForm.alignementDroiteToolStripButton.Checked = false;
                    oForm.alignementCentrerToolStripButton.Checked = true;
                }
                else
                {
                    oForm.alignementGaucheToolStripButton.Checked = false;
                    oForm.alignementDroiteToolStripButton.Checked = true;
                    oForm.alignementCentrerToolStripButton.Checked = false;
                }

            }
            catch(InvalidEnumArgumentException ex)
            {
                MessageBox.Show(g.tableauMessages[(int)ce.InvalidEnumArgumentErreur] + Environment.NewLine + ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(g.tableauMessages[(int)ce.selectionChangedErreur] + Environment.NewLine + ex.ToString());
            }
        }

        #endregion

        #region Méthode pour changer les attributs de police

        public void ChangerAttributsPolice(FontStyle style)
        {
            try
            {
                if (textEditorRichTextBox.SelectionFont != null)
                {
                    if (textEditorRichTextBox.SelectionFont.FontFamily.IsStyleAvailable(style))
                    {
                        textEditorRichTextBox.SelectionFont = new Font(textEditorRichTextBox.SelectionFont, textEditorRichTextBox.SelectionFont.Style ^ style);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(g.tableauMessages[(int)ce.changerStyleErreur] + Environment.NewLine + ex.ToString());
            }
        }

        #endregion

        #region TextEditorKMJ actif

        private void TextEditorKMJ_Activated(object sender, EventArgs e)
        {
            textEditorRichTextBox_SelectionChanged(null, null);
        }

        #endregion
    }
}
