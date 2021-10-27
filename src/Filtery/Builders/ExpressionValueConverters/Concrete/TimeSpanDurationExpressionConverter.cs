using System;
using System.Text.RegularExpressions;

namespace Filtery.Builders.ExpressionValueConverters.Concrete
{
    internal class TimeSpanDurationExpressionConverter
    {
        public object Convert(object value)
        {
            const string Quantity = "quantity";
            const string Unit = "unit";

            const string Days = @"(d(ays?)?)";
            const string Hours = @"(h((ours?)|(rs?))?)";
            const string Minutes = @"(m((inutes?)|(ins?))?)";
            const string Seconds = @"(s((econds?)|(ecs?))?)";

            var timeSpanRegex = new Regex(
                $@"\s*(?<{Quantity}>\d+)\s*(?<{Unit}>({Days}|{Hours}|{Minutes}|{Seconds}|\Z))",
                RegexOptions.IgnoreCase);
            var matches = timeSpanRegex.Matches(value.ToString());

            var ts = new TimeSpan();
            foreach (Match match in matches)
            {
                if (Regex.IsMatch(match.Groups[Unit].Value, @"\A" + Days))
                {
                    ts = ts.Add(TimeSpan.FromDays(double.Parse(match.Groups[Quantity].Value)));
                }
                else if (Regex.IsMatch(match.Groups[Unit].Value, Hours))
                {
                    ts = ts.Add(TimeSpan.FromHours(double.Parse(match.Groups[Quantity].Value)));
                }
                else if (Regex.IsMatch(match.Groups[Unit].Value, Minutes))
                {
                    ts = ts.Add(TimeSpan.FromMinutes(double.Parse(match.Groups[Quantity].Value)));
                }
                else if (Regex.IsMatch(match.Groups[Unit].Value, Seconds))
                {
                    ts = ts.Add(TimeSpan.FromSeconds(double.Parse(match.Groups[Quantity].Value)));
                }
                else
                {
                    // Quantity given but no unit, default to Hours
                    ts = ts.Add(TimeSpan.FromHours(double.Parse(match.Groups[Quantity].Value)));
                }
            }

            return ts.Ticks;
        }
        public bool IsSatisfied(Type type, object value) => type == typeof(TimeSpanDuration);
    }
}