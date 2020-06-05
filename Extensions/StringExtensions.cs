using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssortedModdingTools.Extensions
{
	public static partial class Extensions
	{
		public static void RemoveSpaces(this string str) => str.Replace(" ", "");
	}
}
