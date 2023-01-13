namespace BoardGenerator.Control
{
    partial class BoardEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BoardEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Name = "BoardEditor";
            this.Size = new System.Drawing.Size(359, 287);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BoardEditor_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BoardEditor_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BoardEditor_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BoardEditor_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BoardEditor_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BoardEditor_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
