using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace imitator
{

    public static class Imit43
    {
        #region входные и выходные параметры, константы

        public class InputData
        {
            /// <summary>
            /// Тип объекта
            /// </summary>
            public int Type { get; set; }
            /// <summary>
            /// Подтип объекта
            /// </summary>
            public int SubTupe { get; set; }

            public double H { get; set; } //Высота
            // Скорость полета БЦ, м/с
            public double V { get; set; }

            public double Angle { get; set; } //Угол
            public double Ne { get; set; } //Электронная концентрация плазмы в точке наблюдения
            public double Nu { get; set; } //Эффективная частота со-ударений электронов в точке наблю-дения
            public double NeKrit { get; set; }
            public double Delta { get; set; } //расстояние отхода удар-ной волны от поверхности баллистиче-ской цели
            public double Ageo { get; set; } //геометриче-ские пара-метры удар-ной волны
            public double Bgeo { get; set; } //геометриче-ские пара-метры удар-ной волны
        }

        /// <summary>
        /// ВХОДНАЯ ИНФОРМАЦИЯ
        /// </summary>
        public class InputDataLocal:ShineDot
        {
            /// <summary>
            /// Тип объекта
            /// </summary>
            public int Type{ get; set; }
            /// <summary>
            /// Подтип объекта
            /// </summary>
            public int SubTupe{ get; set; }

            public double H{ get; set; } //Высота
            // Скорость полета БЦ, м/с
            public double V{ get; set; }

            public double Angle{ get; set; } //Угол
            public double Ne{ get; set; } //Электронная концентрация плазмы в точке наблюдения
            public double Nu{ get; set; } //Эффективная частота со-ударений электронов в точке наблю-дения
            public double NeKrit{ get; set; }
            public double Delta{ get; set; } //расстояние отхода удар-ной волны от поверхности баллистиче-ской цели
            public double Ageo{ get; set; } //геометриче-ские пара-метры удар-ной волны
            public double Bgeo{ get; set; } //геометриче-ские пара-метры удар-ной волны
            
        }

        /// <summary>
        /// ВЫХОДНАЯ ИНФОРМАЦИЯ
        /// </summary>
        public class OutputData
        {
            public double Ssum { get; set; }
        }
        
        #endregion

        public static List<OutputData> Exec(List<InputData> aims)
        {
            List<OutputData> outIpr = new List<OutputData>();

            foreach (var aim in aims)
            {
                var aimWithParams=SetTypeInfo(aim);
                outIpr.Add(new OutputData(){Ssum = GetIPR(aimWithParams)});
            }
            return outIpr;
        }

        public static OutputData Exec(InputData aim)
        {
            var aimWithParams = SetTypeInfo(aim);
            return new OutputData() { Ssum = GetIPR(aimWithParams) };
        }

        private static double GetIPR(List<InputDataLocal> dots)
        {
            List<DotParams> NewDots = new List<DotParams>();
            //ВП0
            //Начальные значения частоты и длины волны излучения РЛС
            double LambdaK = (Const.C/Const.F0);
            double KLambda = 2*Math.PI/LambdaK;

            foreach (InputDataLocal dot in dots)
            {
                NewDots.Add(GetReflectionKoeff(LambdaK, KLambda, dot, Const.F0));
            }
            return GetSummaryEpr(NewDots.ToArray());
            
        }

        private static List<InputDataLocal> SetTypeInfo(InputData aim)
        {
            var dotList = new List<InputDataLocal>();
            
                foreach (var dot in Const.GetFlyingObject(aim.Type,aim.SubTupe).ShineDots)
                {
                    var newInputDataItem = new InputDataLocal
                    {
                        Kf = dot.Kf,
                        Xc = dot.Xc,
                        Yc = dot.Yc,
                        Omin = dot.Omin,
                        Omax = dot.Omax,
                        Kiz = dot.Kiz,
                        D = dot.D,
                        D1 = dot.D1,
                        D2 = dot.D2,
                        C = dot.C,
                        A = dot.A,
                        Gamma = dot.Gamma,
                        L = dot.L,
                        Rzatup = dot.Rzatup,
                        
                        H = aim.H,
                        V = aim.V,
                        Ne = aim.Ne,
                        NeKrit = aim.NeKrit,
                        Nu = aim.Nu,
                        Angle = aim.Angle,
                        Ageo = aim.Ageo,
                        Bgeo = aim.Bgeo,
                        Delta = aim.Delta
                    };

                    dotList.Add(newInputDataItem);
                }
            
            return dotList;
        }

        private static DotParams GetReflectionKoeff(double lambdaK, double kLambda, InputDataLocal dot, double fk)
        {
            double s0=0;
            double ips=0;
            double fi=0;
            double le=0;
            //число Маха 
                    double M = dot.V / 330;

            //Проверка условия освещенности для тел вращения
            if (!(dot.Omin <= dot.Angle && dot.Angle <= dot.Omax))
                return new DotParams() { fi = 0, s0 = 0 };

            double d = dot.Xc*Math.Cos(dot.Angle) + dot.Yc*Math.Sin(dot.Angle);
            fi = 4 * Math.PI * d/lambdaK;

            if (dot.Kf == 1)
            {
                //Расчет  модуля  ККО сферы
                if (dot.Ne < 1e11)
                {
                    s0 =
                    dot.Rzatup * Math.Sqrt(Math.PI);

                    double k = GetKp(dot, fk, kLambda);
                    return new DotParams { s0 = k * s0 * dot.Kiz, fi = fi };
                }
                
                if(dot.Angle<1.2)
                {
                    double Xk = dot.Ageo*dot.Ageo/
                        Math.Sqrt(dot.Ageo * dot.Ageo - dot.Bgeo * dot.Bgeo * Math.Tan(dot.Angle) * Math.Tan(dot.Angle));
                    double Dpl = Xk - (dot.Ageo + dot.Delta + dot.Rzatup);


                    if (Dpl<0)
                    {
                       var  SigmaPl = (
                            (M*M - 1)*(M*M - 1)*(1 + Math.Tan(dot.Angle)*Math.Tan(dot.Angle))*
                               Math.Sqrt((1 + Math.Tan(dot.Angle)*Math.Tan(dot.Angle)))*Math.PI*dot.Rzatup*dot.Rzatup
                               )/(
                              (M*M - 1 - Math.Tan(dot.Angle)*Math.Tan(dot.Angle))*
                              (M*M - 1 - Math.Tan(dot.Angle)*Math.Tan(dot.Angle))*Math.Cos(dot.Angle)
                              );
                       return new DotParams { s0 = Math.Sqrt(SigmaPl), fi = fi };
                    }
                    if (Dpl < 1)
                    {
                        double Yk = dot.Rzatup + Math.Tan(dot.Gamma)*Dpl;
                        double Ne =dot.NeKrit*dot.Rzatup*dot.Rzatup/(Yk*Yk);
                        if (Ne>1e11)
                        {
                            double Xpl = dot.Xc - Dpl;
                            double Ypl = dot.Bgeo * dot.Bgeo * Math.Tan(dot.Angle) /
                            Math.Sqrt(dot.Ageo * dot.Ageo - Math.Pow(dot.Bgeo * Math.Tan(dot.Angle), 2));
                            
                            fi = kLambda*(Xpl*Math.Cos(dot.Angle) + Ypl*Math.Sin(dot.Angle));
                        }
                        else return new DotParams();
                    }
                    else return new DotParams();
                        
                    double SigmaPL = Math.Pow((M * M - 1), 2) *
                                         Math.Sqrt(1 + Math.Tan(dot.Angle) * Math.Tan(dot.Angle)) * (1 + Math.Tan(dot.Angle) * Math.Tan(dot.Angle))
                                         * Math.PI * dot.Rzatup * dot.Rzatup /
                                         (
                                         Math.Pow(M * M - 1 - Math.Tan(dot.Angle) * Math.Tan(dot.Angle), 2) *
                                         Math.Cos(dot.Angle)
                                         );
                    s0 = Math.Sqrt(SigmaPL);
                    
                    return new DotParams { s0 = s0, fi = fi };
                }
            }
            else if (dot.Kf == 2)
            {
                //Расчет  модуля  ККО пластины
                s0 = 2 * Math.Sqrt(Math.PI) * dot.D * dot.L / lambdaK;

                ips = Math.PI / 2 - dot.Angle;
            }
            else if (dot.Kf == 3)
            {
                //Расчет  модуля  ККО цилиндрa
                s0 = dot.L * Math.Sqrt(Math.PI * dot.D / lambdaK);

                ips = Math.PI / 2 - dot.Angle;
            }
            else if (dot.Kf == 4)
            {
                double he = Math.Sqrt(
                    (dot.D1 + dot.D2) / (8 * Math.Cos(dot.Gamma))
                    );
                le = Math.Sqrt(0.25 * Math.Pow((dot.D2 - dot.D1), 2) + Math.Pow(dot.L, 2));

                s0 = (2 * Math.Sqrt(Math.PI / lambdaK)) * he * le;

                ips = Math.PI / 2 - (dot.Angle + dot.Gamma);
            }
            else if (dot.Kf == 5)
            {
                s0 = Math.Sqrt(
                    (Math.PI * dot.L * dot.L) /
                    (Math.Pow(Math.PI / 2, 2) + Math.Pow(Math.Log(Math.Abs(0.9 * Math.PI * dot.D / lambdaK), Math.E), 2))
                    );

                ips = Math.PI / 2 - dot.Angle;
            }
            else if (dot.Kf == 6)
            {
                s0 = dot.Kiz * Math.Sqrt(dot.D * lambdaK);
                double k = GetKp(dot, fk, kLambda);
                return new DotParams { s0 = k * s0 * dot.Kiz, fi = fi };
            }
            else if (dot.Kf == 7)
            {
                le = lambdaK*Math.Tan(dot.Gamma)/
                     (Math.Sqrt(8)*Math.PI);

                s0 = dot.D*Math.Sqrt(
                    (Math.PI*dot.D)/(3*lambdaK)
                    );

                ips = dot.Angle - dot.Gamma;
            }


            double l;
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

            if (dot.Kf == 2 || dot.Kf == 3 || dot.Kf == 4 || dot.Kf == 5 || dot.Kf == 7)
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
                        Math.Cos(ips) * Math.Sin(kLambda *l* Math.Sin(ips)) /
                        (kLambda * l * Math.Sin(ips))
                        );
                }
                if (Double.IsNaN(f)) f = 1;
                //Расчет модуля ККО при  произ-вольном угле облучения поверх-ности  тел
                s0 = s0*f;
            }

            if (dot.Kf == 8)
            {
                double arg = kLambda * dot.D * Math.Sin(dot.Angle);
                double besel = 1;

                if (Math.Abs(arg) > 0.00001)
                {
                    besel = MyMath.J1(arg) / arg;
                }

                s0 = Math.PI * Math.Sqrt(Math.PI) * dot.D * dot.D * Math.Cos(dot.Angle) * (besel) /
                    (lambdaK);

            }
            else if (dot.Kf == 9)
            {
                double om = MyMath.Actg(dot.D2 - dot.D1) / (2 * dot.L);
                s0 = (2 * Math.Sqrt(dot.D1) * dot.L * (dot.D2 - dot.D1) * Math.Cos(om - dot.Angle)) /
                    lambdaK * Math.Sqrt(dot.L * dot.L + 0.25 * (dot.D2 - dot.D1) * (dot.D2 - dot.D1));
            }
            else if (dot.Kf == 10)
            {
                double sigma = dot.Kiz *
                            Math.Pow(
                                (dot.C * dot.C * Math.Sin(dot.Angle) * Math.Sin(dot.Angle) +
                                 dot.A * dot.A * Math.Cos(dot.Angle) * Math.Cos(dot.Angle)), 3 / 2) / dot.C;
                s0 = Math.Sqrt(sigma);
            }

            double kp = GetKp(dot,fk, kLambda);
            return new DotParams { s0 = kp * s0 * dot.Kiz, fi = fi };
        }
        
        public static DotParams GetReflectionKoeff(ShineDot dot, double angle, double freq)
        {
            DotParams dotParams = new DotParams();

            double fk = Const.F0 + freq * Const.DeltaF;
            double lambdaK = Const.C / fk;
            double kLambda = 2 * Math.PI / lambdaK;

            double s0 = 0;
            double le = 0;
            double l;
            double ips = 0;


            //Проверка условия освещенности для тел вращения
            if (!(dot.Omin <= angle && angle <= dot.Omax))
                return new DotParams() { fi = 0, s0 = 0 };

            double d = dot.Xc * Math.Cos(angle) + dot.Yc * Math.Sin(angle);
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
                le = Math.Sqrt(0.25 * Math.Pow((dot.D2 - dot.D1), 2) + Math.Pow(dot.L, 2));

                s0 = (2 * Math.Sqrt(Math.PI / lambdaK)) * he * le;

                ips = Math.PI / 2 - (angle + dot.Gamma);
            }
            else if (dot.Kf == 5)
            {
                s0 = Math.Sqrt(
                    (Math.PI * dot.L * dot.L) /
                    (Math.Pow(Math.PI / 2, 2) + Math.Pow(Math.Log(Math.Abs(0.9 * Math.PI * dot.D / lambdaK), Math.E), 2))
                    );

                ips = Math.PI / 2 - angle;
            }
            else if (dot.Kf == 6)
            {
                s0 = dot.Kiz * Math.Sqrt(dot.D * lambdaK);

                ips = angle - (Math.PI / 2 - dot.Gamma / 2);
            }
            else if (dot.Kf == 7)
            {
                le = lambdaK * Math.Tan(dot.Gamma) /
                     (Math.Sqrt(8) * Math.PI);

                s0 = dot.D * Math.Sqrt(
                    (Math.PI * dot.D) / (3 * lambdaK)
                    );

                ips = angle - dot.Gamma;
            }
            else if (dot.Kf == 8)
            {
                double arg = lambdaK * kLambda * dot.D * Math.Sin(angle);
                double besel = 1;

                if (Math.Abs(arg) > 0.00001)
                {
                    besel = MyMath.J1(arg) / arg;
                }

                s0 = Math.PI * Math.Sqrt(Math.PI) * dot.D * dot.D * Math.Cos(angle) * (besel) /
                    (lambdaK);

                dotParams.s0 = s0;

            }
            else if (dot.Kf == 9)
            {
                double om = MyMath.Actg(dot.D2 - dot.D1) / 2 * dot.L;
                s0 = (2 * Math.Sqrt(dot.D1) * dot.L * (dot.D2 - dot.D1) * Math.Cos(om - angle)) /
                    lambdaK * Math.Sqrt(dot.L * dot.L + 0.25 * (dot.D2 - dot.D1));
            }
            else if (dot.Kf == 10)
            {
                double sigma = dot.Kiz *
                            Math.Pow(
                                (dot.C * dot.C * Math.Sin(angle) * Math.Sin(angle) +
                                 dot.A * dot.A * Math.Cos(angle) * Math.Cos(angle)), 3 / 2) / dot.C;
                s0 = Math.Sqrt(sigma);
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

            if (dot.Kf == 2 || dot.Kf == 3 || dot.Kf == 4 || dot.Kf == 5 || dot.Kf == 6 || dot.Kf == 7)
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
                        Math.Cos(ips) * Math.Sin(kLambda * l * Math.Sin(ips)) /
                        (kLambda * l * Math.Sin(ips))
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

        public static double GetKp(InputDataLocal i,double fk,double klambda)
        {
            double Xg = i.Rzatup + i.Delta + i.Ageo;
            double t = Math.Tan(i.Angle);
            double C0 = i.Yc - t*i.Xc;
            double C1 = i.Bgeo*i.Bgeo - i.Ageo*i.Ageo*t*t;
            double C2 = 2*i.Bgeo*i.Bgeo*Xg - 2*i.Ageo*i.Ageo*t*C0;
            double C3 = i.Bgeo * i.Bgeo * Xg * Xg - 
                i.Ageo*i.Ageo*C0*C0 -
                i.Bgeo*i.Bgeo*i.Ageo*i.Ageo;
            double C4 = Math.Sqrt(C2*C2-4*C1*C3);

            double X1 = -(C2 - C4)/(2*C1);
            double X2 = -(C2 + C4)/(2*C1);
            double Y1 = t*X1+C0;
            double Y2= t*X2+C0;

            double delta1 = Math.Sqrt(Math.Pow(i.Xc - X1, 2) + Math.Pow(i.Yc - Y1, 2));
            double delta2 = Math.Sqrt(Math.Pow(i.Xc - X2, 2) + Math.Pow(i.Yc - Y2, 2));
            double delta = Math.Min(delta1, delta2);
            //Расчет  электронной концентрации  и эффективной частоты столкновений электронов в точке прохождения ЭМВ от n-го ф.ц.р.
            double Ne;
            double NUc;
            if (i.Kf == 1)
            {
                 Ne = i.Ne;
                 NUc = i.Nu;
            }
            else
            {
                double yp = i.Yc - Math.Tan(i.Gamma)*i.Delta*Math.Cos(i.Angle);
                double q = (i.Rzatup / yp) * (i.Rzatup / yp);
                Ne = i.NeKrit*q;
                NUc = i.Nu*q;
            }
            //Расчет плазменной частоты
            double Fp = 8984*Math.Sqrt(Ne);
            //Расчет  отношения эффективной частоты столкновений электронов
            double mu2 = NUc/(Math.PI*2*fk);
            //    Расчет отношения плазменной ча-стоты к частоте излучения РЛС      
            double mu1 = Fp/fk;
            //Расчет модуля диэлектрической постоянной ПО
            double e = Math.Sqrt(
                Math.Pow((1 - (mu1*mu1)/(1 + mu2*mu2)), 2)+
                Math.Pow(((mu1 * mu1 * mu2) / (1 + mu2 * mu2)), 2)
                );
            //Расчет постоянной затухания ПО 
            double alfa=klambda*Math.Sqrt(
                0.5*(e - (1 - mu1*mu1/(1+mu2*mu2)))
                );

            //Расчет коэффициента пропускания ПО  
            double Kp = Math.Exp(-delta*alfa);

            //добавим Kizl
            if (0.5 * fk <= Fp && Fp <= 1.25 * fk)
            {
                Kp = 0.5 * Kp;
                i.Kiz = 0.5*i.Kiz;
            }
            else if (Fp > 1.25 * fk)
            {
                Kp = 0.1 * Kp;
                i.Kiz = 0.1 * i.Kiz;
            }

            return Kp;
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

            return SinSumm * SinSumm + CosSumm * CosSumm;
        }

       
    }


}
