namespace GlobalPaint
{
    partial class serverForm2
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
            this.panel = new GlobalPaint.DoubleBufferPanel();
            this.txtChat = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1018, 543);
            this.panel.TabIndex = 0;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            // 
            // txtChat
            // 
            this.txtChat.Location = new System.Drawing.Point(0, 549);
            this.txtChat.Multiline = true;
            this.txtChat.Name = "txtChat";
            this.txtChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChat.Size = new System.Drawing.Size(1018, 175);
            this.txtChat.TabIndex = 0;
            // 
            // serverForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 720);
            this.Controls.Add(this.txtChat);
            this.Controls.Add(this.panel);
            this.Name = "serverForm2";
            this.Text = "세계그림판_서버";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.serverForm2_FormClosing);
            this.Load += new System.EventHandler(this.serverForm2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public DoubleBufferPanel panel;
        public System.Windows.Forms.TextBox txtChat;
    }
}