using System.Net;
using SimpleTCP;
namespace TcpClientApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SimpleTcpClient simpleTcpClient = new SimpleTcpClient();
            while (true)
            {

                Console.WriteLine("Enter the IpAdress to connect to the server: ");
                IPAddress serverIp;
                while (true)
                {
                    string stringIpAdress = Console.ReadLine();
                    if (!IPAddress.TryParse(stringIpAdress, out serverIp))
                    {
                        Console.WriteLine("Ip Adress is in wrong format, try again!");
                    }
                    else
                    {
                        break;
                    }
                }
                const int port = 5000;
                simpleTcpClient.Connect(serverIp.ToString(), port);
                if (!simpleTcpClient.TcpClient.Connected)
                {
                    Console.WriteLine("Server ip or port is not available, Please try again!");
                }
                else
                {
                    while (true)
                    {

                        Console.WriteLine("Welcome to the Math Adder Server");
                        float firstTerm;
                        while (true)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Please Enter the first term:");
                            if (!float.TryParse(Console.ReadLine(), out firstTerm))
                            {
                                Console.WriteLine("Please Enter a whole number or a decimal only. No characters or symbols allowed.");
                            }
                            else
                            {
                                break;
                            }
                        }
                        float secondTerm;
                        while (true)
                        {
                            
                            Console.WriteLine("Please Enter the second term:");
                            if (!float.TryParse(Console.ReadLine(), out secondTerm))
                            {
                                Console.WriteLine("Please Enter a whole number or a decimal only. No characters or symbols allowed.");
                            }
                            else
                            {
                                break;
                            }
                        }
                        float[] terms = new float[2];
                        terms[0] = firstTerm;
                        terms[1] = secondTerm;
                        byte[] bytes = FloatArrayToByteArray(terms);
                        simpleTcpClient.Write(bytes);
                        simpleTcpClient.DataReceived += OnDataRecieved;
                        
                    }
                }

            }


        }
        static void OnDataRecieved(object? sender, Message message)
        {
            
            Console.WriteLine(message.MessageString);
            Console.WriteLine("One more time:");
            Console.ReadKey();
        }
        static byte[] FloatArrayToByteArray(float[] floatArray)
        {
            // Create a byte array with enough space (each float is 4 bytes)
            byte[] byteArray = new byte[floatArray.Length * 4];

            // Copy the float array into the byte array
            Buffer.BlockCopy(floatArray, 0, byteArray, 0, byteArray.Length);

            return byteArray;
        }

    }
}