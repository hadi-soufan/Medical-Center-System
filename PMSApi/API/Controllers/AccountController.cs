using API.DTOs;
using API.Services;
using Application.Appointments;
using Application.Profile;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;

        public AccountController(ApplicationDbContext context, UserManager<AppUser> userManager, TokenService tokenService)
        {
            _context = context;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirstValue(ClaimTypes.Name));

            return CreateUserObject(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);

            if (user is null) return Unauthorized();

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if(result)
            {
                return CreateUserObject(user);
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _userManager.Users.AnyAsync(x => x.UserName == registerDto.Username))
            {
                return BadRequest("Username already exists");
            }

            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
            {
                return BadRequest("Email already exists");
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username,
                PhoneNumber = registerDto.PhoneNumber,
                FatherName = registerDto.FatherName,
                MotherName = registerDto.MotherName,
                DateOfBirth = registerDto.DateOfBirth,
                Nationality = registerDto.Nationality,
                Education = registerDto.Education,
                Gender = registerDto.Gender,
                MaritalStatus = registerDto.MaritalStatus,
                BloodType = registerDto.BloodType,
                Address = registerDto.Address,
                City = registerDto.City,
                ZipCode = registerDto.ZipCode,
                State = registerDto.State,
                Occupation = registerDto.Occupation,
                InsuranceId = registerDto.InsuranceId,
                Role = registerDto.Role,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                if (registerDto.Role == "Doctor")
                {
                    var doctor = new Doctor
                    {
                        DoctorId = Guid.NewGuid(),
                        DoctorLicenseId = registerDto.DoctorLicenseId,
                        UserId = user.Id
                    };

                     _context.Doctors.Add(doctor);
                    await _userManager.AddToRoleAsync(user, "Doctor");
                }
                else if (registerDto.Role == "Patient")
                {
                    var patient = new Patient
                    {
                        PatientId = Guid.NewGuid(),
                        UserId = user.Id
                    };

                    _context.Patients.Add(patient);
                    await _userManager.AddToRoleAsync(user, "Patient");
                }

                await _context.SaveChangesAsync();

                return CreateUserObject(user);
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("all-users")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<List<AppUser>>> GetUsers()
        {
            return HandleResult(await Mediator.Send(new UserList.Query()));
        }



        private UserDto CreateUserObject(AppUser user)
        {
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName,
                Role = user.Role
            };
        }
    }
}
