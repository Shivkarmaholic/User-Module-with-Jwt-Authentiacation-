using AutoMapper;
using KarmaBook.Models;
using KarmaBook.Models.DTOs;
using KarmaBook.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarmaBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserAuthRepository _userAuthRepository;
        private readonly IMapper _mapper;
        ResponseResult result = new ResponseResult();
        public UserAuthController(IUserRepository userRepository,IMapper mapper, IUserAuthRepository userAuthRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userAuthRepository = userAuthRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto newUser)
        {
                     
            try
            {
                var existingUser = await _userRepository.GetUserByEmail(newUser.Email);
                if (existingUser.UserId != 0)
                {
                    result.Success = false;
                    result.Data = null;
                    result.StatusCode = 403;
                    result.Message = "Email Already Exists";
                    result.Pagination = 1;
                    return StatusCode(result.StatusCode,result);
                }

                var res =await _userAuthRepository.Register(_mapper.Map<User>(newUser));
                if (res == true)
                {
                    result.Success = true;
                    result.Data = newUser;
                    result.StatusCode = 200;
                    result.Message = "Added SuccessFully";
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

        [HttpGet("Login")]
        public async Task<IActionResult> Login([FromQuery]LoginDto loggingUser)
        {
            try
            {
                //var existingUser = await _userRepository.GetUserByEmail(loggingUser.Email);
                var result = await _userAuthRepository.Login(loggingUser);                
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
