using System;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Objects
{
    class Target : BaseObject
    {
        public Target(float x, float y, float angle) : base(x, y, angle)
        {

        }
        public override void Render(Graphics g)
        {
            if (!IsColorChanged)
            {
                g.FillEllipse(
                new SolidBrush(Color.LightGreen),
                -15, -15,
                30, 30
                );
                g.DrawEllipse(
                    new Pen(Color.DarkCyan, 2),
                    -15, -15,
                    30, 30
                    );
            }
            else
            {
                g.FillEllipse(
                new SolidBrush(Color.Gray),
                -15, -15,
                30, 30
                );
                g.DrawEllipse(
                    new Pen(Color.White, 2),
                    -15, -15,
                    30, 30
                    );
            }
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, 30, 30);
            return path;
        }
    }
}
