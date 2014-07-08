using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangaDownloader.Controller.Interface;
using Newtonsoft.Json;
using MangaDownloader.Factory;

namespace MangaDownloader.Model
{
    public class Manga
    {
        /// <summary>
        /// list of chapters
        /// </summary>
        private List<Chapter> chapters;

        /// <summary>
        /// manga name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// manga url
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// site which is this manga belong
        /// </summary>
        public EnmSite Site { get; set; }

        /// <summary>
        /// chapter list
        /// </summary>
        [JsonIgnore]
        public List<Chapter> Chapters 
        {
            get
            {
                try
                {
                    if (this.chapters == null)
                        this.chapters = this.ChapterLoader.loadChapter(this);

                    return this.chapters;
                }
                catch (Exception)
                {                    
                    throw;
                }
            }
        }

        /// <summary>
        /// chapter loader component
        /// </summary>
        private IChapterLoader ChapterLoader 
        {
            get
            {
                try
                {
                    return ComponentFactory.getLoader(this.Site);
                }
                catch (Exception)
                {                    
                    throw;
                }
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        public Manga(EnmSite site)
        {
            try
            {
                this.Site = site;
            }
            catch (Exception)
            {                
                throw;
            }
        }        
    }
}
