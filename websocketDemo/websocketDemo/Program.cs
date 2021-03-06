﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace websocketDemo
{
    class Server
    {
        public static void Main()
        {
            TcpListener server = new TcpListener(IPAddress.Parse("172.31.39.194"), 80);

            server.Start();
            Console.WriteLine("Server has started on 172.31.39.194:80.{0}Waiting for a connection...",
                Environment.NewLine);

            TcpClient client = server.AcceptTcpClient();

            Console.WriteLine("A client connected.");

            NetworkStream stream = client.GetStream();

            

            //enter to an infinite cycle to be able to handle every change in stream
            while (true)
            {
                while (!stream.DataAvailable);

                Byte[] bytes = new Byte[client.Available];

                stream.Read(bytes, 0, bytes.Length);

                //translate bytes of request to string
                String data = Encoding.UTF8.GetString(bytes);

                if (new Regex("^GET").IsMatch(data))
                {
                    Console.WriteLine(data);

                    const string eol = "\r\n"; // HTTP/1.1 defines the sequence CR LF as the end-of-line marker

                    Byte[] response = Encoding.UTF8.GetBytes("HTTP/1.1 101 Switching Protocols" + eol
                                                             + "Connection: Upgrade" + eol
                                                             + "Upgrade: websocket" + eol
                                                             + "Sec-WebSocket-Accept: " + Convert.ToBase64String(
                                                                 System.Security.Cryptography.SHA1.Create().ComputeHash(
                                                                     Encoding.UTF8.GetBytes(
                                                                         new System.Text.RegularExpressions.Regex(
                                                                                 "Sec-WebSocket-Key: (.*)").Match(data)
                                                                             .Groups[1].Value.Trim() +
                                                                         "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"
                                                                     )
                                                                 )
                                                             ) + eol
                                                             + eol);

                    stream.Write(response, 0, response.Length);

                }
                else if(bytes[0] != 138)
                {
                    Console.WriteLine(GetMessage(bytes));
                    string responseString = Console.ReadLine();

                    byte[] response = DataFrame(WebSocketDatatype.Text, Encoding.UTF8.GetBytes(responseString));

                    //Console.WriteLine(bytes.Length);

                    stream.Write(response, 0, response.Length);
                }
            }
        }

        private static string GetMessage(Byte[] bytes)
        {
            string DETEXT = "";
            if (bytes[0] == 129)
            {
                int position = 0;
                int Type = 0;
                ulong length = 0;
                if (bytes[1] - 128 >= 0 && bytes[1] - 128 <= 125)
                {
                    length = (ulong)bytes[1] - 128;
                    position = 2;
                }
                else if (bytes[1] - 128 == 126)
                {
                    Type = 1;
                    length = (ulong)256 * bytes[2] + bytes[3];
                    position = 4;
                }
                else if (bytes[1] - 128 == 127)
                {
                    Type = 2;
                    for (int i = 0; i < 8; i++)
                    {
                        ulong pow = Convert.ToUInt64(Math.Pow(256, (7 - i)));
                        length = length + bytes[2 + i] * pow;
                        position = 10;
                    }
                }
                else
                {
                    Type = 3;
                    Console.WriteLine("error 1");
                }

                if (Type < 3)
                {
                    Byte[] key = new Byte[4] { bytes[position], bytes[position + 1], bytes[position + 2], bytes[position + 3] };
                    Byte[] decoded = new Byte[bytes.Length - (4 + position)];
                    Byte[] encoded = new Byte[bytes.Length - (4 + position)];

                    for (long i = 0; i < bytes.Length - (4 + position); i++) encoded[i] = bytes[i + position + 4];

                    for (int i = 0; i < encoded.Length; i++) decoded[i] = (Byte)(encoded[i] ^ key[i % 4]);

                    DETEXT = Encoding.UTF8.GetString(decoded);
                }
            }
            else
            {
                Console.WriteLine("error 2: " + bytes[0].ToString());
            }
            return DETEXT;
        }

        public enum WebSocketDatatype
        {
            Continuation = 0x00,
            Text = 0x01,
            Binary = 0x02,
            ConnectionClose = 0x08
        }

        public static byte[] DataFrame(WebSocketDatatype dataType, byte[] payload, bool isLastFrame = true)
        {
            MemoryStream memoryStream = new MemoryStream();


            // If its the last frame, set the FIN bit
            byte finAndOpcodeByte = isLastFrame ? (byte) 0x80 : (byte) 0x00;


            // Opcode aka. the Datatype is presented in here
            byte firstByte = (byte)(finAndOpcodeByte | (byte)dataType);


            // Writing to the mem buffer.
            memoryStream.WriteByte(firstByte);

            if (payload.Length < 126)
            {
                byte secondByte = (byte)payload.Length;
                memoryStream.WriteByte(secondByte);
            }
            else if (payload.Length <= ushort.MaxValue)
            {
                byte secondByte = (byte) 126;

                memoryStream.WriteByte(secondByte);

                ushort len = (ushort) payload.Length;

                byte upper = (byte)((len >> 8) & 0xff);

                byte lower = (byte)(len & 0xff);

                memoryStream.WriteByte(upper);
                memoryStream.WriteByte(lower);
            }
            else
            {
                // Wat pls no
            }

            foreach(var bytes in payload)
                memoryStream.WriteByte(bytes);

            return memoryStream.ToArray();

        }
    }
}
