using RDTH.Data.Models;

namespace RDTH.Data
{
    public interface ICardService
    {
        CustomerCard GetCurrentUserCard(string Id);
    }
}
