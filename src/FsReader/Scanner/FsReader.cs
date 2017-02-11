using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FsReader.Scanner
{
    public class FsReader
    {
		public void Read(string path)
		{
			List<string> dirs = new List<string>(Directory.GetFileSystemEntries(path));
			List<Entry> entries = new List<Entry>();
			List<Task> tasks = new List<Task>();

			foreach (string dir in dirs)
			{
				var et = new Entry(dir);

				entries.Add(et);
				tasks.Add( Task.Factory.StartNew(() => et.Analize()));
			}

			Task.WhenAll(tasks.ToArray()).Wait();

			foreach (var dir in entries)
			{
				dir.Print();
			}

			Console.WriteLine("FIN");
		}
    }
}
