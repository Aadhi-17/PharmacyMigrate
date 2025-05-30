namespace KRATOS
{
    partial class KRATOSMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KRATOSMainForm));
            this.kratos1 = new KRATOS.UserControls.KRATOS();
            this.SuspendLayout();
            // 
            // kratos1
            // 
            this.kratos1.BackColor = System.Drawing.Color.White;
            this.kratos1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.kratos1.Location = new System.Drawing.Point(2, 12);
            this.kratos1.Name = "kratos1";
            this.kratos1.Size = new System.Drawing.Size(1187, 531);
            this.kratos1.TabIndex = 0;
            // 
            // KRATOSMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1212, 588);
            this.Controls.Add(this.kratos1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KRATOSMainForm";
            this.Text = "KRATOS";
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.KRATOS kratos1;
    }
}

