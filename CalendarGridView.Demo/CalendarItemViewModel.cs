using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarGridView.Demo
{
    public class CalendarItemViewModel : ICalendarItem
    {
        public DateTime Date { get; private set; }

        public string ReadableDate => Date.ToString("ddd, d MMM yy");

        public string Test { get; set; }

        public CalendarItemViewModel(DateTime date)
        {
            Date = date;
            Test = Guid.NewGuid().ToString();
        }
    }
}
