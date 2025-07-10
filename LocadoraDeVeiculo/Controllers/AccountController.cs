using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using LocadoraDeVeiculo.Models;
using LocadoraDeVeiculo.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculo.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ClienteService _clienteService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ClienteService clienteService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _clienteService = clienteService;
        }

        [HttpGet]
        public ActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Registrar(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(model.Email);

                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "já existe uma conta com este email.");
                    return View(model);
                }

                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Senha);

                if (result.Succeeded)
                {
                    var cliente = new ClienteModels
                    {
                        Nome = model.Nome,
                        CPF = model.CPF,
                        CNH = model.CNH,
                        DataNascimento = model.DataNascimento,
                        Email = model.Email,
                        Telefone = model.Telefone,
                        Endereco = model.Endereco,
                        IdentityUserId = user.Id // pra vincular os dados do cliente ao usuário Identity
                    };

                    await _clienteService.Create(cliente);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if(user == null)
                {
                    ModelState.AddModelError(string.Empty, "Usuário");
                    return View(model);
                } 

                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Login Invalido");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout()
        { 
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
