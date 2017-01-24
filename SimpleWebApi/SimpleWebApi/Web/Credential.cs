#region References

using System.Security;

#endregion

namespace SimpleWebApi.Web
{
	public class Credential
	{
		#region Properties

		public SecureString Password { get; set; }

		public string UserName { get; set; }

		#endregion
	}
}