using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace MangaDownloader.Configuration
{
    /// <summary>
    /// application's configurations
    /// singleton pattern
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// folder to save downloaded file
        /// </summary>
        public string DownloadFolder { get; set; }

        /// <summary>
        /// number of concurrency download
        /// </summary>
        public int ConcurrentDownload { get; set; }

        /// <summary>
        /// determine whether downloaded file will be converted to pdf
        /// </summary>
        public bool ConvertPDF { get; set; }

        /// <summary>
        /// private constructor
        /// </summary>
        private Configuration()
        {
            this.DownloadFolder = "C:\\";
            this.ConcurrentDownload = 1;
            this.ConvertPDF = true;
        }

        /// <summary>
        /// check whether configuration file exist
        /// </summary>
        /// <returns></returns>
        public static bool IsConfigurationExist()
        {
            try
            {
                return File.Exists(GlobalVariable.CONFIG_PATH);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// read configuration from xml
        /// </summary>
        /// <returns></returns>
        public static Configuration getConfig()
        {
            try
            {
                //return new configuration if configuration file not exist
                if (!IsConfigurationExist())
                {
                    return new Configuration();
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
                Configuration config;
                
                using (StreamReader reader = new StreamReader(GlobalVariable.CONFIG_PATH))
                { 
                    config = (Configuration)serializer.Deserialize(reader);
                }

                return config;
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// save configuration
        /// </summary>
        public void save()
        {
            try
            {
                //create folder if not exist
                if (!Directory.Exists(Path.GetDirectoryName(GlobalVariable.CONFIG_PATH)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(GlobalVariable.CONFIG_PATH));
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Configuration));

                using (StreamWriter writer = new StreamWriter(GlobalVariable.CONFIG_PATH))
                {
                    serializer.Serialize(writer, this);
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
