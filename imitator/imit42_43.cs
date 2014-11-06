using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace imitator
{
    class imit42_43
    {
        #region входные и выходные параметры, константы

        /// <summary>
        /// ВХОДНАЯ ИНФОРМАЦИЯ
        /// </summary>
        public class InputData:Imit42.InputData
        {
            public int Type;
            public int SubType;
        }
        
        public static List<double[]> GeneralOperator(InputData[] datas)
        {
            var inp43Array = new List< Imit43.InputData>();

            foreach (var data in datas)
            {
              

                var out42=Imit42.GeneralOperator(data);
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
            return Imit43.GeneralOperator(inp43Array.ToArray());
        }

        #endregion
    }
}
