using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangaDownloader.Controller.Interface;
using System.ComponentModel;
using MangaDownloader.Model;
using System.Net;
using System.IO;
using MangaDownloader.Utility;
using HtmlAgilityPack;
using System.Threading;
using System.Drawing;

namespace MangaDownloader.Controller
{
    public class VnSharingDownloader : IDownloader 
    {
        /// <summary>
        /// manual reset event
        /// use to implement pause and resume functionality
        /// </summary>
        private ManualResetEvent _resetEvent;

        /// <summary>
        /// background worker to do async job
        /// </summary>
        private BackgroundWorker worker;

        /// <summary>
        /// chapter to download
        /// </summary>
        private Chapter Chapter;

        /// <summary>
        /// progress changed event
        /// notify when download progress changed
        /// </summary>
        public event System.ComponentModel.ProgressChangedEventHandler ProgressChanged;

        /// <summary>
        /// download completed event
        /// notify when download completed
        /// </summary>
        public event System.ComponentModel.RunWorkerCompletedEventHandler Completed;

        /// <summary>
        /// constructor
        /// </summary>
        public VnSharingDownloader()
        {
            try
            {
                this.worker = new BackgroundWorker();
                this.worker.WorkerReportsProgress = true;
                this.worker.WorkerSupportsCancellation = true;

                this.worker.DoWork += worker_DoWork;
                this.worker.ProgressChanged += worker_ProgressChanged;
                this.worker.RunWorkerCompleted += worker_RunWorkerCompleted;

                this._resetEvent = new ManualResetEvent(false);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// background worker completed event
        /// notify when process finished
        /// raise completed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (this.Completed != null)
                {
                    this.Completed(this, e);
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// background woker progress changed
        /// notify when current progress changed
        /// raise progress changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                if (this.ProgressChanged != null)
                {
                    this.ProgressChanged(this, e);
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// background worker do work event
        /// perform asynchronous action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                this.Download(e);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// perform download item
        /// </summary>
        /// <param name="chapter"></param>
        public void Download(Model.Chapter chapter)
        {
            try
            {
                this.Chapter = chapter;
                this._resetEvent.Set();
                this.worker.RunWorkerAsync();
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// perform download in asynchronous thread
        /// </summary>
        /// <param name="e"></param>
        private void Download(DoWorkEventArgs e)
        {
            try
            {
                WebResponse response;
                Stream stream;
                HtmlDocument document = new HtmlDocument();

                using (response = HtmlUti.Request(this.Chapter.URL))
                {
                    using (stream = response.GetResponseStream())
                    {
                        document.Load(stream);

                        List<HtmlNode> imgNodes = document.DocumentNode.Descendants()
                                                                    .Where(x => x.Name == "div" && x.Attributes.Contains("id")
                                                                        && x.Attributes["id"].Value == "divImage").FirstOrDefault()
                                                                    .Descendants()
                                                                    .Where(x => x.Name == "img").ToList();
                        HtmlNodeCollection nodes = document.DocumentNode.Descendants()
                                                                    .Where(x => x.Name == "div" && x.Attributes.Contains("id")
                                                                        && x.Attributes["id"].Value == "divImage").FirstOrDefault().ChildNodes;
                        int totalPage = imgNodes.Count;
                        int currentPage = 0;

                        for (; currentPage < totalPage; currentPage++)
                        {
                            //wait for start or resume signal
                            this._resetEvent.WaitOne();

                            //cancel process when cancel signal was sent
                            if (this.worker.CancellationPending)
                            {
                                e.Cancel = true;
                                return;
                            }

                            //do download process
                            string imgURL = imgNodes[currentPage].Attributes["src"].Value;

                            this.Download(imgURL, currentPage);

                            int percentage = currentPage / totalPage * 100;

                            this.worker.ReportProgress(percentage);
                        }
                    }
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// download image and save to folder
        /// </summary>
        /// <param name="url"></param>
        private void Download(string url, int pageNo)
        {
            try
            {
                using (WebResponse response = HtmlUti.Request(url))
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        Image img = Image.FromStream(stream);
                        img.Save(this.Chapter.SavePath + "\\" + pageNo.ToString() + "." + img.RawFormat.GetFilenameExtension());
                    }
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// pause download process
        /// </summary>
        public void Pause()
        {
            try
            {
                if (this.worker.IsBusy)
                {
                    this._resetEvent.Reset();
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// resume download process
        /// </summary>
        public void Resume()
        {
            try
            {
                if (this.worker.IsBusy)
                {
                    this._resetEvent.Set();
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// stop download process
        /// </summary>
        public void Stop()
        {
            try
            {
                if (this.worker.IsBusy)
                {
                    this.worker.CancelAsync();
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
