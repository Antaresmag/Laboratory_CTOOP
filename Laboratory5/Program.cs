using System;
using System.Collections.Generic;
using System.Linq;

namespace Strategy
{
	public interface IOrderStrategy
	{
		IEnumerable<string> Order(IEnumerable<string> strings);
	}

	public class OrderAlphabeticallyStrategy : IOrderStrategy
	{
		public IEnumerable<string> Order(IEnumerable<string> strings)
		{
			return strings.OrderBy(x => x);
		}
	}

	public class OrderByLengthStrategy : IOrderStrategy
	{
		public IEnumerable<string> Order(IEnumerable<string> strings)
		{
			return strings.OrderBy(x => x.Length);
		}
	}

	public class StringContainer
	{
		public List<string> Strings { get; set; }

		public IOrderStrategy OrderStrategy { get; set; }

		public StringContainer(List<string> strings)
		{
			Strings = strings;
		}

		public void Order()
		{
			Strings = OrderStrategy.Order(Strings).ToList();
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			Console.Write("Введiть потрiбнi слова через пробел: ");
			var input = Console.ReadLine();
			var strings = input.Split(' ');

			var container = new StringContainer(strings.ToList());

			container.OrderStrategy = new OrderAlphabeticallyStrategy();
			container.Order();
			Console.WriteLine('\n' + container.OrderStrategy.GetType().Name + " Слова за алфавiтом:" + ": ");

			foreach (var str in container.Strings)
			{
				Console.WriteLine(str);
			}

			container.OrderStrategy = new OrderByLengthStrategy();
			container.Order();
			Console.WriteLine('\n' + container.OrderStrategy.GetType().Name + " Слова за кількiстю букв" + ": ");

			foreach (var str in container.Strings)
			{
				Console.WriteLine(str);
			}

			Console.ReadKey();
		}
	}
}
