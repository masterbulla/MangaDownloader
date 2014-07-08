using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace MangaDownloader.Utility
{
    /// <summary>
    /// Html Utility
    /// </summary>
    public static class HtmlUti
    {
        /// <summary>
        /// request a html page
        /// </summary>
        /// <param name="url"></param>
        /// <returns>
        /// stream
        /// </returns>
        public static WebResponse Request(string url)
        {    
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/34.0.1847.137 Safari/537.36";

                return request.GetResponse();
            }
            catch (Exception)
            {
                throw;
            }            
        }
    }
}
