using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaDownloader.Controller.Interface
{
    public interface IUpdater
    {
        /// <summary>
        /// update manga list
        /// </summary>
        /// <param name="fileName"></param>
        void update(string fileName);
    }
}
