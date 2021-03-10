using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Server_DZ2
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress iP = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEnd = new IPEndPoint(iP, 8800);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Bind(iPEnd);
                server.Listen(10);
                Console.WriteLine("Сервер запущен. Ожидание подключений...\n");
                while (true)
                {
                    Socket client = server.Accept();
                    int bytes = 0;
                    StringBuilder builder = new StringBuilder();
                    byte[] data = new byte[1024];
                    do
                    {
                        bytes = client.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (client.Available > 0);
                    Console.WriteLine(builder);
                    Thread.Sleep(1000);
                    string message = "";
                    message = "В " + DateTime.Now.ToShortTimeString() + " от " + "[" +
                                    IPAddress.Parse(((IPEndPoint)server.LocalEndPoint).Address.ToString()) + "]" + " получена строка: Привет, клиент!";
                    data = Encoding.Unicode.GetBytes(message);
                    client.Send(data);
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                server.Close();
            }

            Console.ReadKey();
        }
    }
}
