using System.Drawing;

namespace System.Web.Mvc
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Вычисление цвета по значению ячейки
        /// </summary>
        /// <param name="helper">Объект расширения класса</param>
        /// <param name="value">Значение ячейки</param>
        /// <returns>Цвет контента, контвертированный в стили HTML</returns>
        public static MvcHtmlString CalculateColor(this HtmlHelper helper, double value)
        {
            int R = 0, G = 0, B = 0;
            double? A = null;
            string result;
            Color resTextColor;

            /// в промежутке[-1; 0) задается как rgba(255, 140, 0, abs(value))
            if (-1.0 <= value && value < 0)
            {
                R = 255; G = 140; B = 0; A = Math.Abs(value);
            }
            ///в промежутке(0; 1) задается как rgba(0, 0, 0, abs(value))
            else if (0 < value && value <= 1.0)
            {
                R = 0; G = 0; B = 0; A = Math.Abs(value);
            }
            ///для значения 0 задается как rgb(255, 255, 255)
            else if (value == 0)
            {
                R = 255; G = 255; B = 255;
            }

            if ((R * 0.299 + G * 0.587 + B * 0.114) > 186.0 || A < 0.7)
            {
                resTextColor = Color.Black;
            }
            else
            {
                resTextColor = Color.White;
            }

            if (A != null)
                result = $"background-color:rgba({R},{G}, {B}, {A.ToString().Replace(',', '.')}); color:rgb({resTextColor.R},{resTextColor.G}, {resTextColor.B});";
            else
                result = $"background-color:rgb({R},{G}, {B}); color:rgba({resTextColor.R},{resTextColor.G}, {resTextColor.B}";

            return new MvcHtmlString(result);
        }
    }
}