using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiMediaCV;
using MultiMediaCV.Enums;
using AForge;
using AForge.Video;
using System.Drawing.Drawing2D;

namespace project
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        // كلاس تحريك صور
        GraphicsPath gp = new GraphicsPath();
        float dx, dy;
        EditImage editimage;
        Image img;
        // اول تشغيل البرنامج
        private void Form1_Load(object sender, EventArgs e)
        {
            editimage = new EditImage();
        }
        // فتح الصورة
        private void openFile(object sender, EventArgs e)
        {
            img = editimage.openFile(picture);
        }
        // تشغيل الكاميرا
        private void openCamara(object sender, EventArgs e)
        {
            picture.GetImageFromCamera();
        }
        //حفظ الصورة
        private void saveFile(object sender, EventArgs e)
        {
            editimage.saveFile(picture);
        }
     
        private void Resizeeee(object sender, EventArgs e)
        {
            //     pictureBox1.Image = ImageMode.resizeImage(pictureBox1.Image, 25, 25);
            picture.Image = Edit_Photo.Resize(@"E:\برشلونة 2019-2020\79448668_224261181898793_2341877415459946496_o.jpg", int.Parse(textw.Text), int.Parse(texth.Text));
        }
 
        //نسخ الصورة
        private void btncopy(object sender, EventArgs e)
        {
            
            pictureBox2.Image = Edit_Photo.Copy(picture.Image);
            pictureBox2.Visible = true;
        }
        //قطع جزء من الصورة
        private void btncut(object sender, EventArgs e)
        {
            picture.Crop(Cursor);
        }
        //عكس اتجاه الصورة
        private void btnreverse(object sender, EventArgs e)
        {
            picture.Image = Edit_Photo.Edit_Reverse(picture.Image);
        } 
        // 90تحريك صورة 
        private void btnrotate(object sender, EventArgs e)
        {
            picture.Image = Edit_Photo.Edit_Rotate(picture.Image);
        }

        //دمج صورتين بعكس بعض
        private void btnmirror(object sender, EventArgs e)
        {
            editimage.mirror(picture);
            
        }
        private void azoom1(object sender, EventArgs e)
        {
            picture.ResetZoom();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                downPoint = e.Location;
                if (gp.GetBounds(new Matrix(1, 0, 0, 1, dx, dy)).Contains(e.Location))
                {
                    gp.Transform(new Matrix(1, 0, 0, 1, dx, dy));
                    hitOn = true;
                }
            }
            picture.MouseDownCV(e, Cursor, Color.Yellow);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (hitOn)
                {
                    dx = e.X - downPoint.X;
                    dy = e.Y - downPoint.Y;
                    picture.Invalidate();
                }
                else
                {
                    picture.Left += e.X - downPoint.X;
                    picture.Top += e.Y - downPoint.Y;
                }
            }

            picture.MouseMoveCV(e);
        }

        private void btnred(object sender, EventArgs e)
        {
            picture.Image = Edit_Photo.Edit_Red(img);
        }

        private void btnGreen(object sender, EventArgs e)
        {
            picture.Image = Edit_Photo.Edit_Green(img);
        }

        private void btnBlue(object sender, EventArgs e)
        {
            picture.Image = Edit_Photo.Edit_Blue(img);
        }

        private void btnGray(object sender, EventArgs e)
        {
            picture.Image = Edit_Photo.Edit_Gray(img);
        }

        private void btn_Black(object sender, EventArgs e)
        {
            picture.Image = Edit_Photo.Edit_Black(img);
        }

        private void bnWhite(object sender, EventArgs e)
        {
            picture.Image = Edit_Photo.Edit_White(img);
        }

        private void btnOpposite(object sender, EventArgs e)
        {
            picture.Image = Edit_Photo.Edit_Opposite(img);
        }

        private void btnDelete(object sender, EventArgs e)
        {
            picture.Image = null;
        }

        private void btncaptrue(object sender, EventArgs e)
        {
            picture.CaptuerWebCam(this);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {//مشان تحريك صورة

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            gp.Transform(new Matrix(1, 0, 0, 1, dx, dy));//Translate and paint
            e.Graphics.FillPath(Brushes.Red, gp);
            gp.Transform(new Matrix(1, 0, 0, 1, -dx, -dy));//translate back (reset to old location)
        }
        System.Drawing.Point downPoint;
        bool hitOn;


        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            hitOn = false;
        }



        private void btncayn(object sender, EventArgs e)
        {
            picture.Image = Edit_Photo.Getcayn(img);
        }

        private void btnyellow(object sender, EventArgs e)
        {
            picture.Image = Edit_Photo.Getyello(img);
        }

        private void btnmagenta(object sender, EventArgs e)
        {
            picture.Image = Edit_Photo.GetMagenta(img);
        }

        private void btnfirst(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
                img = Image.FromFile(ofd.FileName);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.Image = img;
        
    }

        private void btntow(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
                img = Image.FromFile(ofd.FileName);
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.Image = img;
        }

        private void btCombinetwopictures(object sender, EventArgs e)
        {
            picture.Image = Edit_Photo.Edit_Combine(pictureBox4.Image, pictureBox5.Image, (byte)trackBar1.Value);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            gp.AddString(textBox1.Text, Font.FontFamily,
            (int)Font.Style, 20, new System.Drawing.Point(10, 10), StringFormat.GenericDefault);
            
        }

        
        public string GetText(Image image)
        {
            Bitmap bm = new Bitmap(image);
            string strbit = "";
            string res = "";
            int length = 0;
            int m = 0;
            for (int i = 0; i < bm.Width; i++)
            {
                for (int j = 0; j < bm.Height; j++)
                {
                    Color c = bm.GetPixel(i, j);
                    strbit += (c.R % 2);
                    m++;
                    if (m == 4 * 8)
                    {
                        for (int k = 0; k < strbit.Length; k += 8)
                        {
                            string temp = strbit.Substring(k, 8);
                            res += (char)Convert.ToByte(temp, 2);
                        }
                        length = Convert.ToInt32(res.Substring(0, 4));
                        res = "";
                        strbit = "";
                    }
                    else if (m == (4 + length) * 8)
                    {
                        break;
                    }
                }
                if (m == (4 + length) * 8)
                {
                    break;
                }
            }
            for (int k = 0; k < strbit.Length; k += 8)
            {
                string temp = strbit.Substring(k, 8);
                res += (char)Convert.ToInt32(temp, 2);
            }
            return res;
        }
        Image IMG;
        public Image GetCode(Image image, string text)
        {
            Bitmap bm = new Bitmap(image);
            int textlength = text.Length;
            text = textlength.ToString("0000") + text;
            byte[] bits = new byte[text.Length * 8];
            int m = 0;
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int o = 128 >> j;//128-64-32-16-8-4-2
                    int w = o & text[i];
                    bits[m++] = (byte)(w == o ? 1 : 0);
                }
            }
            m = 0;
            for (int i = 0; i < bm.Width; i++)
            {
                for (int j = 0; j < bm.Height; j++)
                {
                    Color c = bm.GetPixel(i, j);
                    byte red = (byte)(c.R % 2 == 0 ? c.R : c.R - 1);//set 0
                    red += bits[m++];
                    bm.SetPixel(i, j, Color.FromArgb(red, c.G, c.B));
                    if (m == bits.Length)
                    {
                        break;
                    }
                }
                if (m == bits.Length)
                {
                    break;
                }
            }
            return bm;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (picture.Image != null)
                picture.Image = GetCode(picture.Image, textBox2.Text);
            textBox2.Text = "";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            textBox3.Text = GetText(picture.Image);
        }


        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
           
        }


        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnadd3(object sender, EventArgs e)
        {

        }

    } 
    }
