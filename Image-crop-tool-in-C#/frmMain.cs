using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_crop_tool_in_C_
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // chargement\
            Bitmap bmp = new Bitmap("C:\\Users\\Victoire\\Pictures\\Moi\\Portrait\\jpg\\vicky.jpg");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = (Image)bmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // declaration des events ...

            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);
            pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_MouseMove);
            pictureBox1.MouseEnter += new EventHandler(pictureBox1_MouseEnter);

            Controls.Add(pictureBox1);
        }

        // declaration des var pour crop cordonates
        int crpX, crpY, rectW, rectH;
        // declaration crop pen for croping
        public Pen crpPen = new Pen(Color.White);
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Cursor = Cursors.Cross;
                crpPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                
                // On met les cordonnees initiale...
                crpX = e.X;
                crpY = e.Y;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = "Dimwnaions :" + rectW + "," + rectH;
            Cursor = Cursors.Default;

            Bitmap bmp2 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bmp2, pictureBox1.ClientRectangle);

            Bitmap crpImg = new Bitmap(rectW, rectH);

            for (int i = 0; i < rectW; i++)
            {
                for (int y = 0; y < rectH; y++)
                {
                    Color pxlclr = bmp2.GetPixel(crpX + i, crpY + y);
                    crpImg.SetPixel(i, y, pxlclr);
                }
            }
            pictureBox2.Image = (Image)crpImg;
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Cross;
        } 

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                pictureBox1.Refresh();
                // on met la h et la l ...
                rectW = e.X - crpX;
                rectH = e.Y - crpY;
                Graphics g = pictureBox1.CreateGraphics();
                g.DrawRectangle(crpPen, crpX, crpY, rectW, rectH);
                g.Dispose();
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Default;
        }
    }
}
