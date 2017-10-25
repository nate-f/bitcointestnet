﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Structures.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures.Messages.Tests
{
    [TestClass()]
    public class AddrMessageTests
    {
        [TestMethod()]
        public void AddrMessageTest()
        {
            var addrmessage = Message.CreateMessageFromByteArray(addrmessage1);
        }
        private readonly byte[] addrmessage1 = new byte[] {0xF9, 0xBE, 0xB4, 0xD9, 0x61, 0x64, 0x64, 0x72,  0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                           0x1F, 0x00, 0x00, 0x00, 0xED, 0x52, 0x39, 0x9B,  0x01, 0xE2, 0x15, 0x10, 0x4D, 0x01, 0x00, 0x00,
                                                           0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,  0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF,
                                                           0xFF, 0x0A, 0x00, 0x00, 0x01, 0x20, 0x8D};
    }
}