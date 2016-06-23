using System;
using System.Linq;
using System.Web.Security;
using ChampionshipWeb.Models;

namespace ChampionshipWeb.Providers
{
    public class MyRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string login)
        {
            string[] role_ = new string[] { };
            using (championshipEntities _db = new championshipEntities())
            {
                try
                {
                    // Получаем пользователя
                    user user_ = (from u in _db.users
                                 where u.login == login
                                 select u).FirstOrDefault();
                    if (user_ != null)
                    {
                        // получаем роль
                        role userRole = _db.roles.Find(user_.role);

                        if (userRole != null)
                        {
                            role_ = new string[] { userRole.name };
                        }
                    }
                }
                catch
                {
                    role_ = new string[] { };
                }
            }
            return role_;
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            bool outputResult = false;
            // Находим пользователя
            using (championshipEntities _db = new championshipEntities())
            {
                try
                {
                    // Получаем пользователя
                    user user_ = (from u in _db.users
                                 where u.login == username
                                 select u).FirstOrDefault();
                    if (user_ != null)
                    {
                        // получаем роль
                        role userRole = _db.roles.Find(user_.role);

                        //сравниваем
                        if (userRole != null && userRole.name == roleName)
                        {
                            outputResult = true;
                        }
                    }
                }
                catch
                {
                    outputResult = false;
                }
            }
            return outputResult;
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
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

        public override string[] GetUsersInRole(string roleName)
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