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
        static TcpClient _Cliente;
        #endregion

        #region Main
        static void Main(string[] args)
        {
            IPAddress server = IPAddress.Parse(_ServerIP);

            string txtMessage = DateTime.Now.ToString();
            string codigo = "KDKFM784";

            //Crear un objeto TCPCliente asociado al servidor
            _Cliente = new TcpClient(_ServerIP, _Port);

            Byte[] dades = Encoding.ASCII.GetBytes(txtMessage);
            Byte[] dades2 = Encoding.ASCII.GetBytes(codigo);

            //Instanciamos un NetworkStream para la lectura y escritura
            //a partir del metodo GetStream
            NetworkStream NS = _Cliente.GetStream();

            //A partir de aqui ya podemos enviar el mensaje al servidor
            Console.WriteLine("Enviando: " + txtMessage);
            Console.WriteLine("Enviando: " + codigo);

            //Escribimos nuestros arrays
            NS.Write(dades, 0, dades.Length);
            NS.Write(dades2, 0, dades2.Length);

            int i = 0;
            //Y el socket queda a la escucha de la respuesta del servidor
            while (i < 2)
            {
                byte[] bytesToRead = new byte[_Cliente.ReceiveBufferSize];

                int bytesRead = NS.Read(bytesToRead, 0, _Cliente.ReceiveBufferSize);
                Console.WriteLine("Recibido: " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
                i++;
            }
            Console.ReadLine();
            _Cliente.Close();
        }
        #endregion

        #region Metodos

        #endregion
    }
}
