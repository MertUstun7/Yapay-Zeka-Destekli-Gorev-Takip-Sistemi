namespace Entities.Exceptions
{
    public sealed class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string id) : base($"{id} numaralı kullanıcı bulunamadı.")
        {
        }
    }
}
