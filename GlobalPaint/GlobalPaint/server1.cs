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

namespace GlobalPaint
{
    public partial class serverForm1 : Form
    {
        // Child Form
        private serverForm2 childForm;

        // Server
        public bool m_bStop = false;
        private TcpListener m_server;
        private Thread m_thServer;
        private List<Thread> threads = new List<Thread>();
        private List<NetworkStream> streams = new List<NetworkStream>();

        // Server Packet
        private byte[] sendBuffer = new byte[Constant.PACKET_SIZE];
        private byte[] readBuffer = new byte[Constant.PACKET_SIZE];

        // Client
        private TcpClient m_client;

        // Packet Class
        private Initialize m_initializeClass;
        private PanelInfo m_panelInfoClass;
        private IndexInfo m_indexInfoClass;
        private ChatInfo m_chatInfoClass;

        public serverForm1()
        {
            InitializeComponent();
        }

        public void WriteLog(string msg)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                childForm.txtChat.AppendText(msg + "\n");
            }));
        }

        public void Send(NetworkStream ns)
        {
            ns.Write(sendBuffer, 0, sendBuffer.Length);
            ns.Flush();

            for (int i = 0; i < Constant.PACKET_SIZE; i++)
            {
                sendBuffer[i] = 0;
            }
        }

        public void ServerStart()
        {
            try
            {
                string ip = txtIP.Text;
                int port = int.Parse(txtPort.Text);
                IPAddress ipAddr = IPAddress.Parse(ip);

                m_server = new TcpListener(ipAddr, port);
                m_server.Start();

                m_bStop = true;
                //WriteLog("클라이언트 접속 대기중...");

                while (m_bStop)
                {
                    m_client = m_server.AcceptTcpClient();

                    if (m_client.Connected)
                    {
                        // Make Thread - Client Access
                        CreateThread();
                    }
                }
            }
            catch (Exception ex)
            {
                ServerStop();
                return;
            }
        }

        public void ServerStop()
        {
            if (!m_bStop)
                return;

            m_bStop = false;
            //m_bConnect = false;
            m_server.Stop();
            //m_stream.Close();
            m_thServer.Abort();
            //WriteLog("서버 종료");
        }

        public void CreateThread()
        {
            Thread th = new Thread(new ThreadStart(ProcessClient));
            th.Start();
            threads.Add(th);
        }

        public void ProcessClient()
        {
            // Client and Connect Var
            bool m_bConnect = false;
            TcpClient client;
            NetworkStream stream;
            string clientID;

            // Add client
            client = m_client;
            m_bConnect = true;
            stream = client.GetStream();
            streams.Add(stream);

            clientID = ClientInit(stream);

            while (m_bConnect)
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
                catch
                {
                }

                switch ((int)packet.Type)
                {
                    case (int)PacketType.panelInfo:
                        {
                            // Receive Panel Information from Client
                            m_panelInfoClass = (PanelInfo)Packet.Desserialize(readBuffer);
                            int changedValue = m_panelInfoClass.changedValue;

                            childForm.npencil = m_panelInfoClass.npencil;
                            childForm.nline = m_panelInfoClass.nline;
                            childForm.nrect = m_panelInfoClass.nrect;
                            childForm.ncircle = m_panelInfoClass.ncircle;
                            childForm.index = m_panelInfoClass.index;

                            if (changedValue == 1)
                                childForm.mypencil[childForm.npencil] = m_panelInfoClass.mypencil;
                            else if(changedValue == 2)
                                childForm.mylines[childForm.nline] = m_panelInfoClass.mylines;
                            else if(changedValue == 3)
                                childForm.myrect[childForm.nrect] = m_panelInfoClass.myrect;
                            else if(changedValue == 4)
                                childForm.mycircle[childForm.ncircle] = m_panelInfoClass.mycircle;

                            // Draw Panel
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                childForm.panel.Invalidate(true);
                                childForm.panel.Update();
                            }));

                            // Send All Client
                            for(int i = 0; i < streams.Count; i++)
                            {
                                NetworkStream s = streams[i];

                                if (s == stream)
                                {
                                    // Pass My Stream
                                    continue;
                                }
                                Packet.Serialize(m_panelInfoClass).CopyTo(sendBuffer, 0);
                                try
                                {
                                    Send(s);
                                }
                                catch (Exception e)
                                {
                                    streams.Remove(s);
                                }
                            }
                            
                            break;
                        }
                    case (int)PacketType.indexInfo:
                        {
                            // Receive Index Information from Client
                            m_indexInfoClass = (IndexInfo)Packet.Desserialize(readBuffer);
                            childForm.npencil = m_indexInfoClass.npencil;
                            childForm.nline = m_indexInfoClass.nline;
                            childForm.nrect = m_indexInfoClass.nrect;
                            childForm.ncircle = m_indexInfoClass.ncircle;
                            childForm.index = m_indexInfoClass.index;

                            // Send All Client
                            for (int i = 0; i < streams.Count; i++)
                            {
                                NetworkStream s = streams[i];

                                if (s == stream)
                                {
                                    // Pass My Stream
                                    continue;
                                }
                                Packet.Serialize(m_indexInfoClass).CopyTo(sendBuffer, 0);
                                try
                                {
                                    Send(s);
                                }
                                catch (Exception e)
                                {
                                    streams.Remove(s);
                                }
                            }

                            break;
                        }
                    case (int)PacketType.chatInfo:
                        {
                            // Receive Chat Message from Client
                            m_chatInfoClass = (ChatInfo)Packet.Desserialize(readBuffer);
                            string receiveID = m_chatInfoClass.clientID;
                            string receiveMsg = m_chatInfoClass.msg;

                            // Print Chat Log
                            WriteLog(receiveID + ": " + receiveMsg);

                            // Send All Client
                            for (int i = 0; i < streams.Count; i++)
                            {
                                NetworkStream s = streams[i];

                                if (s == stream)
                                {
                                    // Pass My Stream
                                    continue;
                                }
                                Packet.Serialize(m_chatInfoClass).CopyTo(sendBuffer, 0);
                                try
                                {
                                    Send(s);
                                }
                                catch (Exception e)
                                {
                                    streams.Remove(s);
                                }
                            }
                            break;
                        }
                }
            }
        }

        public string ClientInit(NetworkStream stream)
        {
            // Send Panel Infomation to Client
            byte[] sendInitBuffer = new byte[Constant.INIT_PACKET_SIZE];

            PanelInit panel = new PanelInit();
            panel.Type = (int)PacketType.panelInfo;
            panel.mypencil = childForm.mypencil;
            panel.mylines = childForm.mylines;
            panel.myrect = childForm.myrect;
            panel.mycircle = childForm.mycircle;
            panel.npencil = childForm.npencil;
            panel.nline = childForm.nline;
            panel.nrect = childForm.nrect;
            panel.ncircle = childForm.ncircle;
            panel.index = childForm.index;

            // Serialize
            MemoryStream ms = new MemoryStream(Constant.INIT_PACKET_SIZE);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, panel);
            ms.ToArray().CopyTo(sendInitBuffer, 0);
            stream.Write(sendInitBuffer, 0, sendInitBuffer.Length);
            stream.Flush();

            // Receive ClientID from Client
            stream.Read(readBuffer, 0, Constant.PACKET_SIZE);
            m_initializeClass = (Initialize)Packet.Desserialize(readBuffer);

            return m_initializeClass.clientID;
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            m_thServer = new Thread(new ThreadStart(ServerStart));
            m_thServer.Start();

            childForm = new serverForm2();
            childForm.ShowDialog();
        }
    }
}
