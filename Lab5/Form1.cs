using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab5.Objects;

namespace Lab5
{
    public partial class Form1 : Form
    {
        List<BaseObject> objects = new List<BaseObject>();
        int score = 0;
        Player player;
        Marker marker;
        BlackRealm blackRealm = new(0,0);
        Target target1 = new (0,0,0);
        Target target2 = new (0,0,0);
        public Form1()
        {
            InitializeComponent();

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);
            target1.GenerateRandomly(pbMain.Width, pbMain.Height);
            target2.GenerateRandomly(pbMain.Width, pbMain.Height);
            blackRealm = new BlackRealm(pbMain.Width/3, pbMain.Height);

            player.OnOverlap += (p, obj) =>
            {
                if(!(obj is BlackRealm))
                txtLog.Text = $"[{DateTime.Now:mm:ss:ff}] ????? ????????? ? {obj}\n" + txtLog.Text;
            };

            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };

            player.OnTargetOverlap += (t) =>
            {
                score++;
                t.GenerateRandomly(pbMain.Width, pbMain.Height);
            };

            blackRealm.OnOverlap = (obj1, obj2) => {
                obj2.ChangeColor(true);
            };

            blackRealm.OnNonOverlap = (obj1, obj2) => {
                obj2.ChangeColor(false);
            };

            objects.Add(blackRealm);
            objects.Add(marker);          
            objects.Add(target1);
            objects.Add(target2);
            objects.Add(player);

        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);
            lblScore.Text = $"?????: {score}";
            updatePlayer();
            moveRealm();

            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }

                if (!(obj is BlackRealm))
                {
                    if (blackRealm.Overlaps(obj, g))
                        blackRealm.Overlap(obj);
                    else blackRealm.NonOverlap(obj);
                }
            }

            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }
        private void moveRealm()
        {
            blackRealm.X = (blackRealm.X + 2) % (pbMain.Width * 2);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {        
            pbMain.Invalidate();
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if(marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker);
            }
            marker.X = e.X;
            marker.Y = e.Y;
        }
        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;

                float lenght = MathF.Sqrt(dx * dx + dy * dy);
                dx /= lenght;
                dy /= lenght;

                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;

                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }

            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            player.X += player.vX;
            player.Y += player.vY;
        }
    }
}
