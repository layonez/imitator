using System;
using System.Collections.Generic;

namespace imitator
{
    [Serializable]
    public class Const
    {
        #region Таблицы

        /// <summary>
        /// H,км; Т,К; Betta, град/км
        /// </summary>
        public static readonly double[,] T1 = 
        {
            {0, 288.15, 6.5e-3},
            {11e3, 216.65, 6.5e-3},
            {20e3, 216.65, 0}, 
            {32e3, 228.65, -1e-3}, 
            {47e3, 270.65, -2.8e-3}, 
            {52e3, 270.65, 0},
            {61e3, 252.65, 2e-3 },
            {79e3, 180.65, 4e-3},
            {88.4e3, 180.65, 0 },
            {98.4e3, 210.65, -3e-3},
            {11e4, 215.45, -3e-3},
            {15e4, 935.45, -1.8e-2}
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
            {80000, 0.00002},
            {90000, 3e-6},
            {100000, 9e-7},
            {120000, 9e-8},
            {150000, 2e-9}
        };

        /// <summary>
        /// Высота, м;  a, б/р;	b, с/км;	ρ, б/р;	 g, с/км;
        /// </summary>
        public static readonly double[,] T3 =
        {
            {0, 17, 5.7e-3, 10.5, 2.1e-3},
            {30e3, 16.5, 5.2e-3, 11, 2.3e-3},
            {60e3, 15, 4.3e-3, 11.5, 2.5e-3},
            {90e3, 12, 3.3e-3, 12, 2.7e-3},
            {120e3, 6, 1.67e-3, 12.5, 2.9e-3},
            {150e3, 1.05, 0.87e-2, 13.2, 3.1e-3}
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
            {1e-4, 6e16, 6.4e4},
            {1e-5, 2e16, 6.23e4},
            {1e-6, 7e15, 6.5e4},
            {1e-7, 2.5e15, 6.4e4},
            {1e-8, 6.5e14, 6.6e4},
            {1e-9, 2.1e14, 6.7e4}
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
        #endregion

        #region Константы

        /// <summary>
        /// скорость звука,  м/с
        /// </summary>
        public static double Vs = 340.29;

        /// <summary>
        /// средний радиус Земли, м
        /// </summary>
        public static double Rz = 6371e3;

        /// <summary>
        /// плотность
        /// </summary>
        public static double Ro0 = 1.125;

        /// <summary>
        /// Минимальная частота зон-дирующего радиосигнала, Гц
        /// </summary>
        public static double F0 = 3e9;

        /// <summary>
        /// Шаг по частоте зондиру-ющего радиосигнала, Гц
        /// </summary>
        public static double DeltaF = 5e6;

        /// <summary>
        /// Скорость света, м/с
        /// </summary>
        public static double C = 3e8;

        /// <summary>
        /// Аэродинамический коэффициент лобового сопротивления БЦ
        /// </summary>
        public static double Cx = 0.01;

        /// <summary>
        /// площадь миделевого сечения БЦ 
        /// </summary>
        public static double Sm = 1;

        /// <summary>
        /// Разрешающая способность по дальности РЛС, м
        /// </summary>
        public static double Dr = 5;

        /// <summary>
        /// Разрешающая способность по скорости  РЛС
        /// </summary>
        public static int DV = 20;

        /// <summary>
        /// Размерность дальностно-скоростного портрета по дальности
        /// </summary>
        public static double K0 = 10;

        /// <summary>
        /// Коэффициенты калибровки ЭПР сверхкритического следа
        /// </summary>
        public static double Ksv2 = 0.05;

        /// <summary>
        /// Коэффициенты калибровки ЭПР сверхкритического следа
        /// </summary>
        public static double Ksv3 = 0.05;

        /// <summary>
        /// Длина волны  излучения РЛС
        /// </summary>
        public static double LambdaK = 0.1;

        /// <summary>
        /// Диаметр Миделя
        /// </summary>
        public static double Dm = 0.8;

        /// <summary>
        /// Калибровочный коэфф.
        /// </summary>
        public static double K1 = 0.5;

        /// <summary>
        /// Калибровочный коэфф.
        /// </summary>
        public static double K2 = 0.5;

        /// <summary>
        /// Калибровочный коэфф.
        /// </summary>
        public static double K3 = 0.5;

        /// <summary>
        /// Калибровочный коэфф.
        /// </summary>
        public static double K4 = 0.5;

        /// <summary>
        /// Коэффициент Калибровки D-V портрета 
        /// </summary>
        public static double Kpv = 0.5;

        /// <summary>
        /// Коэффициент Калибровки D-V портрета 
        /// </summary>
        public static double Kv = 0.5; 
        #endregion

        #region Данные целей

        private static readonly FlyingObject[] Objects = 
        {
         new FlyingObject()
            {
            Type = 1,Subtype = 1,
            Cx = 0.06, Sm = 0.5, dHis = 5000, Hmin = 15000, Cxf = 0.01, Smf = 0.5,

            ShineDots= new []
            {
                new ShineDot() {Kf = 1, Xc = 0, Yc = 0, Omin = 0, Omax = 2.9, Rzatup = 0.04, Gamma = 0.2 , Kiz = 0.8, },
                new ShineDot() {Kf = 4, Xc = 0.6,Yc = -0.25,Omin = 0,Omax = 2.9,Rzatup = 0.04,D1 = 0.1, D2 = 0.5,L = 1.2,Gamma = 0.2, Kiz = 0.8},
                new ShineDot() {Kf = 6, Xc = 1.2, Yc = -0.35, Omin = 0, Omax = 3.14, Rzatup = 0.04, Gamma = 0.2, D = 0.3, Kiz = 0.4},
                new ShineDot() {Kf = 8, Xc = 1.2, Yc = 0, Omin = 1.6, Omax = 3.14, Rzatup = 0.04, Gamma = 0.2, D = 0.5, Kiz = 0.8}
            }
        },
        new FlyingObject()
        {
            Type = 1, Subtype = 2,
            Cx = 0.07, Sm = 0.6, dHis = 5000, Hmin = 15000, Cxf = 0.01, Smf = 0.5,

            ShineDots = new[]
            {
                new ShineDot() {Kf = 1, Xc = 0, Yc = 0, Omin = 0,      Omax = 2.95, Rzatup = 0.03, Gamma = 0.15, Kiz = 0.8},
                new ShineDot() {Kf = 4, Xc = 0.7,Yc = -0.25,Omin = 0,  Omax = 2.9,  Rzatup = 0.03, Gamma = 0.2, D1 = 0.1, D2 = 0.5,L = 1.4, Kiz = 0.8},
                new ShineDot() {Kf = 6, Xc = 1.4, Yc = -0.4, Omin = 0, Omax = 3.14, Rzatup = 0.03, Gamma = 0.2, D = 0.5, Kiz = 0.4},
                new ShineDot() {Kf = 8, Xc = 1.4, Yc = 0, Omin = 1.6,  Omax = 3.14, Rzatup = 0.03, Gamma = 0.2, D = 0.5, Kiz = 0.8}
            }
        },
        new FlyingObject()
        {
            Type = 1, Subtype = 3,
            Cx = 0.08, Sm = 0.7, dHis = 5000, Hmin = 15000, Cxf = 0.01, Smf = 0.5,

            ShineDots = new[]
            {
                new ShineDot() {Kf = 1, Xc = 0,   Yc = 0,     Omin = 0,    Omax = 2.9,  Rzatup = 0.08, Gamma = 0.2, Kiz = 0.8},
                new ShineDot() {Kf = 4, Xc = 0.7, Yc = -0.25, Omin = 0,    Omax = 2.9,  Rzatup = 0.08, Gamma = 0.2, D1 = 0.1, D2 = 0.5,L = 1.4, Kiz = 0.8},
                new ShineDot() {Kf = 6, Xc = 1.4, Yc = -0.4,  Omin = 0,    Omax = 3.14, Rzatup = 0.08, Gamma = 0.2, D = 0.5, Kiz = 0.4},
                new ShineDot() {Kf = 8, Xc = 1.4, Yc = 0,     Omin = 1.6,  Omax = 3.14, Rzatup = 0.08, Gamma = 0.2, D = 0.5, Kiz = 0.8}
             }
        },
        new FlyingObject()
        {
            Type = 2, Subtype = 1,
            Cx = 0.06, Sm = 0.5, dHis = 5000, Hmin = 15000, Cxf = 0.01, Smf = 0.5,

            ShineDots = new[]
            {
                new ShineDot() {Kf = 1, Xc = 0,   Yc = 0,     Omin = 0,    Omax = 2.9,  Rzatup = 0.02, Gamma = 0.2, Kiz = 0.8},
                new ShineDot() {Kf = 4, Xc = 0.6, Yc = -0.25, Omin = 0,    Omax = 2.9,  Rzatup = 0.02, Gamma = 0.2, D1 = 0.1, D2 = 0.5,L = 1.2, Kiz = 0.8},
                new ShineDot() {Kf = 6, Xc = 1.2, Yc = -0.35, Omin = 0,    Omax = 3.14, Rzatup = 0.02, Gamma = 0.2, D = 0.3, Kiz = 0.4},
                new ShineDot() {Kf = 8, Xc = 1.2, Yc = 0,     Omin = 1.6,  Omax = 3.14, Rzatup = 0.02, Gamma = 0.2, D = 0.5, Kiz = 0.8}
             }
        },
        new FlyingObject()
        {
            Type = 2, Subtype = 2,
            Cx = 0.05, Sm = 0.5, dHis = 5000, Hmin = 15000, Cxf = 0.01, Smf = 0.5,

            ShineDots = new[]
            {
                new ShineDot() {Kf = 1, Xc = 0,   Yc = 0,     Omin = 0,    Omax = 2.9,  Rzatup = 0.02, Gamma = 0.2, Kiz = 0.8},
                new ShineDot() {Kf = 4, Xc = 0.6, Yc = -0.25, Omin = 0,    Omax = 2.9,  Rzatup = 0.02, Gamma = 0.2, D1 = 0.1, D2 = 0.5,L = 1.2, Kiz = 0.8, },
                new ShineDot() {Kf = 6, Xc = 1.2, Yc = -0.35, Omin = 0,    Omax = 3.14, Rzatup = 0.02, Gamma = 0.2, D = 0.3, Kiz = 0.4},
                new ShineDot() {Kf = 8, Xc = 1.2, Yc = 0,     Omin = 1.6,  Omax = 3.14, Rzatup = 0.02, Gamma = 0.2, D = 0.6, Kiz = 0.8}
             }
        },
        new FlyingObject()
        {
            Type = 2, Subtype = 3,
            Cx = 0.05, Sm = 0.5, dHis = 5000, Hmin = 15000, Cxf = 0.01, Smf = 0.5,

            ShineDots = new[]
            {
                new ShineDot() {Kf = 1, Xc = 0,   Yc = 0,     Omin = 0,    Omax = 2.9,  Rzatup = 0.02, Gamma = 0.2, Kiz = 0},
                new ShineDot() {Kf = 4, Xc = 0.6, Yc = -0.25, Omin = 0,    Omax = 2.9,  Rzatup = 0.02, Gamma = 0.2, D1 = 0.1, D2 = 0.5,L = 1.2, Kiz = 0, },
                new ShineDot() {Kf = 6, Xc = 1.2, Yc = -0.35, Omin = 0,    Omax = 3.14, Rzatup = 0.02, Gamma = 0.2, D = 0.3, Kiz = 0.4},
                new ShineDot() {Kf = 8, Xc = 1.2, Yc = 0,     Omin = 1.6,  Omax = 3.14, Rzatup = 0.02, Gamma = 0.2, D = 0.5, Kiz = 0}
             }
        },
        new FlyingObject()
        {
            Type = 2, Subtype = 4,
            Cx = 0.05, Sm = 0.5, dHis = 5000, Hmin = 15000, Cxf = 0.01, Smf = 0.5,

            ShineDots = new[]
            {
            new ShineDot() {Kf = 1, Xc = 0,   Yc = 0,     Omin = 0,    Omax = 2.9,  Rzatup = 0.02, Gamma = 0.2, Kiz = 0},
            new ShineDot() {Kf = 4, Xc = 0.6, Yc = -0.25, Omin = 0,    Omax = 2.9,  Rzatup = 0.02, Gamma = 0.2, D1 = 0.1, D2 = 0.5,L = 1.2, Kiz = 0, },
            new ShineDot() {Kf = 6, Xc = 1.2, Yc = -0.35, Omin = 0,    Omax = 3.14, Rzatup = 0.02, Gamma = 0.2, D = 0.3, Kiz = 0.4},
            new ShineDot() {Kf = 8, Xc = 1.2, Yc = 0,     Omin = 1.6,  Omax = 3.14, Rzatup = 0.02, Gamma = 0.2, D = 0.6, Kiz = 0}
             }
        },
        new FlyingObject()
        {
            Type = 2, Subtype = 5,
             Cx = 0.05, Sm = 0.5, dHis = 5000, Hmin = 15000, Cxf = 0.03, Smf = 0.5,

            ShineDots = new[]
            {
                new ShineDot() {Kf = 1, Xc = 0,   Yc = 0,     Omin = 0,    Omax = 2.9,  Rzatup = 0.02, Gamma = 0.2, Kiz = 0},
                new ShineDot() {Kf = 4, Xc = 0.6, Yc = -0.25, Omin = 0,    Omax = 2.9,  Rzatup = 0.02, Gamma = 0.2, D1 = 0.1, D2 = 0.5,L = 1.2, Kiz = 0},
                new ShineDot() {Kf = 6, Xc = 1.2, Yc = -0.35, Omin = 0,    Omax = 3.14, Rzatup = 0.02, Gamma = 0.2, D = 0.3, Kiz = 0.4},
                new ShineDot() {Kf = 8, Xc = 1.2, Yc = 0,     Omin = 1.6,  Omax = 3.14, Rzatup = 0.02, Gamma = 0.2, D = 0.6, Kiz = 0}
            }
        }, 
        new FlyingObject()
        {
            Type = 2,Subtype = 6,
            Cx = 0.05, Sm = 0.5, dHis = 5000, Hmin = 15000, Cxf = 0.01, Smf = 0.5,

            ShineDots = new[]
            {
                new ShineDot() {Kf = 1, Xc = 0,   Yc = 0,     Omin = 0,    Omax = 2.9,  Rzatup = 0.02, Gamma = 0.2, Kiz = 0, },
                new ShineDot() {Kf = 4, Xc = 0.6, Yc = -0.25, Omin = 0,    Omax = 2.9,  Rzatup = 0.02, Gamma = 0.2, D1 = 0.1, D2 = 0.5,L = 1.2, Kiz = 0},
                new ShineDot() {Kf = 6, Xc = 1.2, Yc = -0.35, Omin = 0,    Omax = 3.14, Rzatup = 0.02, Gamma = 0.2, D = 0.3, Kiz = 0.4},
                new ShineDot() {Kf = 8, Xc = 1.2, Yc = 0,     Omin = 1.6,  Omax = 3.14, Rzatup = 0.02, Gamma = 0.2, D = 0.6, Kiz = 0}
            }
        }
};

        #endregion

        /// <summary>
        /// Получить летящую цель по её типу и подтипу
        /// </summary>
        public static FlyingObject GetFlyingObject(int type, int subtype)
        {
            foreach (var flyingObject in Objects)
            {
                if (flyingObject.Type == type && flyingObject.Subtype==subtype)
                {
                    return flyingObject;
                }
            }
            return null;
        }
    }
}