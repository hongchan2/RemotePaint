using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;

namespace ShapeClass
{
    [Serializable]
    public class MyPencil
    {
        private List<Point> pList;
        private int thick;
        private Color lineColor;
        private int index;

        public MyPencil()
        {
            pList = new List<Point>();
            thick = 1;
            lineColor = Color.Black;
        }

        public void setPoints(Point start, Point finish, int thick, Color lineColor, int index)
        {
            if (pList.Count() == 0)
                pList.Add(start);
            pList.Add(finish);
            this.thick = thick;
            this.lineColor = lineColor;
            this.index = index;
        }

        public List<Point> getPoints()
        {
            return pList;
        }

        public int getThick()
        {
            return thick;
        }

        public Color getLineColor()
        {
            return lineColor;
        }

        public int getIndex()
        {
            return index;
        }
    }

    [Serializable]
    public class MyLines
    {
        private Point[] point = new Point[2];
        private int thick;
        private Color lineColor;
        private int index;

        public MyLines()
        {
            point[0] = new Point();
            point[1] = new Point();
            thick = 1;
            lineColor = Color.Black;
        }

        public void setPoint(Point start, Point finish, int thick, Color lineColor, int index)
        {
            point[0] = start;
            point[1] = finish;
            this.thick = thick;
            this.lineColor = lineColor;
            this.index = index;
        }

        public Point getPoint1()
        {
            return point[0];
        }

        public Point getPoint2()
        {
            return point[1];
        }

        public int getThick()
        {
            return thick;
        }

        public Color getLineColor()
        {
            return lineColor;
        }

        public int getIndex()
        {
            return index;
        }
    }

    [Serializable]
    public class MyCircle
    {
        private Rectangle rectC;
        private int thick;
        bool isFill;
        private Color lineColor;
        private Color fillColor;
        private int index;

        public MyCircle()
        {
            rectC = new Rectangle();
            thick = 1;
            isFill = false;
            lineColor = Color.Black;
            fillColor = Color.Black;
            index = 0;
        }

        public void SetRectC(Point start, Point finish, int thick, bool isFill, Color lineColor, Color fillColor, int index)
        {
            rectC.X = Math.Min(start.X, finish.X);
            rectC.Y = Math.Min(start.Y, finish.Y);
            rectC.Width = Math.Abs(start.X - finish.X);
            rectC.Height = Math.Abs(start.Y - finish.Y);
            this.thick = thick;
            this.isFill = isFill;
            this.lineColor = lineColor;
            this.fillColor = fillColor;
            this.index = index;
        }

        public Rectangle getRectC()
        {
            return rectC;
        }

        public int getThick()
        {
            return thick;
        }

        public bool getFill()
        {
            return isFill;
        }

        public Color getLineColor()
        {
            return lineColor;
        }

        public Color getFillColor()
        {
            return fillColor;
        }

        public int getIndex()
        {
            return index;
        }
    }

    [Serializable]
    public class MyRect
    {
        private Rectangle rect;
        private int thick;
        private bool isFill;
        private Color lineColor;
        private Color fillColor;
        private int index;

        public MyRect()
        {
            rect = new Rectangle();
            thick = 1;
            isFill = false;
            lineColor = Color.Black;
            fillColor = Color.Black;
            index = 0;
        }

        public void SetRect(Point start, Point finish, int thick, bool isFill, Color lineColor, Color fillColor, int index)
        {
            rect.X = Math.Min(start.X, finish.X);
            rect.Y = Math.Min(start.Y, finish.Y);
            rect.Height = Math.Abs(finish.Y - start.Y);
            rect.Width = Math.Abs(finish.X - start.X);
            this.thick = thick;
            this.isFill = isFill;
            this.lineColor = lineColor;
            this.fillColor = fillColor;
            this.index = index;
        }

        /*
        public void SetSizeAndLoc(Point location, int width, int height)
        {
            rect.X = location.X;
            rect.Y = location.Y;
            rect.Height = height;
            rect.Width = width;
        }

        public Point GetLocation()
        {
            return rect.Location;
        }

        public int GetHeight()
        {
            return rect.Height;
        }

        public int GetWidth()
        {
            return rect.Width;
        }
        */

        public Rectangle getRect()
        {
            return rect;
        }

        public int getThick()
        {
            return thick;
        }

        public bool getFill()
        {
            return isFill;
        }

        public Color getLineColor()
        {
            return lineColor;
        }

        public Color getFillColor()
        {
            return fillColor;
        }

        public int getIndex()
        {
            return index;
        }
    }
}
