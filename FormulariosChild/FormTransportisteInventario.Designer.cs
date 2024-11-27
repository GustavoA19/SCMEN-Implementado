namespace MaterialDesign.FormulariosChild
{
    partial class FormTransportisteInventario
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
            this.dgTransportistas = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.materialLabel8 = new MaterialSkin.Controls.MaterialLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgTransportistas)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgTransportistas
            // 
            this.dgTransportistas.AllowUserToAddRows = false;
            this.dgTransportistas.AllowUserToDeleteRows = false;
            this.dgTransportistas.AllowUserToResizeColumns = false;
            this.dgTransportistas.AllowUserToResizeRows = false;
            this.dgTransportistas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTransportistas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTransportistas.Location = new System.Drawing.Point(0, 54);
            this.dgTransportistas.Name = "dgTransportistas";
            this.dgTransportistas.RowHeadersWidth = 51;
            this.dgTransportistas.RowTemplate.Height = 24;
            this.dgTransportistas.Size = new System.Drawing.Size(992, 669);
            this.dgTransportistas.TabIndex = 130;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.materialLabel8);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(992, 54);
            this.groupBox2.TabIndex = 131;
            this.groupBox2.TabStop = false;
            // 
            // materialLabel8
            // 
            this.materialLabel8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.materialLabel8.AutoSize = true;
            this.materialLabel8.Depth = 0;
            this.materialLabel8.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel8.Location = new System.Drawing.Point(411, 18);
            this.materialLabel8.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel8.Name = "materialLabel8";
            this.materialLabel8.Size = new System.Drawing.Size(190, 19);
            this.materialLabel8.TabIndex = 127;
            this.materialLabel8.Text = "TABLA TRANSPORTISTAS";
            // 
            // FormTransportisteInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(992, 723);
            this.Controls.Add(this.dgTransportistas);
            this.Controls.Add(this.groupBox2);
            this.Name = "FormTransportisteInventario";
            this.Text = "Transportistas";
            ((System.ComponentModel.ISupportInitialize)(this.dgTransportistas)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgTransportistas;
        private System.Windows.Forms.GroupBox groupBox2;
        private MaterialSkin.Controls.MaterialLabel materialLabel8;
    }
}