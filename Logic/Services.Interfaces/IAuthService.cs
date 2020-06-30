using Logic.DataTableObjects;

namespace Logic.Services.Interfaces
{
    public interface IAuthService
    {
        UserDTO CurrentUser { get; set; }
        AuthState Login(string email, string password);
        AuthState Registration(string email, string password);
        void UpdateUserData(UserDTO userDTO);
    }
}