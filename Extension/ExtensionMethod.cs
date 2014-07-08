using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaDownloader
{
    /// <summary>
    /// hold the custom extensions method
    /// </summary>
    public static class ExtensionMethod
    {
        /// <summary>
        /// get image extension name
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetFilenameExtension(this ImageFormat format)
        {
            return ImageCodecInfo.GetImageEncoders().FirstOrDefault(x => x.FormatID == format.Guid).FilenameExtension;
        }
    }
}
