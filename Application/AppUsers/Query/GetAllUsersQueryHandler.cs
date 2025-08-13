using Application.Common.Dtos;
using Application.Common.Errors;
using Application.Common.Interfaces.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AppUsers.Query
{
    public class GetAllUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetAllUsersQuey, CustomResult<List<UsersDto>>>
    {

        public async Task<CustomResult<List<UsersDto>>> Handle(GetAllUsersQuey request, CancellationToken cancellationToken)
        {
            var users = await userRepository.GetAllUsers();
            if (users is null)
            {
                var message = "No users were found";
                return CustomResult<List<UsersDto>>.Failure(CustomErrors.RecordNotFound(message));
            }
            return CustomResult<List<UsersDto>>.Success(users);
        }
    }
}
