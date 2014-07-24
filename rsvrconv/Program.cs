// Copyright (C) 2014 Aura Project
// 
// Written by exec (exec@mabinoger.de)
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.IO;

namespace rsvrconv
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length < 1)
			{
				Console.WriteLine("Usage: rsvrconv <file_name.mp3> [file_name.mp3] ...");
				Console.WriteLine("Press [Enter] to exit.");
				Console.ReadLine();
				return;
			}

			Console.WriteLine("rsvrconv v1.0.0");
			Console.WriteLine();
			Console.WriteLine("Files: {0}", args.Length);
			Console.WriteLine("Let's begin!");

			int i = 0;
			foreach (var arg in args)
			{
				Console.Write("{0}/{1}\r", i++, args.Length);

				if (!File.Exists(arg) || Path.GetExtension(arg) != ".mp3")
				{
					Console.WriteLine("Not a valid file: {0}", Path.GetFileName(arg));
					continue;
				}

				try
				{
					using (var inf = new FileStream(arg, FileMode.Open))
					using (var outf = new FileStream(Path.ChangeExtension(arg, null), FileMode.Create))
					{
						while (inf.Position < inf.Length)
						{
							var ob = inf.ReadByte();
							var b = (byte)Math.Floor(ob / 2f);
							if (ob % 2 != 0)
								b += 128;

							outf.WriteByte(b);
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error while processing '{0}': {1}", arg, ex.Message);
				}
			}

			Console.WriteLine("Done. Press [Enter] to exit.");
			Console.ReadLine();
		}
	}
}
