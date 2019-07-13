using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using ShapeClass;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GlobalPaint
{
    [Serializable]
    public class StoreShape
    {
        public MyPencil[] mypencil;
        public MyLines[] mylines;
        public MyRect[] myrect;
        public MyCircle[] mycircle;

        public int npencil;
        public int nline;
        public int nrect;
        public int ncircle;
        public int index;

        public StoreShape()
        {
            mypencil = new MyPencil[100];
            mylines = new MyLines[100];
            myrect = new MyRect[100];
            mycircle = new MyCircle[100];

            for (int i = 0; i < 100; i++)
            {
                mypencil[i] = new MyPencil();
            }
            for (int i = 0; i < 100; i++)
            {
                mylines[i] = new MyLines();
            }
            for (int i = 0; i < 100; i++)
            {
                myrect[i] = new MyRect();
            }
            for (int i = 0; i < 100; i++)
            {
                mycircle[i] = new MyCircle();
            }
        }
    }

    public partial class serverForm2 : Form
    {
        private Pen pen;
        private SolidBrush brush;

        public MyPencil[] mypencil;
        public MyLines[] mylines;
        public MyRect[] myrect;
        public MyCircle[] mycircle;

        public int npencil;
        public int nline;
        public int nrect;
        public int ncircle;
        public int index;

        private const string filePath = "./server_shapes";

        public serverForm2()
        {
            InitializeComponent();
            SetupVar();
        }

        private void SetupVar()
        {
            pen = new Pen(Color.Black);
            brush = new SolidBrush(Color.Black);
            mypencil = new MyPencil[100];
            mylines = new MyLines[100];
            myrect = new MyRect[100];
            mycircle = new MyCircle[100];
            npencil = 0;
            nline = 0;
            nrect = 0;
            ncircle = 0;
            index = 0;

            SetupMine();
        }

        private void SetupMine()
        {
            for (int i = 0; i < 100; i++)
            {
                mypencil[i] = new MyPencil();
            }
            for (int i = 0; i < 100; i++)
            {
                mylines[i] = new MyLines();
            }
            for (int i = 0; i < 100; i++)
            {
                myrect[i] = new MyRect();
            }
            for (int i = 0; i < 100; i++)
            {
                mycircle[i] = new MyCircle();
            }
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int pIndex = 0;
            int lIndex = 0;
            int rIndex = 0;
            int cIndex = 0;

            for (int i = 0; i <= index; i++)
            {
                if (pIndex <= npencil)
                {
                    if (mypencil[pIndex].getIndex() == i)
                    {
                        // Draw Pencil
                        pen.Width = mypencil[pIndex].getThick();
                        pen.DashStyle = DashStyle.Solid;
                        pen.Color = mypencil[pIndex].getLineColor();

                        if (mypencil[pIndex].getPoints().Count() == 0)
                        { // Do NOTHING
                        }
                        else
                        {
                            e.Graphics.DrawCurve(pen, mypencil[pIndex].getPoints().ToArray());
                            pIndex++;
                        }
                    }
                }
                if (lIndex <= nline)
                {
                    if (mylines[lIndex].getIndex() == i)
                    {
                        // Draw Line
                        pen.Width = mylines[lIndex].getThick();
                        pen.DashStyle = DashStyle.Solid;
                        pen.Color = mylines[lIndex].getLineColor();

                        e.Graphics.DrawLine(pen, mylines[lIndex].getPoint1(), mylines[lIndex].getPoint2());
                        lIndex++;
                    }
                }
                if (rIndex <= nrect)
                {
                    if (myrect[rIndex].getIndex() == i)
                    {
                        // Draw Rectangle
                        pen.Width = myrect[rIndex].getThick();
                        pen.DashStyle = DashStyle.Solid;
                        pen.Color = myrect[rIndex].getLineColor();

                        e.Graphics.DrawRectangle(pen, myrect[rIndex].getRect());

                        if (myrect[rIndex].getFill())
                        {
                            // Fill Rectangle
                            brush.Color = myrect[rIndex].getFillColor();
                            e.Graphics.FillRectangle(brush, myrect[rIndex].getRect());
                        }
                        rIndex++;
                    }
                }
                if (cIndex <= ncircle)
                {
                    if (mycircle[cIndex].getIndex() == i)
                    {
                        // Draw Circle
                        pen.Width = mycircle[cIndex].getThick();
                        pen.DashStyle = DashStyle.Solid;
                        pen.Color = mycircle[cIndex].getLineColor();

                        e.Graphics.DrawEllipse(pen, mycircle[cIndex].getRectC());

                        if (mycircle[cIndex].getFill())
                        {
                            // Fill Circle
                            brush.Color = mycircle[cIndex].getFillColor();
                            e.Graphics.FillEllipse(brush, mycircle[cIndex].getRectC());
                        }
                        cIndex++;
                    }
                }
            }
        }

        private void serverForm2_Load(object sender, EventArgs e)
        {

            if (File.Exists(filePath))
            {
                // file exist
                Stream stm = File.Open(filePath, FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();

                StoreShape sh = (StoreShape)bf.Deserialize(stm);
                stm.Close();

                mypencil = sh.mypencil;
                mylines = sh.mylines;
                mycircle = sh.mycircle;
                myrect = sh.myrect;
                npencil = sh.npencil;
                nline = sh.nline;
                ncircle = sh.ncircle;
                nrect = sh.nrect;
                index = sh.index;
            }
        }

        private void serverForm2_FormClosing(object sender, FormClosingEventArgs e)
        {
            StoreShape sh = new StoreShape();
            sh.mypencil = mypencil;
            sh.mylines = mylines;
            sh.mycircle = mycircle;
            sh.myrect = myrect;
            sh.npencil = npencil;
            sh.nline = nline;
            sh.ncircle = ncircle;
            sh.nrect = nrect;
            sh.index = index;

            Stream stm = File.Open(filePath, FileMode.Create, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stm, sh);
            stm.Close();
        }
    }
}
