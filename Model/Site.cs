using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangaDownloader;
using MangaDownloader.Controller.Interface;
using System.IO;
using Newtonsoft.Json;

namespace MangaDownloader.Model
{
    public class Site
    {
        /// <summary>
        /// site name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// manga list
        /// </summary>
        public List<Manga> Mangas { get; set; }

        /// <summary>
        /// Updater
        /// </summary>
        private IUpdater Updater { get; set; }

        /// <summary>
        /// file name associate with this instance
        /// </summary>
        private string fileName = "";

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="updater"></param>
        public Site(IUpdater updater, string fileName)
        {
            try
            {
                this.Updater = updater;
                this.fileName = fileName;

                //read mangas from file
                this.read();
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// update manga list
        /// </summary>
        public virtual void Update()
        {
            try
            {
                this.Updater.update(this.fileName);
                this.read();
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// deserialize text file to manga list
        /// </summary>
        public virtual void read()
        {
            try
            {
                ///update mangas if data not exist
                if (!File.Exists(this.fileName))
                {
                    this.Updater.update(this.fileName);
                }

                string content;

                using (StreamReader reader = new StreamReader(this.fileName))
                {
                    content = reader.ReadToEnd();
                }

                this.Mangas = (List<Manga>)JsonConvert.DeserializeObject(content, typeof (List<Manga>));
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
