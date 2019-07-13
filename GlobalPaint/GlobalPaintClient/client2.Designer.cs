namespace GlobalPaintClient
{
    partial class clientForm2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(clientForm2));
            this.tsp = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.handToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pencilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.circleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.lineOne = new System.Windows.Forms.ToolStripMenuItem();
            this.lineTwo = new System.Windows.Forms.ToolStripMenuItem();
            this.LineThree = new System.Windows.Forms.ToolStripMenuItem();
            this.LineFour = new System.Windows.Forms.ToolStripMenuItem();
            this.LineFive = new System.Windows.Forms.ToolStripMenuItem();
            this.fillToggle = new System.Windows.Forms.ToolStripButton();
            this.lineColor = new System.Windows.Forms.ToolStripButton();
            this.fillColor = new System.Windows.Forms.ToolStripButton();
            this.txtChat = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.colorDlg = new System.Windows.Forms.ColorDialog();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.panel = new GlobalPaintClient.DoubleBufferPanel();
            this.tsp.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsp
            // 
            this.tsp.AutoSize = false;
            this.tsp.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsp.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2,
            this.fillToggle,
            this.lineColor,
            this.fillColor});
            this.tsp.Location = new System.Drawing.Point(0, 0);
            this.tsp.Name = "tsp";
            this.tsp.Padding = new System.Windows.Forms.Padding(0);
            this.tsp.Size = new System.Drawing.Size(1011, 55);
            this.tsp.TabIndex = 0;
            this.tsp.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.AutoSize = false;
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.handToolStripMenuItem,
            this.pencilToolStripMenuItem,
            this.lineToolStripMenuItem,
            this.circleToolStripMenuItem,
            this.rectToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(55, 55);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // handToolStripMenuItem
            // 
            this.handToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("handToolStripMenuItem.Image")));
            this.handToolStripMenuItem.Name = "handToolStripMenuItem";
            this.handToolStripMenuItem.Size = new System.Drawing.Size(178, 38);
            this.handToolStripMenuItem.Text = "Hand";
            this.handToolStripMenuItem.Click += new System.EventHandler(this.handToolStripMenuItem_Click);
            // 
            // pencilToolStripMenuItem
            // 
            this.pencilToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pencilToolStripMenuItem.Image")));
            this.pencilToolStripMenuItem.Name = "pencilToolStripMenuItem";
            this.pencilToolStripMenuItem.Size = new System.Drawing.Size(178, 38);
            this.pencilToolStripMenuItem.Text = "Pencil";
            this.pencilToolStripMenuItem.Click += new System.EventHandler(this.pencilToolStripMenuItem_Click);
            // 
            // lineToolStripMenuItem
            // 
            this.lineToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("lineToolStripMenuItem.Image")));
            this.lineToolStripMenuItem.Name = "lineToolStripMenuItem";
            this.lineToolStripMenuItem.Size = new System.Drawing.Size(178, 38);
            this.lineToolStripMenuItem.Text = "Line";
            this.lineToolStripMenuItem.Click += new System.EventHandler(this.lineToolStripMenuItem_Click);
            // 
            // circleToolStripMenuItem
            // 
            this.circleToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("circleToolStripMenuItem.Image")));
            this.circleToolStripMenuItem.Name = "circleToolStripMenuItem";
            this.circleToolStripMenuItem.Size = new System.Drawing.Size(178, 38);
            this.circleToolStripMenuItem.Text = "Circle";
            this.circleToolStripMenuItem.Click += new System.EventHandler(this.circleToolStripMenuItem_Click);
            // 
            // rectToolStripMenuItem
            // 
            this.rectToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("rectToolStripMenuItem.Image")));
            this.rectToolStripMenuItem.Name = "rectToolStripMenuItem";
            this.rectToolStripMenuItem.Size = new System.Drawing.Size(178, 38);
            this.rectToolStripMenuItem.Text = "Rect";
            this.rectToolStripMenuItem.Click += new System.EventHandler(this.rectToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lineOne,
            this.lineTwo,
            this.LineThree,
            this.LineFour,
            this.LineFive});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(54, 52);
            this.toolStripDropDownButton2.Text = "toolStripDropDownButton2";
            // 
            // lineOne
            // 
            this.lineOne.Image = ((System.Drawing.Image)(resources.GetObject("lineOne.Image")));
            this.lineOne.Name = "lineOne";
            this.lineOne.Size = new System.Drawing.Size(127, 38);
            this.lineOne.Text = "1";
            this.lineOne.Click += new System.EventHandler(this.lineOne_Click);
            // 
            // lineTwo
            // 
            this.lineTwo.Image = ((System.Drawing.Image)(resources.GetObject("lineTwo.Image")));
            this.lineTwo.Name = "lineTwo";
            this.lineTwo.Size = new System.Drawing.Size(127, 38);
            this.lineTwo.Text = "2";
            this.lineTwo.Click += new System.EventHandler(this.lineTwo_Click);
            // 
            // LineThree
            // 
            this.LineThree.Image = ((System.Drawing.Image)(resources.GetObject("LineThree.Image")));
            this.LineThree.Name = "LineThree";
            this.LineThree.Size = new System.Drawing.Size(127, 38);
            this.LineThree.Text = "3";
            this.LineThree.Click += new System.EventHandler(this.LineThree_Click);
            // 
            // LineFour
            // 
            this.LineFour.Image = ((System.Drawing.Image)(resources.GetObject("LineFour.Image")));
            this.LineFour.Name = "LineFour";
            this.LineFour.Size = new System.Drawing.Size(127, 38);
            this.LineFour.Text = "4";
            this.LineFour.Click += new System.EventHandler(this.LineFour_Click);
            // 
            // LineFive
            // 
            this.LineFive.Image = ((System.Drawing.Image)(resources.GetObject("LineFive.Image")));
            this.LineFive.Name = "LineFive";
            this.LineFive.Size = new System.Drawing.Size(127, 38);
            this.LineFive.Text = "5";
            this.LineFive.Click += new System.EventHandler(this.LineFive_Click);
            // 
            // fillToggle
            // 
            this.fillToggle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fillToggle.Image = ((System.Drawing.Image)(resources.GetObject("fillToggle.Image")));
            this.fillToggle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fillToggle.Name = "fillToggle";
            this.fillToggle.Size = new System.Drawing.Size(91, 52);
            this.fillToggle.Text = "채우기";
            this.fillToggle.Click += new System.EventHandler(this.fillToggle_Click);
            // 
            // lineColor
            // 
            this.lineColor.AutoSize = false;
            this.lineColor.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lineColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.lineColor.Image = ((System.Drawing.Image)(resources.GetObject("lineColor.Image")));
            this.lineColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lineColor.Name = "lineColor";
            this.lineColor.Size = new System.Drawing.Size(52, 52);
            this.lineColor.Text = "toolStripButton2";
            this.lineColor.Click += new System.EventHandler(this.lineColor_Click);
            // 
            // fillColor
            // 
            this.fillColor.AutoSize = false;
            this.fillColor.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.fillColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.fillColor.Image = ((System.Drawing.Image)(resources.GetObject("fillColor.Image")));
            this.fillColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fillColor.Name = "fillColor";
            this.fillColor.Size = new System.Drawing.Size(52, 52);
            this.fillColor.Text = "toolStripButton3";
            this.fillColor.Click += new System.EventHandler(this.fillColor_Click);
            // 
            // txtChat
            // 
            this.txtChat.Location = new System.Drawing.Point(0, 551);
            this.txtChat.Multiline = true;
            this.txtChat.Name = "txtChat";
            this.txtChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChat.Size = new System.Drawing.Size(1011, 158);
            this.txtChat.TabIndex = 2;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(902, 715);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(109, 47);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Say";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(0, 723);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(896, 35);
            this.txtMessage.TabIndex = 4;
            this.txtMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChat_KetDown);
            // 
            // panel
            // 
            this.panel.Location = new System.Drawing.Point(0, 58);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1011, 487);
            this.panel.TabIndex = 1;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.panel.MouseEnter += new System.EventHandler(this.panel_MouseEnter);
            this.panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
            this.panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_MouseUp);
            // 
            // clientForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 763);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtChat);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.tsp);
            this.Name = "clientForm2";
            this.Text = "세계그림판";
            this.Load += new System.EventHandler(this.clientForm2_Load);
            this.tsp.ResumeLayout(false);
            this.tsp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsp;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem handToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pencilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem circleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rectToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem lineOne;
        private System.Windows.Forms.ToolStripMenuItem lineTwo;
        private System.Windows.Forms.ToolStripMenuItem LineThree;
        private System.Windows.Forms.ToolStripMenuItem LineFour;
        private System.Windows.Forms.ToolStripMenuItem LineFive;
        private System.Windows.Forms.ToolStripButton lineColor;
        private System.Windows.Forms.ToolStripButton fillColor;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ToolStripButton fillToggle;
        private System.Windows.Forms.ColorDialog colorDlg;
        public DoubleBufferPanel panel;
        public System.Windows.Forms.TextBox txtMessage;
        public System.Windows.Forms.TextBox txtChat;
    }
}