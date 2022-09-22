using FMS_Store.Models;
using FMS_Store.ViewModels;
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
        public ActionResult<List<Product>> GetAll(int? categoryId)
        {
            try
            {
                if(categoryId == null)
                {
                    var result = _dataRepository.List();
                    return Ok(result);
                    
                }
                    
                else
                {
                    var result = _dataRepository.List(a => a.CategoryId == categoryId );
                    return Ok(result);
                }
                    
                
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
                if (result == null)
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

        
     
    }
}
