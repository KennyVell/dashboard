using Microsoft.EntityFrameworkCore;
using dashboard.Data;
using dashboard.DTOs;
using dashboard.Interfaces;
using dashboard.Models;

namespace dashboard.Services
{
    public class AccountService : IAccountService
    {
        private readonly BaseContext _context;

        public AccountService(BaseContext context)
        {
            _context = context;
        }

        public async Task<User> GoogleLoginAsync(string email, string? name)
        {
            // Busca el usuario por correo electrónico
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                // Si el usuario no existe, lo crea y guarda en la base de datos
                user = new User
                {
                    Email = email,
                    Name = name
                };
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Si el usuario ya existe, actualiza su información
                user.Name = name;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }

            return user;
        }

        public async Task<User> Login(LoginDTO user)
        {
            // Busca el usuario por correo electrónico
            User? userFind = await _context.Users
                .Where(u => u.Email == user.Email)
                .FirstOrDefaultAsync();

            // Verifica la contraseña del usuario
            if (userFind != null && BCrypt.Net.BCrypt.Verify(user.Password, userFind.Password))
            {
                return userFind;
            }
            return null!;
        }

        public async Task<User> Register(UserDTO user)
        {
            // Verifica si el correo electrónico ya está en uso
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                throw new Exception("El correo ya esta en uso.");
            }

            // Crea un nuevo usuario
            User userRegistration = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
            };

            await _context.Users.AddAsync(userRegistration);
            await _context.SaveChangesAsync();
            return userRegistration;
        }
    }
}