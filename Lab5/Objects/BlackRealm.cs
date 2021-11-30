﻿using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Objects
{
    class BlackRealm : BaseObject
    {
        private int _height, _width;

        public BlackRealm(int width, int height) : base(0,0,0)
        {
            _height = height;
            _width = width;
        }
        public override void Render(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Black), -_width, 0,_width, _height);
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddRectangle(new Rectangle(-_width, 0, _width, _height));
            return path;
        }
    }
}
