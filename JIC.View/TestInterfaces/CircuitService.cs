using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIC.Base;
using JIC.Base.Views;

namespace JIC.Crime.View.TestInterfaces
{
    public class CircuitService : ICircuitService
    {
        public SaveCircuitStatus AddCircuit(vw_CircuitData circuitData, out int CircuitID)
        {
            throw new NotImplementedException();
        }

        public DeleteCircuitStatus DeleteCircuit(int ID)
        {
            throw new NotImplementedException();
        }

        public SaveCircuitStatus EditCircuit(vw_CircuitData circuitData)
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetCircuit()
        {
            return new List<vw_KeyValue>()
            {
                new vw_KeyValue
                {
                    ID=1,
                    Name="الدائرة الأولى"
                },
                new vw_KeyValue
                {
                    ID=2,
                    Name="الدائرة الثانية"
                }
            };
        }

        public List<vw_KeyValue> GetCircuitsBySecretairyID(int UserID)
        {
            List<vw_KeyValue> list = new List<vw_KeyValue>();
            vw_KeyValue vw_Key = new vw_KeyValue()
            {
                ID = 1,
                Name = "sdd",
            };
            vw_KeyValue vw_Key2 = new vw_KeyValue()
            {
                ID = 2,
                Name = "dsaf",
            };
            list.Add(vw_Key);
            list.Add(vw_Key2);
            return list;
        }

        public List<vw_KeyValue> GetCircuits()
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetCircuitsByCourtID(int courtID)
        {
            return new List<vw_KeyValue>()
            {
                new vw_KeyValue
                {
                    ID=1,
                    Name="الدائرة الأولى"
                },
                new vw_KeyValue
                {
                    ID=2,
                    Name="الدائرة الثانية"
                }
            };
        }

        public List<vw_CircuitsGrid> GetCircuitsFullData()
        {
            throw new NotImplementedException();
        }

        public List<vw_CircuitsGrid> GetCircuitsFullData(int CourtID)
        {
            throw new NotImplementedException();
        }



        vw_CircuitsGrid ICircuitService.GetCircuitsFullDataByID(int CircuitID)
        {
            throw new NotImplementedException();
        }

        public List<vw_CircuitsGrid> GetCircuitsFullDataByID(int CircuitID)
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetCircuits(int CourtID)
        {
            return new List<vw_KeyValue>()
           {
               new vw_KeyValue()
               {
                   ID=1,
                   Name ="الدائرة الأولى"
               },
               new vw_KeyValue()
               {
                   ID=2,
                   Name ="الدائرة الثانية"
               },
               new vw_KeyValue()
               {
                   ID=3,
                   Name ="الدائرة الثالثة"
               },
           };
        }

        public List<vw_KeyValueLongID> GetCircuitSessionDates(int CircuitID)
        {
            return new List<vw_KeyValueLongID>()
            {
                new vw_KeyValueLongID()
                {
                    Name = new DateTime(2018,1,15).ToShortDateString(),
                    ID =  new DateTime(2018,1,15).Ticks
                }
            };
        }

        public List<vw_KeyValue> GetCircuitsBySecretairyID(int SecretairyID, int CourtID)
        {
            throw new NotImplementedException();
        }

        List<DateTime> GetSessions(int CircuitID)
        {
            return (new List<DateTime>()
            {
                DateTime.Now,
                DateTime.Now
            }
            );
        }

       

        public List<vw_KeyValue> GetCircuitsBySecretairyID(int SecretairyID, int? CourtID)
        {
            List<vw_KeyValue> list = new List<vw_KeyValue>();
            vw_KeyValue vw_KeyValue = new vw_KeyValue();
            vw_KeyValue.ID = 1;
            vw_KeyValue.Name = "asd";
            vw_KeyValue vw_KeyValue1 = new vw_KeyValue();
            vw_KeyValue1.ID = 2;
            vw_KeyValue1.Name = "fjdsaf";
            vw_KeyValue vw_KeyValue2 = new vw_KeyValue();
            vw_KeyValue1.ID = 3;
            vw_KeyValue1.Name = "asdSF";
            list.Add(vw_KeyValue);
            list.Add(vw_KeyValue1);
            list.Add(vw_KeyValue2);
            return list;
        }

        List<DateTime> ICircuitService.GetSessions(int CircuitID)
        {
            throw new NotImplementedException();
        }

        public List<vw_KeyValue> GetCircuitsByCrime(int CrimeID, int CourtID)
        {
            throw new NotImplementedException();
        }
    }
}