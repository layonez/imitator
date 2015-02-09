using System;
using System.Collections;
using System.Collections.Generic;

namespace imitator
{
    public class Imit46
    {
        #region входные и выходные параметры, константы

        /// <summary>
        /// ВЫХОДНАЯ ИНФОРМАЦИЯ
        /// </summary>
        public class OutputData
        {
            /// <summary>
            /// Интегральная ЭПР СПС
            /// </summary>
            public double Ssps{ get; set; }
            /// <summary>
            /// Распределение ЭПР СПС по дальности ( дальностный портрет)
            /// </summary>
            public double Sk{ get; set; }

            /// <summary>
            /// Матрица значений ЭПР ( дальностно – скоростной потрет СПС)
            /// </summary>
            public VSpair[] Skj;

            //информация для отладки
            public double dXkc{ get; set; }
            public double Xkc{ get; set; }
            private int _dV = Const.DV ;

            public int dV
            {
                get { return _dV; }
                set { _dV = value; }
            }
        }

        /// <summary>
        /// ВХОДНАЯ ИНФОРМАЦИЯ
        /// </summary>
        public class InputData
        {
            /// <summary>
            /// Текущая высота полета БЦ, м
            /// </summary>
            public double H{ get; set; }
            /// <summary>
            /// Высота турбулизации следа полета БЦ, м
            /// </summary>
            public double Hturb{ get; set; }

            /// <summary>
            /// Скорость полета БЦ, м/с
            /// </summary>
            public double V{ get; set; }
            /// <summary>
            /// Ширина в заданных точках внутри вязкого следа
            /// </summary>
            public double dXkn{ get; set; }
            /// <summary>
            /// Ширина в заданных точках внутри вязкого следа
            /// </summary>
            public double dXkc{ get; set; }
            /// <summary>
            /// Ширина в заданных точках внутри вязкого следа
            /// </summary>
            public double dXkk{ get; set; }

            public double Xkn{ get; set; }
            public double Xkc{ get; set; }
            public double Xkk{ get; set; }

            /// <summary>
            /// Скорость потока в заданных точках внутри вязкого следа
            /// </summary>
            public double VXkk{ get; set; }
            /// <summary>
            /// Скорость потока в заданных точках внутри вязкого следа
            /// </summary>
            public double VXkn{ get; set; }
            /// <summary>
            /// Скорость потока в заданных точках внутри вязкого следа
            /// </summary>
            public double VXkc{ get; set; }

            /// <summary>
            /// Электронная концентрация в заданных точках внутри вязкого следа
            /// </summary>
            public double NeXkc{ get; set; }
            /// <summary>
            /// Электронная концентрация в заданных точках внутри вязкого следа
            /// </summary>
            public double NeXkn{ get; set; }
            /// <summary>
            /// Электронная концентрация в заданных точках внутри вязкого следа
            /// </summary>
            public double NeXkk{ get; set; }

            /// <summary>
            /// Эффективная частота соударений электронов в заданных точках внутри вязкого следа
            /// </summary>
            public double NuXkk{ get; set; }
            /// <summary>
            /// Эффективная частота соударений электронов в заданных точках внутри вязкого следа
            /// </summary>
            public double NuXkc{ get; set; }
            /// <summary>
            /// Эффективная частота соударений электронов в заданных точках внутри вязкого следа
            /// </summary>
            public double NuXkn{ get; set; }

            /// <summary>
            /// Расстояние от горла до точки перехода из ламинарного в турбулентное течение
            /// </summary>
            public double Xp{ get; set; }
            /// <summary>
            /// Длина сверхкритической части вязкого следа 
            /// </summary>
            public double Xkp{ get; set; }
            /// <summary>
            /// Ракурс наблюдения ЭСБЦ
            /// </summary>
            public double Angle{ get; set; }

            public int Type{ get; set; }
            public int SubType{ get; set; }

        }

        /// <summary>
        /// Структура хранящая значения дальностно-скоростного портрета
        /// </summary>
        public class VSpair
        {
            public double V { get; set; }
            public double S { get; set; }
        }

        #endregion

        public static OutputData[] Exec(List<InputData> data)
        {
            OutputData[] output=new OutputData[data.Count];

            for (int i=0;i<data.Count;i++)
            {

                output[i] = ProcessData(data[i]);
                if (i>0)
                {
                    output[i].Ssps = output[i - 1].Ssps + output[i].Sk;
                }
                else
                {
                    output[i].Ssps = output[i].Sk;
                }

                output[i].Xkc = data[i].Xkc;
                output[i].dXkc = data[i].dXkc;

                if (output[i].Skj==null)
                {
                    output[i].Skj=new VSpair[0];
                }
            }

            return output;
        }

