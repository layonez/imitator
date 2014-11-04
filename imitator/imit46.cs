using System;
using System.Collections;

namespace imitator
{
    class Imit46
    {
        #region входные и выходные параметры, константы

        /// <summary>
        /// ВЫХОДНАЯ ИНФОРМАЦИЯ
        /// </summary>
        public class OutputData
        {
            /// <summary>
            /// Интегральная ЭПР СПС;
            /// </summary>
            public double Ssps;
            /// <summary>
            /// Распределение ЭПР СПС по дальности ( дальностный портрет);
            /// </summary>
            public double Sk;
            /// <summary>
            /// Матрица значений ЭПР ( дальностно – скоростной потрет СПС);
            /// </summary>
            public VSpair[] Skj;
        }

        /// <summary>
        /// ВХОДНАЯ ИНФОРМАЦИЯ
        /// </summary>
        public class InputData
        {
            /// <summary>
            /// Текущая высота полета БЦ, м
            /// </summary>
            public double H;
            /// <summary>
            /// Высота турбулизации следа полета БЦ, м
            /// </summary>
            public double Hturb;

            /// <summary>
            /// Скорость полета БЦ, м/с
            /// </summary>
            public double V;
            /// <summary>
            /// Ширина в заданных точках внутри вязкого следа
            /// </summary>
            public double dXkn;
            /// <summary>
            /// Ширина в заданных точках внутри вязкого следа
            /// </summary>
            public double dXkc;
            /// <summary>
            /// Ширина в заданных точках внутри вязкого следа
            /// </summary>
            public double dXkk;

            public double Xkn;
            public double Xkc;
            public double Xkk;

            /// <summary>
            /// Скорость потока в заданных точках внутри вязкого следа
            /// </summary>
            public double VXkk;
            /// <summary>
            /// Скорость потока в заданных точках внутри вязкого следа
            /// </summary>
            public double VXkn;
            /// <summary>
            /// Скорость потока в заданных точках внутри вязкого следа
            /// </summary>
            public double VXkc;

            /// <summary>
            /// Электронная концентрация в заданных точках внутри вязкого следа
            /// </summary>
            public double NeXkc;
            /// <summary>
            /// Электронная концентрация в заданных точках внутри вязкого следа
            /// </summary>
            public double NeXkn;
            /// <summary>
            /// Электронная концентрация в заданных точках внутри вязкого следа
            /// </summary>
            public double NeXkk;

            /// <summary>
            /// Эффективная частота соударений электронов в заданных точках внутри вязкого следа
            /// </summary>
            public double NuXkk;
            /// <summary>
            /// Эффективная частота соударений электронов в заданных точках внутри вязкого следа
            /// </summary>
            public double NuXkc;
            /// <summary>
            /// Эффективная частота соударений электронов в заданных точках внутри вязкого следа
            /// </summary>
            public double NuXkn;

            /// <summary>
            /// Расстояние от горла до точки перехода из ламинарного в турбулентное течение
            /// </summary>
            public double Xp;
            /// <summary>
            /// Длина сверхкритической части вязкого следа 
            /// </summary>
            public double Xkp;
            /// <summary>
            /// Ракурс наблюдения ЭСБЦ
            /// </summary>
            public double Angle;


        }

        /// <summary>
        /// Константы
        /// </summary>
        public class Cnst
        {
            /// <summary>
            /// Разрешающая способность по дальности РЛС, м
            /// </summary>
            public const double dr = 5;
            /// <summary>
            /// Разрешающая способность по скорости  РЛС
            /// </summary>
            public const double dV = 10;
            /// <summary>
            /// Размерность дальностно-скоростного портрета по дальности
            /// </summary>
            public const double k0 = 10;
            /// <summary>
            /// Коэффициенты калибровки ЭПР сверхкритического следа
            /// </summary>
            public const double Ksv2 = 1;
            /// <summary>
            /// Коэффициенты калибровки ЭПР сверхкритического следа
            /// </summary>
            public const double Ksv3 = 1;
            /// <summary>
            /// Длина волны  излучения РЛС
            /// </summary>
            public static double LambdaK = 0.1;
            /// <summary>
            /// Диаметр Миделя
            /// </summary>
            public const double dm = 0.8;
            /// <summary>
            /// Калибровочный коэфф.
            /// </summary>
            public const double K1 = 0.5;
            /// <summary>
            /// Калибровочный коэфф.
            /// </summary>
            public const double K2 = 0.5;
            /// <summary>
            /// Калибровочный коэфф.
            /// </summary>
            public const double K3 = 0.5;
            /// <summary>
            /// Калибровочный коэфф.
            /// </summary>
            public const double K4 = 0.5;
            /// <summary>
            /// Коэффициент Калибровки D-V портрета 
            /// </summary>
            public const double Kpv = 0.5;
            /// <summary>
            /// Коэффициент Калибровки D-V портрета 
            /// </summary>
            public const double Kv = 0.5;
            
            
            /// <summary>
            /// Частота радиолокационного сигнала
            /// </summary>
            public const double f0 = 3e9;
            /// <summary>
            /// Аэродинамический коэффициент лобового сопротивления БЦ
            /// </summary>
            public const double Cx = 0.01;
            /// <summary>
            /// площадь миделевого сечения БЦ 
            /// </summary>
            public const double Sm = 1;
            /// <summary>
            /// скорость звука,  м/с
            /// </summary>
            public const double c = 340.29;
            /// <summary>
            /// средний радиус Земли, м
            /// </summary>
            public const double Rz = 6371e3;
            /// <summary>
            /// плотность
            /// </summary>
            public const double Ro0 = 1.125;

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

