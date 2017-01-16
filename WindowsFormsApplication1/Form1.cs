using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        
        private string userName;
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream;
        bool threadRunning = false;
        public Form1()
        {
            InitializeComponent();
            Form1.CheckForIllegalCrossThreadCalls = false;
        }

        private void form1_Load(object sender, EventArgs e)
        {
            clientSocket.Connect("127.0.0.1", 8888);
            userName = Prompt.ShowDialog();
            label1.Text = "Server Connected to ...  " + userName;
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = Encoding.ASCII.GetBytes(userName + " $");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();


        }

        //submit button
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                NetworkStream serverStream = clientSocket.GetStream();
                byte[] outStream = Encoding.ASCII.GetBytes(userName + " says:" + textBox2.Text); //+ "$");// + textBox2.Text);//"This was printed$");
                serverStream.Write(outStream, 0, outStream.Length);
                //serverStream.Flush();

                if (!threadRunning)
                {
                    Thread othread = new Thread(new ThreadStart(msg));
                    othread.Start();
                }//end of starting thread
            }//end of try
            catch { }
        }//end of submit button

        public void msg()
        {
            threadRunning = true;
            while (threadRunning)
            {
                serverStream = clientSocket.GetStream();
                byte[] inStream = new byte[100000];
                try
                {
                    serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
                }//end of try
                catch (Exception e) { }
                //{
                  //  break;
                //}
                string returndata = Encoding.ASCII.GetString(inStream);

                if (!(returndata.Equals("\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0")))
                {
                    var cleaned = returndata.Replace("\0", string.Empty);
                    if (!(cleaned.Contains("Here are the names..."))&&!(cleaned.Contains("has switched to:")))
                    {
                        string[] tempSplit = cleaned.Split(':');
                        textBox1.Text += Environment.NewLine + " >> " + tempSplit[0] + " " + tempSplit[2] + "\n";
                    }//end of not containing sequence
                    else
                    {
                        textBox1.Text += Environment.NewLine + "\n >> " + cleaned;
                    }//end of else

                }//end of valid stream

            }//while

            //Close();
            threadRunning = false;
            textBox1.Text = "thread not running anymore.\n"; //debug statement
        }//end of msg

        private void closingForm(object sender, FormClosedEventArgs e)
        {
            threadRunning = false;
        }
    }//end of form1
}
