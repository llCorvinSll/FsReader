using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FsReader.Scanner
{
	public class Entry
	{
		public Entry(string path)
		{
			_path = path;
			_attrs = File.GetAttributes(_path);

			_children = new List<Entry>();


		}

		public async Task Analize()
		{
			if (isDir)
			{
				await ReadChildren();
			}

			if (!isDir)
			{
				FileInfo f = new FileInfo(_path);
				if (f.Exists)
				{
					_size = f.Length;
				}
			}
			else
			{
				foreach (var child in _children)
				{
					_size += child.Size;
				}
			}
		}

		public long Size => _size;

		public string Path => $"{_path}";

		public bool isDir => (_attrs & FileAttributes.Directory) == FileAttributes.Directory;

		public void Print()
		{
			var header = isDir ? "dir :" : "file:";

			Console.WriteLine($"{header} {Path} [{FormatSize(_size)}]");
		}

		private async Task ReadChildren()
		{
			List<string> dirs = new List<string>(Directory.GetFileSystemEntries(_path));
			foreach (string dir in dirs)
			{
				var ne = new Entry(dir);
				_children.Add(ne);

				await ne.Analize();
			}


		}

		private static string FormatSize(long size)
		{
			string[] sizes = { "B", "KB", "MB", "GB", "TB" };

			int order = 0;
			while (size >= 1024 && order < sizes.Length - 1)
			{
				order++;
				size = size / 1024;
			}

			// Adjust the format string to your preferences. For example "{0:0.#}{1}" would
			// show a single decimal place, and no space.
			return String.Format("{0:0.##} {1}", size, sizes[order]);
		}

		private string _path;

		private long _size;

		private List<Entry> _children;

		private FileAttributes _attrs;
	}
}
