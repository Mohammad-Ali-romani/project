using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    class EditImage
    {
        OpenFileDialog openfile;
        SaveFileDialog savefile;
        Image img;
        public EditImage()
        {
            openfile = new OpenFileDialog();
            savefile = new SaveFileDialog();
            savefile.Filter = "JPG|*.jpg |PNG | *.png |TIFF | *.tiff | All | *.BMP  ";
        }
        public Image openFile(PictureBox picture)
        {
            if (openfile.ShowDialog() == DialogResult.OK)
                img = Image.FromFile(openfile.FileName);
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            picture.Image = img;
            return img;
        }
        public void saveFile(PictureBox picture)
        {
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                picture.Image.Save(savefile.FileName);
            }
        }
        public void mirror(PictureBox picture)
        {
            Bitmap simg = new Bitmap(picture.Image);
            int w = simg.Width;
            int h = simg.Height;

            Bitmap mi = new Bitmap(w * 2, h);
            for (int y = 0; y < h; y++)
            {
                for (int x = 0, rx = w * 2 - 1; x < w; x++, rx--)
                {
                    Color p = simg.GetPixel(x, y);
                    mi.SetPixel(x, y, p);
                    mi.SetPixel(rx, y, p);
                }
            }
            picture.Image = mi;
        }
    }
  
}
