using System;


namespace System.Web.Mvc
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString CalculateColor(this HtmlHelper helper, double value)
        {
            string result = "rgb(60, 60, 60)";

            /// в промежутке[-1; 0) задается как rgba(255, 140, 0, abs(value))
            if (-1.0 <= value && value < 0)
                result = $"background-color:rgba(255, 140, 0, { Math.Abs(value).ToString().Replace(',', '.')});";
            ///в промежутке(0; 1) задается как rgba(0, 0, 0, abs(value))
            else if (0 < value && value <= 1.0)
                result = $"background-color:rgba(0, 0, 0, {Math.Abs(value).ToString().Replace(',', '.')});";
            ///для значения 0 задается как rgb(255, 255, 255)
            else if(value == 0)
                result = $"background-color:rgb(255, 255, 255);";

            return new MvcHtmlString(result);
        }
    }
}