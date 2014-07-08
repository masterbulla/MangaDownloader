namespace MangaDownloader
{
    partial class FrmConfig
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
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.nmrConcurrentDownload = new System.Windows.Forms.NumericUpDown();
            this.chkConvert = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nmrConcurrentDownload)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Save Path:";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(78, 6);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(148, 20);
            this.txtPath.TabIndex = 1;
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(232, 4);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(23, 23);
            this.btnLoadFile.TabIndex = 2;
            this.btnLoadFile.Text = "...";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Number of Concurrent Download:";
            // 
            // nmrConcurrentDownload
            // 
            this.nmrConcurrentDownload.Location = new System.Drawing.Point(183, 32);
            this.nmrConcurrentDownload.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nmrConcurrentDownload.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrConcurrentDownload.Name = "nmrConcurrentDownload";
            this.nmrConcurrentDownload.Size = new System.Drawing.Size(43, 20);
            this.nmrConcurrentDownload.TabIndex = 4;
            this.nmrConcurrentDownload.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nmrConcurrentDownload.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkConvert
            // 
            this.chkConvert.AutoSize = true;
            this.chkConvert.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkConvert.Location = new System.Drawing.Point(12, 62);
            this.chkConvert.Name = "chkConvert";
            this.chkConvert.Size = new System.Drawing.Size(105, 17);
            this.chkConvert.TabIndex = 5;
            this.chkConvert.Text = "Convert to PDF?";
            this.chkConvert.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(183, 94);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // FrmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 129);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkConvert);
            this.Controls.Add(this.nmrConcurrentDownload);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLoadFile);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.nmrConcurrentDownload)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nmrConcurrentDownload;
        private System.Windows.Forms.CheckBox chkConvert;
        private System.Windows.Forms.Button btnOK;
    }
}