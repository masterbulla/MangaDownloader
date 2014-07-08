using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MangaDownloader.Model;

namespace MangaDownloader.Controller.Interface
{
    /// <summary>
    /// downloader interface
    /// </summary>
    public interface IDownloader
    {
        /// <summary>
        /// report download progress changed
        /// </summary>
        event ProgressChangedEventHandler ProgressChanged;

        /// <summary>
        /// report when download progress completed
        /// </summary>
        event RunWorkerCompletedEventHandler Completed;

        /// <summary>
        /// download chapter
        /// </summary>
        /// <param name="chapter"></param>
        void Download(Chapter chapter);

        /// <summary>
        /// pause download process
        /// </summary>
        void Pause();

        /// <summary>
        /// resume download process
        /// </summary>
        void Resume();

        /// <summary>
        /// stop download process
        /// </summary>
        void Stop();
    }
}
