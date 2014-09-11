using MangaDownloader.Controller.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaDownloader.Model
{
    public class Chapter
    {
        /// <summary>
        /// chapter name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// chapter url
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// save path
        /// </summary>
        public string SavePath { get; set; }

        /// <summary>
        /// configuration
        /// </summary>
        private Configuration.Configuration Config = Configuration.Configuration.getConfig();
       
        /// <summary>
        /// manga name
        /// </summary>
        public string MangaName
        {
            get
            {
                return this.Manga.Name;
            }
        }

        /// <summary>
        /// dowloader
        /// </summary>
        private IDownloader Downloader { get; set; }

        /// <summary>
        /// manga which this chapter belong
        /// </summary>
        public Manga Manga { get; set; }

        /// <summary>
        /// progress changed event handler
        /// notify when progress changed
        /// </summary>
        public event ProgressChangedEventHandler ProgressChanged;

        /// <summary>
        /// run worker completed event handler
        /// notify when download progress completed
        /// </summary>
        public event RunWorkerCompletedEventHandler Completed;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="manga"></param>
        public Chapter(Manga manga, IDownloader downloader)
        {
            try
            {
                this.Manga = manga;
                this.Downloader = downloader;
                this.SavePath = this.Config.DownloadFolder + "\\" + this.Manga.Site.ToString() + "\\" + this.MangaName + "\\" + this.Name;

                foreach (char invalidCharacter in Path.GetInvalidPathChars())
                {
                    this.SavePath = this.SavePath.Replace(invalidCharacter.ToString(), "");
                }

                this.Downloader.ProgressChanged += Downloader_ProgressChanged;
                this.Downloader.Completed += Downloader_Completed;
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// handle downloader completed event 
        /// raised completed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Downloader_Completed(object sender, RunWorkerCompletedEventArgs e)
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
        /// handler downloader progress changed
        /// raise progress changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Downloader_ProgressChanged(object sender, ProgressChangedEventArgs e)
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
        /// download this manga
        /// </summary>
        public void Download()
        {
            try
            {
                this.Downloader.Download(this);
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
                this.Downloader.Pause();
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
                this.Downloader.Stop();
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
                this.Downloader.Resume();
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
