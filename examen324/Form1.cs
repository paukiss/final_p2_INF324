using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace examen324
{

    public partial class Form1 : Form
    {

        Datos[] D;
        int pos;
        Boolean activar;
        Dictionary<string, Datos[]> textura = new Dictionary<string, Datos[]>();
        int cR, cG, cB;
        int cRt, cGt, cBt;
        Color colorAdd;
        // Constructor
        public Form1()
        {
            InitializeComponent();
            pos = 0;
            activar = false;
            D = new Datos[8];

            D[0] = new Datos(6, 12, 98, Color.Green);
            D[1] = new Datos(4, 8, 90, Color.Green);
            D[2] = new Datos(5, 13, 85, Color.Green);
            D[3] = new Datos(9, 20, 74, Color.Green);
            D[4] = new Datos(11, 17, 107, Color.Green);
            D[5] = new Datos(3, 3, 75, Color.Green);
            D[6] = new Datos(6, 17, 98, Color.Green);
            D[7] = new Datos(6, 10, 35, Color.Green);

            textura.Add("agua", D);

            D = new Datos[8];

            D[0] = new Datos(247, 147, 75, Color.DarkViolet);
            D[1] = new Datos(247, 117, 52, Color.DarkViolet);
            D[2] = new Datos(244, 87, 34, Color.DarkViolet);
            D[3] = new Datos(189, 29, 2, Color.DarkViolet);
            D[4] = new Datos(231, 76, 27, Color.DarkViolet);
            D[5] = new Datos(204, 47, 10, Color.DarkViolet);
            D[6] = new Datos(224, 56, 16, Color.DarkViolet);
            D[7] = new Datos(249, 140, 67, Color.DarkViolet);

            textura.Add("ropaNaranja", D);
        }

        // Cargar Imagen
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            Bitmap bmp = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = bmp;
            int w = pictureBox1.Image.Size.Width;
            int h = pictureBox1.Image.Size.Height;
            Bitmap bmp2 = new Bitmap(pictureBox1.Image, width: pictureBox1.Size.Width - 5, height: pictureBox1.Size.Height - 5);
            pictureBox1.Image = bmp2;
        }

        // Seleccionar Pixeles
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (pos > 7)
            {
                MessageBox.Show("Ya tiene 8 pixeles seleccionados");
                return;
            }

            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Color c = new Color();
            c = bmp.GetPixel(e.X, e.Y);
            cR = c.R; cG = c.G; cB = c.B;
            cRt = 0; cGt = 0; cBt = 0;
            for (int i = e.X; i < e.X + 5; i++)
                for (int j = e.Y; j < e.Y + 5; j++)
                {
                    c = bmp.GetPixel(i, j);
                    cRt = c.R + cRt; cGt = c.G + cGt; cBt = c.B + cBt;
                }
            cRt = cRt / 25;
            cGt = cGt / 25;
            cBt = cBt / 25;
            Console.WriteLine(cRt + " " + cGt + " " + cBt);
            textBox1.Text = c.R.ToString();
            textBox2.Text = c.G.ToString();
            textBox3.Text = c.B.ToString();
            if (activar)
            {
                D[pos] = new Datos(cRt, cGt, cBt, colorAdd);
                pos++;
                conta.Text = "cont: " + pos;
            }

        }

        // Agregar una nueva textura a texturaar
        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Agregar un Nombre a la textura\n2. Seleccion un color\n3. Seleccione 8 pixeles en la imagen");
            pos = 0;
            D = new Datos[8];
            conta.Text = "cont: 0";
            textBox4.Text = "";
            button2.BackColor = Color.FromArgb(50, Color.Red);
            activar = true;
        }
        

        // Seleccionar un color
        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button2.BackColor = colorDialog1.Color;
                colorAdd = colorDialog1.Color;
            }
        }

        // Guadar el flujo de la textura 
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                textura.Add(textBox4.Text, D);
                activar = false;
                while (tableLayoutPanel1.Controls.Count > 0)
                {
                    tableLayoutPanel1.Controls[0].Dispose();
                }
            }
            catch
            {
                Console.WriteLine("No se pudo guardar el color");
            }
        }

        // Obtener pixel RGB del cuadro 2
        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox2.Image);
            Color c = new Color();
            c = bmp.GetPixel(e.X, e.Y);
            textBox1.Text = c.R.ToString();
            textBox2.Text = c.G.ToString();
            textBox3.Text = c.B.ToString();
        }

        // texturaar textura de la imagen
        private void button1_Click_1(object sender, EventArgs e)
        {
            while (tableLayoutPanel1.Controls.Count > 0)
            {
                tableLayoutPanel1.Controls[0].Dispose();
            }
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            int cRto, cGto, cBto;
            Color c = new Color();
            for (int i = 0; i < bmp.Width - 3; i = i + 3)
            {

                for (int j = 0; j < bmp.Height - 3; j = j + 3)
                {

                    cRto = 0; cGto = 0; cBto = 0;
                    for (int k = i; k < i + 3; k++)
                        for (int l = j; l < j + 3; l++)
                        {
                            c = bmp.GetPixel(k, l);
                            cRto = c.R + cRto; cGto = c.G + cGto; cBto = c.B + cBto;
                        }
                    cRto = cRto / 9;
                    cGto = cGto / 9;
                    cBto = cBto / 9;
                    c = bmp.GetPixel(i, j);

                    for (int k = i; k < i + 3; k++)
                    {
                        for (int l = j; l < j + 3; l++)
                        {
                            Boolean sw = true;
                            foreach (KeyValuePair<string, Datos[]> kvp in textura)
                            {
                                for (int m = 0; m < kvp.Value.Length; m++)
                                {
                                    if ((kvp.Value[m].R - 10 <= c.R && c.R <= kvp.Value[m].R + 10) && (kvp.Value[m].G - 10 <= c.G && c.G <= kvp.Value[m].G + 10) && (kvp.Value[m].B - 10 <= c.B && c.B <= kvp.Value[m].B + 10))
                                    {
                                        bmp2.SetPixel(k, l, kvp.Value[m].Color);
                                        sw = false;
                                    }
                                }
                            }
                            if (sw)
                            {
                                c = bmp.GetPixel(k, l);
                                bmp2.SetPixel(k, l, c);

                            }
                        }
                    }
                }
            }
            pictureBox2.Image = bmp2;

            TableLayoutPanel panel = tableLayoutPanel1;

            panel.ColumnCount = 3;
            panel.RowCount = 1;
            panel.ColumnStyles[0].Width = 10;
            panel.ColumnStyles[1].Width = 10;
            panel.ColumnStyles[2].Width = 5;


            panel.Controls.Add(new Label() { Text = "Tipo" }, 0, 0);
            panel.Controls.Add(new Label() { Text = "Color" }, 1, 0);
            panel.Controls.Add(new Label() { Text = "Eliminar" }, 2, 0);

            Button eliminar = new Button();
            foreach (KeyValuePair<string, Datos[]> kvp in textura)
            {
                eliminar = new Button();
                eliminar.Text = "eliminar";
                panel.RowCount = panel.RowCount + 1;
                panel.Controls.Add(new Label() { Text = kvp.Key }, 0, panel.RowCount - 1);
                panel.Controls.Add(new Label() { BackColor = kvp.Value[0].Color }, 1, panel.RowCount - 1);
                //panel.Controls.Add(new Label() { }, 2, panel.RowCount - 1);
            }

        }

        // Obtener columna y fila de la Tabla
        Point? GetRowColIndex(TableLayoutPanel tlp, Point point)
        {
            if (point.X > tlp.Width || point.Y > tlp.Height)
                return null;

            int w = tlp.Width;
            int h = tlp.Height;
            int[] widths = tlp.GetColumnWidths();

            int i;
            for (i = widths.Length - 1; i >= 0 && point.X < w; i--)
                w -= widths[i];
            int col = i + 1;

            int[] heights = tlp.GetRowHeights();
            for (i = heights.Length - 1; i >= 0 && point.Y < h; i--)
                h -= heights[i];

            int row = i + 1;

            return new Point(col, row);
        }

        // Eliminar datos Textura
        private void tableLayoutPanel1_Click(object sender, EventArgs e)
        {
            var cellPos = GetRowColIndex(
               tableLayoutPanel1,
               tableLayoutPanel1.PointToClient(Cursor.Position));


            Control c = tableLayoutPanel1.GetControlFromPosition(0, cellPos.Value.Y);

            if (c != null)
            {
                Console.WriteLine(c.Text);
                textura.Remove(c.Text);
            }
            button1_Click_1(sender, e);
            //Console.Write(cellPos.Value);
        }
    }



}
