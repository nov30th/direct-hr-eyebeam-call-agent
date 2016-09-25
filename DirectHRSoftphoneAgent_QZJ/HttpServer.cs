using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using DHRSoftphone.AgentExceptions;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    public abstract class HttpServer
    {
        public Thread m_thread;
        protected int port;
        private TcpListener listener;
        public bool is_active = true;

        public HttpServer(int port)
        {
            this.port = port;
        }

        public void listen()
        {
            bool isSuccess = false;
            listener = new TcpListener(System.Net.IPAddress.Loopback, port);
            try
            {
                listener.Start();
                isSuccess = true;
            }
            catch (Exception e)
            {
                is_active = false;
                Thread.CurrentThread.Abort();
            }
            finally
            {
                if (isSuccess == false)
                {
                    //Thread.CurrentThread.Abort();
                    throw new AgentException("Bind 7069 failed! Please check whether you started the program already!");
                }
            }
            while (is_active)
            {
                TcpClient s = listener.AcceptTcpClient();
                QzjHttpServer processor = new QzjHttpServer(s, this);
                m_thread = new Thread(new ThreadStart(processor.process));
                m_thread.Start();
                Thread.Sleep(1);
            }
        }

        public abstract void Exit();

        public abstract void handleGETRequest(QzjHttpServer p);

        public abstract void handlePOSTRequest(QzjHttpServer p, StreamReader inputData);
    }
}