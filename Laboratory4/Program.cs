using System;
using System.Collections.Generic;
using System.IO;

namespace Bridge
{
	public interface IReportFormat
	{
		string Content { get; set; }
		void SaveReport(string reportName);
	}

	public class TxtReportFormat : IReportFormat
	{
		public string Content { get; set; }

		public void SaveReport(string reportName)
		{
			File.WriteAllText(reportName + ".txt", Content);
		}
	}

	public class HtmlReportFormat : IReportFormat
	{
		public string Content { get; set; }

		public void SaveReport(string reportName)
		{
			File.WriteAllText(
				reportName + ".html",
				$"<h1>{ Content }</h1>".Replace("\n", "<br/>"));
		}
	}

	public class Report
	{
		public IReportFormat ReportFormat;

		public Report(IReportFormat reportFormat)
		{
			ReportFormat = reportFormat;
		}

		public virtual void CreateReport(string path)
		{
			ReportFormat.SaveReport(path);
		}
	}

	public class DailyReport : Report
	{
		public Dictionary<string, int> DayResult = new Dictionary<string, int>();

		public DailyReport(IReportFormat reportFormat) : base(reportFormat)
		{
		}

		public override void CreateReport(string path)
		{
			ReportFormat.Content = "";

			foreach (var pair in DayResult)
			{
				ReportFormat.Content += $"{ pair.Key }: { pair.Value }₴\n";
			}

			base.CreateReport(path);
		}
	}

	public class WeeklyReport : Report
	{
		public int[] WeekResult = new int[7];

		public WeeklyReport(IReportFormat reportFormat) : base(reportFormat)
		{
		}

		public override void CreateReport(string path)
		{
			ReportFormat.Content = $"Понеділок: { WeekResult[0] }₴\n";
			ReportFormat.Content += $"Вівторок: { WeekResult[1] }₴\n";
			ReportFormat.Content += $"Середа: { WeekResult[2] }₴\n";
			ReportFormat.Content += $"Четвер: { WeekResult[3] }₴\n";
			ReportFormat.Content += $"П'ятниця: { WeekResult[4] }₴\n";
			ReportFormat.Content += $"Субота: { WeekResult[5] }₴\n";
			ReportFormat.Content += $"Неділя: { WeekResult[6] }₴";

			base.CreateReport(path);
		}
	}

	public class Program
	{
		static void Main(string[] args)
		{
			var dailyReport = new DailyReport(new TxtReportFormat())
			{
				DayResult = new Dictionary<string, int>
				{
					{ "Вологі серветки", 9800 },
					{ "Носові серветки", 5600 },
					{ "Рідке мило", 8350 },
					{ "Дизинфікуючий засіб", 1100 }
				}

			};

			dailyReport.CreateReport(DateTime.Now.ToString("dd-MM-yyyy" + "Дений звіт"));
			dailyReport.ReportFormat = new HtmlReportFormat();
			dailyReport.CreateReport(DateTime.Now.ToString("dd-MM-yyyy" + "Дений звіт"));

			var weeklyReport = new WeeklyReport(new TxtReportFormat())
			{
				WeekResult = new int[7] { 21500, 18300, 20950, 17800, 22600, 19300, 19150 }
			};

			weeklyReport.CreateReport(DateTime.Now.ToString("dd-MM-yyyy" + "Тижневий звіт") + " " + DateTime.Now.ToString("dd-MM-yyyy" + "Тижневий звіт"));
			weeklyReport.ReportFormat = new HtmlReportFormat();
			weeklyReport.CreateReport(DateTime.Now.AddDays(-7).ToString("dd-MM-yyyy" + "Тижневий звіт") + " " + DateTime.Now.ToString("dd-MM-yyyy" + "Тижневий звіт"));

			Console.WriteLine("Звіти створені! Можна закрити консоль і зайти у папаку Дебаг");

			Console.ReadKey();
		}
	}
}
