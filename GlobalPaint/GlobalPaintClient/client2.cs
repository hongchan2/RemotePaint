using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Threading;
using ShapeClass;

namespace GlobalPaintClient
{
    public partial class clientForm2 : Form
    {
        // Parent Form
        private clientForm1 parentForm;
        private Thread thread;

        // Paint
        private bool hand;              // add
        private bool pencil;            // add
        private bool line;
        private bool rect;
        private bool circle;
        private Point start;
        private Point finish;
        private Pen pen;
        private SolidBrush brush;       // add
        public int npencil;             // add
        public int nline;
        public int nrect;
        public int ncircle;
        private int thick;
        private bool fillTog;           // add
        public MyPencil[] mypencil;     // add
        public MyLines[] mylines;
        public MyRect[] myrect;
        public MyCircle[] mycircle;
        private Color lineClr;          // add
        private Color fillClr;          // add
        public int index;               // add
        private Point zoomPoint;        // add
        private float zoom;             // add
        private bool zoomStatus;        // add
        public bool serverSend;         // add

        private bool zoomIn;            // add
        private bool zoomOut;           // add

        int panelWidthScale;
        int panelHeightScale;

        private int originalWidth;
        private int originalHeight;


        public clientForm2()
        {
            InitializeComponent();
            this.panel.MouseWheel += panel_MouseWheel;
        }

        public clientForm2(clientForm1 f)
        {
            parentForm = f;
            InitializeComponent();
            this.panel.MouseWheel += panel_MouseWheel;
        }
        
        private void clientForm2_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtMessage;
            txtMessage.Focus();
            SetupVar();
        }

        private void SetupVar()
        {
            thick = 1;
            hand = false;
            pencil = false;
            line = false;
            rect = false;
            circle = false;
            start = new Point(0, 0);
            finish = new Point(0, 0);
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
            fillTog = false;
            lineClr = new Color();
            fillClr = new Color();
            lineClr = Color.Black;
            fillClr = Color.Black;
            index = 0;
            zoom = 1f;
            zoomStatus = false;
            zoomPoint = new Point(0, 0);
            serverSend = false;

            zoomIn = zoomOut = false;
            panelWidthScale = -((panel.Width - (int)(panel.Width * 1.1)) / 2);
            panelHeightScale = -((panel.Height - (int)(panel.Height * 1.1)) / 2);

            originalWidth = panel.Width;
            originalHeight = panel.Height;

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
            parentForm.Init();

            // Make Thread to process Panel Receive
            thread = new Thread(new ThreadStart(parentForm.ReceiveFromServer));
            thread.Start();

        }

