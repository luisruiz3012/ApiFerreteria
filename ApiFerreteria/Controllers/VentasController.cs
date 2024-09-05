using Microsoft.AspNetCore.Mvc;
using VentasLibrary;
using VentasLibrary.Models;

namespace ApiFerreteria.Controllers
{
    [ApiController]
    [Route("api/ventas")]
    public class VentasController : ControllerBase
    {
        private readonly Methods ventasLibrary;

        public VentasController() { 
            ventasLibrary = new Methods();
        }

        [HttpGet]
        [Route("")]
        public dynamic Get()
        {
            try
            {
                var request = ventasLibrary.Get();

                if (request == null)
                {
                    return NotFound();
                }

                return request;
            } catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public dynamic GetById(int id)
        {
            try
            {
                var request = ventasLibrary.GetById(id);

                if (request == null)
                {
                    return NotFound();
                }

                return request;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("")]
        public dynamic Create([FromBody] Venta venta)
        {
            try
            {
                var request = ventasLibrary.Create(venta);

                if (request == null)
                {
                    return BadRequest();
                }

                return request;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public dynamic Update(int id, [FromBody] Venta venta)
        {
            try
            {
                var request = ventasLibrary.Update(id, venta);

                if (request == null)
                {
                    return NotFound();
                }

                return request;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public dynamic Delete(int id)
        {
            try
            {
                var request = ventasLibrary.Delete(id);

                if (request == null)
                {
                    return NotFound();
                }

                return request;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
