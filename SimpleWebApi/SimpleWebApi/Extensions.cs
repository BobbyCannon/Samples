#region References

using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security;
using Speedy;

#endregion

namespace SimpleWebApi
{
	public static class Extensions
	{
		#region Methods

		public static T Get<T>(this HttpContent content)
		{
			return content.ReadAsStringAsync().Result.FromJson<T>();
		}

		public static string ToUnsecureString(this SecureString value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			var unmanagedString = IntPtr.Zero;

			try
			{
				unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(value);
				return Marshal.PtrToStringUni(unmanagedString);
			}
			finally
			{
				Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
			}
		}

		#endregion
	}
}