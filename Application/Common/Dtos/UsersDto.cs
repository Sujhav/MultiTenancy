using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Dtos
{
    public record UsersDto(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        AddressDto Address,
        string PhoneNo);

}
