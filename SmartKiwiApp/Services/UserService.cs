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

    public async Task UpdateUserName(Guid currentUSerID, string newName)
    {
        var userToUpdate = await _userRepository.GetUserById(currentUSerID);
        if(userToUpdate == null)
        {
            throw new ArgumentException("Não foi possivel realizar a alteração");
        }
    
        await _userRepository.UpdateName(userToUpdate, newName);
    }

    public async Task UpdateUserEmail(Guid currentUSerID, string currentUserPassword,string newName)
    {
        var userToUpdate = await _userRepository.GetUserById(currentUSerID);
        if(userToUpdate == null)
        {
            throw new ArgumentException("Não foi possivel realizar a alteração");
        }
        if(!userToUpdate.ValidatePassword(currentUserPassword, _passwordService))
        {
            throw new ArgumentException("Não foi possivel realizar a alteração");
        }
        await _userRepository.UpdateEmail(userToUpdate, newName);
    }

    public async Task UpdateUserPassword(Guid currentUSerId, string newPassword, string informedPassword)
    {
        var userToUpdate = await _userRepository.GetUserById(currentUSerId);
        if(userToUpdate == null || !userToUpdate.ValidatePassword(informedPassword, _passwordService))
        {
            throw new ArgumentException("Não foi possivel realizar a alteração");
        }
        await _userRepository.UpdatePassword(userToUpdate, newPassword, informedPassword, _passwordService);
    }

    public async Task DeleteCurrentUser(Guid currentUserId,string informedPassword)
    {
        var userToDelete = await _userRepository.GetUserById(currentUserId);
        if(userToDelete == null || !userToDelete.ValidatePassword(informedPassword, _passwordService))
        {
            throw new ArgumentException("Não foi possivel realizar a alteração");
        }

        await _userRepository.DeleteUser(userToDelete);

    }
}