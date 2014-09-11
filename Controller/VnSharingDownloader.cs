using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MangaDownloader.Controller.Interface;
using System.ComponentModel;
using MangaDownloader.Model;
using System.Net;
using System.IO;
using MangaDownloader.Utility;
using HtmlAgilityPack;
using System.Threading;
using System.Drawing;

using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.Reflection;

namespace MangaDownloader.Controller
{
    public class VnSharingDownloader : IDownloader
    {
        #region Fields

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
        /// webbrowser control, used to achieve list of images url
        /// </summary>
        private WebBrowser browser;

        /// <summary>
        /// array of images url
        /// </summary>
        private string[] imagesURL;

        #endregion

        #region EventHandler

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

        #endregion 

        #region Constructor

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

                this.browser = new WebBrowser();
                this.browser.ScriptErrorsSuppressed = true;

                this.browser.DocumentCompleted += browser_DocumentCompleted;

                this._resetEvent = new ManualResetEvent(false);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        #endregion

        #region WebBrowser Event
        
        /// <summary>
        /// hadle when web browser loaded completely
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                if (e.Url.Equals(this.Chapter.URL))
                {
                    var result = this.browser.Document.InvokeScript("eval", new string[] { "lstImages" });
                    int length = (int)result.GetType().InvokeMember("length", BindingFlags.GetProperty, null, result, null);

                    this.imagesURL = new string[length];

                    for (int i = 0; i < length; i++)
                    {
                        imagesURL[i] = (string)result.GetType().InvokeMember("shift", BindingFlags.InvokeMethod, null, result, null);
                    }

                    this._resetEvent.Set();
                    this.worker.RunWorkerAsync();
                }                
            }
            catch (Exception)
            {
                
                throw;
            }          
        } 

        #endregion

        #region Background Worker Event

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

        #endregion

        #region Download Methods

        /// <summary>
        /// perform download item
        /// </summary>
        /// <param name="chapter"></param>
        public void Download(Model.Chapter chapter)
        {
            try
            {                
                this.Chapter = chapter;

                this.browser.Navigate(this.Chapter.URL);               
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
                for (int i = 0; i < this.imagesURL.Length; i++)
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
                    this.Download(this.imagesURL[i], i + 1);

                    int percentage = (int)((float)i / (float)(this.imagesURL.Length - 1) * 100);

                    this.worker.ReportProgress(percentage);
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
                        Image img = Image.FromStream(stream, false);

                        string saveFilePath = this.Chapter.SavePath + "\\" + pageNo.ToString() + img.RawFormat.GetFilenameExtension().Replace("*", "").Split(';').FirstOrDefault();

                        if (!Directory.Exists(saveFilePath))
                            Directory.CreateDirectory(Path.GetDirectoryName(saveFilePath));                                      

                        img.Save(saveFilePath);
                    }
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }

        #endregion

        #region Handle Download Thread (stop / pause / resume)

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

        #endregion
    }
}
