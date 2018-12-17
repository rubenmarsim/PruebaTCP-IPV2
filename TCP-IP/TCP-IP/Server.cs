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
            StartListener();

            //Cuando recibe una peticion de conexion la acepta creando un socket de tipo
            //TCPClient preparado para poder recibir y enviar datos. Este nuevo TCPClient
            //se crea con el metodo AcceptTCPClient            
            _Client = _Listener.AcceptTcpClient();

            //Se crea el NetworkStream asociado al socket client y se dimensiona el buffer
            _NS = _Client.GetStream();
            byte[] buffer = new byte[_Client.ReceiveBufferSize];

            int i = 0;
            while (i < 2)
            {
                int bytesRead = _NS.Read(buffer, 0, _Client.ReceiveBufferSize);

                //string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Recibido : " + Encoding.ASCII.GetString(buffer, 0, bytesRead));

                Console.WriteLine("Enviando de vuelta : " + Encoding.ASCII.GetString(buffer, 0, bytesRead));
                _NS.Write(buffer, 0, bytesRead);
                i++;
            }

            PararServer();
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Definimos la IPAddress y creamos e instanciamos un objeto TCPListener 
        /// i lo activamos para que este a la escucha
        /// </summary>
        private static void StartListener()
        {
            _IP = IPAddress.Parse(_ServerIP);
            _Listener = new TcpListener(_IP, _Port);
            _Listener.Start();
        }

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
