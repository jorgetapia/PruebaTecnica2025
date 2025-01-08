
namespace Ejercicio1POO
{
    /// <summary>
    /// Clase derivada que representa un sensor de temperatura.
    /// </summary>
    public class SensorTemperatura : Sensor
    {
        /// <summary>
        /// Valor de temperatura recibido por el sensor.
        /// </summary>
        public double ValorTemperatura { get; private set; }

        /// <summary>
        /// Constructor que recibe el ID, tipo y un valor inicial de temp.
        /// </summary>
        /// <param name="id">Identificador único del sensor.</param>
        /// <param name="tipo">Tipo de sensor.</param>
        /// <param name="valorInicial">Valor inicial de temp.</param>
        public SensorTemperatura(int id, string tipo, double valorInicial) : base(id, tipo)
        {
            ValorTemperatura = valorInicial;
        }

        /// <summary>
        /// Implementamos el método abstracto ProcesarDatos() para un sensor de temp e imprimimos los valores.
        /// </summary>
        public override void ProcesarDatos()
        {
                        
                Console.WriteLine($"Sensor de Temperatura (ID {ID}): La temperatura es {ValorTemperatura} °C.");
            
        }
    }
}
