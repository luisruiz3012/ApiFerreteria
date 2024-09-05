using Microsoft.AspNetCore.Mvc;
using ProductosLibrary;
using ProductosLibrary.Models;

namespace ApiFerreteria.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController : ControllerBase
    {
        private readonly Methods _productosLibrary;

        public ProductosController()
        {
            _productosLibrary = new Methods();
        }

        [HttpGet]
        [Route("")]
        public dynamic Get()
        {
            try
            {
                var request = _productosLibrary.Get();

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
                var request = _productosLibrary.GetById(id);

                if (request == null) { return NotFound(); };

                return request;
            } catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("")]
        public dynamic Create([FromBody] Producto producto)
        {
            try
            {
                var request = _productosLibrary.Create(producto);

                if (request == null) { return BadRequest(); };

                return request;
            } catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public dynamic Update(int id, [FromBody] Producto producto)
        {
            try
            {
                var request = _productosLibrary.Update(id, producto);

                if (request == null) { return NotFound(); };

                return request;
            } catch (Exception ex)
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
                var request = _productosLibrary.Delete(id);

                if (request == null) { return NotFound(); };

                return request;
            } catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
