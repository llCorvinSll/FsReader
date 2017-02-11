using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.CommandLineUtils;

namespace FsReader
{
    public class Program
    {
        public static void Main(string[] args)
        {
			CommandLineApplication app = new CommandLineApplication(throwOnUnexpectedArg: true);

			app.Argument(
				"path",
				"", 
				false
			);

			CommandOption greeting = app.Option(
				"-$|-p |--path <greeting>",
				"path to analyzed directory",
				CommandOptionType.SingleValue);





			app.OnExecute(() =>
			{
				if (greeting.HasValue())
				{
					var reader = new Scanner.FsReader();

					Console.WriteLine($"{greeting.ValueName} - {greeting.Value()}");

					reader.Read(greeting.Value());

					Console.ReadKey();
				}

				return 0;
			});

			app.Execute(args);
        }
    }
}
