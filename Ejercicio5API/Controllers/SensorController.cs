using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ejercicio5API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  // Requiere JWT válido
    public class SensorController : ControllerBase
    {
        //Datos de ejemplo
        private static readonly List<SensorData> _ListaSensorsData = new List<SensorData>
        {
            new SensorData { Id = 1, Nombre = "Sensor Temp 1", Valor = 25 },
            new SensorData { Id = 2, Nombre = "Sensor Hum 1", Valor = 35 },
            new SensorData { Id = 3, Nombre = "Sensor Temp 2", Valor = 24 },
            new SensorData { Id = 4, Nombre = "Sensor Hum 2", Valor = 36 },
           
        };

        /// <summary>
        /// obtenemos la lista de sensores con paginación.
        /// </summary>
        [HttpGet]
        public IActionResult GetSensor([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 2)
        {
            
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 2;

            
            int totalItems = _ListaSensorsData.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

          
            var sensors = _ListaSensorsData
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            
            var response = new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Data = sensors
            };

            return Ok(response);
        }
    }

    // Modelo de datos para prueba
    public class SensorData
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public double Valor { get; set; }
    }
}
