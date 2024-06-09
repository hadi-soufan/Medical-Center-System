using API.DTOs;
using API.Services;
using Application.Profile;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Security.Claims;

namespace API.Controllers
{
    /// <summary>
    /// Controller responsible for managing user accounts.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;

        /// <summary>
        /// Constructor for AccountController.
        /// </summary>
        /// <param name="context">ApplicationDbContext instance.</param>
        /// <param name="userManager">UserManager instance.</param>
        /// <param name="tokenService">TokenService instance.</param>
        public AccountController(ApplicationDbContext context, UserManager<AppUser> userManager, TokenService tokenService)
        {
            _context = context;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        /// <inheritdoc />
        // GET: /api/account/
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirstValue(ClaimTypes.Name));

            return CreateUserObject(user);
        }

        /// <inheritdoc />
        // POST: /api/account/login
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

        /// <inheritdoc />
        // POST: /api/account/register
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
                else if (registerDto.Role == "Receptionist")
                {
                    var receptionist = new Receptionist
                    {
                        ReceptionistId = Guid.NewGuid(),
                        UserId = user.Id
                    };

                    _context.Receptionists.Add(receptionist);
                    await _userManager.AddToRoleAsync(user, "Receptionist");
                }
                else if (registerDto.Role == "Staff")
                {
                    var staff = new Staff
                    {
                        StaffId = Guid.NewGuid(),
                        UserId = user.Id
                    };

                    _context.Staffs.Add(staff);
                    await _userManager.AddToRoleAsync(user, "Staff");
                }
                else if (registerDto.Role == "Nurse")
                {
                    var nurse = new Nurse
                    {
                        NurseId = Guid.NewGuid(),
                        UserId = user.Id
                    };

                    _context.Nurses.Add(nurse);
                    await _userManager.AddToRoleAsync(user, "Nurse");
                }
                else if (registerDto.Role == "Accountant")
                {
                    var accountant = new Accountant
                    {
                        AccountantId = Guid.NewGuid(),
                        UserId = user.Id
                    };

                    _context.Accountants.Add(accountant);
                    await _userManager.AddToRoleAsync(user, "Accountant");
                }


                

                await _context.SaveChangesAsync();

                return CreateUserObject(user);
            }

            return BadRequest(result.Errors);
        }

        /// <inheritdoc />
        // GET: /api/account/all-users
        [HttpGet("all-users")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<List<AppUser>>> GetUsers()
        {
            return HandleResult(await Mediator.Send(new UserList.Query()));
        }

        /// <inheritdoc />
        // GET: /api/account/user/{username}
        [HttpGet("user/{displayName}")]
        public async Task<IActionResult> GetUserById(string displayName)
        {
            var query = new UserDetails.Query { DisplayName = displayName };
            return HandleResult(await Mediator.Send(query));
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
