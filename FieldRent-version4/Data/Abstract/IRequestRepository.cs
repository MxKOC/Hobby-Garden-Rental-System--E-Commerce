using FieldRent.Entity;

namespace FieldRent.Data.Abstract
{
    public interface IRequestRepository
    {
        IQueryable<Request> Requests { get; }
        void CreateRequest(Request request);
    }
}