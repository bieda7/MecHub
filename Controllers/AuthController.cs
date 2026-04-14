using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using MecHub.Data;
using MecHub.Models;
using System.Security.Claims;


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

    public IActionResult LoginGoogle()
    {
        var redirectUrl = Url.Action("GoogleResponse");
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };

        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

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
                Senha = ""
            };

            _context.Add(usuario);
            _context.SaveChanges();
        }

        return RedirectToAction("Index", "Home");
    }
}