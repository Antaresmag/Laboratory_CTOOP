using System;
using System.Threading;

namespace State
{
	class Program
	{
		static void Main(string[] args)
		{
			var trafficLights = new TrafficLights();
			trafficLights.TurnOn();
			Console.ReadKey();
		}
	}

	class TrafficLights
	{
		public ITrafficLightsState State { get; set; }

		public void TurnOn()
		{
			State = new GreenLightState();

			while (true)
			{
				State.ForbidTraffic(this);
				State.ForbidTraffic(this);
				State.AllowTraffic(this);
				State.AllowTraffic(this);
			}
		}
	}

	interface ITrafficLightsState
	{
		void AllowTraffic(TrafficLights trafficLights);
		void ForbidTraffic(TrafficLights trafficLights);
		void RefreshView();
	}

	class RedLightState : ITrafficLightsState
	{
		public void AllowTraffic(TrafficLights trafficLights)
		{
			trafficLights.State.RefreshView();
			trafficLights.State = new YellowLightState();
		}

		public void ForbidTraffic(TrafficLights trafficLights)
		{
			trafficLights.State.RefreshView();
		}

		public void RefreshView()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine('o');
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine('o');
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine('o');
			Thread.Sleep(5000);
		}
	}

	class YellowLightState : ITrafficLightsState
	{
		public void AllowTraffic(TrafficLights trafficLights)
		{
			trafficLights.State.RefreshView();
			trafficLights.State = new GreenLightState();
		}

		public void ForbidTraffic(TrafficLights trafficLights)
		{
			trafficLights.State.RefreshView();
			trafficLights.State = new RedLightState();
		}

		public void RefreshView()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine('o');
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine('o');
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine('o');
			Thread.Sleep(2000);
		}
	}

	class GreenLightState : ITrafficLightsState
	{
		public void AllowTraffic(TrafficLights trafficLights)
		{
			trafficLights.State.RefreshView();
		}

		public void ForbidTraffic(TrafficLights trafficLights)
		{
			trafficLights.State.RefreshView();
			trafficLights.State = new YellowLightState();
		}

		public void RefreshView()
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine('o');
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine('o');
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine('o');
			Thread.Sleep(5000);
		}
	}
}
