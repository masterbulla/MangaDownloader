using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MangaDownloader.Utility
{
    /// <summary>
    /// invoker util
    /// use to invoke member in cross thread
    /// </summary>
    public static class InvokeUti
    {
        /// <summary>
        /// delegate to cross thread invoke UI's control's property
        /// </summary>
        /// <param name="control"></param>
        /// <param name="propName"></param>
        /// <param name="value"></param>
        delegate void InvokePropertyDelegate(Control control, string propName, object value);

        /// <summary>
        /// invoke control property from cross thread
        /// </summary>
        /// <param name="control"></param>
        /// <param name="propName"></param>
        /// <param name="value"></param>
        public static void InvokeProperty(Control control, string propName, object value)
        {
            try
            {
                if (control.InvokeRequired)
                {
                    control.Invoke(new InvokePropertyDelegate(InvokeProperty), control, propName, value);
                }
                else
                {
                    control.GetType().InvokeMember(propName, System.Reflection.BindingFlags.SetProperty, null, control, new object[] { value });
                }
            }
            catch (Exception)
            {                
                throw;
            }
        }
    }
}
