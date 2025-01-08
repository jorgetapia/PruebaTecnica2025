

namespace Ejercicio1POO
{
    /// <summary>
    /// Clase derivada que representa un sensor de humedad.
    /// </summary>
    public class SensorHumedad : Sensor
    {
        /// <summary>
        /// Valor de humedad recogido por el sensor.
        /// </summary>
        public double ValorHumedad { get; private set; }

        /// <summary>
        /// Constructor que recibe el ID, tipo y un valor inicial de humedad.
        /// Invoca el constructor de la clase base mediante base(id, tipo).
        /// </summary>
        /// <param name="id">Identificador único del sensor.</param>
        /// <param name="tipo">Tipo de sensor.</param>
        /// <param name="valorInicial">Valor inicial de humedad.</param>
        public SensorHumedad(int id, string tipo, double valorInicial) : base(id, tipo)
        {
            ValorHumedad = valorInicial;
        }

        /// <summary>
        /// Implementación del método abstracto ProcesarDatos() para un sensor de humedad.
        /// </summary>
        public override void ProcesarDatos()
        {
            
                Console.WriteLine($"Sensor de Humedad (ID {ID}): La humedad es {ValorHumedad}%");
            
        }
    }
}
