using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace MangaDownloader
{
    /// <summary>
    /// custom button
    /// </summary>
    public class CustomButton : Button 
    {
        private bool onHover = false;
        /// <summary>
        /// get or set border color of control
        /// </summary>
        [Category("Appearance")]
        public Color BorderColor 
        { 
            get
            {
                return this.FlatAppearance.BorderColor;
            }
            set
            {
                this.FlatAppearance.BorderColor = value;
            }
        }

        /// <summary>
        /// get or set border size of control
        /// </summary>
        [Category("Appearance")]
        public int BorderSize
        {
            get;
            set;
        }

        /// <summary>
        /// get or set color of control when hover
        /// </summary>
        [Category("Appearance")]
        public Color HoverColor { get; set; }

        public CustomButton()
        {
            this.FlatAppearance.BorderSize = 0;
        }

        /// <summary>
        /// override on paint method
        /// draw border with selected color
        /// </summary>
        /// <param name="pevent"></param>
        protected override void OnPaint(PaintEventArgs pevent)
        {
            try
            {
                Console.WriteLine(this.onHover.ToString());

                if (this.onHover)
                {
                    this.FlatAppearance.BorderSize = this.BorderSize;
                }
                else
                {
                    this.FlatAppearance.BorderSize = 0;
                }


                base.OnPaint(pevent);                
            }
            catch (Exception)
            {                
                throw;
            }            
        }

        protected override void OnMouseHover(EventArgs e)
        {
            try
            {
                Console.WriteLine("Mouse Hover");
                this.onHover = true;
                this.Invalidate();
                base.OnMouseHover(e);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            try
            {
                Console.WriteLine("Mouse Leave");
                this.onHover = false;
                this.Invalidate();
                base.OnMouseLeave(e);
            }
            catch (Exception)
            {
                throw;
            }
        }        
    }
}
