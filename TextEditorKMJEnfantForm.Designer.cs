namespace TextEditorKMJ
{
    partial class TextEditorKMJ
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
            this.textEditorRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // textEditorRichTextBox
            // 
            this.textEditorRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEditorRichTextBox.HideSelection = false;
            this.textEditorRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.textEditorRichTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.textEditorRichTextBox.Name = "textEditorRichTextBox";
            this.textEditorRichTextBox.Size = new System.Drawing.Size(681, 391);
            this.textEditorRichTextBox.TabIndex = 0;
            this.textEditorRichTextBox.Text = "";
            this.textEditorRichTextBox.SelectionChanged += new System.EventHandler(this.textEditorRichTextBox_SelectionChanged);
            // 
            // TextEditorKMJ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 391);
            this.Controls.Add(this.textEditorRichTextBox);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TextEditorKMJ";
            this.Text = "TextEditor - KMJ";
            this.Activated += new System.EventHandler(this.TextEditorKMJ_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TextEditorKMJ_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.RichTextBox textEditorRichTextBox;
    }
}