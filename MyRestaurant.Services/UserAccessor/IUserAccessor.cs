using MyRestaurant.Models;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public interface IUserAccessor
    {
        CurrentUser GetCurrentUser();
    }
}
