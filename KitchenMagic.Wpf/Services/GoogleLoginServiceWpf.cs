using KitchenMagic.Common;
using KitchenMagic.Common.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KitchenMagic.Wpf.Services
{
	internal class GoogleLoginServiceWpf : IGoogleLoginService
	{
		public async Task<bool> Login()
		{
			// Generates state and PKCE values.
			var state = RandomDataBase64url(32);
			var codeVerifier = RandomDataBase64url(32);
			var codeChallenge = Base64urlencodeNoPadding(Sha256(codeVerifier));
			const string codeChallengeMethod = "S256";

			// Creates a redirect URI using an available port on the loopback address.
			var redirectURI = $"http://{IPAddress.Loopback}:{GetRandomUnusedPort()}/";

			// Creates an HttpListener to listen for requests on that redirect URI.
			var http = new HttpListener();
			http.Prefixes.Add(redirectURI);
			http.Start();

			// Creates the OAuth 2.0 authorization request.
			var authorizationRequest =
				$"{Configuration.GoogleAuthorizationEndpoint}?response_type=code&scope=openid%20profile&redirect_uri={Uri.EscapeDataString(redirectURI)}&client_id={Configuration.GoogleClientID}&state={state}&code_challenge={codeChallenge}&code_challenge_method={codeChallengeMethod}";

			// Opens request in the browser.
			System.Diagnostics.Process.Start(authorizationRequest);

			// Waits for the OAuth authorization response.
			var context = await http.GetContextAsync();

			// Sends an HTTP response to the browser.
			var response = context.Response;
			var responseString = "<html><body>Thank you!</body></html>";
			var buffer = Encoding.UTF8.GetBytes(responseString);
			response.ContentLength64 = buffer.Length;
			var responseOutput = response.OutputStream;
			await responseOutput.WriteAsync(buffer, 0, buffer.Length);

			await Task.Delay(5 * 1000);
			responseOutput.Close();
			http.Stop();

			// Checks for errors.
			if (context.Request.QueryString.Get("error") != null)
			{
				Console.WriteLine($@"OAuth authorization error: {context.Request.QueryString.Get("error")}.");
				return false;
			}

			if (context.Request.QueryString.Get("code") == null
				|| context.Request.QueryString.Get("state") == null)
			{
				Console.WriteLine(@"Malformed authorization response. " + context.Request.QueryString);
				return false;
			}

			// extracts the code
			var code = context.Request.QueryString.Get("code");
			var incomingState = context.Request.QueryString.Get("state");

			// Compares the receieved state to the expected value, to ensure that
			// this app made the request which resulted in authorization.
			if (incomingState != state)
			{
				Console.WriteLine(@"Received request with invalid state ({0})", incomingState);
				return false;
			}

			Console.WriteLine(@"Authorization code: " + code);

			// Starts the code exchange at the Token Endpoint.
			PerformCodeExchange(code, codeVerifier, redirectURI);

			return true;
		}

		public async Task<bool> Logout()
		{
			await Task.Delay(1 * 1000);
			Configuration.AccessToken = string.Empty;
			return true;
		}

		/// <summary>
		/// Base64url no-padding encodes the given input buffer.
		/// </summary>
		/// <param name="buffer"></param>
		/// <returns></returns>
		public static string Base64urlencodeNoPadding(byte[] buffer)
		{
			var base64 = Convert.ToBase64String(buffer);

			// Converts base64 to base64url.
			base64 = base64.Replace("+", "-");
			base64 = base64.Replace("/", "_");
			// Strips padding.
			base64 = base64.Replace("=", "");

			return base64;
		}

		public static int GetRandomUnusedPort()
		{
			var listener = new TcpListener(IPAddress.Loopback, 0);
			listener.Start();
			var port = ((IPEndPoint)listener.LocalEndpoint).Port;
			listener.Stop();
			return port;
		}

		/// <summary>
		/// Returns URI-safe data with a given input length.
		/// </summary>
		/// <param name="length">Input length (nb. output will be longer)</param>
		/// <returns></returns>
		public static string RandomDataBase64url(uint length)
		{
			var rng = new RNGCryptoServiceProvider();
			var bytes = new byte[length];
			rng.GetBytes(bytes);
			return Base64urlencodeNoPadding(bytes);
		}

		/// <summary>
		/// Returns the SHA256 hash of the input string.
		/// </summary>
		/// <param name="inputStirng"></param>
		/// <returns></returns>
		public static byte[] Sha256(string inputStirng)
		{
			var bytes = Encoding.ASCII.GetBytes(inputStirng);
			var sha256 = new SHA256Managed();
			return sha256.ComputeHash(bytes);
		}

		private async void PerformCodeExchange(string code, string codeVerifier, string redirectURI)
		{
			Console.WriteLine(@"Exchanging code for tokens...");

			// builds the  request
			var tokenRequestURI = "https://www.googleapis.com/oauth2/v4/token";
			var tokenRequestBody =
				$"code={code}&redirect_uri={Uri.EscapeDataString(redirectURI)}&client_id={Configuration.GoogleClientID}&code_verifier={codeVerifier}&client_secret={Configuration.GoogleClientSecret}&scope=&grant_type=authorization_code";

			// sends the request
			var tokenRequest = (HttpWebRequest)WebRequest.Create(tokenRequestURI);
			tokenRequest.Method = "POST";
			tokenRequest.ContentType = "application/x-www-form-urlencoded";
			tokenRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
			var byteVersion = Encoding.ASCII.GetBytes(tokenRequestBody);
			tokenRequest.ContentLength = byteVersion.Length;
			var stream = tokenRequest.GetRequestStream();
			await stream.WriteAsync(byteVersion, 0, byteVersion.Length);
			stream.Close();

			try
			{
				// gets the response
				var tokenResponse = await tokenRequest.GetResponseAsync();
				using (var reader = new StreamReader(tokenResponse.GetResponseStream()))
				{
					// reads response body
					var responseText = await reader.ReadToEndAsync();
					Console.WriteLine(responseText);

					// converts to dictionary
					var tokenEndpointDecoded = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseText);

					var accessToken = tokenEndpointDecoded["access_token"];
					Configuration.AccessToken = accessToken;
				}
			}
			catch (WebException ex)
			{
				if (ex.Status == WebExceptionStatus.ProtocolError)
				{
					var response = ex.Response as HttpWebResponse;
					if (response != null)
					{
						Console.WriteLine(@"HTTP: " + response.StatusCode);
						using (var reader = new StreamReader(response.GetResponseStream()))
						{
							// reads response body
							var responseText = await reader.ReadToEndAsync();
							Console.WriteLine(responseText);
						}
					}
				}
			}
		}

		public async Task<string> GetUserInfo()
		{
			Console.WriteLine(@"Making API Call to UserInfo...");

			// builds the  request
			var userinfoRequestURI = "https://www.googleapis.com/oauth2/v3/userinfo";

			// sends the request
			var userinfoRequest = (HttpWebRequest)WebRequest.Create(userinfoRequestURI);
			userinfoRequest.Method = "GET";
			userinfoRequest.Headers.Add($"Authorization: Bearer {Configuration.AccessToken}");
			userinfoRequest.ContentType = "application/x-www-form-urlencoded";
			userinfoRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

			// gets the response
			var userinfoResponse = await userinfoRequest.GetResponseAsync();
			using (var userinfoResponseReader = new StreamReader(userinfoResponse.GetResponseStream()))
			{
				// reads response body
				var userinfoResponseText = await userinfoResponseReader.ReadToEndAsync();
				return userinfoResponseText;
			}
		}
	}
}