        public static OutputData[] GeneralOperator(InputData[] data)
        {
            OutputData[] output=new OutputData[data.Length];

            for (int i=0;i<data.Length;i++)
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
                if (data.NeXkc < 1.1e9*Math.Pow(Cnst.LambdaK, -2))
                {
                    //ВП2
                    return new OutputData();
                }
                else
                {
                    return GetESSforLaminar(data);
                }

            }
            else
            {
                //ЛО 04
                if (data.Xkp > data.Xp)
                {
                    //ЛО 4
                    if (data.dXkc < data.Xkp)
                    {
                        if (data.dXkc < data.Xp)
                        {
                            return GetESSforLaminar(data);
                        }

                        return GetESSSuperCritical(data);
                    }

                    if (data.Xkc < data.Xp)
                    {
                        return GetESSSubCritical(data);
                    }
                }
                //ЛО 004
                else if (data.Xkc<0.5*(data.Xkp+data.Xp))
                {
                    return GetESSforLaminar(data);
                }
                else
                {
                    return GetESSSubCritical(data);
                }
            }

            return new OutputData() { Skj = Sk, Sk = s };
        }

        private static OutputData GetESSSubCritical(InputData data)
        {
            double s;
            VSpair[] Sk;

            //ВП13
            //Расчет  ЭПР докритического следа
            double Sed = GetSed(data);
            //ВП14	 Расчет объема докритического участка СПС
            double dVk = (Math.PI/4)*Cnst.dr*data.dXkc*data.dXkc*(1/Math.Cos(data.Angle));
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
            double dV = Cnst.Kv*data.VXkc/Math.Sqrt(data.Xkc);
            //Расчет количества элементов разрешения 
            double m = Math.Round((dV + data.VXkn - data.VXkk)*Math.Cos(data.Angle)/(Cnst.dV));
            if (m > 20)
                m = 20;

            double jMin = (data.VXkk)*Math.Cos(data.Angle)/
                          (Cnst.dV);
            jMin = Math.Round(jMin);

            Sk = GetSk(jMin + m, jMin, m, s);

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
            Sksi = Cnst.K1*data.dXkc;
            R = (1.9726e-1) + (2.2647e-4)*Math.Pow(data.dXkc/Cnst.dm, 1.0833);
            Lksi = Cnst.dm*R;

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
                Sed = Cnst.Ksv2*Math.Pow(Math.PI*2/Cnst.LambdaK, 4)*Math.Sin(data.Angle)*
                      Math.Sin(data.Angle)*Fksi;
            }
            else
            {
                //ВП9
                double F = GetF(data, Lksi, Sksi);
                //ВП10
                //Определение ЭПР сверхкритического турбулентного участка следа единичной длины  значительной шероховатости
                Sed = Cnst.Ksv3*data.dXkc*Lksi*Lksi*F/
                      (2*Sksi*Sksi);
            }
            //Расчет  ЭПР сверхкритического следа
            s = Sed*Cnst.dr/Math.Cos(data.Angle);
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
            double dV = Cnst.Kv*data.VXkc/Math.Sqrt(data.Xkc);

            double m = Math.Round((data.VXkn + dV - data.VXkk)*Math.Cos(data.Angle)/(Cnst.dV));
            if (m > 20)
            {
                m = 20;
            }

            //Вычисление граничных значений J
            double jMin = Math.Round(1 + (data.VXkk*Math.Cos(data.Angle))/(Cnst.dV));
            double jMax = jMin + m;
            Sk = GetSk1(jMax, jMin, m, s);

