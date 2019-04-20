using System;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace NamedPipesCSharp
{
    class Program
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        struct MyStruct
        {
            public int MyNumber;
            public int MyAnotherNumber;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
            public string MyText;
        }

        static void Main(string[] args)
        {
            Task.Factory.StartNew(() =>
            {
                var server = new NamedPipeServerStream("MyTestPipe", PipeDirection.Out);

                var myStruct = new MyStruct{ MyNumber = 8611, MyAnotherNumber=9027, MyText="Hello World!"};

                var size = 2 * sizeof(int);
                size += myStruct.MyText.Length;
                byte[] buffer = new byte[21];

                IntPtr memoryPointer = Marshal.AllocHGlobal(21);
                Marshal.StructureToPtr(myStruct, memoryPointer, true);
                Marshal.Copy(memoryPointer, buffer, 0, 21);
                Marshal.FreeHGlobal(memoryPointer);

                while (true)
                {
                    server.WaitForConnection();
                    Console.WriteLine(String.Format("New Consumer connected: {0}", DateTime.Now.ToShortTimeString()));
                    var writer = new BinaryWriter(server);
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
