using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MangaDownloader.Model;
using MangaDownloader.Controller;
using MangaDownloader.Utility;
using System.Threading;

namespace MangaDownloader
{
    public partial class FrmDownload : Form
    {
        /// <summary>
        /// list item to be downloaded
        /// </summary>
        private BindingList<DownloadItem> lstDownload;

        /// <summary>
        /// manual reset event
        /// use to hold the process when the number of concurrent download reach its limit
        /// </summary>
        private ManualResetEvent _eventReset;

        /// <summary>
        /// queue item download
        /// </summary>
        private Queue<DownloadItem> queue;

        /// <summary>
        /// current system state
        /// </summary>
        private EnmSystemState state;

        /// <summary>
        /// background worker to do async task
        /// </summary>
        private BackgroundWorker worker;

        /// <summary>
        /// configuration
        /// </summary>
        private Configuration.Configuration config;

        /// <summary>
        /// current system state
        /// change state button when changed
        /// </summary>
        private EnmSystemState State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;

                switch (this.state)
                {
                    case EnmSystemState.Done :
                        this.btnPause.Enabled = false;
                        this.btnStop.Enabled = false;
                        this.btnResume.Enabled = false;
                        this.btnStart.Enabled = true;
                        break;
                    case EnmSystemState.Pause :
                        this.btnPause.Enabled = false;
                        this.btnStop.Enabled = true;
                        this.btnResume.Enabled = true;
                        this.btnStart.Enabled = false;
                        break;
                    case EnmSystemState.Working :
                        this.btnPause.Enabled = true;
                        this.btnStop.Enabled = true;
                        this.btnResume.Enabled = false;
                        this.btnStart.Enabled = false;
                        break;
                    default :
                        throw new Exception();                        
                }
            }
        }

        /// <summary>
        /// lock object
        /// use to synchornize update number of concurrent download
        /// </summary>
        private object objLock;

        /// <summary>
        /// number of current concurrent download
        /// </summary>
        private int noOfDonwload;

        /// <summary>
        /// number of current concurrent download
        /// </summary>
        private int NoOfDownload
        {
            get
            {
                lock (this.objLock)
                {
                    return this.noOfDonwload;
                }
            }
            set
            {
                lock (this.objLock)
                {
                    this.noOfDonwload = value;

                    if (this.noOfDonwload >= this.config.ConcurrentDownload)
                    {
                        //signal download thread to wait when concurrent download limit reach
                        this._eventReset.Reset();
                    }
                    else
                    {
                        //signal download thread to continue when concurrent download still available
                        this._eventReset.Set();
                    }
                }
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        public FrmDownload()
        {
            try
            {
                InitializeComponent();

                //initialize field                
                this.lstDownload = new BindingList<DownloadItem>();
                this._eventReset = new ManualResetEvent(false);
                this.worker = new BackgroundWorker();
                this.config = Configuration.Configuration.getConfig();
                this.queue = new Queue<DownloadItem>();
                this.objLock = new object();
                this.noOfDonwload = 0;
                this.State = EnmSystemState.Done;
                this.dgvDownload.AutoGenerateColumns = false;

                //register events
                this.lstDownload.ListChanged += lstDownload_ListChanged;
                this.btnAdd.Click += btnAdd_Click;
                this.Load += FrmDownload_Load;
                this.btnStart.Click += btnStart_Click;
                this.btnStop.Click += btnStop_Click;
                this.btnPause.Click += btnPause_Click;
                this.btnResume.Click += btnResume_Click;
                this.worker.DoWork += worker_DoWork;

                this.Text = GlobalVariable.getTitle("Download Screen");                
            }
            catch (Exception)
            {                
                throw;
            }            
        }

        /// <summary>
        /// worker do work event
        /// run async work
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                this._eventReset.Set();
                this.Download();
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// handle button resume click event
        /// resume all paused process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnResume_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DownloadItem item in this.lstDownload.Where(x => x.State == EnmDownloadState.Pause))
                {
                    item.Resume();
                }

                this.State = EnmSystemState.Working;
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// handle button pause click event
        /// pause all working process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnPause_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DownloadItem item in this.lstDownload.Where(x => x.State == EnmDownloadState.Downloading))
                {
                    item.Pause();
                }

                this.State = EnmSystemState.Pause;
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// handle button stop click event
        /// stop current download process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DownloadItem item in this.lstDownload.Where(x => x.State != EnmDownloadState.Cancelled
                    && x.State != EnmDownloadState.Done))
                {
                    item.Stop();
                }

                this.State = EnmSystemState.Done;
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// handle button start click event 
        /// start download process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                //do nothing if there's no item to download
                if (this.dgvDownload.DataSource == null || this.dgvDownload.Rows.Count == 0)
                {
                    return;
                }

                this.State = EnmSystemState.Working;
                this.worker.RunWorkerAsync();
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// handle listDownload list changed event
        /// update datagridview data source
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lstDownload_ListChanged(object sender, ListChangedEventArgs e)
        {
            try
            {
                //InvokeUti.InvokeProperty(this.dgvDownload, "DataSource", this.lstDownload);
                //this.SetDGVValue(this.lstDownload);
                this.dgvDownload.Invoke((MethodInvoker) (() => this.dgvDownload.DataSource = this.lstDownload));
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// handle form load event
        /// show form configuration if file config not exist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrmDownload_Load(object sender, EventArgs e)
        {
            try
            {
                //show form config if configuration file not exist
                if (!Configuration.Configuration.IsConfigurationExist())
                {
                    FrmConfig frm = new FrmConfig();
                    frm.ShowDialog();
                }
            }
            catch (Exception)
            {                
                MessageBox.Show(GlobalVariable.ERROR_MESSAGE, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// handle button add click event
        /// show form select download
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (FrmSelectDownload frm = new FrmSelectDownload())
                {                    
                    frm.ItemAdded += frm_ItemAdded;
                    frm.ShowDialog();
                }                
            }
            catch (Exception)
            {
                MessageBox.Show(GlobalVariable.ERROR_MESSAGE, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// handle form select download item added event
        /// add item to list download item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frm_ItemAdded(object sender, ItemAddedEventArgs e)
        {
            try
            {
                DownloadItem[] items = (DownloadItem[])e.Items;

                for (int i = 0; i < items.Length; i++)
                {
                    this.lstDownload.Add(items[i]);
                    this.queue.Enqueue(items[i]);
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// run download process in async thread
        /// </summary>
        private void Download()
        {
            try
            {
                //continuosly run while current system state is working
                while (this.State == EnmSystemState.Working)
                {
                    //get each item from queue and do download task
                    while (this.queue.Count > 0)
                    {
                        //wait for Set()
                        this._eventReset.WaitOne();

                        DownloadItem item = this.queue.Dequeue();
                        item.Completed += item_Completed;
                        item.Start();

                        this.NoOfDownload += 1;
                    }
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// item download completed event
        /// subtract number of current download to 1
        /// set current system state to done if there's no item to download
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void item_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                this.NoOfDownload -= 1;

                if (this.queue.Count == 0 && this.lstDownload.Where(x => x.State == EnmDownloadState.Downloading).Count() == 0)
                {
                    this.State = EnmSystemState.Done;
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }

        private delegate void SetDGVValueDelegate(BindingList<DownloadItem> items);

        private void SetDGVValue(BindingList<DownloadItem> items)
        {
            if (dgvDownload.InvokeRequired)
            {
                dgvDownload.Invoke(new SetDGVValueDelegate(SetDGVValue), items);
            }
            else
            {
                dgvDownload.DataSource = items;
            }
        }
    }
}
