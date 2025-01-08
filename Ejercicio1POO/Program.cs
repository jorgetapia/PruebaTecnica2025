using Ejercicio1POO;

/// <summary>
/// Clase Main para comprobar la jerarquía de sensores IoT.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        // Instanciamos y pasamos valores para probar la intgración.
        Sensor sensorTemp = new SensorTemperatura(1, "Temperatura", 25.3);
        Sensor sensorHum = new SensorHumedad(2, "Humedad", 75.0);

        // creamos una lista a fin de iterar los resultados.
        List<Sensor> listaSensores = new List<Sensor>();
        listaSensores.Add(sensorTemp);
        listaSensores.Add(sensorHum);

        // iteremos la lista y llamamos al metodo procesarDatos() que es quien imprime los resultados de acuerdo a la clase que corresponda, TEmp o Humedad
        foreach (var sensor in listaSensores)
        {
            sensor.ProcesarDatos();
        }

        // Espera para visualizar la salida en consola (opcional).
        Console.WriteLine("\nPresione cualquier tecla para salir...");
        Console.ReadKey();
    }
}