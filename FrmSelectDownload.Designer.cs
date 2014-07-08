namespace MangaDownloader
{
    partial class FrmSelectDownload
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lsbSite = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lsbChapter = new System.Windows.Forms.ListBox();
            this.lsbManga = new System.Windows.Forms.ListBox();
            this.txtSearchManga = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnQueue = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lsbSite);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(146, 326);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Site";
            // 
            // lsbSite
            // 
            this.lsbSite.FormattingEnabled = true;
            this.lsbSite.Location = new System.Drawing.Point(6, 17);
            this.lsbSite.Name = "lsbSite";
            this.lsbSite.Size = new System.Drawing.Size(134, 303);
            this.lsbSite.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lsbChapter);
            this.groupBox2.Controls.Add(this.lsbManga);
            this.groupBox2.Controls.Add(this.txtSearchManga);
            this.groupBox2.Location = new System.Drawing.Point(164, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(409, 326);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Manga Area";
            // 
            // lsbChapter
            // 
            this.lsbChapter.FormattingEnabled = true;
            this.lsbChapter.Location = new System.Drawing.Point(207, 43);
            this.lsbChapter.Name = "lsbChapter";
            this.lsbChapter.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lsbChapter.Size = new System.Drawing.Size(195, 277);
            this.lsbChapter.TabIndex = 2;
            // 
            // lsbManga
            // 
            this.lsbManga.FormattingEnabled = true;
            this.lsbManga.Location = new System.Drawing.Point(6, 43);
            this.lsbManga.Name = "lsbManga";
            this.lsbManga.Size = new System.Drawing.Size(195, 277);
            this.lsbManga.TabIndex = 1;
            // 
            // txtSearchManga
            // 
            this.txtSearchManga.Location = new System.Drawing.Point(6, 17);
            this.txtSearchManga.Name = "txtSearchManga";
            this.txtSearchManga.Size = new System.Drawing.Size(195, 20);
            this.txtSearchManga.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(579, 175);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnQueue
            // 
            this.btnQueue.Location = new System.Drawing.Point(579, 146);
            this.btnQueue.Name = "btnQueue";
            this.btnQueue.Size = new System.Drawing.Size(75, 23);
            this.btnQueue.TabIndex = 3;
            this.btnQueue.Text = "Queue";
            this.btnQueue.UseVisualStyleBackColor = true;
            // 
            // FrmSelectDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 343);
            this.Controls.Add(this.btnQueue);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmSelectDownload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select mangas to download";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lsbSite;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lsbChapter;
        private System.Windows.Forms.ListBox lsbManga;
        private System.Windows.Forms.TextBox txtSearchManga;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnQueue;
    }
}