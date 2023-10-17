using API.PastillApp.Domain.Entities;
using API.PastillApp.Repositories.Interface;
using API.PastillApp.Services.DTOs;
using API.PastillApp.Services.Interfaces;
using AutoMapper;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;

namespace API.PastillApp.Services.Services
{
    public class TokenService : ITokenService
{
    private readonly ITokenRepository _tokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly FirebaseApp _firebaseApp;
    private readonly IMapper _mapper;

    public TokenService(ITokenRepository tokenRepository, IUserRepository userRepository, IMapper mapper, FirebaseApp firebaseApp)
    {
        _tokenRepository = tokenRepository;
        _userRepository = userRepository;
        _firebaseApp = firebaseApp;
        _mapper = mapper;
    }

        public async Task<ResponseDTO> CreateOrUpdateToken(CreateTokenDTO tokenDTO)
        {
            try
            {
                // Verificar si ya existe un token con el mismo valor
                var existingToken = await _tokenRepository.GetTokenByValue(tokenDTO.DeviceToken);

                if (existingToken == null)
                {
                    // Si no existe, crea un nuevo token
                    var newToken = _mapper.Map<Token>(tokenDTO);

                    // Buscar el UserId por el correo electrónico proporcionado
                    var user = await _userRepository.GetUserByEmail(tokenDTO.UserEmail.ToLower());

                    if (user != null)
                    {
                        newToken.UserId = user.UserId;
                    }

                    await _tokenRepository.AddToken(newToken);
                }
                else
                {
                    // Si el token ya existe, verifica si el usuario existe antes de actualizar UserId y UserEmail
                    var user = await _userRepository.GetUserByEmail(tokenDTO.UserEmail.ToLower());

                    if (user == null)
                    {
                        return new ResponseDTO
                        {
                            isSuccess = false,
                            message = "El usuario con el correo especificado no existe.",
                        };
                    }

                    existingToken.UserId = user.UserId;
                    existingToken.UserEmail = user.Email;
                    await _tokenRepository.UpdateToken(existingToken);
                }

                return new ResponseDTO
                {
                    isSuccess = true,
                    message = "Token creado o actualizado con éxito",
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear o actualizar el token: {ex.Message}");
                return new ResponseDTO
                {
                    isSuccess = false,
                    message = "Error al crear o actualizar el token",
                };
            }
        }


        public async Task<ResponseDTO> DeleteToken(string tokenValue)
    {
        try
        {
            var tokenToDelete = await _tokenRepository.GetTokenByValue(tokenValue);
            if (tokenToDelete == null)
            {
                return new ResponseDTO
                {
                    isSuccess = false,
                    message = "Token no encontrado",
                };
            }

            await _tokenRepository.DeleteToken(tokenToDelete);

            return new ResponseDTO
            {
                isSuccess = true,
                message = "Token eliminado con éxito",
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar el token: {ex.Message}");
            return new ResponseDTO
            {
                isSuccess = false,
                message = "Error al eliminar el token",
            };
        }
    }

        public async Task<Token> GetTokenByUserEmail(string userEmail)
        {
            try
            {
                userEmail = userEmail.ToLower(); // Asegúrate de que el correo electrónico esté en minúsculas

                var token = await _tokenRepository.GetTokenByUserEmail(userEmail);

                return token;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el token por correo electrónico: {ex.Message}");
                // Puedes manejar el error según tus necesidades, por ejemplo, lanzar una excepción personalizada.
                throw;
            }
        }

        public async Task<IEnumerable<Token>> GetAllTokens()
        {
            try
            {
                var tokens = await _tokenRepository.GetAllTokens();
                return tokens;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener todos los tokens: {ex.Message}");
                // Maneja el error según tus necesidades, por ejemplo, lanza una excepción personalizada o devuelve una lista vacía.
                throw;
            }
        }

        public async Task<ResponseDTO> SendMessage(string title, string body, string token)
        {
            var messaging = FirebaseMessaging.GetMessaging(_firebaseApp);

            var message = new Message
            {
                Notification = new Notification
                {
                    Title = title,
                    Body = body
                },
                Data = new Dictionary<string, string>
                {
                    { "tipo", "POP" }
                },
                Token = token
            };

            try
            {
                string response = await messaging.SendAsync(message);
                return new ResponseDTO { isSuccess = true, message =  response };
            }
            catch(FirebaseMessagingException e)
            {
                return new ResponseDTO { isSuccess = false, message = e.Message };
            }
        }
    }
}
