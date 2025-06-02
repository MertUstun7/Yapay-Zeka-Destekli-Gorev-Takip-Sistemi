namespace Entities.Exceptions
{
    public sealed class AssignmentNotFoundException : NotFoundException
    {
        public AssignmentNotFoundException(string id) : base($"{id} numaralı kullanıcı için bir görev bulunamadı.")
        {
        }
    }
}
