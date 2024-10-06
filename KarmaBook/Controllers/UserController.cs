using AutoMapper;
using KarmaBook.Models;
using KarmaBook.Models.DTOs;
using KarmaBook.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarmaBook.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;    
        private readonly IMapper _mapper;
        ResponseResult result = new ResponseResult();
        public UserController(IUserRepository userRepository,IMapper mapper, IUserAuthRepository userAuthRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var res =await _userRepository.GetById(id);
                 if (res.UserId != 0)
                {
                    result.Success = true;
                    result.Data = res;
                    result.StatusCode = 200;
                    result.Message = "User Id found successfully";
                    result.Pagination = 1;
                }
                else
                {
                    result.Success = false;
                    result.Data = res;
                    result.StatusCode = 400;
                    result.Message = "User Id not found";
                    result.Pagination = 1;
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res =await _userRepository.GetAll();
                if (res.Count != 0)
                {
                    result.Success = true;
                    result.Data = res;
                    result.StatusCode = 200;
                    result.Message = "Users found successfully";
                    result.Pagination = 1;
                }
                else
                {
                    result.Success = false;
                    result.Data = res;
                    result.StatusCode = 400;
                    result.Message = "Users not found";
                    result.Pagination = 1;
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }

    }
}
