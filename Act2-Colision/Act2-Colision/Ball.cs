using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics.Metrics;

namespace Act2_Colision
{
    public class Ball
    {
        public float diameter, radio;
        public int speedX, speedY;
        public Point pos, dir;
        public Size space;

        public Ball(Random rand, Size size)
        {
            space = size;
            pos = new Point(rand.Next(0, size.Width), rand.Next(0, size.Height));
            dir = new Point(1 + rand.Next(size.Width) / 100, 1 + rand.Next(size.Height) / 100);

            speedX = rand.Next(-10, 10);
            speedY = rand.Next(-10, 10);

            diameter = rand.Next(25, 65); ;
            radio = diameter / 2;
        }
        public void Render(Graphics g)
        {
            g.FillEllipse(Brushes.Purple, pos.X - radio, pos.Y - radio, diameter, diameter);
            g.DrawEllipse(Pens.Cyan, pos.X - radio, pos.Y - radio, diameter, diameter);
        }

        private bool isColliding = false;

        public void Update(Block block)
        {
            if ((pos.X - radio) <= 2)
            {
                pos.X = 3 + (int)radio;
                speedX *= -1;
            }
            if ((pos.X + radio) >= space.Width - 2)
            {
                pos.X = (int)(space.Width - radio);
                speedX *= -1;
            }
            if ((pos.Y - radio) <= 2)
            {
                pos.Y = 3 + (int)radio;
                speedY *= -1;
            }
            if ((pos.Y + radio) >= space.Height - 2)
            {
                pos.Y = (int)(space.Height - radio);
                speedY *= -1;
            }
            //Colisión con el bloque
            if (CheckCollisionWithBlock(block))
            {
                //Verificar si la pelota ya está en estado de colisión
                if (!isColliding)
                {
                    //Pelota en estado de colisión, ejecutar lógica de colisión
                    //Invertir la dirección, ajustar posición, etc.
                    speedX *= -1;
                    speedY *= -1;

                    //Establecer el estado de colisión a true
                    isColliding = true;
                }
            }
            else
            {
                //La pelota ya no está en colisión, restablecer el estado
                isColliding = false;
            }

            float desplazamientoX = speedX * (float)Math.Cos(dir.X);
            float desplazamientoY = speedY * (float)Math.Sin(dir.Y);

            pos.X = (int)(pos.X + desplazamientoX);
            pos.Y = (int)(pos.Y + desplazamientoY);
        }
        public bool CheckCollisionWithBlock(Block block)
        {
            //Verificar si la pelota colisiona con el bloque
            if (pos.X - radio <= block.pos.X + block.size.Width &&
                pos.X + radio >= block.pos.X &&
                pos.Y - radio <= block.pos.Y + block.size.Height &&
                pos.Y + radio >= block.pos.Y)
            {
                return true; //Hay colisión
            }

            return false; //No hay colisión
        }
    }
}