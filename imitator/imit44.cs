using System;
using System.Linq;

namespace imitator
{
    class Imit44
    {
        #region входные и выходные параметры

        /// <summary>
        /// ВХОДНАЯ ИНФОРМАЦИЯ
        /// </summary>
        public class InputData
        {
            public int Type;
            public int SubType;
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
        
        #endregion

        private static double Cx;
        private static double Sm;
        public static OutputData Exec(InputData data)
        {
            Cx = Const.Cx;
            Sm = Const.Sm;
            //определим плотность
            double Ro = Imit42.GetDensity(data.H);

            OutputData outData= new OutputData();

            //ЛО1
            //if (data.H >= 80000)
            //    return outData;

            //ВП1
            //Расчет высоты начала турбулизации вязкого следа 
            double Rm = Math.Pow(Sm/Math.PI, 0.5);
            double RoTurb = 0.3/(Rm*data.V);
            double Hturb = GetHturb(RoTurb);
            double NeR = 0.1 * data.NeKrit;

            //ВП01
            //Расчет  параметров электронной концентрации
            if (data.Type != 0)
            {
                var dot = Const.GetFlyingObject(data.Type,data.SubType);

                if (data.Type == 2 &&
                    (data.SubType == 4 || data.SubType == 5 || data.SubType == 6) &&
                    data.H > dot.Hmin)
                {
                    Cx = dot.Cxf;
                    Sm = dot.Smf;

                    double U;
                    if (Hturb - dot.dHis <= data.H && data.H <= Hturb)
                    {
                        U = (Hturb - data.H) / dot.dHis;
                    }
                    else if (dot.Hmin + dot.dHis <= data.H && data.H <= Hturb - dot.dHis)
                    {
                        U = 1;
                    }
                    else //if (dot.Hmin <= data.H && data.H <= dot.Hmin + dot.dHis)
                    {
                        U = (data.H - dot.Hmin) / dot.dHis;
                    }
                    NeR = 4e10 * U * Math.Pow((0.3 / (Rm * data.V * Ro)), 1.1) * Math.Pow((1e-3 * data.V), 6.04);
                }
            }
            //Расчет аэродинамической силы, действующей на БЦ при полете в атмосфере
            double R = 0.5*Cx*Sm*Ro*data.V*data.V;

            //Расчет расстояния от горла до точки перехода из ламинарного в турбулентное течение 
            double Xp = GetXp(Rm,data.V,Ro);
            double NuR = 0.1*data.NuKrit;
            double NeKp = (1.24e-8)*Const.F0*Const.F0;

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
                outData = GetParamsInTurb(data, Rm, R, NeR, NuR, NeKp, Xp);
            }

            outData.Xp = Xp;
            outData.Hturb = Hturb;

            return outData;

        }
        
        #region Приватные методы 

        private static double GetHturb(double roTurb)
        {
            double mu;
            for (int i = 0; i < Const.T2.GetLength(0) - 1; i++)
            {
                if (roTurb >= Const.T2[i, 1] && roTurb <= Const.T2[i + 1, 1])
                {
                    mu = Math.Log(Const.T2[i + 1, 1] / Const.T2[i, 1]) / (-Const.T2[i + 1, 0] + Const.T2[i, 0]);
                    return Const.T2[i, 0] - (Math.Log(roTurb) - Math.Log(Const.T2[i, 1])) / mu;
                }
            }
            mu = Math.Log(Const.T2[27, 1] / Const.T2[26, 1]) / (-Const.T2[27, 0] + Const.T2[26, 0]);
            return Const.T2[26, 0] - (Math.Log(roTurb) - Math.Log(Const.T2[26, 1])) / mu;
        }

        private static double GetXp(double Rm, double V, double Ro)
        {
            double Re = (5.3e4) * Rm * V * Ro;
            return Rm * (256.5 - 45 * (Math.Log10(Re) - 1));
        }

        private static double GetSpeed(double V, double Xk)
        {
            const double powArg = -0.64;
            return V *Math.Pow((Xk / Math.Sqrt(Cx * Sm)+1),powArg);
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

            double Dkn = DZkn*Math.Sqrt(Cx*Sm);
            double Dkc = DZkc*Math.Sqrt(Cx*Sm);
            double Dkk = DZkk*Math.Sqrt(Cx*Sm);

            //Расчет скорости потока в заданных точках ламинарного вязкого следа
            double Vkn = GetSpeed(data.V, data.Xkn);
            double Vkc = GetSpeed(data.V, data.Xkc);
            double Vkk = GetSpeed(data.V, data.Xkk);

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
        private static OutputData GetParamsInTurb(InputData data, double Rm, double R, double NeR, double NuR, double NeKp, double Xp)
        {
            double Xkp;
            double alfa = 0.42;


            //Расчет ширины вязкого следа в заданных точках внутри турбулентного вязкого следа 
            double DZkn = GetDzetaInTurb(data.Xkn, R, alfa);
            double DZkc = GetDzetaInTurb(data.Xkc, R, alfa);
            double DZkk = GetDzetaInTurb(data.Xkk, R, alfa);

            double Dkn = DZkn * Math.Sqrt(Cx * Sm);
            double Dkc = DZkc * Math.Sqrt(Cx * Sm);
            double Dkk = DZkk * Math.Sqrt(Cx * Sm);

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
            double x1 = Math.Exp(-0.033*Xk);
            double x2 = NeR/(1 + 0.023*Xk);

            return x1*x2;
        }

        private static double GetElectrKoncInTurb(double NeR, double Xk)
        {
            double x1 = Math.Exp(-0.033 * Xk);
            double x2 = NeR / (1 + 0.023 * Xk);

            return x1 * x2;
        }

        private static double GetDzeta(double Xk,double R)
        {
            double k = Math.Log10(
                Xk/(Math.Sqrt(Cx*Sm)+1)
                );

            return
                1 + (7 + 1.5*Math.Log10(R))*k -
                (3/(Math.Log10(R) + 8))*k*k;

        }

        private static double GetDzetaInTurb(double Xk, double R, double alfa)
        {
            double x1 = 0.2 * (4 + Math.Log10(R));
            double x2 = 1 + Math.Pow((Xk / Math.Sqrt(Cx * Sm)), alfa);

            return x1 * x2;
        }
        
        private static double GetTrackLength(double NeR, double Rm, double NeKp)
        {
            if (NeR <= (1.24e-8)*Const.F0*Const.F0)
                return 0;

            for (double X = 0;; X = X + Rm)
            {
                double l = Math.Exp(-0.033*X);
                double r = (((1.24e-8) * Const.F0 * Const.F0) / NeR) * (1 + 0.023 * X);

                if (l<=r)
                    return X;
            }
        }

        private static double GetTrackLengthInTurb(double NeR, double Rm,double  NeKp)
        {
            if (NeR <= (1.24e-8) * Const.F0 * Const.F0)
                return 0;
            
            for (double X = 0; ; X = X + Rm)
            {
                double l = Math.Exp(-0.033 * X);
                double r = (NeKp / NeR) * (1 + 0.023 * X);

                if (l <= r)
                    return X;
            }
        }
        
        #endregion
    }
}

        