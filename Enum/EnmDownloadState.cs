using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaDownloader
{
    public enum EnmDownloadState
    {
        Queue = 0,
        Downloading = 1,
        Pause = 2,
        Cancelled = 3,
        Done = 4,
    }
}
