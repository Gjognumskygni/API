using Gjognumskygni.Domain;
using System;
using System.Collections.Generic;

namespace Gjognumskygni.API.Services
{
    public interface IParlamentRoleRepository
    {
        public Person GetPerson(int memberOfParlamentId);

        public MemberOfParliamentRole GetRole(int memberOfParlamentId);

        public ICollection<MemberOfParliamentRole> GetRoles(int memberOfParlamentId);

        public Person GetAllParlamentMembersAtDate(DateTime dateTime);
    }
}
