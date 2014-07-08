using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MangaDownloader
{
    public partial class FrmConfig : Form
    {
        /// <summary>
        /// disable close button
        /// </summary>
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        /// <summary>
        /// configuration
        /// </summary>
        private Configuration.Configuration config;

        /// <summary>
        /// constructor
        /// </summary>
        public FrmConfig()
        {
            InitializeComponent();
            this.btnLoadFile.Click += btnLoadFile_Click;
            this.btnOK.Click += btnOK_Click;
            this.Load += FrmConfig_Load;

            this.config = Configuration.Configuration.getConfig();
        }

        /// <summary>
        /// handle form load event
        /// initialize data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrmConfig_Load(object sender, EventArgs e)
        {
            try
            {
                this.txtPath.Text = this.config.DownloadFolder;
                this.nmrConcurrentDownload.Value = this.config.ConcurrentDownload;
                this.chkConvert.Checked = this.config.ConvertPDF;
            }
            catch (Exception)
            {                
                MessageBox.Show(GlobalVariable.ERROR_MESSAGE, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// handle btnOK click event
        /// save configuration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string path = this.txtPath.Text.Trim();

                if (string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(path))
                {
                    MessageBox.Show("Hey man, you forgot to choose a path to go.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.txtPath.Focus();
                    return;
                }

                if (!Directory.Exists(path))
                {
                    MessageBox.Show("Hey man, that directory doesnot exist. Choose a valid one.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.txtPath.Focus();
                    return;
                }

                this.config.DownloadFolder = path;
                this.config.ConcurrentDownload = (int)this.nmrConcurrentDownload.Value;
                this.config.ConvertPDF = this.chkConvert.Checked;
                this.config.save();

                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show(GlobalVariable.ERROR_MESSAGE, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// show folder browse dialog to select saved folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnLoadFile_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.SelectedPath = this.config.DownloadFolder;
                DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    this.txtPath.Text = dialog.SelectedPath;
                }
            }
            catch (Exception)
            {
                MessageBox.Show(GlobalVariable.ERROR_MESSAGE, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
