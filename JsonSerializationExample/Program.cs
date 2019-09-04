using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonSerializationExample
{
	internal class Program
	{
		#region Methods

		private static void Main(string[] args)
		{
			var json = @"[
	{
		""v"" : ""1.0""
	},
	{
		""v"" : ""1.1""
	},
	{
		""v"" : ""1.2""
	},
	{
		""v"" : [
			""1.0"",
			""1.1""
		]
	}
]";
			var data = (JArray) JsonConvert.DeserializeObject(json);
			
			Console.WriteLine("JSON dynamic");
			
			foreach (var i in data)
			{
				Console.Write("\t");
				Console.WriteLine(((dynamic) i).v);
				Console.Write("\t\t");
				Console.WriteLine(((dynamic) i).v.GetType().FullName);
			}
			
			Console.WriteLine("");
			Console.WriteLine("JSON Type");

			var collection = JsonConvert.DeserializeObject<IEnumerable<VType>>(json);
			
			foreach (var i in collection)
			{
				Console.Write("\t");
				Console.WriteLine(i.v);
			}

			Console.ReadKey(true);
		}

		#endregion
	}

	public class VType
	{
		public string v;
	}
}