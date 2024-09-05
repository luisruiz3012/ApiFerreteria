using EmpleadosLibrary;
using EmpleadosLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiFerreteria.Controllers
{
    [ApiController]
    [Route("api/empleados")]
    public class EmpleadosController : ControllerBase
    {

        private readonly Methods empleadosLibrary;
        public EmpleadosController() {
            empleadosLibrary = new Methods();
        }

        [HttpGet]
        [Route("")]
        public dynamic Get()
        {
            try
            {
                var request = empleadosLibrary.Get();

                if (request == null) { return NotFound(); };

                return request;
            } catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public dynamic GetById(int id) {
            try
            {
                var request = empleadosLibrary.GetById(id);

                if (request == null) { return NotFound(); };

                return request;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("")]
        public dynamic Create([FromBody] Empleado empleado)
        {
            try
            {
                var request = empleadosLibrary.Create(empleado);

                if (request == null) { return BadRequest(); };

                return request;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public dynamic Update(int id, [FromBody] Empleado empleado)
        {
            try
            {
                var request = empleadosLibrary.Update(id, empleado);

                if (request == null) { return NotFound(); };

                return request;
            } catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public dynamic Delete(int id)
        {
            try
            {
                var request = empleadosLibrary.Delete(id);

                if (request == null) { return NotFound(); };

                return request;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message}");
            }
        }
    }
}
