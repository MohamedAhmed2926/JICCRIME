using JIC.Utilities.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Utilities
{
    public static class Extensions
    {
        public static bool CheckUserAlreadyLoggedIn(this List<SessionUser> lstSessionUser, int UserID)
        {

            return lstSessionUser.Count(u => u.UserID == UserID) != 0;
        }

        public static void RemoveUserSession(this List<SessionUser> lstSessionUser, string SessionID)
        {
            if (lstSessionUser != null)
                if (lstSessionUser.Find(s=>s.SessionID==SessionID) != null)
                    lstSessionUser.RemoveAll(u => u.SessionID == SessionID);
        }

        public static void RemoveUserSession(this List<SessionUser> lstSessionUser, int UserID)
        {
            lstSessionUser.RemoveAll(u => u.UserID == UserID);
        }
    }
}
