using FMS_Store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FMS_Store.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IRepo<Product> _dataRepository;
        public ProductApiController(IRepo<Product> dataRepository)
        {
            _dataRepository = dataRepository;   
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            try
            {
                var result = _dataRepository.List();
                return Ok(result);  
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<Product> Get(int id)
        {
            try
            {
                var result = _dataRepository.Find(id);
                if(result == null)
                {
                    return NotFound();  
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult<bool> Delete(int id)
        {
            try
            {
                _dataRepository.Delete(id);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
