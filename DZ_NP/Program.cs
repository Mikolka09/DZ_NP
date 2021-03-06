using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace DZ_NP
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress iP = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEnd = new IPEndPoint(iP, 8008);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Bind(iPEnd);
                socket.Listen(10);

                Console.WriteLine("Сервер запущен. Ожидание подключений...");
                while (true)    
                {
                    Socket socket1 = socket.Accept();
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] data = new byte[256];

                    do
                    {
                        bytes = socket1.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (socket1.Available > 0);
                    string message = "";
                    if (builder.ToString() == "1")
                        message = DateTime.Now.ToShortDateString();
                    if (builder.ToString() == "2")
                        message = DateTime.Now.ToShortTimeString();
                    data = Encoding.Unicode.GetBytes(message);
                    socket1.Send(data);
                    socket1.Shutdown(SocketShutdown.Both);
                    socket1.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
