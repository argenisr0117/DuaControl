using System;
using System.Linq;
using System.Threading.Tasks;
using DuaControl.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DuaControl.Web.Data.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly DataContext _dataContext;

        public UserHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User> GetUsersAsync(User user)
        {
            var query = _dataContext.Users
                .Include(x => x.UserRoles)
                .AsQueryable();

            //if (!string.IsNullOrWhiteSpace(request.LastName))
            //    query = query.Where(x => x.LastName.StartsWith(request.LastName));

            //if (request.RoleId.HasValue)
            //    query = query.Where(x => x.UserRoles.Any(r => r.RoleId == request.RoleId));

            //if (request.IsActive.HasValue)
            //    query = query.Where(x => x.IsActive == request.IsActive.Value);

            //string orderBy = request.SortField.ToString();
            //if (QueryHelper.PropertyExists<User>(orderBy))
            //    query = request.SortOrder == SortOrder.Ascending ? query.OrderByProperty(orderBy) : query.OrderByPropertyDescending(orderBy);
            //else
            //    query = query.OrderBy(x => x.LastLoginDate);

            //var result = await PagedList<User>.CreateAsync(query, request.PageIndex, request.PageSize);
            return user;
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException(nameof(userName));

            var query = _dataContext.Users
                .Where(x => x.UserName == userName);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var query = _dataContext.Users
                .Where(x => x.Id == userId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> AddUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();

            return user.Id;
        }

        public async Task UpdateUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _dataContext.Users.Update(user);
            await _dataContext.SaveChangesAsync();
        }
    }
}