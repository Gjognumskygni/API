using Domain;
using System;
using System.Collections.Generic;

namespace DAL
{
    public interface IParlamentRoleRepository
    {
        public Person GetPerson(int memberOfParlamentId);

        public MemberRole GetRole(int memberOfParlamentId);

        public ICollection<MemberRole> GetRoles(int memberOfParlamentId);

        public Person GetAllParlamentMembersAtDate(DateTime dateTime);
    }
}
