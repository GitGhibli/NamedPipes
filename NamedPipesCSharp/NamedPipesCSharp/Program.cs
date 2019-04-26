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
        //slo by najprv poslat info o tom, co sa bude posielat
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
            var task = Task.Factory.StartNew(() =>
            {
                var server = new NamedPipeServerStream("MyTestPipe", PipeDirection.Out, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
                
                server.WaitForConnection();
                Console.WriteLine(String.Format("New Consumer connected: {0}", DateTime.Now.ToShortTimeString()));
                Thread.Sleep(2000);
                server.Write(GetByteArray(new MyStruct { MyNumber = 1, MyAnotherNumber = 9027, MyText = "Hello World!", MyAnotherText = "Hello another World!" }));
                Console.WriteLine("Struct Wrote");
                server.Write(GetByteArray(new MyStruct { MyNumber = 2, MyAnotherNumber = 9027, MyText = "Hello World!", MyAnotherText = "Hello another World!" }));
                Console.WriteLine("Struct Wrote");
                server.Write(GetByteArray(new MyStruct { MyNumber = 3, MyAnotherNumber = 9027, MyText = "Hello World!", MyAnotherText = "Hello another World!" }));
                Console.WriteLine("Struct Wrote");
                server.Disconnect();
            });


            task.Wait();
        }

        static byte[] GetByteArray(MyStruct myStruct)
        {
            int size = 255 + 255 + 8;
            byte[] buffer = new byte[size];

            IntPtr memoryPointer = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(myStruct, memoryPointer, true);
            Marshal.Copy(memoryPointer, buffer, 0, size);
            Marshal.FreeHGlobal(memoryPointer);

            return buffer;
        }
    }
}
