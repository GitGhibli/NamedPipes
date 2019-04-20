using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace NamedPipesCSharp
{
    class Program
    {
        [StructLayout(LayoutKind.Sequential, Pack =1, CharSet = CharSet.Ansi)]
        struct MyStruct
        {
            public int MyNumber;

            public int MyAnotherNumber;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string MyText;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string MyAnotherText;

        }

        static void Main(string[] args)
        {
            Task.Factory.StartNew(() =>
            {
                var server = new NamedPipeServerStream("MyTestPipe", PipeDirection.Out);

                var myStruct = new MyStruct{ MyNumber = 8611, MyAnotherNumber=9027, MyText="Hello World!", MyAnotherText="Hello another World!"};
                
                int size = 255 + 255 + 8;
                byte[] buffer = new byte[size];

                IntPtr memoryPointer = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(myStruct, memoryPointer, true);
                Marshal.Copy(memoryPointer, buffer, 0, size);
                Marshal.FreeHGlobal(memoryPointer);

                while (true)
                {
                    server.WaitForConnection();
                    Console.WriteLine(String.Format("New Consumer connected: {0}", DateTime.Now.ToShortTimeString()));
                    var writer = new BinaryWriter(server);
                    Thread.Sleep(2000);
                    writer.Write(buffer);
                    writer.Flush();
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
