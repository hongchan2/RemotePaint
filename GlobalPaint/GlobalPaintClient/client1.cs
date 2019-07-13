using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using PacketDefine;
using System.Runtime.Serialization.Formatters.Binary;

namespace GlobalPaintClient
{
    public partial class clientForm1 : Form
    {
        // Child Form
        private clientForm2 childForm;

        // Cliet
        private TcpClient client;
        private bool m_bConnect = false;

        // Client Packet
        private NetworkStream stream;
        private byte[] sendBuffer = new byte[Constant.PACKET_SIZE];
        private byte[] readBuffer = new byte[Constant.PACKET_SIZE];

        // Packet Class
        private PanelInit m_panelInitClass;
        private PanelInfo m_panelInfoClass;
        private IndexInfo m_indexInfoClass;
        private ChatInfo m_chatInfoClass;

        public clientForm1()
        {
            InitializeComponent();
        }

        public void Send()
        {
            stream.Write(sendBuffer, 0, sendBuffer.Length);
            stream.Flush();

            for (int i = 0; i < Constant.PACKET_SIZE; i++)
            {
                sendBuffer[i] = 0;
            }
        }

        public void Connect()
        {
            string ip = txtIP.Text;
            int port = int.Parse(txtPort.Text);
            client = new TcpClient();
            IPAddress ipAddr = IPAddress.Parse(ip);

            try
            {
                client.Connect(ipAddr, port);
            }
            catch
            {
                m_bConnect = false;
                return;
            }

            m_bConnect = true;
            stream = client.GetStream();
        }

        public void Init()
        {
            // Receive Panel Information from Server
            byte[] readInitBuffer = new byte[Constant.INIT_PACKET_SIZE];
            stream.Read(readInitBuffer, 0, Constant.INIT_PACKET_SIZE);

            // Desserialize
            MemoryStream ms = new MemoryStream(Constant.INIT_PACKET_SIZE);
            foreach (byte b in readInitBuffer)
            {
                ms.WriteByte(b);
            }
            ms.Position = 0;
            BinaryFormatter bf = new BinaryFormatter();
            Object obj = bf.Deserialize(ms);
            ms.Close();
            m_panelInitClass = (PanelInit)obj;

            childForm.mypencil = m_panelInitClass.mypencil;
            childForm.mylines = m_panelInitClass.mylines;
            childForm.myrect = m_panelInitClass.myrect;
            childForm.mycircle = m_panelInitClass.mycircle;
            childForm.npencil = m_panelInitClass.npencil;
            childForm.nline = m_panelInitClass.nline;
            childForm.nrect = m_panelInitClass.nrect;
            childForm.ncircle = m_panelInitClass.ncircle;
            childForm.index = m_panelInitClass.index;

            // Draw Panel
            childForm.panel.Invalidate(true);
            childForm.panel.Update();

            // Send ClientID to Server
            Initialize init = new Initialize();
            init.clientID = txtID.Text;

            Packet.Serialize(init).CopyTo(sendBuffer, 0);
            Send();
        }

        public void SendPanelInfo(int num)
        {
            // Send Panel Infomation to Server
            PanelInfo panel = new PanelInfo();
            panel.Type = (int)PacketType.panelInfo;

            panel.changedValue = num;
            panel.npencil = childForm.npencil;
            panel.nline = childForm.nline;
            panel.nrect = childForm.nrect;
            panel.ncircle = childForm.ncircle;
            panel.index = childForm.index;

            if(num == 1)
                panel.mypencil = childForm.mypencil[panel.npencil];
            else if(num == 2)
                panel.mylines = childForm.mylines[panel.nline];
            else if(num == 3)
                panel.myrect = childForm.myrect[panel.nrect];
            else if(num == 4)
                panel.mycircle = childForm.mycircle[panel.ncircle];

            //MessageBox.Show(panel.npencil.ToString());

            Packet.Serialize(panel).CopyTo(sendBuffer, 0);
            Send();
        }

