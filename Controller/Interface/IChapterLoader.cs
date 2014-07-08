using MangaDownloader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaDownloader.Controller.Interface
{
    /// <summary>
    /// component to load chapters for a specific manga
    /// </summary>
    public interface IChapterLoader
    {
        /// <summary>
        /// load list of chapters
        /// </summary>
        /// <param name="manga"></param>
        List<Chapter> loadChapter(Manga manga);
    }
}
