using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace imitator
{
    class Imit42
    {
        #region входные и выходные параметры, константы

        /// <summary>
        /// ВХОДНАЯ ИНФОРМАЦИЯ
        /// </summary>
        public class InputData
        {
            public double Fi;
            /// <summary>
            /// Радиус сферического затупления баллистической цели, м
            /// </summary>
            public double Rzatup;
            /// <summary>
            /// Текущий угол между линией визирования и осью элемен-та СБЦ, рад
            /// </summary>
            public double Angle; 
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
            public double Ne1; //Электронная концентрация плазмы в точке наблюдения;
            public double Nu1; //Эффективная частота со-ударений электронов в точке наблю-дения

            public double NeKrit;
            public double NuKrit;

            public double Ne; //Электронная концентрация плазмы в точке наблюдения;
            public double Nu; //Эффективная частота со-ударений электронов в точке наблю-дения
            public double Delta; //расстояние отхода удар-ной волны от поверхности баллистиче-ской цели
            public double A; //геометриче-ские пара-метры удар-ной волны
            public double B; //геометриче-ские пара-метры удар-ной волны

            public double M; //Число Маха

        }
        
        public class Cnst
        {
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

            /// <summary>
            /// H,км; Т,К; Betta, град/км
            /// </summary>
            public static readonly double[,] T1 = 
            {
                {-2e3, 301.15, -6.5e-3},
                {0, 288.15, -6.5e-3},
                {11e3, 216.65, -6.5e-3},
                {20e3, 216.65, 0}, 
                {32e3, 288.65, 1e-3}, 
                {47e3, 270.65, 2.8e-3}, 
                {51e3, 270.65, 0}
            };

            /// <summary>
            /// Высота,м	Плотность,кг/м³
            /// </summary>
            public static readonly double[,] T2 = 
            {
                {0,1.225 },
                {500, 1.1673},
                {1000, 1.1117},
                {1500, 1.0581}, 
                {2000, 1.0065}, 
                {2500, 0.9569}, 
                {3000, 0.9093},
                {4000, 0.8194},
                {5000, 0.7365},
                {6000, 0.6601},
                {7000, 0.59}, 
                {8000, 0.5258}, 
                {9000, 0.4671}, 
                {10000, 0.4135}, 
                {11000, 0.3648},
                {12000, 0.3119},
                {14000, 0.2279}, 
                {16000, 0.1665}, 
                {18000, 0.1216}, 
                {20000, 0.0889},
                {24000, 0.0469},
                {28000, 0.0251},
                {32000, 0.0136},
                {36000, 0.0073}, 
                {40000, 0.004},
                {50000, 0.00103},
                {60000, 0.00031},
                {80000, 0.00002}
            };

            /// <summary>
            /// Высота, м;  a, б/р;	b, с/км;	ρ, б/р;	 g, с/км;
            /// </summary>
            public static readonly double[,] T3 =
            {
                {0, 17, 5.7e-3, 8, 0.9e-3},
                {30e3, 17.5, 5.2e-3, 9, 1.1e-3},
                {60e3, 15, 3.3e-3, 10, 1.6e-3},
                {90e3, 20, 3.3e-3, 11, 2.2e-3},
                {120e3, 6, 0.67e-3, 12.5, 2.9e-3},
            };

            /// <summary>
            /// nu;  А(nu), эл/см3;  В(nu), К
            /// </summary>
            public static readonly double[,] T4 =
            {
                {1, 7.6e18, 6.63e4},
                {0.1, 2.5e18, 6.5e4},
                {1e-2, 6.3e17, 6.4e4},
                {1e-3, 1.35e17, 6.29e4},
                {1e-4, 6e16, 6.4e4}
            };

            /// <summary>
            /// ai, 1/град	bi, 1/град2
            /// Fi(O)
            /// </summary>
            public static readonly double[,] T5 =
            {
                {-1.7e-2, -3e-4},
                {-1e-3, -1.2e-4}
                
            };

        }

        #endregion

        #region дополнительные классы, используемые в логике

        public struct HeightDependCoeffs
        {
            public double a;
            public double b;
            public double ro;
            public double g;
        }
        
        #endregion

        public static OutputData GeneralOperator(InputData data)
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
            double nu = ROs/Cnst.Ro0;
            double NeKrit = GetElectronConc(nu,Ts);

            double x1 =(7.2e9)*ROs*Math.Sqrt(Ts);
            double x2 = NeKrit * (5.5)/(Ts*Math.Sqrt(Ts))*
                Math.Log(220 * Ts / Math.Pow(NeKrit,0.33333))  ;
            double x3 = (1.3e-10) * NeKrit * Math.Sqrt(Ts);

            double nuKrit = x1 + x2 + x3;
            
            //ВП6
            //Оценка электронной концентрации и эф-фективной частоты соударений электро-нов в точке наблюдения

            double e1 = -(
                (2.02)*data.Angle +
                (0.41)*data.Angle*data.Angle);
            double e2 = (400 * data.Rzatup * data.Rzatup);

            double ex = Math.Exp(e1 / e2);


              double Ne = NeKrit*ex;
              double nuEffect = nuKrit * Math.Exp(
                  -( (1.01)*data.Angle + (0.294) * data.Angle * data.Angle)/
                       (400*data.Rzatup * data.Rzatup)
                   );
            //ВП7
            //Оценка геометрических параметров ударной волны
              var d1 = 1/
                    ((ROs / RO) - 1);
            double delta = 0.67*data.Rzatup*d1;
                

            //число Маха 
            double M = data.V/330;
            double a = (data.Rzatup + delta) * (M * M - 1);
            double b = (data.Rzatup + delta) * Math.Sqrt(M * M - 1);


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
            
            for (int i = 0; i < 5; i++)
                {
                    if (nu <= Cnst.T4[i, 0] && nu >= Cnst.T4[i+1, 0])
                    {
                        //double n = -Math.Log10(Cnst.T4[i + 1, 1]/Cnst.T4[i, 1]);
                        //double A = Cnst.T4[i, 1]*Math.Pow(nu/Cnst.T4[i, 0], n);

                        double n = Math.Log10(Cnst.T4[i, 1]) - 
                            (Math.Log10(Cnst.T4[i + 1, 1]) - Math.Log10(Cnst.T4[i, 1]))*
                            (Math.Log10(nu) - Math.Log10(Cnst.T4[i, 0]));
                        double A = Math.Pow(10, n);
                        
                        double B = Cnst.T4[i, 2] +
                                   (Cnst.T4[i + 1, 2] - Cnst.T4[i, 2])*(Math.Log(nu) - Math.Log(Cnst.T4[i, 0]))
                                   /(Math.Log(Cnst.T4[i + 1, 0]) - Math.Log(Cnst.T4[i, 0]));
                        
                        //todo сделать проверку Ne на точность вычисления
                        
                        return A*Math.Exp(-B/Ts);
                        
                    }
                }
            throw new Exception("В таблице 4 не найдено подходящего элемента.");
            
            
        }

        /// <param name="h">высота от поверхности ОЗЭ</param>
        private static HeightDependCoeffs GetHCoefficients(double h)
        {
            HeightDependCoeffs coeffs = new HeightDependCoeffs();
            for (int i = 0; i < 5; i++)
            {
                if (h >= Cnst.T3[i, 0] && h <= Cnst.T3[i + 1, 0])
                {
                    coeffs.a = Cnst.T3[i, 1] +
                               (Cnst.T3[i + 1, 1] - Cnst.T3[i, 1])*
                               (h - Cnst.T3[i, 0])/(Cnst.T3[i + 1, 0] - Cnst.T3[i, 0]);
                    coeffs.b = Cnst.T3[i, 2] +
                              (Cnst.T3[i + 1, 2] - Cnst.T3[i, 2]) *
                              (h - Cnst.T3[i, 0]) / (Cnst.T3[i + 1, 0] - Cnst.T3[i, 0]);
                    coeffs.ro= Cnst.T3[i, 3] +
                              (Cnst.T3[i + 1,3 ] - Cnst.T3[i, 3]) *
                              (h - Cnst.T3[i, 0]) / (Cnst.T3[i + 1, 0] - Cnst.T3[i, 0]);
                    coeffs.g = Cnst.T3[i, 4] +
                              (Cnst.T3[i + 1, 4] - Cnst.T3[i,4]) *
                              (h - Cnst.T3[i, 0]) / (Cnst.T3[i + 1, 0] - Cnst.T3[i, 0]);
                    return coeffs;
                }
            }
            throw new Exception("В таблице 3 не найдено подходящего элемента.");
        
        }

        /// <param name="h">высота от поверхности ОЗЭ</param>
        private static double GetDensity(double h)
        {
            for (int i = 0; i < 27; i++)
            {
                if (h >= Cnst.T2[i, 0] && h <= Cnst.T2[i+1, 0])
                {
                    double mu = Math.Log(Cnst.T2[i + 1, 1] / Cnst.T2[i, 1]) / (-Cnst.T2[i + 1, 0] + Cnst.T2[i, 0]);
                    return Cnst.T2[i, 1]*Math.Exp(-mu*(h - Cnst.T2[i, 0]));
                }
            }

            double mu1 = Math.Log(Cnst.T2[27, 1] / Cnst.T2[26, 1]) / (-Cnst.T2[27, 0] + Cnst.T2[26, 0]);
            return Cnst.T2[26, 1] * Math.Exp(-mu1 * (h - Cnst.T2[26, 0]));

        }
        
        /// <param name="h">высота от поверхности ОЗЭ</param>
        private static double GetTemperature(double h)
        {
            double H = Cnst.Rz*h/(Cnst.Rz + h);
            
            for (int i = 0; i < 7; i++)
            {
                if (H <= Cnst.T1[i, 0])
                {
                    return Cnst.T1[i, 1] + Cnst.T1[i, 2]*(H - Cnst.T1[i, 0]);
                }
            }
            return Cnst.T1[6, 1] + Cnst.T1[6, 2] * (H - Cnst.T1[6, 0]);
        }
    }
}
