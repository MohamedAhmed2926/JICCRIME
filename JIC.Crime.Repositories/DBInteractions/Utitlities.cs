using JIC.Base.Testing;
using JIC.Crime.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JIC.Repositories.DBInteractions
{
    public class Utitlities
    {
        internal static string CurrentUsername
        {
            get { return (HttpContext.Current.Session["CurrentUsername"] ?? "Unknown").ToString(); }
        }
        public delegate Base.TestStat DbDoing();
        public delegate int DbDoingReturnID();
        public static JIC.Base.TestStat Transe(JICCrimeContext db, DbDoing g)
        {
            using (var trans = db.Database.BeginTransaction())
            {
                try
                {
                    Base.TestStat t= g.Invoke();
                   
                    trans.Rollback();
                    return t;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    ILog log = LogeHelper.GetInstance;
                    log.LogException(e, "");

                    return Base.TestStat.Fail;
                }
            }
        }
        public static int DBTranse(JICCrimeContext db, DbDoingReturnID g)
        {
            using (var trans = db.Database.BeginTransaction())
            {
                try
                {
                   

                   trans.Rollback();
                    return g.Invoke();
                
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    ILog log = LogeHelper.GetInstance;
                    log.LogException(e, "");

                   return 0;
                }
            }
        }
    }
}
