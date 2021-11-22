using System.Drawing;
using System.Drawing.Drawing2D;

namespace Lab5.Objects
{
    internal class BaseObject
    {
        public float X;
        public float Y;
        public float Angle;
        public BaseObject(float x, float y, float angle)
        {
            X = x;
            Y = y;
            Angle = angle;
        }
        public virtual void Render(Graphics g)
        {

        }
        public Matrix GetTransform()
        {
            var matrix = new Matrix();
            matrix.Translate(X, Y);
            matrix.Rotate(Angle);
            return matrix;
        }
    }

}
