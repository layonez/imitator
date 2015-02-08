using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace imitator
{
    class Imit44_46
    {

        #region входные и выходные параметры, константы

        
        /// <summary>
        /// ВХОДНАЯ ИНФОРМАЦИЯ
        /// </summary>
        public class InputData:Imit44.InputData
        {
            public double angle{ get; set; }
        }

        #endregion


        public static Imit46.OutputData[] Exec(List<InputData> data)
        {
            Imit44.OutputData[] Out_44=new Imit44.OutputData[data.Count];
            List<Imit46.InputData> Inp_46 = new List<Imit46.InputData>();

            for (int i = 0; i < data.Count; i++)
            {
                Out_44[i] = Imit44.Exec(data[i]);

                Inp_46.Add(new Imit46.InputData()
                {
                    Angle = data[i].angle,
                    H = data[i].H,
                    V = data[i].V,

                    Xkn = data[i].Xkn,
                    Xkc = data[i].Xkc,
                    Xkk = data[i].Xkk,

                    Hturb = Out_44[i].Hturb,

                    NeXkc = Out_44[i].NeXkc,
                    NeXkk = Out_44[i].NeXkk,
                    NeXkn = Out_44[i].NeXkn,

                    NuXkc = Out_44[i].NuXkc,
                    NuXkk = Out_44[i].NuXkk,
                    NuXkn = Out_44[i].NuXkn,
                    
                    VXkc = Out_44[i].VXkc,
                    VXkk = Out_44[i].VXkk,
                    VXkn = Out_44[i].VXkn,

                    Xkp = Out_44[i].Xkp,
                    Xp = Out_44[i].Xp,
                    dXkc = Out_44[i].dXkc,
                    dXkk = Out_44[i].dXkk,
                    dXkn = Out_44[i].dXkn,

                    Type = data[i].Type,
                    SubType = data[i].SubType
                });
            }

            return Imit46.Exec(Inp_46);
        }
    }
}
