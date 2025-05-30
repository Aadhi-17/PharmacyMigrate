namespace KRATOS
{
    partial class DBSelectDialog
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
            this.txtDataBase = new System.Windows.Forms.TextBox();
            this.bttnSerach = new System.Windows.Forms.Button();
            this.lxtbxDataBase = new System.Windows.Forms.ListBox();
            this.bttnChoose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDataBase
            // 
            this.txtDataBase.Location = new System.Drawing.Point(13, 32);
            this.txtDataBase.Margin = new System.Windows.Forms.Padding(4);
            this.txtDataBase.Name = "txtDataBase";
            this.txtDataBase.Size = new System.Drawing.Size(417, 22);
            this.txtDataBase.TabIndex = 315;
            this.txtDataBase.TextChanged += new System.EventHandler(this.txtDataBase_TextChanged_1);
            // 
            // bttnSerach
            // 
            this.bttnSerach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bttnSerach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(64)))), ((int)(((byte)(107)))));
            this.bttnSerach.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnSerach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bttnSerach.ForeColor = System.Drawing.Color.White;
            this.bttnSerach.Location = new System.Drawing.Point(440, 32);
            this.bttnSerach.Margin = new System.Windows.Forms.Padding(4);
            this.bttnSerach.Name = "bttnSerach";
            this.bttnSerach.Size = new System.Drawing.Size(99, 28);
            this.bttnSerach.TabIndex = 316;
            this.bttnSerach.Text = "Search";
            this.bttnSerach.UseVisualStyleBackColor = false;
            this.bttnSerach.Click += new System.EventHandler(this.bttnSerach_Click_1);
            // 
            // lxtbxDataBase
            // 
            this.lxtbxDataBase.FormattingEnabled = true;
            this.lxtbxDataBase.ItemHeight = 16;
            this.lxtbxDataBase.Location = new System.Drawing.Point(13, 82);
            this.lxtbxDataBase.Name = "lxtbxDataBase";
            this.lxtbxDataBase.Size = new System.Drawing.Size(526, 324);
            this.lxtbxDataBase.TabIndex = 317;
            this.lxtbxDataBase.SelectedIndexChanged += new System.EventHandler(this.lxtbxDataBase_SelectedIndexChanged);
            this.lxtbxDataBase.DoubleClick += new System.EventHandler(this.lxtbxDataBase_DoubleClick);
            this.lxtbxDataBase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lxtbxDataBase_KeyDown);
            // 
            // bttnChoose
            // 
            this.bttnChoose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bttnChoose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(64)))), ((int)(((byte)(107)))));
            this.bttnChoose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bttnChoose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bttnChoose.ForeColor = System.Drawing.Color.White;
            this.bttnChoose.Location = new System.Drawing.Point(440, 424);
            this.bttnChoose.Margin = new System.Windows.Forms.Padding(4);
            this.bttnChoose.Name = "bttnChoose";
            this.bttnChoose.Size = new System.Drawing.Size(99, 28);
            this.bttnChoose.TabIndex = 318;
            this.bttnChoose.Text = "Choose";
            this.bttnChoose.UseVisualStyleBackColor = false;
            this.bttnChoose.Click += new System.EventHandler(this.bttnChoose_Click);
            // 
            // DBSelectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 480);
            this.Controls.Add(this.bttnChoose);
            this.Controls.Add(this.lxtbxDataBase);
            this.Controls.Add(this.bttnSerach);
            this.Controls.Add(this.txtDataBase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DBSelectDialog";
            this.Text = "Select DataBase";
            this.Load += new System.EventHandler(this.DBSelectDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDataBase;
        private System.Windows.Forms.Button bttnSerach;
        private System.Windows.Forms.ListBox lxtbxDataBase;
        private System.Windows.Forms.Button bttnChoose;
    }
}