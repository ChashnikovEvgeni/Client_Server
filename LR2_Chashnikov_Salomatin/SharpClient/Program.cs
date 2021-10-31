using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SharpClient
{
    class Program
    {
        static void ProcessMessages()
        {
            while(true)
            {
                var m = Message.send(MessageRecipients.MR_BROKER, MessageTypes.MT_GETDATA);
                switch(m.header.type)
                {
                    case MessageTypes.MT_DATA:
                        Console.WriteLine(m.data);
                        break;
                    default:
                        Thread.Sleep(100);
                        break;
                }
            }
        }
        static void Main(string[] args)
        {
            Thread t = new Thread(ProcessMessages);
            t.Start();

            var m = Message.send(MessageRecipients.MR_BROKER, MessageTypes.MT_INIT);
            while(true)
            {
                Console.WriteLine("Send message");
                Console.WriteLine("1.Send All");
                Console.WriteLine("2.Send only one client");
                Console.WriteLine("3.Exit");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Write your message:");
                        string s = Console.ReadLine();
                        Message.send(MessageRecipients.MR_ALL, MessageTypes.MT_DATA, s);
                        break;
                    case 2:
                        Console.WriteLine("Write uesers ID:");
                        Message.send(MessageRecipients.MR_ME, MessageTypes.INFO, "");
                        int selected_ID = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Write your message:");
                        string users_message = Console.ReadLine();
                        Message.send(MessageRecipients.MR_USER, MessageTypes.MT_DATA, users_message);
                        break;
                    case 3:
                        Console.WriteLine("Application is closing");
                        Message.send(MessageRecipients.MR_ME, MessageTypes.MT_EXIT);
                        Environment.Exit(0);
                        break;
                }


              //  Message.send(MessageRecipients.MR_ALL, MessageTypes.MT_DATA, Console.ReadLine());
            }
        }
    }
}
