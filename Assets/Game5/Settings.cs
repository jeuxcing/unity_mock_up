using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;  
using UnityEngine;


public class Settings : MonoBehaviour
    {
        public enum ClientStatus
        {
            Inactive,
            Activating,
            Active,
            Deactivating
        }

        public enum Command
        {
            Led = 76,
            Segment = 83
        }

        public enum Strip
        {
            Horizontal = 72,
            Vertical = 86,
            Ring = 82
        }
    
        [SerializeField] private string host;
        [SerializeField] private string port;
        private Listener _listener;
        public ClientStatus _clientStatus = ClientStatus.Active;

        public Segment[,] _H;
        private Segment[,] _V;
        private Segment[,] _R;

        private void Start()
        {
            _listener = new Listener(host, port, HandleMessage);
            Init();
            OnStartClient();
            EventManager.Instance.onClientStarted.AddListener(() => _clientStatus = ClientStatus.Active);
            EventManager.Instance.onStopClient.AddListener(OnStopClient);
            EventManager.Instance.onClientStopped.AddListener(() => _clientStatus = ClientStatus.Inactive);

        }

        private void Init(){
            _H = new Segment[5,4];
            _V = new Segment[4,5];
            _R = new Segment[5,5];
            Segment[] s = GameObject.FindObjectsOfType<Segment>();

            for (int i = 0; i < s.Length; i++){
                if (s[i].orientation == Segment.Orientation.horizontal){
                    Debug.Log(s[i].coordinates[0]);
                    _H[s[i].coordinates[0],s[i].coordinates[1]] = s[i];
                }
                if (s[i].orientation == Segment.Orientation.vertical){
                    Debug.Log(s[i].coordinates[0]);
                    _V[s[i].coordinates[0],s[i].coordinates[1]] = s[i];
                }
                if (s[i].orientation == Segment.Orientation.ring){
                    Debug.Log(s[i].coordinates[0]);
                    _R[s[i].coordinates[0],s[i].coordinates[1]] = s[i];
                }
            }  
        }

        private void Update()
        {
            if (_clientStatus == ClientStatus.Active)
                _listener.DigestMessage();
        }

        private void OnDestroy()
        {
            if (_clientStatus != ClientStatus.Inactive)
                OnStopClient();
        }

        private void HandleMessage(byte[] b)
        {
            string str = "";
            for (var i=5; i<b.Length; i++) str += b[i]+ "-";
            Debug.Log(str);

            if ((b[0]) == (int)Command.Led)
                HandleCommandLed(b);
            if ((b[0]) == (int)Command.Segment)
                HandleCommandSegment(b);
            
        }

        private void HandleCommandLed(byte[] b)
        {
            Color c = new Color((float)b[5]/255, (float)b[6]/255, (float)b[7]/255, 1);

            if ((b[1]) == (int)Strip.Horizontal)
                _H[b[2],b[3]].SetSingleColor(b[4],c);
            if ((b[1]) == (int)Strip.Vertical)
                _V[b[2],b[3]].SetSingleColor(b[4],c);  
            if ((b[1]) == (int)Strip.Ring)
                _R[b[2],b[3]].SetSingleColor(b[4],c);
        }

        private void HandleCommandSegment(byte[] b)
        {
            Color c = new Color((float)b[6]/255, (float)b[7]/255, (float)b[8]/255, 1);
            Color[] colors = new Color[24];    

            for (var i=b[4]; i<b[5]; i++)
                colors[i] = c;

            if ((b[1]) == (int)Strip.Horizontal)
                _H[b[2],b[3]].SetColors(colors, b[4], b[5]);
            if ((b[1]) == (int)Strip.Vertical)
                _V[b[2],b[3]].SetColors(colors, b[4], b[5]);
            if ((b[1]) == (int)Strip.Ring)
                _R[b[2],b[3]].SetColors(colors, b[4], b[5]);
        }

        private void OnStartClient()
        {
            Debug.Log("Starting client...");
            _listener.Start();
        }

        private void OnStopClient()
        {
            Debug.Log("Stopping client...");
            _listener.Stop();
            Debug.Log("Client stopped!");
        }
    }

