using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangaDownloader.Controller.Interface;
using HtmlAgilityPack;
using MangaDownloader.Utility;
using System.Windows.Forms;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.IO;
using MangaDownloader.Model;
using System.Net;
using Newtonsoft.Json;

namespace MangaDownloader.Controller
{
    /// <summary>
    /// updater for VnSharing
    /// </summary>
    public class VnSharingUpdater : IUpdater 
    {         
        /// <summary>
        /// root URL
        /// </summary>
        private string rootURL = "http://truyen.vnsharing.net";

        /// <summary>
        /// manga list url
        /// </summary>
        private string directoryURL = "http://truyen.vnsharing.net/DanhSach";

        /// <summary>
        /// list of manga
        /// </summary>
        private List<Manga> Mangas;

        /// <summary>
        /// constructor
        /// </summary>
        public VnSharingUpdater()
        {
            try
            {
                this.Mangas = new List<Manga>();

                
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// update list of manga
        /// </summary>
        public void update(string fileName)
        {
            try
            {
                WebResponse response;
                Stream stream;
                HtmlDocument document = new HtmlDocument();

                //offset of manga directory current page
                int currentPage = 1;

                //offset of manga directory last page
                int lastPage;

                response = HtmlUti.Request(this.directoryURL);
                stream = response.GetResponseStream();
                document.Load(stream, Encoding.UTF8);
                lastPage = this.getLastPage(document);
                

                do
                {
                    IEnumerable<HtmlNode> trNodes = (from node in document.DocumentNode.Descendants()
                                                     where node.Name == "table" && node.Attributes.Contains("class")
                                                            && node.Attributes["class"].Value == "listing"
                                                     select node).FirstOrDefault().ChildNodes.Where(x => x.Name == "tr").Skip(2);

                    foreach (HtmlNode node in trNodes)
                    {
                        Manga manga = new Manga(EnmSite.VnSharing);
                        manga.URL = this.rootURL + node.ChildNodes.Where(x => x.Name == "td").FirstOrDefault().ChildNodes["a"].Attributes["href"].Value + "&confirm=yes";
                        manga.Name = node.ChildNodes.Where(x => x.Name == "td").FirstOrDefault().ChildNodes["a"].InnerText.Trim();

                        this.Mangas.Add(manga);
                    }

                    //close current connection
                    //in order to make a new one
                    stream.Close();
                    response.Close();

                    currentPage++;

                    //make new connection for next loop
                    if (currentPage <= lastPage)
                    {
                        response = HtmlUti.Request(this.directoryURL + "?Page=" + currentPage.ToString());
                        stream = response.GetResponseStream();
                        document.Load(stream, Encoding.UTF8);
                    }
                }
                while(currentPage <= lastPage);

                this.write(fileName);
            }
            catch (Exception)
            {               
                throw;
            }
        }        

        /// <summary>
        /// get last page of manga list
        /// </summary>
        private int getLastPage(HtmlDocument document)
        {
            try
            {
                HtmlNode ulNode = (from node in document.DocumentNode.Descendants()
                                   where node.Name == "ul"  && node.Attributes.Contains("class")
                                        && node.Attributes["class"].Value == "pager"
                                   select node).FirstOrDefault();

                return int.Parse(ulNode.ChildNodes.Where(x => x.Name == "li").LastOrDefault().ChildNodes["a"].Attributes["page"].Value);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// write updated mangas to file
        /// </summary>
        /// <param name="fileName"></param>
        private void write(string fileName)
        {
            try
            {
                //create directory if not exist
                if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                }

                string updatedMangas = JsonConvert.SerializeObject(this.Mangas);

                using (StreamWriter writer = new StreamWriter(fileName, false))
                {
                    writer.Write(updatedMangas);                    
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }        
    }
}
