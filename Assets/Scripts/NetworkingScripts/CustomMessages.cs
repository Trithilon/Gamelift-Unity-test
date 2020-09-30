using System;
using System.Text;
using MLAPI.Messaging;
using MLAPI.Serialization.Pooled;
using UnityEngine;

namespace NetworkingScripts
{
    public class CustomMessages
    {
        private const string MessageName = "chat";
        public event EventHandler<string> ChatReceived;

        public CustomMessages()
        {
            CustomMessagingManager.RegisterNamedMessageHandler(MessageName, (sender, payload) =>
            {
                Debug.Log("MESSAGE HANDLER");
                using (var reader = PooledBitReader.Get(payload))
                {
                    ChatReceived?.Invoke(this, reader.ReadString().ToString());
                }
            });
        }

        public void SendChatMessage(ulong clientId, string message)
        {
            using (var messageStream = PooledBitStream.Get())
            {
                using (var writer = PooledBitWriter.Get(messageStream))
                {
                    writer.WriteString(message);
                    CustomMessagingManager.SendNamedMessage(MessageName, clientId, messageStream);
                }
            }
        }
    }
}