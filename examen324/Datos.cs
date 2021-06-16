using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace examen324
{
    public class Datos
    {
        int r, g, b;
        Color color;
 
        public Datos(int r, int g, int b, Color c)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            Color = c;
        }
        
        public Color Color { get => color; set => color = value; }
        public int R { get => r; set => r = value; }
        public int G { get => g; set => g = value; }
        public int B { get => b; set => b = value; }
    }
}