        private void handToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hand = true;
            pencil = false;
            line = false;
            rect = false;
            circle = false;
        }

        private void pencilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hand = false;
            pencil = true;
            line = false;
            rect = false;
            circle = false;
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hand = false;
            pencil = false;
            line = true;
            rect = false;
            circle = false;
        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hand = false;
            pencil = false;
            line = false;
            rect = false;
            circle = true;
        }

        private void rectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hand = false;
            pencil = false;
            line = false;
            rect = true;
            circle = false;
        }

        private void lineOne_Click(object sender, EventArgs e)
        {
            thick = 1;
        }

        private void lineTwo_Click(object sender, EventArgs e)
        {
            thick = 2;
        }

        private void LineThree_Click(object sender, EventArgs e)
        {
            thick = 3;
        }

        private void LineFour_Click(object sender, EventArgs e)
        {
            thick = 4;
        }

        private void LineFive_Click(object sender, EventArgs e)
        {
            thick = 5;
        }

        private void fillToggle_Click(object sender, EventArgs e)
        {
            if (fillTog)
                fillTog = false;
            else
                fillTog = true;
        }

        private void lineColor_Click(object sender, EventArgs e)
        {
            if(colorDlg.ShowDialog() == DialogResult.OK)
            {
                lineClr = colorDlg.Color;
            }
        }

        private void fillColor_Click(object sender, EventArgs e)
        {
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                fillClr = colorDlg.Color;
            }
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Send Panel to Server
            if (!serverSend)
            {
                // Call by Client (Not Call by Server)
                int changedShape = 1;
                if (pencil)
                    changedShape = 1;
                else if (line)
                    changedShape = 2;
                else if (rect)
                    changedShape = 3;
                else if (circle)
                    changedShape = 4;
                if (pencil || line || rect || circle)
                    parentForm.SendPanelInfo(changedShape);
            }
            
            /*
            // Zoom Or Move
            if (hand && zoomStatus)
            {
                Point newLoc = new Point();
                Size newSize = new Size();
                
                for (int i = 0; i < nrect; i++)
                {
                    Point originalLoc = myrect[i].GetLocation();
                    int originalWidth = myrect[i].GetWidth();
                    int originalHeight = myrect[i].GetHeight();

                    // 스케일 계산
                    int widthScale = 0;
                    int heightScale = 0;
                    if (zoomIn)
                    {
                        widthScale = (int)(originalWidth * 0.1);
                        heightScale = (int)(originalHeight * 0.1);
                    }
                    else if (zoomOut)
                    {
                        widthScale = -((int)(originalWidth * 0.1));
                        heightScale = -((int)(originalHeight * 0.1));
                    }

                    originalWidth += originalLoc.X;
                    originalHeight += originalLoc.Y;

                    // 현재 위치와 계산해서 얼만큼 스케일 할 것인지 필요! - 이렇게 하지 않으면 도형의 상대적 위치 달라짐
                    Point differLocPoint = new Point();
                    Point differSizePoint = new Point();
                    if (originalLoc.X > zoomPoint.X)
                    {
                        differLocPoint.X = originalLoc.X - zoomPoint.X;
                        if(originalLoc.Y > zoomPoint.Y)
                        {
                            // 마우스 포인터의 오른쪽, 아래에 위치 
                            differLocPoint.Y = originalLoc.Y - zoomPoint.Y;

                            newLoc.X = originalLoc.X + (differLocPoint.X / 20);
                            newLoc.Y = originalLoc.Y + (differLocPoint.Y / 20);
                        }
                        else
                        {
                            // 마우스 포인터의 오른쪽, 위에 위치
                            differLocPoint.Y = zoomPoint.Y - originalLoc.Y;

                            newLoc.X = originalLoc.X + (differLocPoint.X / 20);
                            newLoc.Y = originalLoc.Y - (differLocPoint.Y / 20);
                        }
                    }
                    else
                    {
                        differLocPoint.X = zoomPoint.X - originalLoc.X;
                        if(originalLoc.Y > zoomPoint.Y)
                        {
                            // 마우스 포인터의 왼쪽, 아래에 위치
                            differLocPoint.Y = originalLoc.Y - zoomPoint.Y;

                            newLoc.X = originalLoc.X - (differLocPoint.X / 20);
                            newLoc.Y = originalLoc.Y + (differLocPoint.Y / 20);
                        }
                        else
                        {
                            // 마우스 포인터의 왼쪽, 위에 위치
                            differLocPoint.Y = zoomPoint.Y - originalLoc.Y;

                            newLoc.X = originalLoc.X - (differLocPoint.X / 20);
                            newLoc.Y = originalLoc.Y - (differLocPoint.Y / 20);
                        }
                    }


                    if (originalWidth > zoomPoint.X)
                    {
                        differSizePoint.X = originalWidth - zoomPoint.X;
                        if (originalHeight > zoomPoint.Y)
                        {
                            // 마우스 포인터의 오른쪽, 아래에 위치 
                            differSizePoint.Y = originalHeight - zoomPoint.Y;

                            newSize.Width = originalWidth + (differLocPoint.X / 20);
                            newSize.Height = originalHeight + (differLocPoint.Y / 20);
                        }
                        else
                        {
                            // 마우스 포인터의 오른쪽, 위에 위치
                            differSizePoint.Y = zoomPoint.Y - originalHeight;

                            newSize.Width = originalWidth + (differLocPoint.X / 20);
                            newSize.Height = originalHeight - (differLocPoint.Y / 20);
                        }
                    }
                    else
                    {
                        differSizePoint.X = zoomPoint.X - originalWidth;
                        if (originalHeight > zoomPoint.Y)
                        {
                            // 마우스 포인터의 왼쪽, 아래에 위치
                            differSizePoint.Y = originalHeight - zoomPoint.Y;

                            newSize.Width = originalWidth - (differLocPoint.X / 20);
                            newSize.Height = originalHeight + (differLocPoint.Y / 20);
                        }
                        else
                        {
                            // 마우스 포인터의 왼쪽, 위에 위치
                            differSizePoint.Y = zoomPoint.Y - originalHeight;

                            newSize.Width = originalWidth - (differLocPoint.X / 20);
                            newSize.Height = originalHeight - (differLocPoint.Y / 20);
                        }
                    }

                    myrect[i].SetSizeAndLoc(newLoc, newSize.Width - newLoc.X, newSize.Height - newLoc.Y);
                }

                // zoom 위치 초기화
                zoomStatus = false;
                zoomPoint.X = 0;
                zoomPoint.Y = 0;
            }
            */
            
            int pIndex = 0;
            int lIndex = 0;
            int rIndex = 0;
            int cIndex = 0;

            for(int i = 0; i <= index; i++)
            {
                if(pIndex <= npencil)
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
                if(lIndex <= nline)
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
                if(rIndex <= nrect)
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
                if(cIndex <= ncircle)
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

            pen.Width = thick;
            pen.DashStyle = DashStyle.Solid;
            pen.Color = lineClr;
            brush.Color = fillClr;
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            start.X = e.X;
            start.Y = e.Y;
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if ((start.X == 0) && (start.Y == 0))
                return;

            finish.X = e.X;
            finish.Y = e.Y;

            if(pencil == true)
            {
                mypencil[npencil].setPoints(start, finish, thick, lineClr, index);
            }
            if (line == true)
            {
                mylines[nline].setPoint(start, finish, thick, lineClr, index);
            }
            if (rect == true)
            {
                myrect[nrect].SetRect(start, finish, thick, fillTog, lineClr, fillClr, index);
            }
            if (circle == true)
            {
                mycircle[ncircle].SetRectC(start, finish, thick, fillTog, lineClr, fillClr, index);
            }

            panel.Invalidate(true);
            panel.Update();
        }

        private void panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (pencil == true)
                npencil++;
            if (line == true)
                nline++;
            if (rect == true)
                nrect++;
            if (circle == true)
                ncircle++;

            // Painting Order
            index++;

            // Send Changed number;
            parentForm.SendChangedIndexInfo();

            start.X = 0;
            start.Y = 0;
            finish.X = 0;
            finish.Y = 0;
        }

        private void panel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (hand)
            {
                /*
                // Get Current Point
                zoomPoint.X = e.X;
                zoomPoint.Y = e.Y;

                if (e.Delta > 0)
                {
                    // Zoom-Out
                    zoom = Math.Max(zoom - 0.1F, 0.1F);
                    zoomOut = true;
                    zoomIn = false;
                    //MessageBox.Show("축소 " + zoom);
                }
                else if (e.Delta < 0)
                {
                    // Zoom-In
                    zoom = Math.Min(zoom + 0.1F, 10F);
                    zoomIn = true;
                    zoomOut = false;
                    //MessageBox.Show("확대 " + zoom);
                }

                zoomStatus = true;
                panel.Invalidate(true);
                panel.Update();
                */
            }
        }

        private void panel_MouseEnter(object sender, EventArgs e)
        {
            // To Scroll
            //panel.Focus();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            parentForm.SendChatMessage();
        }

        private void txtChat_KetDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                parentForm.SendChatMessage();
            }
        }
    }
}
