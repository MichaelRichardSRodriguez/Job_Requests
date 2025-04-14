using Job_Requests.Models;
using Job_Requests.Models.ViewModels;
using System.Linq.Expressions;

namespace Job_Requests.DataAccess.Services
{
    public interface IUserRoleManagementService
    {

        Task<IEnumerable<ApplicationUser>> GetUsersWithRolesAsync(Expression<Func<ApplicationUser, bool>>? filter = null,
													bool tracked = false, 
                                                    params string[]? includeProperties);


	}
}
