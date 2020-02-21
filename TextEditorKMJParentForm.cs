/*
    Programmeurs:   ZOGONA Jonathan

    Date:           29 Oct. 2019
 
    Solution:       TextEditiorKMJ.sln
    Projet:		    TextEditiorKMJ.csproj
    Classe:         TextEditorKMJParentForm.cs
 
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
using System.Drawing.Text;

using g = TextEditorKMJ.TextEditorKMJGenerale;
using ce = TextEditorKMJ.TextEditorKMJGenerale.CEMessages;

namespace TextEditorKMJ
{
    public partial class TextEditorKMJParentForm : Form
    {

        #region Déclaration des variables

        private string filtreString;
        private string initialdirectory;
        private string statusINS;
        private ComboBox myComboBox;

        #endregion

        #region Initialisation - Menus et barre d'outils

        public TextEditorKMJParentForm()
        {
            InitializeComponent();
        }

        private void TextEditorKMJParentForm_Load(object sender, EventArgs e)
        {
            // Initialisation des messages d'erreurs
            g.InitMessages();

            // Appel de la méthode AssocieImages
            AssocierImagesMenuBarreOutils();

            filtreString = "Fichiers rtf (*.rtf)|*.rtf|Tous les fichiers (*.*)|*.*";
            initialdirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            int indexFiltre = 0;
            string extensionDefautString = "rtf";

            textEditorKMJOpenFileDialog.InitialDirectory = initialdirectory;
            textEditorKMJOpenFileDialog.AddExtension = true;
            textEditorKMJOpenFileDialog.CheckFileExists = true;
            textEditorKMJOpenFileDialog.CheckPathExists = true;
            textEditorKMJOpenFileDialog.DefaultExt = extensionDefautString;
            textEditorKMJOpenFileDialog.Title = "Ouvrir un texte";
            textEditorKMJOpenFileDialog.FilterIndex = indexFiltre;


            textEditorKMJOpenFileDialog.Filter = filtreString;

            //Appel la méthode pour Désactiver Opérations MenusBarreOutils
            DesactiverOperationsMenusBarreOutils();

            //Identifier la culture courante

            cultureToolStripStatusLabel.Text = System.Globalization.CultureInfo.CurrentCulture.NativeName;

            //Identifier la touche CAPS LOCK(Majuscule)

            if (System.Console.CapsLock)
                capsLockToolStripStatusLabel.Text = "MAJ";

            if (this.ActiveMdiChild == null)
                infoToolStripStatusLabel.Text = "Créer ou ouvrir un TextEditor";

            statusINS = "INS";

            fontsToolStripComboBox.SelectedIndexChanged -= fontsToolStripComboBox_SelectedIndexChanged; 
            sizeToolStripComboBox.SelectedIndexChanged -= sizeToolStripComboBox_SelectedIndexChanged; // Pourquoi ?

            myComboBox = fontsToolStripComboBox.ComboBox;
            myComboBox.DisplayMember = "Name";
            myComboBox.ItemHeight = 20;
            myComboBox.DrawMode = DrawMode.OwnerDrawVariable;
            myComboBox.DrawItem += MyComboBox_DrawItem;
            myComboBox.MeasureItem += MyComboBox_MeasureItem;

            AfficherPolicesInstalleesOrdinateur();

            fontsToolStripComboBox.SelectedIndexChanged += fontsToolStripComboBox_SelectedIndexChanged;
            sizeToolStripComboBox.SelectedIndexChanged += sizeToolStripComboBox_SelectedIndexChanged;   

            textEditorFontDialog.MinSize = 8;
            textEditorFontDialog.MaxSize = 14;

        }

        #endregion

        #region Méthodes partagées

        #region Nouveau document dans une nouvelle fenetre enfant

        private void Nouveau_Click(object sender, EventArgs e)
        {
            try
            {
                TextEditorKMJ textEditor = new TextEditorKMJ();
                textEditor.MdiParent = this;

                textEditor.Text += " (" + TextEditorKMJ.numero.ToString() + ")";

                ActiverOperationsMenusBarreOutils();

                textEditor.ModeInseree = true;

                textEditor.Show();

                infoToolStripStatusLabel.Text = this.ActiveMdiChild.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(g.tableauMessages[(int)ce.creationTextEditorKMJ] + Environment.NewLine + ex.ToString());
            }
        }

        #endregion

        #region Menu fenetre - Arranger les fenetres documents

        private void ArrangerFenetres(object sender, EventArgs e)
        {
            ToolStripMenuItem oMenu = sender as ToolStripMenuItem;

            this.LayoutMdi((MdiLayout)fenetreToolStripMenuItem.DropDownItems.IndexOf(oMenu));
            g.EnleverCrochet(fenetreToolStripMenuItem);
            oMenu.Checked = true;
        }

        #endregion

        #region Changer l'orientation du texte de la barre de menu selon le ToolStripPanel dans lequel il est placé

        private void ToolStripPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            if (sender == textEditorKMJHautToolStripPanel || sender == textEditorKMJBasToolStripPanel)
            {
                if (e.Control == textEditorKMJMenuStrip)
                {
                    questionToolStripComboBox.Visible = true;
                    textEditorKMJMenuStrip.TextDirection = ToolStripTextDirection.Horizontal;
                    textEditorKMJMenuStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
                }
                else
                {
                    fontsToolStripComboBox.Visible = true;
                    sizeToolStripComboBox.Visible = true;
                    textEditorKMJToolStrip.TextDirection = ToolStripTextDirection.Horizontal;
                    textEditorKMJMenuStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
                }
            }
            else
            {
                if (e.Control == textEditorKMJMenuStrip)
                {
                    questionToolStripComboBox.Visible = false;
                    textEditorKMJMenuStrip.TextDirection = ToolStripTextDirection.Vertical90;
                    textEditorKMJMenuStrip.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
                }
                else
                {
                    fontsToolStripComboBox.Visible = false;
                    sizeToolStripComboBox.Visible = false;
                    textEditorKMJToolStrip.TextDirection = ToolStripTextDirection.Vertical90;
                    textEditorKMJMenuStrip.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
                }
            }
        }

        #endregion

        #region Enregistrer

        private void Enregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.MdiChildren.Count() >= 1 && this.ActiveMdiChild != null)
                {
                    TextEditorKMJ textEditor;
                    textEditor = this.ActiveMdiChild as TextEditorKMJ;

                    if (sender == enregistrerToolStripButton || sender == enregistrerToolStripMenuItem)
                        textEditor.Enregitrer();
                    else
                        textEditor.EnregitrerSous();

                    infoToolStripStatusLabel.Text = this.ActiveMdiChild.Text;

                }
                else
                {
                    TextEditorKMJ textEditor = this.ActiveMdiChild as TextEditorKMJ;
                    textEditor.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(g.tableauMessages[(int)ce.enregistrementErreur] + Environment.NewLine + ex.ToString());
            }
        }

        #endregion

        #region Ouvrir

        private void Ouvrir_Click(object sender, EventArgs e)
        {
            try
            {
                textEditorKMJOpenFileDialog.InitialDirectory = initialdirectory;

                if (textEditorKMJOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (textEditorKMJOpenFileDialog.FileName.EndsWith("rtf", StringComparison.CurrentCulture))
                    {
                        TextEditorKMJ textEditor = new TextEditorKMJ();
                        textEditor.Text = textEditorKMJOpenFileDialog.FileName;
                        textEditor.MdiParent = this;
                        textEditor.textEditorRichTextBox.LoadFile(textEditorKMJOpenFileDialog.FileName);
                        textEditor.textEditorRichTextBox.Modified = false;
                        textEditor.Enregistre = true;
                        textEditor.ModeInseree = true;
                        textEditor.Show();
                        textEditorKMJOpenFileDialog.InitialDirectory = textEditorKMJOpenFileDialog.FileName;
                        initialdirectory = textEditorKMJOpenFileDialog.FileName;

                        ActiverOperationsMenusBarreOutils();

                        infoToolStripStatusLabel.Text = this.ActiveMdiChild.Text;
                    }
                    else
                    {
                        MessageBox.Show(g.tableauMessages[(int)ce.erreurMauvaiseExtension]);
                    }
                }
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show(g.tableauMessages[(int)ce.erreurOutOfMemory]);
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show(g.tableauMessages[(int)ce.erreurFileNotFound]);
            }
            catch (ArgumentException)
            {
                MessageBox.Show(g.tableauMessages[(int)ce.erreurArgumentException]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(g.tableauMessages[(int)ce.ouvrirErreur] + Environment.NewLine + ex.ToString());
            }
            finally
            {
                textEditorKMJOpenFileDialog.Dispose();
            }
        }

        #endregion

        #region Fermeture 

        #region Button Fermer

        private void Fermer_Click(object sender, EventArgs e)
        {
            TextEditorKMJ textEditor = this.ActiveMdiChild as TextEditorKMJ;

            if (this.ActiveControl != null)
            {
                textEditor.Close();
            }

        }

        #endregion

        #region Quitter le formulaire

        private void Quitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #endregion

        #endregion

        #region Méthodes privées

        #region Associer les images des menus et celles de la barre d'outils

        public void AssocierImagesMenuBarreOutils()
        {
            //Fichier ToolStripMenuItem
            nouveauToolStripMenuItem.Image = nouveauToolStripButton.Image;
            ouvrirToolStripMenuItem.Image = ouvrirToolStripButton.Image;
            enregistrerToolStripMenuItem.Image = enregistrerToolStripButton.Image;
            imprimerToolStripMenuItem.Image = imprimerToolStripButton.Image;

            //Edition ToolStripMenuItem
            couperToolStripMenuItem.Image = couperToolStripButton.Image;
            copierToolStripMenuItem.Image = copierToolStripButton.Image;
            collerToolStripMenuItem.Image = collerToolStripButton.Image;
        }
        #endregion

        #endregion

        #region Edition

        private void EditionTexte_Click(object sender, EventArgs e)
        {
            try
            {
                RichTextBox rtb;

                rtb = this.ActiveMdiChild.ActiveControl as RichTextBox;

                if (sender == couperToolStripMenuItem || sender == couperToolStripButton)
                    rtb.Cut();

                else if (sender == copierToolStripMenuItem || sender == copierToolStripButton)
                    rtb.Copy();

                else if (sender == collerToolStripMenuItem || sender == collerToolStripButton)
                    rtb.Paste();

                else if (sender == selectionnerToutToolStripMenuItem)
                    rtb.SelectAll();

                else
                    rtb.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(g.tableauMessages[(int)ce.editionErreur] + Environment.NewLine + ex.ToString());
            }
        }

        #endregion

        #region StylePolice

        private void StylePolice_Click(object sender, EventArgs e)
        {
            try
            {
                TextEditorKMJ oTextEditor = this.ActiveMdiChild as TextEditorKMJ;

                if (sender == grasToolStripButton)
                    oTextEditor.ChangerAttributsPolice(FontStyle.Bold);
                else if (sender == italiqueToolStripButton)
                    oTextEditor.ChangerAttributsPolice(FontStyle.Italic);
                else
                    oTextEditor.ChangerAttributsPolice(FontStyle.Underline);
            }
            catch (Exception ex)
            {
                MessageBox.Show(g.tableauMessages[(int)ce.changerStyleErreur] + Environment.NewLine + ex.ToString());
            }
        }

        #endregion

        #region Alignment

        private void Alignement_Click(object sender, EventArgs e)
        {
            try
            {
                TextEditorKMJ oTextEditor = this.ActiveMdiChild as TextEditorKMJ;

                if (sender == alignementGaucheToolStripButton)
                {
                    alignementDroiteToolStripButton.Checked = false;
                    alignementCentrerToolStripButton.Checked = false;

                    oTextEditor.textEditorRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
                }
                else if (sender == alignementDroiteToolStripButton)
                {
                    alignementGaucheToolStripButton.Checked = false;
                    alignementCentrerToolStripButton.Checked = false;

                    oTextEditor.textEditorRichTextBox.SelectionAlignment = HorizontalAlignment.Right;
                }
                else
                {
                    alignementGaucheToolStripButton.Checked = false;
                    alignementDroiteToolStripButton.Checked = false;

                    oTextEditor.textEditorRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(g.tableauMessages[(int)ce.changerAlignementErreur] + Environment.NewLine + ex.ToString());
            }

        }

        #endregion

        #region Méthode désactiver opérations MenusBarreOutils

        private void DesactiverOperationsMenusBarreOutils()
        {
            try
            {
                foreach (ToolStripItem oToolStripItem in textEditorKMJMenuStrip.Items)
                {
                    if (oToolStripItem is ToolStripMenuItem)
                    {
                        foreach (ToolStripItem oToolStripMenuItem in (oToolStripItem as ToolStripMenuItem).DropDownItems)
                        {
                            if (oToolStripMenuItem is ToolStripMenuItem)
                                oToolStripMenuItem.Enabled = false;
                        }
                    }
                }
                foreach (ToolStripItem oToolStripItem in textEditorKMJToolStrip.Items)
                {
                    oToolStripItem.Enabled = false;
                }

                nouveauToolStripButton.Enabled = true;
                nouveauToolStripMenuItem.Enabled = true;
                ouvrirToolStripButton.Enabled = true;
                ouvrirToolStripMenuItem.Enabled = true;
                quitterToolStripMenuItem.Enabled = true;
                aidetextEditorToolStripMenuItem.Enabled = true;
                aProposToolStripMenuItem.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region Méthode activer opérations MenusBarreOutils

        private void ActiverOperationsMenusBarreOutils()
        {
            try
            {
                foreach (ToolStripItem oToolStripItem in textEditorKMJMenuStrip.Items)
                {
                    if (oToolStripItem is ToolStripMenuItem)
                    {
                        foreach (ToolStripItem oToolStripMenuItem in (oToolStripItem as ToolStripMenuItem).DropDownItems)
                        {
                            if (oToolStripMenuItem is ToolStripMenuItem)
                                oToolStripMenuItem.Enabled = true;
                        }
                    }
                }
                foreach (ToolStripItem oToolStripItem in textEditorKMJToolStrip.Items)
                {
                    oToolStripItem.Enabled = true;
                }

                couperToolStripButton.Enabled = false;
                couperToolStripMenuItem.Enabled = false;
                copierToolStripButton.Enabled = false;
                copierToolStripMenuItem.Enabled = false;

                if (Clipboard.ContainsText() || Clipboard.ContainsImage())
                {
                    collerToolStripButton.Enabled = true;
                    collerToolStripMenuItem.Enabled = true;
                }
                else
                {
                    collerToolStripButton.Enabled = false;
                    collerToolStripMenuItem.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region Méthode MdiChildActivate

        private void TextEditorKMJParentForm_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
            {
                DesactiverOperationsMenusBarreOutils();
                infoToolStripStatusLabel.Text = "Créer ou ouvrir un TextEditor";
                statusINS = "INS";

                if (this.OwnedForms.Length > 0)
                    this.OwnedForms[0].Close();

            }
            else
            {

                if ((this.ActiveMdiChild as TextEditorKMJ).ModeInseree)
                    statusINS = "INS";
                else
                    statusINS = "RFP";

                infoToolStripStatusLabel.Text = this.ActiveMdiChild.Text;
            }

            insertToolStripStatusLabel.Text = statusINS;
        }

        #endregion

        #region Méthode KeyDown

        private void TextEditorKMJParentForm_KeyDown(object sender, KeyEventArgs e)
        {

            if (Control.IsKeyLocked(Keys.CapsLock))
                capsLockToolStripStatusLabel.Text = "MAJ";
            else
                capsLockToolStripStatusLabel.Text = string.Empty;


            if (e.KeyCode == Keys.Insert)
            {
                if (statusINS == "INS")
                {
                    statusINS = "RFP";
                    if (this.ActiveMdiChild != null)
                        (this.ActiveMdiChild as TextEditorKMJ).ModeInseree = false;
                }
                else
                {
                    statusINS = "INS";
                    if (this.ActiveMdiChild != null)
                        (this.ActiveMdiChild as TextEditorKMJ).ModeInseree = true;
                }

                insertToolStripStatusLabel.Text = statusINS;
            }
        }

        #endregion

        #region Mise en page
        private void fontsToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            RichTextBox rtb = (this.ActiveMdiChild as TextEditorKMJ).textEditorRichTextBox;

            Font myFont = rtb.SelectionFont;

            if (myFont != null)
            {
                try
                {
                    rtb.SelectionFont = new Font(fontsToolStripComboBox.Text, myFont.Size, myFont.Style);
                    this.ActiveMdiChild.ActiveControl.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void sizeToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RichTextBox rtb = (this.ActiveMdiChild as TextEditorKMJ).textEditorRichTextBox;

                Font myFont = rtb.SelectionFont;

                if (myFont != null)
                {
                    if (myFont.Size != 13)
                    {
                        try
                        {
                            rtb.SelectionFont = new Font(myFont.FontFamily, int.Parse(sizeToolStripComboBox.Text), myFont.Style);
                            this.ActiveMdiChild.ActiveControl.Focus();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Deux tailles différentes sont sélectionnées.");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void formatPoliceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditorKMJ textEditor = this.ActiveMdiChild as TextEditorKMJ;
            RichTextBox rtf = textEditor.textEditorRichTextBox;
            try
            {
                textEditorFontDialog.Font = textEditor.textEditorRichTextBox.SelectionFont;

                if (textEditorFontDialog.ShowDialog() == DialogResult.OK)
                    rtf.SelectionFont = textEditorFontDialog.Font;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        #endregion

        #region Affichage des polices

        #region DrawItem

        private void MyComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {            

            try
            {
                Graphics g = e.Graphics;

                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                e.DrawBackground();

                if ((e.State & DrawItemState.Focus) != 0)
                    e.DrawFocusRectangle();

                g.DrawString(((Font)myComboBox.Items[e.Index]).Name, (Font)myComboBox.Items[e.Index], Brushes.Black, e.Bounds);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        #endregion

        #region MeasureItem
        private void MyComboBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            try
            {
                ComboBox oComboBox = sender as ComboBox;
                int lastIndex = oComboBox.Items.Count - 1;
                Font oFont = oComboBox.Items[lastIndex] as Font;
                float size = e.Graphics.MeasureString(oFont.Name, oFont).Width;

                if (size > myComboBox.DropDownWidth)
                    oComboBox.DropDownWidth = (int)(size + 1);
                oComboBox.Width = oComboBox.DropDownWidth;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        #endregion

        #region Afficher Polices
        private void AfficherPolicesInstalleesOrdinateur()
        {

            myComboBox.Font = new Font("Microsoft Sans Serif", 11.25F);
            
            try
            {
                Font oFont = null;
            
                FontCollection oPolicesInstallees = new InstalledFontCollection();

                foreach (FontFamily oPolice in oPolicesInstallees.Families)
                {
                    if (oPolice.IsStyleAvailable(FontStyle.Regular))
                        oFont = new Font(oPolice, 12.0F, FontStyle.Regular);
                    
                    myComboBox.Items.Add(oFont);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        //public void AfficherPolicesInstalleesOrdinateur()
        //{
        //    try
        //    {
        //        InstalledFontCollection oInstalledFonts = new InstalledFontCollection();

        //        foreach (FontFamily oFontFamily in oInstalledFonts.Families)
        //            fontsToolStripComboBox.Items.Add(oFontFamily.Name);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        #endregion

        #endregion

        #region Recherche
        private void rechercherToolStripMenuItem_Click(object sender, EventArgs e)
        {									

            if (this.OwnedForms.Length == 0)											
            {
                rechercherForm rechercherDialog;											

                try
                {
                    rechercherDialog = new rechercherForm();
                    
                    rechercherDialog.Owner = this;
                    
                    rechercherDialog.Mot = (this.ActiveMdiChild.ActiveControl as RichTextBox).SelectedText;
                    rechercherDialog.Show(); 										
                }
                catch (Exception rechercherException)
                {
                    MessageBox.Show(g.tableauMessages[(int)ce.erreurIndeterminee] + rechercherException.Message, "Rechercher", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        #endregion

        #region About Box

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditorKMJAboutBox aboutBox = new TextEditorKMJAboutBox();

            aboutBox.ShowDialog();
        }

        #endregion
    }
}