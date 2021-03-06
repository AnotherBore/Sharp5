using System.Drawing;
using System.Drawing.Drawing2D;
using Lab5;

namespace Lab5.Objects
{
    internal class BaseObject
    {
        Random random = new Random();
        public float X;
        public float Y;
        public float Angle;
        public bool IsColorChanged;
        public Action<BaseObject, BaseObject> OnOverlap, OnNonOverlap;
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
        public virtual GraphicsPath GetGraphicsPath()
        {
            return new GraphicsPath();
        }
        public virtual bool Overlaps(BaseObject obj, Graphics g)
        {
            var path1 = this.GetGraphicsPath();
            var path2 = obj.GetGraphicsPath();

            path1.Transform(this.GetTransform());
            path2.Transform(obj.GetTransform());

            var region = new Region(path1);
            region.Intersect(path2);
            return !region.IsEmpty(g);
        }

        public virtual void Overlap(BaseObject obj)
        {
            if (this.OnOverlap != null)
            {
                this.OnOverlap(this, obj);
            }
        }
        public virtual void NonOverlap(BaseObject obj)
        {
            if (this.OnNonOverlap != null)
            {
                this.OnNonOverlap(this, obj);
            }
        }
        public virtual void GenerateRandomly(int maxX, int maxY)
        {
            X = random.Next(15, maxX);
            Y = random.Next(15, maxY);
        }
        public void ChangeColor(bool isChanged)
        {
            IsColorChanged = isChanged;
        }
    }
}
