using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaDownloader.Utility
{
    /// <summary>
    /// file utility
    /// perform save or read file
    /// </summary>
    public static class FileUti
    {
        /// <summary>
        /// write string to file
        /// </summary>
        /// <param name="content"></param>
        /// <param name="path"></param>
        public static void write(string content, string path)
        {
            try 
	        {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(content);
                }
	        }
	        catch (Exception)
	        {
		        throw;
	        }
        }

        /// <summary>
        /// read string from stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string read(Stream stream)
        {
            try
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
