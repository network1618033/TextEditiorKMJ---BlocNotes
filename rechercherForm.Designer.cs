namespace TextEditorKMJ
{
    partial class rechercherForm
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
            this.motARechercheTextBox = new System.Windows.Forms.TextBox();
            this.annulerButton = new System.Windows.Forms.Button();
            this.suivantButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // motARechercheTextBox
            // 
            this.motARechercheTextBox.Location = new System.Drawing.Point(34, 29);
            this.motARechercheTextBox.Name = "motARechercheTextBox";
            this.motARechercheTextBox.Size = new System.Drawing.Size(307, 22);
            this.motARechercheTextBox.TabIndex = 0;
            // 
            // annulerButton
            // 
            this.annulerButton.Location = new System.Drawing.Point(216, 74);
            this.annulerButton.Name = "annulerButton";
            this.annulerButton.Size = new System.Drawing.Size(125, 40);
            this.annulerButton.TabIndex = 1;
            this.annulerButton.Text = "Annuler";
            this.annulerButton.UseVisualStyleBackColor = true;
            this.annulerButton.Click += new System.EventHandler(this.annulerButton_Click);
            // 
            // suivantButton
            // 
            this.suivantButton.Location = new System.Drawing.Point(34, 74);
            this.suivantButton.Name = "suivantButton";
            this.suivantButton.Size = new System.Drawing.Size(137, 40);
            this.suivantButton.TabIndex = 2;
            this.suivantButton.Text = "Suivant";
            this.suivantButton.UseVisualStyleBackColor = true;
            this.suivantButton.Click += new System.EventHandler(this.suivantButton_Click);
            // 
            // rechercherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 145);
            this.Controls.Add(this.suivantButton);
            this.Controls.Add(this.annulerButton);
            this.Controls.Add(this.motARechercheTextBox);
            this.Name = "rechercherForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rechercher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox motARechercheTextBox;
        private System.Windows.Forms.Button annulerButton;
        private System.Windows.Forms.Button suivantButton;
    }
}