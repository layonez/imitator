using System;

namespace imitator
{
    
    public static class Imit33
    {
        private const double F0 = 3e9;
        private const double Df = 5e6;
        private const double C = 3e8;
        
        public static double[,] Exec(ShineDot[] dots)
        {
            double[,] outputData = new double[180,32];

            //Организация цикла по углу
            for (int ang = 0; ang < 180; ang++)
            {
                double angle = ang * Math.PI/180;
                //Организация цикла по частотам
                for (int freq = 0; freq < 32; freq++)
                {
                    DotParams[] resList = new DotParams[dots.Length];

                    //Организация цикла по блестящим точкам (центрам отражения)
                    for (int i=0; i<dots.Length; i++)
                    {
                        resList[i] = GetReflectionKoeff(dots[i], angle, freq);
                    }
                    //Считаем интеренфереционную картину
                    outputData[ang, freq] = GetSummaryEpr(resList);
                }
            }

            return outputData;
        }

        private static double GetSummaryEpr(DotParams[] resList)
        {
            double SinSumm = 0;
            double CosSumm = 0;

            foreach (DotParams dot in resList)
            {
                SinSumm += dot.s0 * Math.Sin(dot.fi);
                CosSumm += dot.s0 * Math.Cos(dot.fi);
            }

            return SinSumm*SinSumm + CosSumm*CosSumm;
        }

        public static DotParams GetReflectionKoeff(ShineDot dot, double angle, double freq)
        {
            DotParams dotParams = new DotParams();

            double fk = F0 + freq * Df;
            double lambdaK = C / fk;
            double kLambda = 2 * Math.PI / lambdaK;

            double s0 = 0;
            double le = 0;
            double l;
            double ips=0;
            
            
            //Проверка условия освещенности для тел вращения
            if (!(dot.Omin <= angle && angle <= dot.Omax))
                return new DotParams(){fi = 0,s0=0};

            double d = dot.Xc*Math.Cos(angle) + dot.Yc*Math.Sin(angle);
            //Расчет двойного набега фаз
            dotParams.fi = 4 * Math.PI * d / lambdaK;

            if (dot.Kf == 1)
            {
                //Расчет  модуля  ККО сферы
                s0 = 
                    dot.Rzatup * Math.Sqrt(Math.PI)
                    ;
                //ips = Math.PI/2 - angle;

                dotParams.s0 = s0;
                return dotParams;
            }
            else if (dot.Kf == 2)
            {
                //Расчет  модуля  ККО пластины
                s0 = 2 * Math.Sqrt(Math.PI) * dot.D * dot.L / lambdaK;

                ips = Math.PI / 2 - angle;
            }
            else if (dot.Kf == 3)
            {
                //Расчет  модуля  ККО цилиндрa
                s0 = dot.L * Math.Sqrt(Math.PI * dot.D / lambdaK);

                ips = Math.PI / 2 - angle;
            }
            else if (dot.Kf == 4)
            {
                double he = Math.Sqrt(
                    (dot.D1 + dot.D2) / (8 * Math.Cos(dot.Gamma))
                    );
                le = Math.Sqrt( 0.25 * Math.Pow( (dot.D2 - dot.D1) , 2) + Math.Pow(dot.L,2) );

                s0 = (2 * Math.Sqrt(Math.PI / lambdaK)) * he * le;

                ips = Math.PI/2 - (angle + dot.Gamma);
            }
            else if (dot.Kf == 5)
            {
                s0 = Math.Sqrt(
                    (Math.PI * dot.L * dot.L) / 
                    ( Math.Pow(Math.PI/2,2) + Math.Pow( Math.Log(Math.Abs(0.9*Math.PI*dot.D / lambdaK) ,Math.E) , 2) )
                    );

                ips = Math.PI / 2 - angle;
            }
            else if (dot.Kf == 6)
            {
                s0 = dot.Kiz * Math.Sqrt(dot.D * lambdaK);

                ips = angle -( Math.PI / 2 - dot.Gamma / 2);
            }
            else if (dot.Kf == 7)
            {
                le = lambdaK*Math.Tan(dot.Gamma)/
                     (Math.Sqrt(8) * Math.PI);

                s0 = dot.D * Math.Sqrt(
                    (Math.PI * dot.D) / (3 * lambdaK)
                    );

                ips = angle - dot.Gamma;
            }
            else if (dot.Kf == 8)
            {
                double arg = lambdaK*kLambda*dot.D*Math.Sin(angle);
                double besel = 1;

                if (Math.Abs(arg) > 0.00001)
                {
                    besel = MyMath.J1(arg)/arg;
                }

                s0 = Math.PI * Math.Sqrt(Math.PI) * dot.D * dot.D * Math.Cos(angle) * (besel) / 
                    (lambdaK);
                  
                dotParams.s0 = s0;
                
            }
            else if (dot.Kf == 9)
            {
                double om = MyMath.Actg(dot.D2 - dot.D1)/2*dot.L;
                s0 = (2*Math.Sqrt(dot.D1)*dot.L*(dot.D2 - dot.D1)*Math.Cos(om - angle))/
                    lambdaK * Math.Sqrt(dot.L * dot.L + 0.25 * (dot.D2 - dot.D1));

                dotParams.s0 = s0;
            }
            else if (dot.Kf == 10)
            {
                double x = (dot.C*dot.C*Math.Sin(angle)*Math.Sin(angle) +
                            dot.A*dot.A*Math.Cos(angle)*Math.Cos(angle));
                double sigma = dot.Kiz*
                            x* Math.Sqrt(x)/dot.C;
                s0 = Math.Sqrt(sigma);

                dotParams.s0 = s0;
            }

            if (dot.Kf == 7 || dot.Kf == 4)
            {
                l = le;
            }
            else if (dot.Kf == 8)
            {
                l = dot.Xc;
            }
            else
            {
                l = dot.L;
            }

            if (dot.Kf == 2 ||dot.Kf == 3 ||dot.Kf == 4 ||dot.Kf == 5 ||dot.Kf == 6 ||dot.Kf == 7)
            {
                double f;
                if (Math.Abs(ips) < 0.001)
                {
                    f = 1;
                }
                else
                {
                    //Расчет диаграммного множите-ля для тел
                    f = Math.Abs(
                        Math.Cos(ips)*Math.Sin(kLambda*l*Math.Sin(ips))/
                        (kLambda*l*Math.Sin(ips))
                        );
                }
                if (Double.IsNaN(f)) f = 1;
                //Расчет модуля ККО при  произ-вольном угле облучения поверх-ности  тел
                dotParams.s0 = s0 * f;

                return dotParams;
            }

            //todo обработка kf 9,10
            return dotParams;
        }

    }

