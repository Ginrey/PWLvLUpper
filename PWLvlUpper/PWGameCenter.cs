using System;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using xNet.Collections;
using xNet.Net;

namespace PWLvlUpper
{
	internal class PWGameCenter
	{
		private HttpRequest request;

		private static System.Text.RegularExpressions.Regex regAccessToken = new System.Text.RegularExpressions.Regex("access_token\":\"(.*?)\"", System.Text.RegularExpressions.RegexOptions.Compiled);

		private static System.Text.RegularExpressions.Regex regLocation = new System.Text.RegularExpressions.Regex("Location=\"(.*?)\"", System.Text.RegularExpressions.RegexOptions.Compiled);

		private static System.Text.RegularExpressions.Regex regPersList = new System.Text.RegularExpressions.Regex("Pers Id=\"(.*?)\" Title=\"(.*?)\"", System.Text.RegularExpressions.RegexOptions.Compiled);

		private static System.Text.RegularExpressions.Regex regAutoLogin = new System.Text.RegularExpressions.Regex("PersId=\"(.*?)\" Key=\"(.*?)\"", System.Text.RegularExpressions.RegexOptions.Compiled);

		public void Init(ProxyClient proxy = null)
		{
            proxy.CreateConnection(endpoint.Address.ToString(), endpoint.Port, null).Client;
            this.request = new HttpRequest();
			this.request.AllowAutoRedirect=(false);
			this.request.Cookies=(new CookieDictionary(false));
			this.request.UserAgent= "Downloader/12200";
			this.request.Proxy=(proxy);
		}

		private RequestParams createTokenParams(string email, string password)
		{
			RequestParams requestParams = new RequestParams();
			requestParams["client_id"] = "gamecenter.mail.ru";
			requestParams["grant_type"] = "password";
			requestParams["username"] = email;
			requestParams["password"] = password;
			return requestParams;
		}

		private string getAuthMail(string email, string password)
		{
			HttpResponse httpResponse = this.request.Post(GlobalConfig.ServerStrings[29], this.createTokenParams(email, password), true);
			if (httpResponse.ToString().Contains("invalid username or password"))
			{
				throw new System.Exception("wrong");
			}
			if (httpResponse.ToString().Contains("user is blocked"))
			{
				throw new System.Exception("user is blocked");
			}
			string value = PWGameCenter.regAccessToken.Match(httpResponse.ToString()).Groups[1].Value;
			string s = string.Format(GlobalConfig.ServerStrings[30], value);
			httpResponse = this.request.Post(GlobalConfig.ServerStrings[31], new BytesContent(System.Text.Encoding.ASCII.GetBytes(s)));
			string text = PWGameCenter.regLocation.Match(httpResponse.ToString()).Groups[1].Value;
			//text = NativeMethods.processString(text);
			httpResponse = this.request.Get(text, null);
			if (httpResponse.Cookies.ContainsKey("ukey"))
			{
				throw new System.Exception("wrong");
			}
			return string.Format("Mpop=\"{0}\"", httpResponse.Cookies["Mpop"]);
		}

		private string getAuthOther(string email, string password)
		{
			return string.Format("Username=\"{0}\" Password=\"{1}\"", email, password);
		}

		private string escapeString(string s)
		{
			return System.Security.SecurityElement.Escape(s);
		}

		public string[] Auth(string email, string password, bool checkValidOnly)
		{
			email = this.escapeString(email);
			password = this.escapeString(password);
			string[] array = email.Split(new char[]
			{
				'@'
			});
			string arg;
			if (array[1] == "mail.ru" || array[1] == "bk.ru" || array[1] == "inbox.ru" || array[1] == "list.ru" || array[1] == "mail.ua")
			{
				arg = this.getAuthMail(email, password);
			}
			else
			{
				arg = this.getAuthOther(email, password);
			}
			HttpResponse httpResponse = null;
			string s = string.Format(GlobalConfig.ServerStrings[32], arg);
			try
			{
				httpResponse = this.request.Post(GlobalConfig.ServerStrings[33], new BytesContent(System.Text.Encoding.ASCII.GetBytes(s)));
			}
			catch (HttpException ex)
			{
				if (ex.InnerException.Message.Contains("wrong password"))
				{
					throw new System.Exception("wrong");
				}
				if (ex.InnerException.Message.Contains("user has no such project") || ex.InnerException.Message.Contains("user notfound") || ex.InnerException.Message.Contains("SZError") || string.IsNullOrWhiteSpace(ex.InnerException.Message))
				{
					throw new System.Exception("no exists");
				}
				throw ex;
			}
			System.Text.RegularExpressions.Match match = PWGameCenter.regPersList.Match(httpResponse.ToString());
			if (!match.Success)
			{
				throw new System.Exception("wrong");
			}
			if (checkValidOnly)
			{
				return null;
			}
			string value = match.Groups[1].Value;
			string arg_1A1_0 = match.Groups[2].Value;
			s = string.Format(GlobalConfig.ServerStrings[34], arg);
			httpResponse = this.request.Post(GlobalConfig.ServerStrings[35], new BytesContent(System.Text.Encoding.ASCII.GetBytes(s)));
			match = PWGameCenter.regAutoLogin.Match(httpResponse.ToString());
			string value2 = match.Groups[1].Value;
			string value3 = match.Groups[2].Value;
			return new string[]
			{
				value2,
				value,
				value3
			};
		}
	}
}
