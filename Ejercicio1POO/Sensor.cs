
namespace Ejercicio1POO
{
    public abstract class Sensor
    {
        /// <summary>
        /// Identificador único del sensor.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Tipo de sensor ("Temperatura", "Humedad", etc).
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Constructor de la clase base Sensor. Inicializa las propiedades 
        /// comunes ID y Tipo.
        /// </summary>
        /// <param name="id">Identificador único del sensor.</param>
        /// <param name="tipo">Tipo de sensor.</param>
        protected Sensor(int id, string tipo)
        {
            ID = id;
            Tipo = tipo;
        }

        /// <summary>
        /// Método abstracto que cada sensor debe implementar para procesar sus datos.
        /// Al ser abstracto, obliga a las clases derivadas a sobreescribirlo.
        /// </summary>
        public abstract void ProcesarDatos();
    }
}
