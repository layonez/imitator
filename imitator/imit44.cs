using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace imitator
{
    class Imit44
    {
        #region входные и выходные параметры, константы

        /// <summary>
        /// ВХОДНАЯ ИНФОРМАЦИЯ
        /// </summary>
        public class InputData
        {
            /// <summary>
            /// Электронная концентрация 
            /// в критической точке цели, см^-3
            /// </summary>
            public double NeKrit;
            /// <summary>
            /// Эффективная частота соударений электронов  
            /// в критической точке цели , см^-3
            /// </summary>
            public double NuKrit;
            /// <summary>
            ///Координаты точек начала, центра и окончания 
            ///k-го элемента разрешения по оси следа относительно горла СПС, м
            /// </summary>
            public double Xkn;
            /// <summary>
            ///Координаты точек начала, центра и окончания 
            ///k-го элемента разрешения по оси следа относительно горла СПС, м
            /// </summary>
            public double Xkc;
            /// <summary>
            ///Координаты точек начала, центра и окончания 
            ///k-го элемента разрешения по оси следа относительно горла СПС, м
            /// </summary>
            public double Xkk;
            /// <summary>
            /// Текущая высота полета БЦ, м
            /// </summary>
            public double H;
            /// <summary>
            /// Скорость полета БЦ, м/с
            /// </summary>
            public double V;
        }

        /// <summary>
        /// ВЫХОДНАЯ ИНФОРМАЦИЯ
        /// </summary>
        public class OutputData
        {
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
            /// Высота турбулизации следа
            /// </summary>
            public double Hturb;

        }

        /// <summary>
        /// Константы
        /// </summary>
        public class Cnst
        {
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

        #endregion

        public static OutputData GeneralOperator(InputData data)
        {
            //определим плотность
            double Ro = Imit42.GetDensity(data.H);

            OutputData outData= new OutputData();

            //ЛО1
            //if (data.H >= 80000)
            //    return outData;

            //ВП1
            //Расчет высоты начала турбулизации вязкого следа 
            double Rm = Math.Pow(Cnst.Sm/Math.PI, 0.5);
            double RoTurb = 0.3/(Rm*data.V);
            double Hturb = GetHturb(RoTurb);

            //Расчет расстояния от горла до точки перехода из ламинарного в турбулентное течение 
            double Xp = GetXp(Rm,data.V,Ro);

            //ВП01
            //Расчет  параметров электронной концентрации
            double NeR = 0.1*data.NeKrit;
            double NuR = 0.1*data.NuKrit;
            double NeKp = (1.24e-8)*Cnst.f0*Cnst.f0;

            //Расчет аэродинамической силы, действующей на БЦ при полете в атмосфере
            double R = 0.5*Cnst.Cx*Cnst.Sm*Ro*data.V*data.V;

            //ЛО2
            //Проверка условия о прохождении высоты начала турбулизации вязкого следа
            if (data.H>Hturb)
            {
                outData = GetParams(data, NeR, NuR, R,NeKp,Rm);
            }
            else if (data.Xkk < Xp)
            {
                outData = GetParams(data, NeR, NuR, R, NeKp, Rm);
            }
            else
            {
                outData = GetParamsInTurb(data, Rm, R, NeR, NuR, NeKp);
            }

            outData.Xp = Xp;
            outData.Hturb = Hturb;

            return outData;

        }

        private static double GetHturb(double roTurb)
        {
            double mu;
            for (int i = 0; i < Imit42.Cnst.T2.GetLength(0)-1; i++)
            {
                if (roTurb >= Imit42.Cnst.T2[i, 1] && roTurb <= Imit42.Cnst.T2[i + 1, 1])
                {
                    mu = Math.Log(Imit42.Cnst.T2[i + 1, 1] / Imit42.Cnst.T2[i, 1]) / (-Imit42.Cnst.T2[i + 1, 0] + Imit42.Cnst.T2[i, 0]);
                    return Imit42.Cnst.T2[i, 0] - (Math.Log(roTurb) - Math.Log(Imit42.Cnst.T2[i, 1])) / mu;
                }
            }
            mu = Math.Log(Imit42.Cnst.T2[27, 1] / Imit42.Cnst.T2[26, 1]) / (-Imit42.Cnst.T2[27, 0] + Imit42.Cnst.T2[26, 0]);
            return Imit42.Cnst.T2[26, 0] - (Math.Log(roTurb) - Math.Log(Imit42.Cnst.T2[26, 1])) / mu;
        }

        private static double GetXp(double Rm, double V, double Ro)
        {
            double Re = (5.3e4) * Rm * V * Ro;
            return Rm*(256.5 - 45*(Math.Log10(Re) - 1));
        }

        #region Приватные методы 

        private static double GetSpeed(double V, double Xkn)
        {
            double powArg = -0.6666;
            return V *Math.Pow((Xkn / Math.Sqrt(Cnst.Cx * Cnst.Sm)+1),powArg);
        }
        
        /// <summary>
        /// Расчет аэродинамической силы,ширины вязкого следа,
        /// скорости потока в заданных точках,
        /// эффективной частоты соударений электронов,
        /// электронной концентрации в заданных точках
        /// </summary>
        private static OutputData GetParams(InputData data, double NeR, double NuR, double R,double NeKp, double Rm)
        {
            //Расчет ширины вязкого следа в заданных точках внутри ламинарного вязкого следа 
            double DZkn = GetDzeta(data.Xkn, R);
            double DZkc = GetDzeta(data.Xkc, R);
            double DZkk = GetDzeta(data.Xkk, R);

            double Dkn = DZkn*Math.Sqrt(Cnst.Cx*Cnst.Sm);
            double Dkc = DZkc*Math.Sqrt(Cnst.Cx*Cnst.Sm);
            double Dkk = DZkk*Math.Sqrt(Cnst.Cx*Cnst.Sm);

            //Расчет скорости потока в заданных точках ламинарного вязкого следа
            double Vkn = data.V*(1/(DZkn*DZkn));
            double Vkc = data.V*(1/(DZkc*DZkc));
            double Vkk = data.V*(1/(DZkk*DZkk));

            //Расчет электронной концентрации в заданных точках ламинарного вязкого следа 
            double NeXkn = GetElectrKonc(NeR, data.Xkn);
            double NeXkc = GetElectrKonc(NeR, data.Xkc);
            double NeXkk = GetElectrKonc(NeR, data.Xkk);

            //Расчет эффективной частоты соударений электронов в заданных точках внутри турбулентного
            //вязкого следа
            double NuXkn = GetСollisionFreq(NuR, data.Xkn);
            double NuXkc = GetСollisionFreq(NuR, data.Xkc);
            double NuXkk = GetСollisionFreq(NuR, data.Xkk);

            //Расчет длины сверхкритической части ламинарного вязкого следа
            double Xkp = GetTrackLength(NeR, Rm, NeKp);

            return new OutputData()
            {
                NuXkc = NuXkc,
                NuXkn = NuXkn,
                NuXkk = NuXkk,
                NeXkc = NeXkc,
                NeXkk = NeXkk,
                NeXkn = NeXkn,
                VXkc = Vkc,
                VXkk = Vkk,
                VXkn = Vkn,
                dXkc = Dkc,
                dXkk = Dkk,
                dXkn = Dkn,
                Xkp = Xkp
            };
        }
        
        /// <summary>
        /// Расчет аэродинамической силы,ширины вязкого следа,
        /// скорости потока,
        /// эффективной частоты соударений электронов,
        /// электронной концентрации в заданных точках
        /// </summary>
        private static OutputData GetParamsInTurb(InputData data, double Rm, double R, double NeR, double NuR, double NeKp)
        {
            double Xkp;
            double alfa = 0.4;


            //Расчет ширины вязкого следа в заданных точках внутри турбулентного вязкого следа 
            double DZkn = GetDzetaInTurb(data.Xkn, R, alfa);
            double DZkc = GetDzetaInTurb(data.Xkc, R, alfa);
            double DZkk = GetDzetaInTurb(data.Xkk, R, alfa);

            double Dkn = DZkn * Math.Sqrt(Cnst.Cx * Cnst.Sm);
            double Dkc = DZkc * Math.Sqrt(Cnst.Cx * Cnst.Sm);
            double Dkk = DZkk * Math.Sqrt(Cnst.Cx * Cnst.Sm);

            //Расчет скорости потока в заданных точках ламинарного вязкого следа
            double Vkn = GetSpeed(data.V, data.Xkn);
            double Vkc = GetSpeed(data.V, data.Xkc);
            double Vkk = GetSpeed(data.V, data.Xkk);

            //Расчет электронной концентрации в заданных точках внутри турбулентного вязкого следа
            double NeXkn = GetElectrKoncInTurb(NeR, data.Xkn);
            double NeXkc = GetElectrKoncInTurb(NeR, data.Xkc);
            double NeXkk = GetElectrKoncInTurb(NeR, data.Xkk);

            //Расчет эффективной частоты соударений электронов в 
            //заданных точках внутри турбулентного вязкого следа
            double NuXkn = GetСollisionFreqInTurb(NuR, data.Xkn);
            double NuXkc = GetСollisionFreqInTurb(NuR, data.Xkc);
            double NuXkk = GetСollisionFreqInTurb(NuR, data.Xkk);

            //Расчет длины сверхкритической части турбулентного  вязкого следа 
            Xkp = GetTrackLengthInTurb(NeR, Rm, NeKp);

            return new OutputData()
            {
                NuXkc = NuXkc,
                NuXkn = NuXkn,
                NuXkk = NuXkk,
                NeXkc = NeXkc,
                NeXkk = NeXkk,
                NeXkn = NeXkn,
                VXkc = Vkc,
                VXkk = Vkk,
                VXkn = Vkn,
                dXkc = Dkc,
                dXkk = Dkk,
                dXkn = Dkn,
                Xkp = Xkp
            };
        }
        
        private static double GetСollisionFreq(double NuR, double Xkn)
        {
            double x1 = NuR/(1 + 0.014*Xkn);
            double x2 = Math.Exp(-0.023*Xkn);

            return x1*x2;
        }

        private static double GetСollisionFreqInTurb(double NuR, double Xkn)
        {
            double x1 = NuR / (1 + 0.011 * Xkn);
            double x2 = Math.Exp(-0.019 * Xkn);

            return x1 * x2;
        }

        private static double GetElectrKonc(double NeR, double Xk)
        {
            double x1 = Math.Exp(-0.037*Xk);
            double x2 = NeR/(1 + 0.025*Xk);

            return x1*x2;
        }

        private static double GetElectrKoncInTurb(double NeR, double Xk)
        {
            double x1 = Math.Exp(-0.029 * Xk);
            double x2 = NeR / (1 + 0.021 * Xk);

            return x1 * x2;
        }

        private static double GetDzeta(double Xk,double R)
        {
            double k = Math.Log10(
                Xk/(Math.Sqrt(Cnst.Cx*Cnst.Sm)+1)
                );

            return
                1 + (7 + 1.5*Math.Log10(R))*k -
                (3/(Math.Log10(R) + 8))*k*k;

        }

        private static double GetDzetaInTurb(double Xk, double R, double alfa)
        {
            double x1 = 0.2 * (4 + Math.Log10(R));
            double x2 = 1 + Math.Pow((Xk / Math.Sqrt(Cnst.Cx * Cnst.Sm)), alfa);

            return x1 * x2;
        }
        
        private static double GetTrackLength(double NeR, double Rm, double NeKp)
        {
            if (NeR <= (1.24e-8)*Cnst.f0*Cnst.f0)
                return 0;

            for (double X = 0;; X = X + Rm)
            {
                double l = Math.Exp(-0.037*X);
                double r = (((1.24e-8) * Cnst.f0 * Cnst.f0) / NeR) * (1 + 0.025 * X);

                if (l<=r)
                    return X;
            }
        }

        private static double GetTrackLengthInTurb(double NeR, double Rm,double  NeKp)
        {
            if (NeR <= (1.24e-8) * Cnst.f0 * Cnst.f0)
                return 0;
            
            for (double X = 0; ; X = X + Rm)
            {
                double l = Math.Exp(-0.029 * X);
                double r = (NeKp / NeR) * (1 + 0.021 * X);

                if (l <= r)
                    return X;
            }
        }
        
        #endregion
    }
}

        