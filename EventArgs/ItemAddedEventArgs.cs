using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaDownloader
{
    /// <summary>
    /// event arguments for ItemAddedEventHandler
    /// </summary>
    public class ItemAddedEventArgs
    {
        /// <summary>
        /// item has been added
        /// </summary>
        public object Items { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="Items"></param>
        public ItemAddedEventArgs(object Items)
        {
            try
            {
                this.Items = Items;
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
