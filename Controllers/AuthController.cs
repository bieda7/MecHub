using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using MecHub.Data;
using MecHub.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MecHub.ViewModel;

public class AuthController : Controller
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult TestLogin()
    {
        return Content("<a href='/Auth/LoginGoogle'>Login com Google</a>", "text/html");
    }
    [HttpGet]
    public IActionResult LoginLocal()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginLocal(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // 🔍 Buscar usuário
        var usuario = _context.usuario
            .FirstOrDefault(u => u.Email == model.Email);

        if (usuario == null)
        {
            ModelState.AddModelError("", "Usuário ou senha inválidos");
            return View(model);
        }

        // 🔐 Verificar senha com HASH
        var hasher = new PasswordHasher<Usuario>();

        var resultado = hasher.VerifyHashedPassword(
            usuario,
            usuario.Senha,
            model.Senha
        );

        if (resultado == PasswordVerificationResult.Failed)
        {
            ModelState.AddModelError("", "Usuário ou senha inválidos");
            return View(model);
        }
        usuario.TipoLogin = TipoLoginEnum.Local;
        _context.SaveChanges();

        // 🧠 Criar Claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim("UserId", usuario.Id.ToString())
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        var principal = new ClaimsPrincipal(identity);

        // 🍪 Login (cookie)
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties
            {
                IsPersistent = model.LembrarMe
            }
        );

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult LoginGoogle()
    {
        var redirectUrl = Url.Action("GoogleResponse");
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };

        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet]
    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        if (!result.Succeeded || result.Principal == null)
        {
            return Content("Falha ao autenticar usuário");
        }

        var claims = result.Principal.Identities.FirstOrDefault()?.Claims;

        if (claims == null)
        {
            return Content("Não foi possível obter os dados do usuário");
        }


        var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var googleId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var nome = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        var usuario = _context.usuario.FirstOrDefault(u => u.IdGoogle == googleId);

        if (usuario == null)
        {
            usuario = new Usuario
            {
                Nome = nome,
                Email = email,
                IdGoogle = googleId,
                // Senha = ""
            };

            _context.Add(usuario);
            usuario.TipoLogin = TipoLoginEnum.Google;
            _context.SaveChanges();
        }

        return RedirectToAction("Index", "Mecanico");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        return RedirectToAction("LoginLocal", "Auth");
    }
}