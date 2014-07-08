using MangaDownloader.Controller;
using MangaDownloader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MangaDownloader
{
    /// <summary>
    /// global variables use across the app
    /// </summary>
    public static class GlobalVariable
    {
        /// <summary>
        /// list of supported sites;
        /// </summary>
        private static List<Site> sites;

        /// <summary>
        /// list of supported sites
        /// </summary>
        public static List<Site> Sites
        {
            get
            {
                try
                {
                    if (sites == null)
                        sites = getSites();

                    return sites;
                }
                catch (Exception)
                {                    
                    throw;
                }
            }
        }

        /// <summary>
        /// folder that hold data 
        /// </summary>
        public static string DATA_FOLDER
        {
            get
            {
                return Application.StartupPath + "\\Data";
            }
        }

        /// <summary>
        /// configuration file
        /// </summary>
        public static string CONFIG_PATH
        {
            get
            {
                return Application.StartupPath + "\\Configuration\\configuration.xml";
            }
        }

        /// <summary>
        /// common error message
        /// </summary>
        public static string ERROR_MESSAGE
        {
            get
            {
                return "Awww Shit! A wild bug appeared. Call me to handle him.";
            }
        }

        /// <summary>
        /// get list of supported site
        /// </summary>
        /// <returns></returns>
        private static List<Site> getSites()
        {
            try
            {
                List<Site> sites = new List<Site>();

                //VnSharing site
                Site site = new Site(new VnSharingUpdater(), DATA_FOLDER + "\\VnSharing.txt");
                site.Name = "VnSharing";
                sites.Add(site);

                return sites;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// decorate form title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string getTitle(string title)
        {
            try
            {
                title += " - ";
                title += Application.ProductName;
                title += " - ";
                title += Application.ProductVersion;

                return title;
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
