using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Cliente
    {
        #region Variables e Instancias Globales
        const int _Port = 21;
        const string _ServerIP = "127.0.0.1";
        static string _txtMessage;
        static string _Codigo;
        static TcpClient _Cliente;
        static IPAddress _IPServer;
        static NetworkStream _NS;
        static Byte[] _dataMessage;
        static Byte[] _dataCode;
        #endregion

        #region Main
        static void Main(string[] args)
        {
            LlenarDatos();

            //Instanciamos un NetworkStream para la lectura y escritura
            //a partir del metodo GetStream
            _NS = _Cliente.GetStream();

            //A partir de aqui ya podemos enviar el mensaje al servidor
            Console.WriteLine("Enviando: " + _txtMessage);
            Console.WriteLine("Enviando: " + _Codigo);

            //Escribimos nuestros arrays
            _NS.Write(_dataMessage, 0, _dataMessage.Length);
            _NS.Write(_dataCode, 0, _dataCode.Length);

            int i = 0;
            //Y el socket queda a la escucha de la respuesta del servidor
            while (i < 2)
            {
                byte[] bytesToRead = new byte[_Cliente.ReceiveBufferSize];

                int bytesRead = _NS.Read(bytesToRead, 0, _Cliente.ReceiveBufferSize);
                Console.WriteLine("Recibido: " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
                i++;
            }
            Console.ReadLine();
            _Cliente.Close();
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Llenamos todos los datos de las variables que vamos a necesitar mas adelante
        /// </summary>
        private static void LlenarDatos()
        {
            _IPServer = IPAddress.Parse(_ServerIP);
            _txtMessage = DateTime.Now.ToString();
            _Codigo = "KDKFM784";

            //Crear un objeto TCPCliente asociado al servidor
            _Cliente = new TcpClient(_ServerIP, _Port);

            _dataMessage = Encoding.ASCII.GetBytes(_txtMessage);
            _dataCode = Encoding.ASCII.GetBytes(_Codigo);
        }
        #endregion
    }
}
