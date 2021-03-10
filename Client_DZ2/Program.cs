using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;


namespace Client_DZ2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IPAddress iP = IPAddress.Parse("127.0.0.1");
                IPEndPoint iPEnd = new IPEndPoint(iP, 8800);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socket.Connect(iPEnd);
                string message = "В " + DateTime.Now.ToShortTimeString() + " от " + "[" +
                                IPAddress.Parse(((IPEndPoint)socket.LocalEndPoint).Address.ToString()) + "]" + " получена строка: Привет, сервер!";
                byte[] data = new byte[1024];
                data = Encoding.Unicode.GetBytes(message);
                socket.Send(data);

                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (socket.Available > 0);
                Console.WriteLine(builder);

                Console.ReadKey();
                socket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
