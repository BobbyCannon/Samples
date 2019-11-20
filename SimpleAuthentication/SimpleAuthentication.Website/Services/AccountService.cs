#region References

using SimpleAuthentication.Website.Models;

#endregion

namespace SimpleAuthentication.Website.Services
{
	public class AccountService
	{
		#region Methods

		public string AuthenticateUser(Credentials credential)
		{
			if (credential.EmailAddress == "test@domain.com" && credential.Password == "Password!")
			{
				return "test@domain.com";
			}

			return null;
		}

		#endregion
	}
}