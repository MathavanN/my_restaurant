using MyRestaurant.Models;
using System.Text;
using System.Threading.Tasks;

namespace MyRestaurant.Services
{
    public interface IUserAccessorService
    {
        CurrentUser GetCurrentUser();
    }
}
