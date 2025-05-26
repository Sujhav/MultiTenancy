using Domain.Common.Models;
using Domain.Entities;
using Domain.Users.ValuesObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public sealed class Users : AggregateRoot<UserId, Guid>
    {


        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Email Email { get; private set; }
        public string Passsword { get; private set; }
        public Address Address { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public ICollection<Roles> Roles { get; set; }
        public ICollection<RolesUsers> RolesUsers { get; set; }

        private Users(UserId Id, string firstName, string lastname, Email email, string password, Address address, PhoneNumber phone) : base(Id)
        {

            FirstName = firstName;
            LastName = lastname;
            Email = email;
            Passsword = password;
            Address = address;
            PhoneNumber = phone;
        }

        public static Users CreateUsers(string firstName, string lastname, Email email, string password, Address address, PhoneNumber phone)
        {
            return new Users(UserId.CreateUnique(), firstName, lastname, email, password, Address.GetAddress(address.District, address.City), phone);
        }
        private Users() { }


    }
}
