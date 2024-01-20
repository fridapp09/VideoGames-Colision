using System.Diagnostics;

namespace Act2_Colision
{
    public partial class MyForm : Form
    {
        Ball ball;
        Bitmap bmp;
        Graphics g;
        Block block;
        public MyForm()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            ball = new Ball(new Random(), PCT_CANVAS.Size);

            //Generar posiciones y tamaños aleatorios para el bloque
            do
            {
                block = new Block(new Point(new Random().Next(0, PCT_CANVAS.Width - 150), 
                    new Random().Next(0, PCT_CANVAS.Height - 150)), 
                    new Size(new Random().Next(80, 220), 
                    new Random().Next(40, 160)));
            } while (CheckCollision(ball, block)); //Verificar si hay colisión inicial

            //Tamaño fijo bloque
            //block = new Block(new Point(450, 200), new Size(90, 130));

            //Imprimir las posiciones en el visor de salida
            Debug.WriteLine($"Pelota: ({ball.pos.X}, {ball.pos.Y}), Bloque: ({block.pos.X}, {block.pos.Y})");

            bmp = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
            g = Graphics.FromImage(bmp);
            PCT_CANVAS.Image = bmp;

            //Asocia el evento Paint del PictureBox con el método PCT_CANVAS_Paint
            PCT_CANVAS.Paint += PCT_CANVAS_Paint;

            //Método para verificar colisión entre la pelota y el bloque
            bool CheckCollision(Ball ball, Block block)
            {
                //Verificar si hay colisión entre la pelota y el bloque
                return (ball.pos.X - ball.radio <= block.pos.X + block.size.Width &&
                        ball.pos.X + ball.radio >= block.pos.X &&
                        ball.pos.Y - ball.radio <= block.pos.Y + block.size.Height &&
                        ball.pos.Y + ball.radio >= block.pos.Y);
            }
        }
        private void TIMER_Tick(object sender, EventArgs e)
        {
            g.Clear(Color.Transparent);

            //Dibuja el bloque
            g.FillRectangle(Brushes.HotPink, block.pos.X, block.pos.Y, block.size.Width, block.size.Height);
            g.DrawRectangle(Pens.Yellow , block.pos.X, block.pos.Y, block.size.Width, block.size.Height);

            //Actualiza la posición de la pelota y otros elementos
            ball.Update(block);
            ball.Render(g);

            label1.Text = "DIR: " + ball.dir.ToString();
            label2.Text = "SPEED: " + ball.speedX + " , " + ball.speedY;
            label3.Text = "SIZE : " + ball.diameter;

            PCT_CANVAS.Invalidate();
        }
        private void PCT_CANVAS_Paint(object sender, PaintEventArgs e)
        {
            //Dibuja el contenido de la imagen en el PictureBox
            e.Graphics.DrawImage(bmp, Point.Empty);
        }
        private void BTN_EXE_Click(object sender, EventArgs e)
        {
            Init();
        }
    }
}
