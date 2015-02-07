namespace imitator
{
    /// <summary>
    /// Класс, описывающий летящую цель 
    /// </summary>
    public class FlyingObject
    {
        /// <summary>
        /// Тип цели
        /// </summary>
        public int Type;
        /// <summary>
        /// Подтип цели
        /// </summary>
        public int Subtype;

        /// <summary>
        /// Коэффициент лобового сопротивления
        /// </summary>
        public double Cx;
        /// <summary>
        /// Площадь миделевого сечения
        /// </summary>
        public double Sm;
        /// <summary>
        /// Интервал высот изменения электронной концентрации  искусственного следа
        /// </summary>
        public double dHis;
        /// <summary>
        /// Минимальная высота функционирования  генератора плазмы 
        /// </summary>
        public double Hmin;
        /// <summary>
        /// Параметры имитируемого ББ с помощью генератора плазмы
        /// </summary>
        public double Smf;
        /// <summary>
        /// Параметры имитируемого ББ с помощью генератора плазмы
        /// </summary>
        public double Cxf;

        public ShineDot[] ShineDots;
    }
}