        private static OutputData ProcessData(InputData data)
        {
            double s = 0;
            VSpair[] Sk = new VSpair[] { };

            //ЛО1
            if (data.Hturb < data.H)
            {
                //ЛО2 
                if (data.NeXkc < 1.1e9*Math.Pow(Const.LambdaK, -2))
                {
                    //ВП2
                    return new OutputData();
                }
                else
                {
                    return GetEsSforLaminar(data);
                }

            }
            else
            {
                //ЛО 04
                if (data.Xkp > data.Xp)
                {
                    //ЛО 4
                    if (data.Xkc < data.Xkp)
                    {
                        if (data.Xkc < data.Xp)
                        {
                            return GetEsSforLaminar(data);
                        }

                        return GetESSSuperCritical(data);
                    }

                    return GetESSSubCritical(data);
                }
                //ЛО 004
                else if (data.Xkc<0.1*(data.Xkp+data.Xp))
                {
                    return GetEsSforLaminar(data);
                }
                else
                {
                    return GetESSSubCritical(data);
                }
            }
        }

        private static OutputData GetESSSubCritical(InputData data)
        {
            double s;
            VSpair[] Sk;

            //ВП13
            //Расчет  ЭПР докритического следа
            double Sed = GetSed(data);
            //ВП14	 Расчет объема докритического участка СПС
            double dVk = (Math.PI/4)*Const.Dr*data.dXkc*data.dXkc*(1/Math.Cos(data.Angle));
            //ВП15	Расчет ЭПР i-го  докритического участка
            s = Sed*dVk;

            //ЛО6
            if (s < 1e-5)
            {
                return new OutputData();
            }
            if (s > 10)
            {
                s = 10;
            }

            //ВП16	Расчет диапазона изменения скорости
            double dV = Const.Kv*data.VXkc/Math.Sqrt(data.Xkc);
            //Расчет количества элементов разрешения 
            double m = Math.Round(Math.Abs(dV + data.VXkn - data.VXkk)*Math.Cos(data.Angle)/(Const.DV)) + 3;
            if (m > 20)
                m = 20;

            double jMin = (data.VXkk)*Math.Cos(data.Angle)/
                          (Const.DV);
            jMin = Math.Round(jMin);

            Sk = GetSk(jMin, m, s, false);

            return new OutputData() {Skj = Sk, Sk = s};
        }

        private static OutputData GetESSSuperCritical(InputData data)
        {
            double s;
            VSpair[] Sk;
            double Sksi;
            double R;
            double Lksi;
            //ВП6
            //Вычисление среднего квадратичного отклонения и продольного размера неоднородностей
            Sksi = Const.K1*data.dXkc;
            R = (1.9726e-1) + (2.2647e-4)*Math.Pow(data.dXkc/Const.Dm, 1.0833);
            Lksi = Const.Dm*R;

            double Sed = 0;
            //ЛО5
            //Проверка условия шероховатости поверхности СПС
            if (Sksi*Sksi/(Lksi*Lksi) < 0.1)
            {
                //ВП7
                //Определение пространственного спектра 
                double Fksi = GetFksi(Sksi, Lksi);
                //ВП8
                //Определение ЭПР сверхкритического  турбулентного участка следа единичной длины 
                Sed = Const.Ksv2*Math.Pow(Math.PI*2/Const.LambdaK, 4)*Math.Sin(data.Angle)*
                      Math.Sin(data.Angle)*Fksi;
            }
            else
            {
                //ВП9
                double F = GetF(data, Lksi, Sksi);
                //ВП10
                //Определение ЭПР сверхкритического турбулентного участка следа единичной длины  значительной шероховатости
                Sed = Const.Ksv3*data.dXkc*Lksi*Lksi*F/
                      (2*Sksi*Sksi);
            }
            //Расчет  ЭПР сверхкритического следа
            s = Sed*Const.Dr/Math.Cos(data.Angle);
            //ЛО5
            if (s < 1e-5)
            {
                s = 0;
                return new OutputData();
            }
            if (s > 10)
            {
                s = 10;
            }

            //ВП11
            //Расчет количества ячеек в портрете
            double dV = Const.Kv*data.VXkc/Math.Sqrt(data.Xkc);

            double m = Math.Round((data.VXkn + dV - data.VXkk)*Math.Cos(data.Angle)/(Const.DV)) + 1;
            if (m > 20)
            {
                m = 20;
            }

            //Вычисление граничных значений J
            double jMin = Math.Round(1 + (data.VXkk*Math.Cos(data.Angle))/(Const.DV));
            Sk = GetSk(jMin, m, s, false);

            return new OutputData() {Skj = Sk, Sk = s};
        }

