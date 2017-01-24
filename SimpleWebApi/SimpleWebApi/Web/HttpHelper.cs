#region References

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Speedy;

#endregion

namespace SimpleWebApi.Web
{
	/// <summary>
	/// This class is used for making GET and POST calls to an HTTP endpoint.
	/// </summary>
	public class HttpHelper
	{
		#region Constants

		public const int TimeOutInSeconds = 100;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new HTTP helper to point at a specific URI, and with the specified session identifier.
		/// </summary>
		/// <param name="baseUri"> </param>
		public HttpHelper(string baseUri)
		{
			BaseUri = baseUri;
			Cookies = new CookieCollection();
			Credential = null;
			Headers = new Dictionary<string, string>();
			Timeout = TimeSpan.FromSeconds(TimeOutInSeconds);
		}

		#endregion

		#region Properties

		public string BaseUri { get; set; }

		public CookieCollection Cookies { get; set; }

		public Credential Credential { get; set; }

		public IDictionary<string, string> Headers { get; set; }

		public bool IsAuthenticated
		{
			get
			{
				foreach (Cookie cookie in Cookies)
				{
					if (cookie.Name == ".ASPXAUTH")
					{
						return true;
					}
				}

				return false;
			}
		}

		/// <summary>
		/// Gets or sets the number of milliseconds to wait before the request times out. The default value is 100 seconds.
		/// </summary>
		public TimeSpan Timeout { get; set; }

		#endregion

		#region Methods

		public HttpResponseMessage Delete(string uri)
		{
			using (var handler = new HttpClientHandler())
			{
				using (var client = CreateHttpClient(handler))
				{
					var response = client.DeleteAsync(uri).Result;
					return ProcessResponse(response, handler);
				}
			}
		}

		public virtual T Get<T>(string uri)
		{
			using (var result = Get(uri))
			{
				if (!result.IsSuccessStatusCode)
				{
					throw new Exception(result.ReasonPhrase);
				}

				return result.Content.Get<T>();
			}
		}

		public virtual HttpResponseMessage Get(string uri)
		{
			using (var handler = new HttpClientHandler())
			{
				using (var client = CreateHttpClient(handler))
				{
					var response = client.GetAsync(uri).Result;
					return ProcessResponse(response, handler);
				}
			}
		}

		public virtual TResult Post<TContent, TResult>(string uri, TContent content)
		{
			using (var result = InternalPost(uri, content))
			{
				if (!result.IsSuccessStatusCode)
				{
					throw new Exception(result.ReasonPhrase);
				}

				return result.Content.ReadAsStringAsync().Result.FromJson<TResult>();
			}
		}

		public virtual HttpResponseMessage Post<TContent>(string uri, TContent content)
		{
			return InternalPost(uri, content);
		}

		public virtual TResult Put<TContent, TResult>(string uri, TContent content)
		{
			using (var result = InternalPut(uri, content))
			{
				if (!result.IsSuccessStatusCode)
				{
					throw new Exception(result.ReasonPhrase);
				}

				return result.Content.ReadAsStringAsync().Result.FromJson<TResult>();
			}
		}

		public virtual HttpResponseMessage Put<TContent>(string uri, TContent content)
		{
			return InternalPut(uri, content);
		}

		private HttpClient CreateHttpClient(HttpClientHandler handler)
		{
			foreach (Cookie ck in Cookies)
			{
				handler.CookieContainer.Add(ck);
			}

			var client = new HttpClient(handler)
			{
				BaseAddress = new Uri(BaseUri),
				Timeout = Timeout
			};

			if (Credential != null)
			{
				var value = $"{Credential.UserName}:{Credential.Password.ToUnsecureString()}";
				var headerValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(value)));
				client.DefaultRequestHeaders.Authorization = headerValue;
			}

			Headers.ForEach(x => client.DefaultRequestHeaders.Add(x.Key, x.Value));
			return client;
		}

		private HttpResponseMessage InternalPost<T>(string uri, T content)
		{
			using (var handler = new HttpClientHandler())
			{
				using (var client = CreateHttpClient(handler))
				{
					var json = content.ToJson();
					using (var objectContent = new StringContent(json, Encoding.UTF8, "application/json"))
					{
						var response = client.PostAsync(uri, objectContent).Result;
						return ProcessResponse(response, handler);
					}
				}
			}
		}

		private HttpResponseMessage InternalPut<T>(string uri, T content)
		{
			using (var handler = new HttpClientHandler())
			{
				using (var client = CreateHttpClient(handler))
				{
					var json = content.ToJson();
					using (var objectContent = new StringContent(json, Encoding.UTF8, "application/json"))
					{
						var response = client.PutAsync(uri, objectContent).Result;
						return ProcessResponse(response, handler);
					}
				}
			}
		}

		private HttpResponseMessage ProcessResponse(HttpResponseMessage response, HttpClientHandler handler)
		{
			if (handler.CookieContainer != null && Uri.IsWellFormedUriString(BaseUri, UriKind.Absolute))
			{
				Cookies = handler.CookieContainer.GetCookies(new Uri(BaseUri));
			}

			return response;
		}

		#endregion
	}
}