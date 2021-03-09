using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace DZ_NP_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                bool res = true;
                while (res)
                {
                    IPAddress iP = IPAddress.Parse("127.0.0.1");
                    IPEndPoint iPEnd = new IPEndPoint(iP, 8008);
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(iPEnd);
                    Console.WriteLine("Какие данные получить от сервера:\n" +
                                      "1. Дата\n2. Время\n3. Выход\n");
                    Console.Write("Ваш выбор: ");
                    string message = Console.ReadLine();
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    socket.Send(data);

                    data = new byte[256];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = socket.Receive(data, data.Length, 0);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (socket.Available > 0);
                    Console.WriteLine();
                    Console.WriteLine("Ответ сервера: " + builder.ToString());
                    Console.WriteLine();
                    Console.Write("Желаете сделать еще запрос (Да-1, Нет-2): ");
                    string var = Console.ReadLine();
                    if (message == "3" || var == "2")
                    {
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                        res = false;
                    }
                    if (var == "1")
                    {
                        Console.Clear();
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                        res = true;
                    }
                }
                Console.WriteLine("\nСвязь с сервером прервана! Для выхода нажмите любую кнопку!");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
