using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using UyumsoftProje2.Models;

namespace UyumsoftProje2.Security
{
    public class MyRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            Entities model = new Entities();
            UYE uye = model.UYE.FirstOrDefault(x => x.ad == username);
            
            string rol = uye.kullaniciRolü;
            char[] chars = rol.ToCharArray();
            string[] roller = new string[chars.Length];
            for(int i=0;i<roller.Length;i++)
            {
                roller[i] = chars[i].ToString();
            }
            return roller;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}