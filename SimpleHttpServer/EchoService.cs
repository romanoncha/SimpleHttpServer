using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    class EchoService
    {
        private TcpClient connectionSocket;
        private String[] programs = {"Calculator", "Gallery", "MusicPlayer"};

        public EchoService(TcpClient connectionSocket)
        {
            // TODO: Complete member initialization
            this.connectionSocket = connectionSocket;
        }
        public void DoIt()
        {
            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            string message = sr.ReadLine();
            string answer, command = "";
            while (message != null && message != "")
            {

                //Console.WriteLine("Client: " + message);
                answer = "Server started program ";
                
                if (message.IndexOf("GET /?") == 0)
                {
                    for (int i = 6; i < message.Length; i++)
                    {
                        if (message[i] != ' ')
                            command += message[i];
                        else
                            break;
                    }
                    for (int i = 0; i < programs.Length; i++)
                    {
                        if (programs[i].ToLower().IndexOf(command) == 0)
                            answer += programs[i];
                    }
                    Console.WriteLine("Command from client: " + command);
                    if (answer == "Server started program ")
                        sw.WriteLine("The program does not installed on server");
                    else
                        sw.WriteLine(answer);
                }               

                //sw.WriteLine(answer);
                message = sr.ReadLine();

            }
            connectionSocket.Close();
        }

        public string answer { get; set; }
    }
}
