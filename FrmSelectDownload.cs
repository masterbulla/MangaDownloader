using MangaDownloader.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MangaDownloader
{
    public partial class FrmSelectDownload : Form
    {
        /// <summary>
        /// Item Added Event Handler
        /// notify subcriber items has been added to queue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ItemAddedEventHandler(object sender, ItemAddedEventArgs e);

        /// <summary>
        /// item added event
        /// notify subcriber items has been added to queue
        /// </summary>
        public event ItemAddedEventHandler ItemAdded;

        /// <summary>
        /// constructor
        /// </summary>
        public FrmSelectDownload()
        {
            try
            {
                InitializeComponent();

                //register event
                this.Load += FrmSelectDownload_Load;
                this.lsbSite.SelectedIndexChanged += lsbSite_SelectedIndexChanged;
                this.lsbManga.SelectedIndexChanged += lsbManga_SelectedIndexChanged;
                this.txtSearchManga.TextChanged += txtSearchManga_TextChanged;
                this.btnQueue.Click += btnQueue_Click;
                this.btnClose.Click += btnClose_Click;
                this.PreviewKeyDown += FrmSelectDownload_PreviewKeyDown;
                this.KeyDown += FrmSelectDownload_KeyDown;
            }
            catch (Exception)
            {                
                throw;
            }           
        }

        void FrmSelectDownload_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (ModifierKeys == Keys.Control && e.KeyCode == Keys.A)
                {
                    for (int i = 0; i < this.lsbChapter.Items.Count; i++)
                    {
                        this.lsbChapter.SetSelected(i, true);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// handle preview key down event on form
        /// support for ctrl + A hot key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrmSelectDownload_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (ModifierKeys == Keys.Control && e.KeyCode == Keys.A)
                {                    
                    for (int i = 0; i < this.lsbChapter.Items.Count; i++)
                    {
                        this.lsbChapter.SetSelected(i, true);
                    }
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// handle button close click event
        /// close this form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// handle button queue click event
        /// raise event to notify items has been added to queue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnQueue_Click(object sender, EventArgs e)
        {
            try
            {
                DownloadItem[] downloadItems = new DownloadItem[this.lsbChapter.SelectedItems.Count];

                for (int i = 0; i < this.lsbChapter.SelectedItems.Count; i++)
                {
                    Chapter chapter = (Chapter)this.lsbChapter.SelectedItems[i];
                    DownloadItem item = new DownloadItem(chapter);
                    downloadItems[i] = item;
                }

                if (this.ItemAdded != null)
                {
                    this.ItemAdded(this, new ItemAddedEventArgs(downloadItems));
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// handle txt search manga text changed event
        /// search manga which is start with input character
        /// if text == "", get all list of manga
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtSearchManga_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Model.Site site = (Model.Site)this.lsbSite.SelectedItem;
                string searchString = this.txtSearchManga.Text.Trim();
                List<Manga> mangas;

                if (string.IsNullOrEmpty(searchString) || string.IsNullOrWhiteSpace(searchString))
                {
                    mangas = site.Mangas;
                }
                else
                {
                    mangas = (from manga in site.Mangas
                              where manga.Name.ToUpper().StartsWith(searchString.ToUpper())
                              select manga).ToList();
                }

                this.lsbManga.DataSource = new BindingSource() { DataSource = mangas };
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurred, please try again later!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// handle lsbManga selected index changed, load list of chapters associate with selected manga
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lsbManga_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Manga manga = (Manga)this.lsbManga.SelectedItem;

                this.lsbChapter.DataSource = new BindingSource() { DataSource = manga.Chapters };
                this.lsbChapter.DisplayMember = this.lsbChapter.ValueMember = "Name";

                this.lsbChapter.Focus();
                this.lsbChapter.SetSelected(0, true);
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurred, please try again later!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// handle lsbSite selected index changed, load list of manga
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lsbSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Model.Site site = (Model.Site)this.lsbSite.SelectedItem;
                this.lsbManga.DataSource = new BindingSource() { DataSource = site.Mangas };
                this.lsbManga.DisplayMember = this.lsbManga.ValueMember = "Name";

                //initilize auto complete string collection from manga name
                AutoCompleteStringCollection src = new AutoCompleteStringCollection();
                foreach (Manga manga in site.Mangas)
                {
                    src.Add(manga.Name);
                }

                this.txtSearchManga.AutoCompleteMode = AutoCompleteMode.Suggest;
                this.txtSearchManga.AutoCompleteSource = AutoCompleteSource.CustomSource;
                this.txtSearchManga.AutoCompleteCustomSource = src;                
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurred, please try again later!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// handle form load event
        /// initialize data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrmSelectDownload_Load(object sender, EventArgs e)
        {
            try
            {
                this.lsbSite.DataSource = new BindingSource() { DataSource = GlobalVariable.Sites };
                this.lsbSite.DisplayMember = this.lsbSite.ValueMember = "Name";
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurred, please try again later!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
