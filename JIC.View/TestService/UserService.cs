using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.TestService
{
    public class UserServiceTest
    {
        public List<vw_KeyValue> GetAllJudges()
        {
            return new List<vw_KeyValue>()
            {
                new vw_KeyValue
                {
                    ID=1,
                    Name="قاضى 1"
                },
                new vw_KeyValue
                {
                    ID=2,
                    Name="قاضى 2"
                },
                new vw_KeyValue
                {
                    ID=3,
                    Name="قاضى 3"
                },
                new vw_KeyValue
                {
                    ID=4,
                    Name="قاضى 4"
                },
                new vw_KeyValue
                {
                    ID=5,
                    Name="قاضى 5"
                },
                new vw_KeyValue
                {
                    ID=6,
                    Name="قاضى 6"
                },
                new vw_KeyValue
                {
                    ID=7,
                    Name="قاضى 7"
                },
                new vw_KeyValue
                {
                    ID=8,
                    Name="قاضى 8"
                },
                new vw_KeyValue
                {
                    ID=9,
                    Name="قاضى 9"
                },
                new vw_KeyValue
                {
                    ID=10,
                    Name="قاضى 10"
                },
                new vw_KeyValue
                {
                    ID=11,
                    Name="قاضى 11"
                },
                new vw_KeyValue
                {
                    ID=12,
                    Name="قاضى 21"
                }
            };
        }

        public List<vw_KeyValue> GetAllCycles()
        {
            return new List<vw_KeyValue>()
            {
                new vw_KeyValue
                {
                    ID=1,
                    Name="الدور 1"
                },
                new vw_KeyValue
                {
                    ID=2,
                    Name="الدور 2"
                },
                new vw_KeyValue
                {
                    ID=3,
                    Name="الدور 3"
                },
                new vw_KeyValue
                {
                    ID=4,
                    Name="الدور 4"
                }
            };
        }
    }
}