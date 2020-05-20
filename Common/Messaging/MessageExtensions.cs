using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace Common.Messaging
{
    public static class MessageExtensions
    {
        public static Message CreateMessage<T>(this Message message, string label, T obj)
        {
            message.Label = label;
            var body = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(obj));
            message.Body = body;
            return message;
        }
    }
}
