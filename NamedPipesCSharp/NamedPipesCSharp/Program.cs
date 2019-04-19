using System;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;

namespace NamedPipesCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Factory.StartNew(() =>
            {
                var server = new NamedPipeServerStream("MyTestPipe", PipeDirection.Out);

                while (true)
                {
                    server.WaitForConnection();
                    StreamWriter writer = new StreamWriter(server);
                    writer.WriteLine("Hello World!");
                    writer.Flush();
                    //StreamReader reader = new StreamReader(server);
                    //Console.WriteLine(reader.ReadLine());
                    server.Disconnect();
                }
            });

            Task.Delay(1000).Wait();

            //var client = new NamedPipeClientStream("MyTestPipe");
            //client.Connect();
            //StreamReader clreader = new StreamReader(client);
            //StreamWriter clwriter = new StreamWriter(client);

            string input = Console.ReadLine();

            //clwriter.WriteLine(input);

            //clwriter.Flush();
        }
    }
}
