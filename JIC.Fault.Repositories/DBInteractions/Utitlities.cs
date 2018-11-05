using JIC.Base.Testing;
using JIC.Fault.Repositories;
using System;
using System.Web;

namespace JIC.Repositories.DBInteractions
{
    public class Utitlities
    {
        internal static string CurrentUsername
        {
            get { return (HttpContext.Current == null ? "Unknown" : HttpContext.Current.Session["CurrentUsername"] ?? "Unknown").ToString(); }
        }
        public delegate Base.TestStat DbDoing();
        public delegate int DbDoingReturnID();
        public static JIC.Base.TestStat Transe(JICFaultContext db, DbDoing g)
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
        public static int DBTranse(JICFaultContext db, DbDoingReturnID g)
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
