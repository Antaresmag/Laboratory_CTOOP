using System;
using System.Collections.Generic;
using System.Linq;

namespace ChainOfResponsibility
{
	public interface IHandler
	{
		IHandler Next { get; set; }
		void Handle(ATM atm, Request request);
	}

	public class BaseHandler : IHandler
	{
		public IHandler Next { get; set; }

		public virtual void Handle(ATM atm, Request request)
		{
			Next?.Handle(atm, request);
		}
	}

	public class VerifyCashHandler : BaseHandler
	{
		public override void Handle(ATM atm, Request request)
		{
			if (request.Sum <= 0 || request.Sum > atm.Sum)
			{
				Console.WriteLine("Cash cannot be issued");
				return;
			}

			var sum = request.Sum;

			foreach (var banknote in atm.Cash
				.Where(x => x.Value > 0)
				.OrderByDescending(x => x.Key))
			{
				var amount = sum / banknote.Key;

				if (amount == 0)
					continue;

				if (amount > banknote.Value)
					amount = banknote.Value;

				sum -= banknote.Key * amount;
				request.CashToIssue.Add(banknote.Key, amount);
			}

			if (sum != 0)
			{
				Console.WriteLine("Cash cannot be issued");
				request.CashToIssue.Clear();
				return;
			}

			base.Handle(atm, request);
		}
	}

	public class IssueCashHandler : BaseHandler
	{
		public override void Handle(ATM atm, Request request)
		{
			foreach (var banknote in request.CashToIssue)
				atm.Cash[banknote.Key] -= banknote.Value;

			base.Handle(atm, request);
		}
	}

	public class ResultInformHandler : BaseHandler
	{
		public override void Handle(ATM atm, Request request)
		{
			Console.WriteLine($"{ request.Sum } UAH is issued:");

			foreach (var banknote in request.CashToIssue)
				Console.WriteLine($"{ banknote.Key } UAH x{ banknote.Value }");

			base.Handle(atm, request);
		}
	}

	public class Request
	{
		public int Sum { get; set; }

		public Dictionary<int, int> CashToIssue { get; set; } =
			new Dictionary<int, int>();

		public Request(int sum)
		{
			Sum = sum;
		}
	}

	public class ATM
	{
		public Dictionary<int, int> Cash { get; set; } =
			new Dictionary<int, int>();

		public int Sum => Cash.Sum(x => x.Key * x.Value);

		public void LoadCash()
		{
			var rand = new Random(DateTime.Now.Millisecond);

			Cash.Add(100, rand.Next(500));
			Cash.Add(50, rand.Next(500));
			Cash.Add(20, rand.Next(500));
			Cash.Add(5, rand.Next(500));
		}
	}

	public class Program
	{
		static void Main(string[] args)
		{
			var atm = new ATM();
			atm.LoadCash();

			IHandler handler = new VerifyCashHandler
			{
				Next = new IssueCashHandler
				{
					Next = new ResultInformHandler()
				}
			};

			while (true)
			{
				Console.Write("Enter sum: ");
				var sum = int.Parse(Console.ReadLine());
				handler.Handle(atm, new Request(sum));
			}
		}
	}
}