            return new OutputData() {Skj = Sk, Sk = s};
        }

        /// <summary>
        /// ВП3-ВП5
        /// Расчет ЭПР ламинарного участка следа
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static OutputData GetESSforLaminar(InputData data)
        {
            double s;
            VSpair[] Sk;
            double dr = Cnst.dr/Math.Cos(data.Angle);
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
            double m1 = 1 + ((data.VXkn - data.VXkk)*Math.Cos(data.Angle))/Cnst.dV;
            m1 = Math.Round(m1);
            if (m1 > 20)
                m1 = 20;
            //Расчет диапазона изменения 
            double jMin1 = Math.Round(1 + (data.VXkk*Math.Cos(data.Angle))/Cnst.dV);
            double jMax1 = jMin1 + m1;

            //ВП5
            Sk = GetSk(jMax1, jMin1, m1, s);

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
            double L0 = Cnst.K3*data.dXkc;
            double F = Cnst.K3*Math.Pow(data.Xkc*Cnst.Cx*Cnst.Sm, 0.3333)*
                    (1 - (2/3)*Cnst.K3*Cnst.K3*Math.Pow(data.Xkc*Cnst.Cx*Cnst.Sm, 0.6666) +
                     0.2*Math.Pow(Cnst.K3, 4)*Math.Pow(data.Xkc*Cnst.Cx*Cnst.Sm, 1.3333));
            double Sed = (10e-19)*
                5.64*Math.Pow(2*Math.PI/Cnst.LambdaK, 2)*data.NeXkc*data.NeXkc*Math.Pow(L0, 3)*F/
                      Math.Pow(1 + 4 * (Math.Pow(2 * Math.PI / Cnst.LambdaK, 2)) * L0 * L0, 1.8333);
            return Sed;
        }

        private static double GetF(InputData data, double Lksi, double Sksi)
        {
            double F = 0;
            double dA = Math.PI/200;
            for (int i = 1; i < 101; i++)
            {
                double a = i*dA;
                double D = Lksi*
                        Math.Sqrt(1 - Math.Sin(a)*Math.Sin(a)*Math.Sin(data.Angle)*Math.Sin(data.Angle))/
                        (2*Sksi*Math.Sin(a)*Math.Sin(data.Angle));
                F += Math.Exp(-D)*dA/
                     (Math.Pow(Math.Sin(a), 4)*Math.Pow(Math.Sin(data.Angle), 4));
            }
            return F;
        }

        private static double GetSigma(InputData data, double dr)
        {
            double s1 = Math.PI*data.dXkc*dr*dr/
                     Cnst.LambdaK;
            double s2 = Math.Pow(Math.Sin(data.Angle),4);
                //Math.Pow(Math.Sin(Math.PI * 2 * dr * Math.Cos(data.Angle) /
                //     Cnst.LambdaK), 2) /
                //     Math.Pow(Math.PI * 2 * dr * Math.Cos(data.Angle) /
                //     Cnst.LambdaK, 2);
            double s = s1*s2;
            return s;
        }

        private static VSpair[] GetSk(double jMax, double jMin, double m, double s)
        {
            double dim = 1;

            VSpair[] S = new VSpair[(int)m];
            
            int i = 0;

            for (double j = jMin; i < m; j = j + dim,i++)
            {
               double V = j * Cnst.dV;
               //double sk = (s/m)*(1 - Cnst.Kpv*(1/m)*(j - (jMin + m/2)));
               double sk = (s / m) * (0.3 + Math.Sin(
                   Math.PI * (j - jMin) / m)
                   );  
                S[i] = new VSpair {S = sk, V = V};
            }
            return S;
        }
       
        private static VSpair[] GetSk1(double jMax, double jMin, double m, double s)
        {
            double dim = 1;

            VSpair[] S = new VSpair[(int)m];

            int i = 0;

            for (double j = jMin; i < m; j = j + dim, i++)
            {
                //double sk = (s / m) * (1 - 0.5 * (1 / m) * (j - (jMin + m / 2)));

                double sk = (s / m) * (0.3+Math.Sin(
                    Math.PI*(j - jMin)/m)
                    );  

                double V = j * Cnst.dV;
                S[i] = new VSpair { S = sk, V = V };
            }
            return S;
        }

        private static double GetFksi(double Sksi, double Lksi)
        {
            return (Sksi*Sksi*Lksi*Lksi/(Math.PI*2))*
                   Math.Exp(-Math.PI*Math.PI*Lksi*Lksi/(Cnst.LambdaK*Cnst.LambdaK));
        }
    }
}