        /// <summary>
        /// ВП3-ВП5
        /// Расчет ЭПР ламинарного участка следа
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static OutputData GetEsSforLaminar(InputData data)
        {
            double s;
            VSpair[] Sk;
            double dr = Const.Dr/Math.Cos(data.Angle);
            s = GetSigma(data, dr);

            //ЛО3
            if (s < 10e-5)
            {
                return new OutputData();
            }
            if (s > 10)
            {
                s = 10;
            }
            //ВП4
            //Расчет количества ячеек в портрете
            double m = ((data.VXkn - data.VXkk)*Math.Cos(data.Angle))/Const.DV;
            m = Math.Round(m);
            if (m > 20)
                m = 20;
            //Расчет диапазона изменения 
            double jMin = Math.Round(1 + (data.VXkk*Math.Cos(data.Angle))/Const.DV);
            
            //ВП5
            Sk = GetSk(jMin, m, s,true);

            return new OutputData() {Skj = Sk, Sk = s};
        }

        private static double Sum(VSpair[] sk)
        {
            double sum = 0;
            for (int i = 0; i < sk.Length; i++)
            {
                sum += sk[i].S;
            }
            return sum;
        }

        private static double GetSed(InputData data)
        {
            double L0 = Const.K3*data.dXkc;
            double F = Const.K3 * Math.Pow(data.Xkc * Const.Cx * Const.Sm, 0.3333) *
                    (1 - (2 / 3) * Const.K3 * Const.K3 * Math.Pow(data.Xkc * Const.Cx * Const.Sm, 0.6666) +
                     0.2 * Math.Pow(Const.K3, 4) * Math.Pow(data.Xkc * Const.Cx * Const.Sm, 1.3333));
            double Sed = (10e-19)*
                Math.Pow(2*Math.PI/Const.LambdaK, 2)*data.NeXkc*data.NeXkc*Math.Pow(L0, 3)*F/
                      Math.Pow(1 + 4 * (Math.Pow(2 * Math.PI / Const.LambdaK, 2)) * L0 * L0, 1.8333);
            return Sed;
        }

        private static double GetF(InputData data, double Lksi, double Sksi)
        {
            double F = 0;
            const double dA = Math.PI/200;
            for (int i = 1; i < 101; i++)
            {
                double a = i*dA;
                double D = 2*Lksi*
                        Math.Sqrt(1 - Math.Sin(a)*Math.Sin(a)*Math.Sin(data.Angle)*Math.Sin(data.Angle))/
                        (Sksi*Math.Sin(a)*Math.Sin(data.Angle));
                F += Math.Exp(-D)*dA/
                     (Math.Pow(Math.Sin(a), 4)*Math.Pow(Math.Sin(data.Angle), 4));
            }
            return F;
        }

        private static double GetSigma(InputData data, double dr)
        {
            double s1 = 0.2*data.dXkc*dr*dr/
                     Const.LambdaK;
            double s2 = Math.Pow(Math.Sin(data.Angle),4);
                //Math.Pow(Math.Sin(Math.PI * 2 * dr * Math.Cos(data.Angle) /
                //     Cnst.LambdaK), 2) /
                //     Math.Pow(Math.PI * 2 * dr * Math.Cos(data.Angle) /
                //     Cnst.LambdaK, 2);
            double s = s1*s2;

            if (s>0.1)
            {
                s = 0.1;
            }
            return s;
        }

        private static VSpair[] GetSk(double jMin, double m, double s, bool isForLaminar)
        {
            double dim = 1;

            VSpair[] S = new VSpair[(int)m];
            
            int i = 0;

            for (double j = jMin; i < m; j = j + dim,i++)
            {
                double V = j * Const.DV;
                double sk;
                //Разный расчет для ламинарного и докритического
                if (isForLaminar)
                {
                    sk = (s / m) * (0.3 + Math.Abs(Math.Cos(
                   Math.PI * (j - jMin) / m))
                   );  
                }
                else
                {
                    sk = (s / m) * (0.5 + Math.Abs(Math.Sin(
                    Math.PI * (j - jMin) / m + 0.5))
                 );  
                }
                S[i] = new VSpair {S = sk, V = V};
            }
            return S;
        }
       
        private static double GetFksi(double Sksi, double Lksi)
        {
            return (Sksi*Sksi*Lksi*Lksi/(Math.PI*2))*
                   Math.Exp(-Math.PI*Math.PI*Lksi*Lksi/(Const.LambdaK*Const.LambdaK));
        }
    }
}
