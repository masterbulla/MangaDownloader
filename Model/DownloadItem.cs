using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaDownloader.Model
{
    /// <summary>
    /// item to download
    /// </summary>
    public class DownloadItem : INotifyPropertyChanged
    {
        /// <summary>
        /// current downloaded progress
        /// </summary>
        private string progress = "0%";

        /// <summary>
        /// current download state
        /// </summary>
        private EnmDownloadState state = EnmDownloadState.Queue;

        /// <summary>
        /// chapter to download
        /// </summary>
        public Chapter Chapter { get; set; }

        /// <summary>
        /// site
        /// </summary>
        public string Site
        {
            get
            {
                return this.Chapter.Manga.Site.ToString();
            }
        }

        /// <summary>
        /// manga name
        /// </summary>
        public string MangaName
        {
            get
            {
                return this.Chapter.Manga.Name;
            }
        }

        /// <summary>
        /// chapter
        /// </summary>
        public string ChapterName
        {
            get
            {
                return this.Chapter.Name;
            }
        }

        /// <summary>
        /// current downloaded progress
        /// raise property changed event when changed
        /// </summary>
        public string Progress
        {
            get
            {
                return this.progress;
            }
            set
            {
                this.progress = value;

                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Progress"));
                }
            }
        }

        /// <summary>
        /// current download state
        /// raise property changed event when changed
        /// </summary>
        public EnmDownloadState State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;

                if (this.PropertyChanged != null)
                {
                    //this.PropertyChanged(this, new PropertyChangedEventArgs("State"));
                }
            }
        }

        /// <summary>
        /// save folder
        /// </summary>
        public string SavePath
        {
            get
            {
                return this.Chapter.SavePath;
            }
        }

        /// <summary>
        /// property changed event
        /// raise when property change
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// run worker completed event handler
        /// notify when download process completed
        /// </summary>
        public event RunWorkerCompletedEventHandler Completed;

        /// <summary>
        /// constructor
        /// dependency with Chapter
        /// </summary>
        /// <param name="chapter"></param>
        public DownloadItem(Chapter chapter)
        {
            try
            {
                this.Chapter = chapter;
                this.Chapter.ProgressChanged += Chapter_ProgressChanged;
                this.Chapter.Completed += Chapter_Completed;
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// handle chapter download completed
        /// change state to Done if proccess has not been cancelled
        /// raise completed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Chapter_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (!e.Cancelled)
                {
                    this.State = EnmDownloadState.Done;
                }
                else
                {
                    this.State = EnmDownloadState.Done;
                }

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
        /// handle chapter progress changed
        /// update progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Chapter_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                this.Progress = e.ProgressPercentage.ToString() + "%";
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// start download process
        /// </summary>
        public void Start()
        {
            try
            {
                this.State = EnmDownloadState.Downloading;
                this.Chapter.Download();
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
                this.State = EnmDownloadState.Cancelled;
                this.Chapter.Stop();
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
                if (this.state == EnmDownloadState.Downloading)
                {
                    this.State = EnmDownloadState.Pause;
                    this.Chapter.Pause();
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
                if (this.State == EnmDownloadState.Pause)
                {
                    this.State = EnmDownloadState.Downloading;
                    this.Chapter.Resume();
                }                
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
