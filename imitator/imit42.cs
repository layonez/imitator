using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace imitator
{
    public class Imit42
    {
        #region входные и выходные параметры

        /// <summary>
        /// ВХОДНАЯ ИНФОРМАЦИЯ
        /// </summary>
        public class InputData
        {
            public double Fi{ get; set; }
            /// <summary>
            /// Текущий угол между линией визирования и осью элемен-та СБЦ, рад
            /// </summary>
            public double Angle{ get; set; } 
            /// <summary>
            /// Текущая высота полета БЦ, м
            /// </summary>
            public double H{ get; set; } 
            /// <summary>
            /// Скорость полета БЦ, м/с
            /// </summary>
            public double V{ get; set; }

            public int Type{ get; set; }
            public int SubType{ get; set; }
        }

        /// <summary>
        /// ВЫХОДНАЯ ИНФОРМАЦИЯ
        /// </summary>
        public class OutputData
        {
            public double NeKrit{ get; set; }
            public double NuKrit{ get; set; }

            public double Ne{ get; set; } //Электронная концентрация плазмы в точке наблюдения
            public double Nu{ get; set; } //Эффективная частота со-ударений электронов в точке наблю-дения
            public double Delta{ get; set; } //расстояние отхода удар-ной волны от поверхности баллистиче-ской цели
            public double A{ get; set; } //геометриче-ские пара-метры удар-ной волны
            public double B{ get; set; } //геометриче-ские пара-метры удар-ной волны

            public double M{ get; set; } //Число Маха

        }
        
        #endregion

        #region дополнительные классы, используемые в логике

        private struct HeightDependCoeffs
        {
            public double a;
            public double b;
            public double ro;
            public double g;
        }
        
        #endregion

        public static OutputData Exec(InputData data)
        {
            //ВП1
            //Оценка текущих значений температуры, давления и плотности воздуха в набегающем потоке
            double T = GetTemperature(data.H);
            double RO = GetDensity(data.H);

            //ВП3
            //Оценка абсолютных значений температуры и плотности воздуха в критической точке
            HeightDependCoeffs coeffs = GetHCoefficients(data.H);

            double Ts = T*(
                            coeffs.a + 
                                coeffs.b*(data.V - 4000)
                );
            double ROs = RO * (coeffs.ro + coeffs.g * (data.V - 3000));

            //ВП4
            //Оценка электронной концентрации и эффективной частоты соударений электронов в критической точке
            double nu = ROs/Const.Ro0;
            double NeKrit = GetElectronConc(nu,Ts);

            double x1 =(7.2e9)*ROs*Math.Sqrt(Ts);
            double x2 = NeKrit * (5.5)/(Ts*Math.Sqrt(Ts))*
                Math.Log(220 * Ts / Math.Pow(NeKrit,0.33333))  ;
            double x3 = (1.3e-10) * NeKrit * Math.Sqrt(Ts);

            double nuKrit = x1 + x2 + x3;


            double Rzatup = Const.GetFlyingObject(data.Type, data.SubType).ShineDots.First().Rzatup;
            //ВП6
            //Оценка электронной концентрации и эф-фективной частоты соударений электро-нов в точке наблюдения

            double e1 = -(
                (2.02) * data.Angle +
                (0.41) * data.Angle * data.Angle);
            double e2 = (400 * Rzatup * Rzatup);

            double ex = Math.Exp(e1 / e2);


            double Ne = NeKrit * ex;
            double nuEffect = nuKrit * Math.Exp(
                -((1.01) * data.Angle + (0.294) * data.Angle * data.Angle) /
                     (400 * Rzatup * Rzatup)
                 );
            //ВП7
            //Оценка геометрических параметров ударной волны
            var d1 = 1 /
                  ((ROs / RO) - 1);
            double delta = 0.67 * Rzatup * d1;


            //число Маха 
            double M = data.V / 330;
            double a = (Rzatup + delta) * (M * M - 1);
            double b = (Rzatup + delta) * Math.Sqrt(M * M - 1);




            return new OutputData() { A = a, B = b, Delta = delta, Ne = Ne, Nu = nuEffect, NeKrit = NeKrit, NuKrit = nuKrit, M = M };
        }


        /// <summary>
        /// Рассчитывает электронную концентрацию
        /// </summary>
        /// <param name="nu">частота соударений электронов </param>
        /// <param name="Ts">абсолютное значение температуры</param>
        private static double GetElectronConc(double nu, double Ts)
        {
            if (nu > 1)
                nu = 1;
            else if (nu < 1e-4)
                nu = 1e-4;

            return ElectronConc(nu, Ts);
        }

        /// <summary>
        /// Оценка электронной концентрации и эффективной частоты соударений элек-тронов в критической точке
        /// Используя табличные данные
        /// </summary>
        private static double ElectronConcFromTable(double nu, double Ts)
        {
            for (int i = 0; i < imitator.Const.T4.GetLength(0); i++)
            {
                if (nu <= imitator.Const.T4[i, 0] && nu >= imitator.Const.T4[i + 1, 0])
                {
                    //double n = -Math.Log10(Cnst.T4[i + 1, 1]/Cnst.T4[i, 1]);
                    //double A = Cnst.T4[i, 1]*Math.Pow(nu/Cnst.T4[i, 0], n);

                    double n = Math.Log10(imitator.Const.T4[i, 1]) -
                               (Math.Log10(imitator.Const.T4[i + 1, 1]) - Math.Log10(imitator.Const.T4[i, 1]))*
                               (Math.Log10(nu) - Math.Log10(imitator.Const.T4[i, 0]));
                    double A = Math.Pow(10, n);

                    double B = imitator.Const.T4[i, 2] +
                               (imitator.Const.T4[i + 1, 2] - imitator.Const.T4[i, 2])*
                               (Math.Log(nu) - Math.Log(imitator.Const.T4[i, 0]))
                               /(Math.Log(imitator.Const.T4[i + 1, 0]) - Math.Log(imitator.Const.T4[i, 0]));

                    //todo сделать проверку Ne на точность вычисления

                    return A*Math.Exp(-B/Ts);
                }
            }
            throw new Exception("В таблице 4 не найдено подходящего элемента.");
        }
        
        /// <summary>
        /// Оценка электронной концентрации и эффективной частоты соударений элек-тронов в критической точке
        /// Аналитически
        /// </summary>
        private static double ElectronConc(double nu, double Ts)
        {
            double NeKrit = 7.6e18*Math.Pow(nu, 0.57)*Math.Exp(-(6.4e4/Ts));
            return NeKrit;
        }

        /// <param name="h">высота от поверхности ОЗЭ</param>
        private static HeightDependCoeffs GetHCoefficients(double h)
        {
            HeightDependCoeffs coeffs = new HeightDependCoeffs();
            for (int i = 0; i < imitator.Const.T3.GetLength(0)-1; i++)
            {
                if (h >= imitator.Const.T3[i, 0] && h <= imitator.Const.T3[i + 1, 0])
                {
                    coeffs.a = imitator.Const.T3[i, 1] +
                               (imitator.Const.T3[i + 1, 1] - imitator.Const.T3[i, 1])*
                               (h - imitator.Const.T3[i, 0])/(imitator.Const.T3[i + 1, 0] - imitator.Const.T3[i, 0]);
                    coeffs.b = imitator.Const.T3[i, 2] +
                              (imitator.Const.T3[i + 1, 2] - imitator.Const.T3[i, 2]) *
                              (h - imitator.Const.T3[i, 0]) / (imitator.Const.T3[i + 1, 0] - imitator.Const.T3[i, 0]);
                    coeffs.ro= imitator.Const.T3[i, 3] +
                              (imitator.Const.T3[i + 1,3 ] - imitator.Const.T3[i, 3]) *
                              (h - imitator.Const.T3[i, 0]) / (imitator.Const.T3[i + 1, 0] - imitator.Const.T3[i, 0]);
                    coeffs.g = imitator.Const.T3[i, 4] +
                              (imitator.Const.T3[i + 1, 4] - imitator.Const.T3[i,4]) *
                              (h - imitator.Const.T3[i, 0]) / (imitator.Const.T3[i + 1, 0] - imitator.Const.T3[i, 0]);
                    return coeffs;
                }
            }
            int j = imitator.Const.T3.GetLength(0) - 2;

            coeffs.a = imitator.Const.T3[j, 1] +
                               (imitator.Const.T3[j + 1, 1] - imitator.Const.T3[j, 1]) *
                               (h - imitator.Const.T3[j, 0]) / (imitator.Const.T3[j + 1, 0] - imitator.Const.T3[j, 0]);
            coeffs.b = imitator.Const.T3[j, 2] +
                              (imitator.Const.T3[j + 1, 2] - imitator.Const.T3[j, 2]) *
                              (h - imitator.Const.T3[j, 0]) / (imitator.Const.T3[j + 1, 0] - imitator.Const.T3[j, 0]);
            coeffs.ro = imitator.Const.T3[j, 3] +
                              (imitator.Const.T3[j + 1, 3] - imitator.Const.T3[j, 3]) *
                              (h - imitator.Const.T3[j, 0]) / (imitator.Const.T3[j + 1, 0] - imitator.Const.T3[j, 0]);
            coeffs.g = imitator.Const.T3[j, 4] +
                              (imitator.Const.T3[j + 1, 4] - imitator.Const.T3[j, 4]) *
                              (h - imitator.Const.T3[j, 0]) / (imitator.Const.T3[j + 1, 0] - imitator.Const.T3[j, 0]);
                    return coeffs;
        
        }

        /// <param name="h">высота от поверхности ОЗЭ</param>
        public static double GetDensity(double h)
        {
            for (int i = 0; i < imitator.Const.T2.GetLength(0)-1;i++)
            {
                if (h >= imitator.Const.T2[i, 0] && h <= imitator.Const.T2[i+1, 0])
                {
                    double mu = -Math.Log(imitator.Const.T2[i + 1, 1] / imitator.Const.T2[i, 1]) / (imitator.Const.T2[i, 0]-imitator.Const.T2[i + 1, 0]);
                    return imitator.Const.T2[i, 1]*Math.Exp(-mu*(h - imitator.Const.T2[i, 0]));
                }
            }

            double mu1 = Math.Log(imitator.Const.T2[imitator.Const.T2.GetLength(0) - 1, 1] / imitator.Const.T2[imitator.Const.T2.GetLength(0) - 2, 1]) / (-imitator.Const.T2[imitator.Const.T2.GetLength(0) - 1, 0] + imitator.Const.T2[imitator.Const.T2.GetLength(0) - 2, 0]);
            return imitator.Const.T2[imitator.Const.T2.GetLength(0) - 2, 1] * Math.Exp(-mu1 * (h - imitator.Const.T2[imitator.Const.T2.GetLength(0) - 2, 0]));

        }
        
        /// <param name="h">высота от поверхности ОЗЭ</param>
        private static double GetTemperature(double h)
        {
            double H = imitator.Const.Rz*h/(imitator.Const.Rz + h);

            for (int i = 0; i < imitator.Const.T1.GetLength(0); i++)
            {
                if (H <= imitator.Const.T1[i, 0])
                {
                    double Tz = imitator.Const.T1[i, 1];
                    double Bz = imitator.Const.T1[i, 2];
                    double Hz = imitator.Const.T1[i, 0];
                    return Tz + Bz * (Hz - H);
                }
            }
            return imitator.Const.T1[imitator.Const.T1.GetLength(0)-1 , 1] + imitator.Const.T1[imitator.Const.T1.GetLength(0) - 1, 2] * (H - imitator.Const.T1[imitator.Const.T1.GetLength(0) - 1, 0]);
        }
    }
}
