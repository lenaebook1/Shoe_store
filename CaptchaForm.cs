using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class CaptchaForm : Form
    {
        public string Correct;
        public readonly Random random = new Random();


        public CaptchaForm()
        {
            InitializeComponent();
            GenerateImage();
        }

        public void GenerateImage()
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            int lenght = 5;

            Correct = new string(Enumerable.Repeat(chars, lenght).Select(s => s[random.Next(s.Length)]).ToArray());

            // очистка, так как не удаляется сборщиком мусора
            if (pictureBox1.Image != null ) { pictureBox1.Image.Dispose() ; }
            var bitmap = new Bitmap(440, 270);

            using (var g = Graphics.FromImage(bitmap))
            {
                // шум на фоне
                for (int i = 0; i < 100; i++)
                    bitmap.SetPixel(random.Next(440), random.Next(270), Color.Gray);

                // сглаживание
                g.SmoothingMode = SmoothingMode.AntiAlias;


                using (var font = new Font("Arial", 45, FontStyle.Italic))
                {
                    var path = new GraphicsPath();
                    float curX = 25;

                    foreach (char c  in Correct)
                    {
                        // генерация наклона
                        float rot = (float)(random.NextDouble() * 20+5);

                        // смещение 
                        float yOff = (float)(random.NextDouble() * 20-10);

                        float centerY = 135 + yOff;

                        var state = g.Save();

                        g.TranslateTransform(curX, centerY);
                        g.RotateTransform(rot);

                        var format = new StringFormat(StringFormatFlags.NoClip);
                        format.Alignment = StringAlignment.Center;
                        format.LineAlignment = StringAlignment.Center;

                        g.DrawString(c.ToString(), font, Brushes.Black, 0, 0, format);

                        g.Restore(state);

                        curX += 95;                

                        

                    }
                           


                }

                for (int i = 0; i < 7; i++)
                    g.DrawLine(Pens.Gray, random.Next(440), random.Next(270), random.Next(440), random.Next(270));
            }

            pictureBox1.Image = bitmap;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GenerateImage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Введите текст с картинки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox1.Text.Trim().ToUpper() == Correct.ToUpper())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Попробуйте еще раз", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GenerateImage();
            }
        }

       
    }
}
