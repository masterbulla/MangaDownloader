using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangaDownloader.Controller;

namespace MangaDownloader.Factory
{
    /// <summary>
    /// factory to initialize component instance
    /// </summary>
    public static class ComponentFactory 
    {
        /// <summary>
        /// get chapter loader component base on site
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        public static Controller.Interface.IChapterLoader getLoader(EnmSite site)
        {
            try
            {
                switch (site)
                {
                    case EnmSite.VnSharing:
                        return new VnSharingChapterLoader();
                        break;
                    default:
                        throw new Exception();
                        break;
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
