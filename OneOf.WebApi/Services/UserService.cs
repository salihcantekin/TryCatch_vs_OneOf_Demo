using OneOf.WebApi.Models;

namespace OneOf.WebApi.Services;

public class UserService
{
    public UserLoginResponse Login_WithException(UserLoginRequest request)
    {
        bool isUserConfirmed = request.EmailAddress is not null;
        bool isPaswordCorrect = request.Password is not null;

        if (!isPaswordCorrect)
            throw new Exception("Password is not correct");
         
        if (!isUserConfirmed)
            throw new Exception("User is not confirmed");

        return new UserLoginResponse("Salih Cantekin", "access-token", "refresh-token");
    }

    public OneOf<UserLoginResponse, Exception> Login_WithOneOf(UserLoginRequest request)
    {
        bool isUserConfirmed = request.EmailAddress is not null;
        bool isPaswordCorrect = request.Password is not null;
        
        if (!isPaswordCorrect)
            return new Exception("Password is not correct");

        if (!isUserConfirmed)
            return new Exception("User is not confirmed");

        return new UserLoginResponse("Salih Cantekin", "access-token", "refresh-token");
    }
}