    public class ShineDot 
    {
        public int Kf;
        public double Xc;
        public double Yc;
        public double Rzatup;
        public double D1;
        public double D2;
        public double L;
        public double D;
        public double Kiz;
        public double A;
        public double C;
        public double Omin;
        public double Omax;
        public double Gamma;
    }

    public struct DotParams
    {
        public double s0;
        public double fi;
    }

    public class MyMath
    {
        public static double Actg(double x)
        {
            if (x >= 0) 
                return Math.Asin(1 / Math.Sqrt(1 + Math.Sqrt(x)));
            
            return Math.PI - Math.Asin(1 / Math.Sqrt(1 + Math.Sqrt(x)));
        }

        /// <summary>
        /// Возвращает значение функции Бесселя 1го рода
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double J1(double x)
        {
            double ax;
            double y;
            double ans1, ans2;

            if ((ax = Math.Abs(x)) < 8.0)
            {
                y = x * x;
                ans1 = x * (72362614232.0 + y * (-7895059235.0 + y * (242396853.1
                    + y * (-2972611.439 + y * (15704.48260 + y * (-30.16036606))))));
                ans2 = 144725228442.0 + y * (2300535178.0 + y * (18583304.74
                    + y * (99447.43394 + y * (376.9991397 + y * 1.0))));

                return ans1 / ans2;
            }

            double z = 8.0 / ax;
            double xx = ax - 2.356194491;
            y = z * z;

            ans1 = 1.0 + y * (0.183105e-2 + y * (-0.3516396496e-4
                                                 + y * (0.2457520174e-5 + y * (-0.240337019e-6))));
            ans2 = 0.04687499995 + y * (-0.2002690873e-3
                                        + y * (0.8449199096e-5 + y * (-0.88228987e-6
                                                                      + y * 0.105787412e-6)));
            double ans = Math.Sqrt(0.636619772 / ax) *
                         (Math.Cos(xx) * ans1 - z * Math.Sin(xx) * ans2);
            if (x < 0.0) ans = -ans;

            return ans;
        }
    }


}
