using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.Units
{
    public class TimeRange
    {
        private DateTime _Start, _End;

        public TimeRange(DateTime start, DateTime end)
        {
            _Start = start;
            _End = end;
        }

        public DateTime Start
        { get { return _Start; } }
        public DateTime End
        { get { return _End; } }

        public List<DateTime> Milestones(Int32 interval, TimeUnit.Units timeUnit, Boolean normalized)
        {
            List<DateTime> _milestones = new List<DateTime>();

            DateTime _currentDate, _endDate;
            if(normalized)
            {
                _currentDate = GetNormalizedInitialDate(_Start, interval, timeUnit);
                _endDate = GetNormalizedEndDate(_End, interval, timeUnit);
            }
            else
            {
                _currentDate = _Start;
                _endDate = _End;
            }   
            
            while (_currentDate < _endDate)
            {
                _milestones.Add(_currentDate); 
                switch (timeUnit)
                {
                    case CSI.Library.Objects.Auxiliaries.Units.TimeUnit.Units.Day:
                        _currentDate = _currentDate.AddDays(interval);
                        break;
                    case CSI.Library.Objects.Auxiliaries.Units.TimeUnit.Units.Week:
                        _currentDate = _currentDate.AddDays(interval * 7);
                        break;
                    case CSI.Library.Objects.Auxiliaries.Units.TimeUnit.Units.Month:
                        _currentDate = _currentDate.AddMonths(interval);
                        break;
                    case CSI.Library.Objects.Auxiliaries.Units.TimeUnit.Units.Year:
                        _currentDate = _currentDate.AddYears(interval);
                        break;
                    default:
                        break;
                }
            }

            return _milestones;
        }

        public static DateTime GetNormalizedInitialDate(DateTime date, Int32 interval, TimeUnit.Units timeUnit)
        {
            switch (timeUnit)
            {
                case CSI.Library.Objects.Auxiliaries.Units.TimeUnit.Units.Week:
                    int delta = DayOfWeek.Monday - date.DayOfWeek;
                    return date.AddDays(delta);
                case CSI.Library.Objects.Auxiliaries.Units.TimeUnit.Units.Month:
                    return new DateTime(date.Year, date.Month, 1);
                case CSI.Library.Objects.Auxiliaries.Units.TimeUnit.Units.Year:
                    return new DateTime(date.Year, 1, 1);
                default:
                    return date;
            }
        }
        public static DateTime GetNormalizedEndDate(DateTime date, Int32 interval, TimeUnit.Units timeUnit)
        {
            switch (timeUnit)
            {
                case CSI.Library.Objects.Auxiliaries.Units.TimeUnit.Units.Week:
                    int delta = DayOfWeek.Sunday - date.DayOfWeek;
                    return date.AddDays(delta);
                case CSI.Library.Objects.Auxiliaries.Units.TimeUnit.Units.Month:
                    return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
                case CSI.Library.Objects.Auxiliaries.Units.TimeUnit.Units.Year:
                    return new DateTime(date.Year, 12, 31);
                default:
                    return date;
            }
        }
        public Boolean IsInRange(DateTime date)
        {
            return date >= _Start && date <= _End;
        }
    }
}
