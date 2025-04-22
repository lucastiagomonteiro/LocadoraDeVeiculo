
using Microsoft.AspNetCore.Identity;

namespace LocadoraDeVeiculo.Service
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public SeedUserRoleInitial(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task  SeedRoleAsync()
        {
            if(!await _roleManager.RoleExistsAsync("Customer"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Customer";
                role.NormalizedName = "Customer";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();
                IdentityResult roleResult = await _roleManager.CreateAsync(role);

            }

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "Admin";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();
                IdentityResult roleResult = await _roleManager.CreateAsync(role);

            }

            if (!await _roleManager.RoleExistsAsync("Employee"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Employee";
                role.NormalizedName = "Employee";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();
                IdentityResult roleResult = await _roleManager.CreateAsync(role);

            }
        }

        public async Task SeedUserRoleAsync()
        {

            if (await _userManager.FindByEmailAsync("Customer@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "Customer@localhost";
                user.Email = "Customer@localhost";
                user.NormalizedEmail = "Customer@localhost";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "Cliente@2023");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Customer");
                }

            }

            if (await _userManager.FindByEmailAsync("Admin@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "Admin@localhost";
                user.Email = "Admin@localhost";
                user.NormalizedEmail = "Admin@localhost";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "Admin@2023");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }


            if (await _userManager.FindByEmailAsync("Employee@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "Employee@localhost";
                user.Email = "Employee@localhost";
                user.NormalizedEmail = "Employee@localhost";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "Employee@2023");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Employee");
                }
            }
        }
    }
}
