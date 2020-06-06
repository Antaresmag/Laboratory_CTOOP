using System;
using System.Linq;

namespace Proxy
{
	interface IAccount
	{
		string Id { get; set; }

		double Balance { get; set; }

		string Password { get; set; }

		void PrintBalance();

		void Withdraw(double amount);
	}

	class Account : IAccount
	{
		public string Id { get; set; }

		public double Balance { get; set; }

		public string Password { get; set; }

		public void PrintBalance()
		{
			Console.WriteLine($"Баланс: ${ string.Format("{0:0.00}", Balance) }.");
		}

		public void Withdraw(double amount)
		{
			if (Balance >= amount)
			{
				Balance -= amount;
				Console.WriteLine($"${ string.Format("{0:0.00}", amount) } було вилучено.");
			}
			else
			{
				Console.WriteLine("Не вистачає грошей на рахунку.");
			}

			PrintBalance();
		}
	}

	class Client
	{
		public string FirstName { get; set; }

		public string MiddleName { get; set; }

		public string LastName { get; set; }

		public Account Account { get; set; }
	}

	class ATM : IAccount
	{
		private Account _account;

		private bool IsAuthorized => _account != null;

		public string Id
		{
			get => IsAuthorized ? _account.Id : throw new UnauthorizedAccessException();
			set
			{
				if (IsAuthorized)
					throw new UnauthorizedAccessException();

				_account.Id = value;
			}
		}

		public double Balance
		{
			get => IsAuthorized ? _account.Balance : throw new UnauthorizedAccessException();
			set
			{
				if (IsAuthorized)
					throw new UnauthorizedAccessException();

				_account.Balance = value;
			}
		}

		public string Password
		{
			get => IsAuthorized ? _account.Password : throw new UnauthorizedAccessException();
			set
			{
				if (IsAuthorized)
					throw new UnauthorizedAccessException();

				_account.Password = value;
			}
		}

		public ATM(Account account)
		{
			SetAccount(account);
		}

		public void SetAccount(Account account)
		{
			if (IsAuthorized)
				Quit();

			do
			{
				Console.Write("Пароль: ");

				if (Console.ReadLine() == account.Password)
				{
					_account = account;
					Console.WriteLine("Успіх!");
				}
				else
				{
					Console.WriteLine("Неправильний пароль.");
				}
			} while (!IsAuthorized);
		}

		public void PrintBalance()
		{
			_account.PrintBalance();
		}

		public void Withdraw(double amount)
		{
			_account.Withdraw(amount);
		}

		public void Quit()
		{
			_account = null;
		}
	}

	class Program
	{
		static void Main()
		{
			var client = new Client
			{
				FirstName = "Oleksii",
				MiddleName = "Ivanovych",
				LastName = "Kunderenko",
				Account = new Account
				{
					Id = Guid.NewGuid().ToString(),
					Balance = 100,
					Password = "1234"
				}
			};

			var atm = new ATM(client.Account);

			while (true)
			{
				Console.Write("Команда: ");
				var request = Console.ReadLine()
					.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

				if (request.Length <= 0)
				{
					Console.WriteLine("Введiть команду.");
					continue;
				}

				try
				{
					switch (request[0])
					{
						case "/вивести":
							if (request.Length != 2)
							{
								Console.WriteLine("/команда виведення повинна містити - 1 параметр.");
								continue;
							}

							if (double.TryParse(request[1], out var amount) && amount > 0)
							{
								atm.Withdraw(amount);
							}
							else
							{
								Console.WriteLine("Неправильна сума.");
							}
							break;
						case "/баланс":
							atm.PrintBalance();
							break;
						case "/вих":
							atm.Quit();
							return;
						default:
							Console.WriteLine("Неправильна команда.");
							break;
					}
				}
				catch (UnauthorizedAccessException)
				{
					Console.WriteLine("Немає доступу.");
				}
			}
		}
	}
}
