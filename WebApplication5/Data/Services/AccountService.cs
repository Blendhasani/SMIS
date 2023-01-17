using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Data.Static;
using WebApplication5.Models;

namespace WebApplication5.Data.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _context= context;
            _userManager= userManager;
            _signInManager= signInManager;
         
        }


        [HttpPost]
        public async Task Register(Student student)
        {
           

            var newUser = new ApplicationUser()
            {
                FullName = student.Name,
                Email = student.Email,
                UserName = student.Email
                

            };

            var newUserResponse = await _userManager.CreateAsync(newUser, student.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            await _context.SaveChangesAsync();


         /*   return newUser;*/
        }


        [HttpPost]
        public async Task EditUser(string email,Student student)
        {
            var users = _context.Users.FirstOrDefault(s=>s.
            Email == email);


            var newUser = new ApplicationUser()
            {
                FullName = users.FullName,
                Email = users.Email,
                UserName = users.Email


            };

            var newUserResponse = await _userManager.UpdateAsync(newUser);

            if (newUserResponse.Succeeded)
          
            await _context.SaveChangesAsync();


            /*   return newUser;*/
        }
        [HttpPost]
        public async Task RegisterTeacher(Teacher teacher)
        {


            var newUser = new ApplicationUser()
            {
                FullName = teacher.Name,
                Email = teacher.Email,
                UserName = teacher.Email


            };

            var newUserResponse = await _userManager.CreateAsync(newUser, teacher.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.Teacher);
            await _context.SaveChangesAsync();


            /*   return newUser;*/
        }
    }
}
