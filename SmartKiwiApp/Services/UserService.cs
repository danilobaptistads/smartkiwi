using SmartKiwiApp.Models;
using SmartKiwiApp.Repository;

namespace SmartKiwiApp.Services;
public class UserService
{
    private  readonly IUserRepository _userRepository;
    private  readonly IPasswordService _passwordService;
    public UserService(IUserRepository repository, IPasswordService passwordService)
    {
        _userRepository = repository;
        _passwordService = passwordService;
    }
    public async Task CreateNewUSer(string name, string email, string rawPassword)
    {
        
        var hashedPassword = _passwordService.ProcssesHashNewPassword(rawPassword);
        var newUser = new User(name, email, hashedPassword);
        var emailAlreadyExist = await _userRepository.GetUserByEmail(newUser.Email);
        if (emailAlreadyExist !=null)
        {
            throw new InvalidOperationException("Não foi possível realizar o cadastro");
        }
        await _userRepository.Add(newUser);
        
    }
}