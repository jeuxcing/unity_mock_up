using System;
using System.Collections.Concurrent;
using System.Threading;
using UnityEngine;
using NetMQ;
using NetMQ.Sockets;


    public class Listener
    {
        private Thread _clientThread;
        public readonly string _host;
        public readonly string _port;
        private readonly Action<byte[]> _messageCallback;
        private bool _clientCancelled;

        private readonly ConcurrentQueue<byte[]> _messageQueue = new ConcurrentQueue<byte[]>();

        public Listener(string host, string port, Action<byte[]> messageCallback)
        {
            _host = host;
            _port = port;
            _messageCallback = messageCallback;
        }

        public void Start()
        {
            _clientCancelled = false;
            _clientThread = new Thread(ListenerWork);
            _clientThread.Start();
        }

        public void Stop()
        {
            _clientCancelled = true;
            _clientThread?.Join();
            _clientThread = null;
        }

        private void ListenerWork()
        {
            AsyncIO.ForceDotNet.Force();
            using (var subSocket = new SubscriberSocket())
            {
                subSocket.Options.ReceiveHighWatermark = 1000;
                subSocket.Connect($"tcp://{_host}:{_port}");
                Debug.Log(_host);
                Debug.Log(_port);
                Debug.Log("Client started!");
                subSocket.SubscribeToAnyTopic();
                while (!_clientCancelled)
                {
                    if (!subSocket.TryReceiveFrameBytes(out var message)) continue;
                    _messageQueue.Enqueue(message);
                }
                subSocket.Close();
            }
            NetMQConfig.Cleanup();
        }

        public void DigestMessage()
        {
            while (!_messageQueue.IsEmpty)
            {
                if (_messageQueue.TryDequeue(out var message))
                    _messageCallback(message);
                else
                    break;
            }
        }
    }
