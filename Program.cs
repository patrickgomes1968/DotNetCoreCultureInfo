using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
public class Example
{
   public static void Main()
   {
      Console.WriteLine("Using the Persian Calendar:");
      PersianCalendar persian = new PersianCalendar();
     // new PersianCalendar().ToDateTime(year, month, day, "persian");
      DateTime date1 = persian.ToDateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0,0); // ToDateTime(1389, 5, 27, 0,0,0,0);
      Console.WriteLine(date1.ToString());
      Console.WriteLine("{0}/{1}/{2}\n", persian.GetMonth(date1), 
                                       persian.GetDayOfMonth(date1), 
                                       persian.GetYear(date1));

      Console.WriteLine("Using the Hijri Calendar:");
      // Get current culture so it can later be restored.
      //CultureInfo dftCulture = Thread.CurrentThread.CurrentCulture;  //Not for .Net Core
      CultureInfo dftCulture = CultureInfo.DefaultThreadCurrentCulture; //for .net Core

      // Define Hijri calendar.
      HijriCalendar hijri = new HijriCalendar();
      // Make ar-SY the current culture and Hijri the current calendar.
      //Thread.CurrentThread.CurrentCulture = new CultureInfo("ar-SY"); //Not for .net Core
      CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ar-SY"); //for .net Core
      CultureInfo current = CultureInfo.CurrentCulture;
      current.DateTimeFormat.Calendar = hijri;
      string dFormat = current.DateTimeFormat.ShortDatePattern;
      // Ensure year is displayed as four digits.
      dFormat = Regex.Replace(dFormat, "/yy$", "/yyyy");
      current.DateTimeFormat.ShortDatePattern = dFormat;
      // DateTime date2 = new DateTime(1431, 9, 9, hijri); //Not for .net Core
      DateTime date2 = hijri.ToDateTime(1439,1,10, 0,0,0,0); // for .net Core
      Console.WriteLine("{0} culture using the {1} calendar: {2:D}", current, 
                        GetCalendarName(hijri), date2);

      // Restore previous culture.
      //Thread.CurrentThread.CurrentCulture = dftCulture;// Not for .netCore
      CultureInfo.DefaultThreadCurrentCulture = dftCulture; //for .net Core
      Console.WriteLine("{0} culture using the {1} calendar: {2:D}", 
                        CultureInfo.CurrentCulture, 
                        GetCalendarName(CultureInfo.CurrentCulture.Calendar), 
                        date2); 
   }

   private static string GetCalendarName(Calendar cal)
   {
      return Regex.Match(cal.ToString(), "\\.(\\w+)Calendar").Groups[1].Value;
   }
}