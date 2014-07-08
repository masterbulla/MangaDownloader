using HtmlAgilityPack;
using MangaDownloader.Controller.Interface;
using MangaDownloader.Model;
using MangaDownloader.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MangaDownloader.Controller
{
    /// <summary>
    /// vnsharing chapter loader component
    /// </summary>
    public class VnSharingChapterLoader : IChapterLoader
    {
        /// <summary>
        /// root URL
        /// </summary>
        private string rootURL = "http://truyen.vnsharing.net";

        /// <summary>
        /// load list of chapter
        /// </summary>
        /// <param name="manga"></param>
        /// <returns></returns>
        public List<Model.Chapter> loadChapter(Manga manga)
        {
            try
            {
                List<Chapter> chapters = new List<Chapter>();

                using (WebResponse response = HtmlUti.Request(manga.URL))
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        HtmlDocument document = new HtmlDocument();
                        document.Load(stream, Encoding.UTF8);

                        IEnumerable<HtmlNode> listingNodes = document.DocumentNode.Descendants()
                                                                    .Where(x => x.Name == "table" && x.Attributes.Contains("class")
                                                                        && x.Attributes["class"].Value == "listing").FirstOrDefault()
                                                                    .ChildNodes.Where(x => x.Name == "tr").Skip(2);

                        foreach (HtmlNode node in listingNodes)
                        {
                            HtmlNode chapterNode = node.ChildNodes.Where(x => x.Name == "td").FirstOrDefault().ChildNodes["a"];
                            Chapter chapter = new Chapter(manga, new VnSharingDownloader());
                            chapter.Name = chapterNode.InnerText.Trim();
                            chapter.URL = this.rootURL += chapterNode.Attributes["href"].Value;

                            chapters.Insert(0, chapter);
                        }
                    }
                }

                return chapters;
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
