using System;

namespace MyPowerShell
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				var host = new PowerShellHost();
				Console.WriteLine("Hello World!");
			}
			catch (Exception ex)
			{
				Console.WriteLine("**** Exception ****");
				Console.WriteLine(ex.Message);
				Console.WriteLine();
				Console.WriteLine(ex.StackTrace);
			}

			Console.ReadKey();
		}
	}
}
