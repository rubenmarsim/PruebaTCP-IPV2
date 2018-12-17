using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCP_IP
{
    class Server
    {
        #region Variables e Instancias Globales
        const int _Port = 21;
        const string _ServerIP = "127.0.0.1";
        static IPAddress _IP;
        static TcpListener _Listener;
        static TcpClient _Client;
        static NetworkStream _NS;
        #endregion

        #region Main
        static void Main(string[] args)
        {
            _IP = IPAddress.Parse(_ServerIP);

            //Crear e instanciar un objeto TCPListener i activarlo para que este a la escucha
            _Listener = new TcpListener(_IP, _Port);
            _Listener.Start();

            //Quan rep una peticio de connexio, l'accepta creant unsocket de tipus TCPClient
            //preparat per a poder rebre i enviar dades. Aquest nou TCPClient es crea amb el 
            //metode AcceptTCPClient
            _Client = _Listener.AcceptTcpClient();

            //Es crea el NetworkStream associat al socket client i es dimensiona el buffer
            _NS = _Client.GetStream();
            byte[] buffer = new byte[_Client.ReceiveBufferSize];

            int i = 0;
            string strCliente = "CODI:";
            while (i < 2)
            {
                int bytesRead = _NS.Read(buffer, 0, _Client.ReceiveBufferSize);

                //string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Recibido : " + Encoding.ASCII.GetString(buffer, 0, bytesRead));

                Console.WriteLine("Enviando de vuelta : " + strCliente + Encoding.ASCII.GetString(buffer, 0, bytesRead));
                _NS.Write(buffer, 0, bytesRead);
                i++;
            }

            PararServer();
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Metodo para parar el servidor
        /// </summary>
        private static void PararServer()
        {
            _NS.Close();
            _Client.Close();
            _Listener.Stop();
            Console.ReadLine();
        }
        #endregion
    }
}
