using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace imitator
{
    internal class Imit42_43
    {
        public static List<Imit43.OutputData> Exec(List<Imit42.InputData> datas)
        {
            var inp43Array = new List<Imit43.InputData>();

            foreach (var data in datas)
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
                inp43Array.Add(inp43);
            }
            return Imit43.Exec(inp43Array);
        }

        public static List<Imit43.OutputData> Exec(Imit42.InputData data)
        {
            var inp43Array = new List<Imit43.InputData>();
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
            inp43Array.Add(inp43);

            return Imit43.Exec(inp43Array);
        }
    }
}
