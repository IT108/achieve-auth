using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServer.Infrastructure.Constants
{
	public static class Domains
	{

		private static Dictionary<string, string> AllowedDomains =  new Dictionary<string, string>(){
			{"IT108", "it108.org" }
		};

		public static bool IsValid(string displayName)
		{
			return AllowedDomains.ContainsKey(displayName);
		}
	}
}