        public void SendChangedIndexInfo()
        {
            // Send Index Infomation to Server
            IndexInfo index = new IndexInfo();
            index.Type = (int)PacketType.indexInfo;

            index.npencil = childForm.npencil;
            index.nline = childForm.nline;
            index.nrect = childForm.nrect;
            index.ncircle = childForm.ncircle;
            index.index = childForm.index;


            Packet.Serialize(index).CopyTo(sendBuffer, 0);
            Send();
        }

        public void SendChatMessage()
        {
            // Send Chat Message to Server
            ChatInfo chat = new ChatInfo();
            chat.Type = (int)PacketType.chatInfo;

            chat.clientID = txtID.Text;
            chat.msg = childForm.txtMessage.Text;

            Packet.Serialize(chat).CopyTo(sendBuffer, 0);
            Send();

            // Print Chat Log
            string msg = txtID.Text + ": " + childForm.txtMessage.Text + "\r\n";
            childForm.txtChat.AppendText(msg);

            // Clear Chat
            childForm.txtMessage.Text = "";
        }

        public void ReceiveFromServer()
        {
            while (true)
            {
                try
                {
                    stream.Read(readBuffer, 0, Constant.PACKET_SIZE);
                }
                catch
                {
                    return;
                }

                Packet packet = new Packet();
                try
                {
                    packet = (Packet)Packet.Desserialize(readBuffer);
                }
                catch { }

                switch ((int)packet.Type)
                {
                    case (int)PacketType.panelInfo:
                        {
                            // Receive Panel Information from Server
                            m_panelInfoClass = (PanelInfo)Packet.Desserialize(readBuffer);
                            int changedValue = m_panelInfoClass.changedValue;

                            childForm.npencil = m_panelInfoClass.npencil;
                            childForm.nline = m_panelInfoClass.nline;
                            childForm.nrect = m_panelInfoClass.nrect;
                            childForm.ncircle = m_panelInfoClass.ncircle;
                            childForm.index = m_panelInfoClass.index;

                            if (changedValue == 1)
                                childForm.mypencil[childForm.npencil] = m_panelInfoClass.mypencil;
                            else if (changedValue == 2)
                                childForm.mylines[childForm.nline] = m_panelInfoClass.mylines;
                            else if (changedValue == 3)
                                childForm.myrect[childForm.nrect] = m_panelInfoClass.myrect;
                            else if (changedValue == 4)
                                childForm.mycircle[childForm.ncircle] = m_panelInfoClass.mycircle;


                            // Draw Panel
                            childForm.serverSend = true;
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                childForm.panel.Invalidate(true);
                                childForm.panel.Update();
                            }));
                            childForm.serverSend = false;
                            break;
                        }
                    case (int)PacketType.indexInfo:
                        {
                            // Receive Panel Information from Server
                            m_indexInfoClass = (IndexInfo)Packet.Desserialize(readBuffer);

                            childForm.npencil = m_indexInfoClass.npencil;
                            childForm.nline = m_indexInfoClass.nline;
                            childForm.nrect = m_indexInfoClass.nrect;
                            childForm.ncircle = m_indexInfoClass.ncircle;
                            childForm.index = m_indexInfoClass.index;

                            break;
                        }
                    case (int)PacketType.chatInfo:
                        {
                            // Receive Chat Message from Server
                            m_chatInfoClass = (ChatInfo)Packet.Desserialize(readBuffer);

                            string receiveID = m_chatInfoClass.clientID;
                            string receiveMsg = m_chatInfoClass.msg;
                            string msg = receiveID + ": " + receiveMsg + "\r\n";

                            // Print Chat Log
                            this.Invoke((MethodInvoker)(() => {
                                childForm.txtChat.AppendText(msg);
                            }));
                            break;
                        }
                }
            }
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            Connect();
            if (m_bConnect)
            {
                // Connected!
                childForm = new clientForm2(this);
                childForm.ShowDialog();
            }
        }
    }
}
