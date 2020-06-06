using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Template
{
	public abstract class FileProcessor<T>
	{
		protected FileStream _fileStream;

		public T Data { get; set; }

		public void Execute(string fileName)
		{
			if (_fileStream == null)
				return;

			OpenFile(fileName);
			ProcessFile();
			CloseFile();
		}

		protected void OpenFile(string fileName)
		{
			_fileStream = File.Open(fileName, FileMode.OpenOrCreate);
		}

		public abstract void ProcessFile();

		protected void CloseFile()
		{
			_fileStream?.Close();
		}
	}

	public class FileReader : FileProcessor<List<int>>
	{
		public override void ProcessFile()
		{
			if (_fileStream == null || !_fileStream.CanRead)
				return;

			var bytes = new byte[1024];
			_fileStream.Read(bytes, 0, bytes.Length);
			Data = bytes.Select(x => (int)x).ToList();
		}
	}

	public class FileWriter : FileProcessor<List<int>>
	{
		public FileWriter(List<int> data)
		{
			Data = data;
		}

		public override void ProcessFile()
		{
			if (_fileStream == null || !_fileStream.CanRead)
				return;

			byte[] bytes = new byte[Data.Count * sizeof(int)];
			Buffer.BlockCopy(Data.ToArray(), 0, bytes, 0, bytes.Length);
			_fileStream.Write(bytes, 0, bytes.Length);
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var writer = new FileWriter(new List<int> { 19, -26, 1998, 6, -8 });
			writer.Execute("file.dat");

			var reader = new FileReader();
			reader.Execute("file.dat");

			foreach (var num in writer.Data)
			{
				Console.WriteLine(num);
			}

			Console.ReadKey();
		}
	}
}
