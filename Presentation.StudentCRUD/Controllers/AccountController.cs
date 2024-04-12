using Domain.StudentCRUD;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.StudentCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new AppUser { UserName = model.Email, Email = model.Email };

            // Check if the specified role exists
            var roleExists = await _roleManager.RoleExistsAsync(model.Role);


            if (!roleExists)
            {
                // If the role doesn't exist, return error
                return BadRequest("Invalid role specified.");
            }

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Assign the specified role to the user
                await _userManager.AddToRoleAsync(user, model.Role);



                return Ok("User registered successfully.");
            }

            return BadRequest(result.Errors);
        }

        [HttpGet("Getuser")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok("User deleted successfully.");
            }
            return BadRequest(result.Errors);
        }

        [HttpPut, Route("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(string userId, string email, string username, string phoneNumber)
        {
            var student = await _userManager.FindByIdAsync(userId);
            if (student != null)
            {
                student.Email = email;
                student.UserName = username;
                student.PhoneNumber = phoneNumber;
            }

            await _userManager.UpdateAsync(student);
            return StatusCode(StatusCodes.Status200OK, "Successfully updated");
        }

        [HttpPut, Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string userId, string password)
        {
            var student = await _userManager.FindByIdAsync(userId);
            if (student != null)
            {
                student.Email = email;
                student.UserName = username;
                student.PhoneNumber = phoneNumber;
            }

            await _userManager.UpdateAsync(student);
            return StatusCode(StatusCodes.Status200OK, "Successfully updated");
        }



        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
