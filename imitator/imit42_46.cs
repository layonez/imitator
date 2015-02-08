using System;
using System.Collections.Generic;
using System.Linq;

namespace imitator
{
    public class Imit42_46
    {
        #region входные и выходные параметры

        /// <summary>
        /// ВХОДНАЯ ИНФОРМАЦИЯ
        /// </summary>
        public class InputData:Imit42.InputData
        {
           
        }

        /// <summary>
        /// ВЫХОДНАЯ ИНФОРМАЦИЯ
        /// </summary>
        public class OutputData : Imit46.OutputData
        {
            public double Ssum { get; set; }

            public OutputData(Imit46.OutputData p)
            {
                this.Sk = p.Sk;
                this.Skj = p.Skj;
                this.Ssps = p.Ssps;
                this.Xkc = p.dXkc;
                this.dXkc = p.dXkc;
                this.dV = p.dV;
            }
        }
        
        #endregion

        public static OutputData[] Exec(InputData data)
        {
            var out42 = Imit42.Exec(data);
            var inp43 = new Imit43.InputData()
            {
                Type = data.Type,
                SubTupe = data.SubType,
                V = data.V,
                H = data.H,
                Angle = data.Angle,
                Ne = out42.Ne,
                NeKrit = out42.NeKrit,
                Nu = out42.Nu,
                Delta = out42.Delta,
                Ageo = out42.A,
                Bgeo = out42.B
            };
            var res1 = Imit43.Exec(inp43);

            var res2 = Imit44_46.Exec(Convert(res1, data, out42));
            var res3 = (from r in res2 select new OutputData(r){Ssum = res1.Ssum}).ToArray();

            return res3;
        }

        private static List<Imit44_46.InputData> Convert(Imit43.OutputData data, InputData inputData, Imit42.OutputData out42)
        {
            var res = new List<Imit44_46.InputData>();
            for (int i = 0; i < 20; i++)
            {
                double k = i;
                var Xkn = 5 + (k)*10/Math.Cos(0.2);
                var Xkc = Xkn + 5/Math.Cos(0.2);
                var Xkk = Xkc + 5/Math.Cos(0.2);

                res.Add(new Imit44_46.InputData()
                {
                    Type = inputData.Type,
                    SubType = inputData.SubType,
                    H = inputData.H,
                    V = inputData.V,
                    angle = inputData.Angle,
                    NeKrit = out42.NeKrit,
                    NuKrit = out42.NuKrit,
                    Xkn = Xkn,
                    Xkc = Xkc,
                    Xkk = Xkk
                });
            }

            return res;
        }
    }
}