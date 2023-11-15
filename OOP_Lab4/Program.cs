using System;

struct Date
{
    private int day;
    private int month;
    private int year;

    public Date(int day, int month, int year)
    {
        try
        {
            if (IsValidDate(day, month, year))
            {
                this.day = day;
                this.month = month;
                this.year = year;
            }
            else
            {
                throw new ArgumentException("Невірна дата");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    public Date(string dateString)
    {
        try
        {
            string[] parts = dateString.Split('.');
            int day = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int year = int.Parse(parts[2]);
            if (IsValidDate(day, month, year))
            {
                this.day = day;
                this.month = month;
                this.year = year;
            }
            else
            {
                throw new ArgumentException("Невірна дата");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    public static bool IsLeapYear(int year)
    {
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }

    public static bool IsValidDate(int day, int month, int year)
    {
        if (month >= 1 && month <= 12)
        {
            int[] daysInMonth = { 0, 31, IsLeapYear(year) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            return day >= 1 && day <= daysInMonth[month];
        }
        return false;
    }

    public static Date AddDays(Date date, int days)
    {
        try
        {
            DateTime tempDate = new DateTime(date.year, date.month, date.day);
            tempDate = tempDate.AddDays(days);
            return new Date(tempDate.Day, tempDate.Month, tempDate.Year);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
            return date;
        }
    }

    public static Date SubtractDays(Date date, int days)
    {
        try
        {
            DateTime tempDate = new DateTime(date.year, date.month, date.day);
            tempDate = tempDate.AddDays(-days);
            return new Date(tempDate.Day, tempDate.Month, tempDate.Year);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
            return date;
        }
    }

    public static bool IsLeapYear(Date date)
    {
        return IsLeapYear(date.year);
    }

    public static int CompareDates(Date date1, Date date2)
    {
        return DateTime.Compare(new DateTime(date1.year, date1.month, date1.day), new DateTime(date2.year, date2.month, date2.day));
    }

    public static int CalculateDaysDifference(Date date1, Date date2)
    {
        try
        {
            DateTime dateTime1 = new DateTime(date1.year, date1.month, date1.day);
            DateTime dateTime2 = new DateTime(date2.year, date2.month, date2.day);
            return (int)(dateTime2 - dateTime1).TotalDays;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
            return 0;
        }
    }

    public string ToCustomFormat()
    {
        return $"{month} {day}, {year}";
    }

    public override string ToString()
    {
        return $"{day:D2}.{month:D2}.{year:D4}";
    }
}

class Program
{
    static void Main()
    {
        try
        {
            Date[] dates = ReadDatesFromInput(2);
            DisplayDates(dates);

            SortDates(ref dates);
            DisplayDates(dates);

            ModifyDate(ref dates[0]);
            DisplayDates(dates);

            Date minDate, maxDate;
            GetMinMaxDate(dates, out minDate, out maxDate);
            Console.WriteLine($"Мінімальна дата: {minDate}, Максимальна дата: {maxDate}");

            int daysToAdd = 10;
            Date futureDate = Date.AddDays(dates[0], daysToAdd);
            Console.WriteLine($"Майбутня дата ({daysToAdd} днів пізніше): {futureDate}");

            int daysToSubtract = 5;
            Date pastDate = Date.SubtractDays(dates[1], daysToSubtract);
            Console.WriteLine($"Попередня дата ({daysToSubtract} днів раніше): {pastDate}");

            bool isLeapYear = Date.IsLeapYear(dates[0].year);
            Console.WriteLine($"Чи високосний рік: {isLeapYear}");

            int dateComparison = Date.CompareDates(dates[0], dates[1]);
            Console.WriteLine($"Результат порівняння дат: {dateComparison}");

            int daysDifference = Date.CalculateDaysDifference(dates[0], dates[1]);
            Console.WriteLine($"Різниця в днях: {daysDifference}");

            string customDateFormat = dates[0].ToCustomFormat();
            Console.WriteLine($"Дата у власному форматі: {customDateFormat}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    static Date[] ReadDatesFromInput(int n)
    {
        Date[] dates = new Date[n];
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Введіть дату {i + 1} (формат: дд.мм.рррр): ");
            string input = Console.ReadLine();
            dates[i] = new Date(input);
        }
        return dates;
    }

    static void DisplayDates(Date[] dates)
    {
        Console.WriteLine("Дати:");
        foreach (var date in dates)
        {
            Console.WriteLine(date);
        }
        Console.WriteLine();
    }

    static void SortDates(ref Date[] dates)
    {
        Array.Sort(dates, (d1, d2) => d1.ToString().CompareTo(d2.ToString()));
    }

    static void ModifyDate(ref Date date)
    {
        date = Date.AddDays(date, 5);
    }

    static void GetMinMaxDate(Date[] dates, out Date minDate, out Date maxDate)
    {
        minDate = maxDate = dates[0];
        foreach (var date in dates)
        {
            if (Date.CompareDates(date, minDate) < 0)
                minDate = date;
            if (Date.CompareDates(date, maxDate) > 0)
                maxDate = date;
        }
    }
}
