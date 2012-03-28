using System;
using System.Net;

namespace Raintv.Common
{
	public class CookieAwareWebClient : WebClient
	{
		[System.Security.SecuritySafeCritical]
		public CookieAwareWebClient()
			: base(){}

		public CookieContainer CookieContainer = new CookieContainer();

		protected override WebRequest GetWebRequest(Uri address)
		{
			var request = base.GetWebRequest(address);
			if (request is HttpWebRequest)
			{
				(request as HttpWebRequest).CookieContainer = CookieContainer;
			}
			return request;
		}
	}
}
