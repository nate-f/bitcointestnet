using Structures.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinNode
{
    public class Application
    {
        public static void Main(String[] args)
        {
            while(true)
            {
                //main message loop
                //take in network traffic and process it
            }
        }
        public static void Process(byte[] bits)
        {
            Message m = Message.CreateMessageFromByteArray(bits);
            if(m is VersionMessage)
            {
                var versionMessage = m as VersionMessage;
                //process the version message
            }
        }
    }
}
