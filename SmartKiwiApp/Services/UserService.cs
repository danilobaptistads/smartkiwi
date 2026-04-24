using SmartKiwiApp.Models;
using SmartKiwiApp.Repository;

namespace SmartKiwiApp.Services;
public class UserService
{
    private  readonly IUserRepository _userRepository;
    private  readonly IHashService _hashService;
    public UserService(IUserRepository repository, IHashService hashService)
    {
        _userRepository = repository;
        _hashService = hashService;
    }
    public async Task CreateNewUSer(string name, string email, string rawPassword)
    {
        
        var hashedPassword = _hashService.HashPassword(rawPassword);
        var newUser = new User(name, email, rawPassword);
        var emailAlreadyExist = await _userRepository.GetUserByEmail(newUser.Email);
        if (emailAlreadyExist !=null)
        {
            throw new InvalidOperationException("Não foi possível realizar o cadastro");
        }
        await _userRepository.Add(newUser);
        
    }
}