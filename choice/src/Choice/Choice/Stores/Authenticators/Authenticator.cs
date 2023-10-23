using Choice.Domain.Models;
using System;

namespace Choice.Stores.Authenticators
{
    public class Authenticator : IAuthenticator
    {
        public event Action StateChanged;

		private User _user;

		public User State
		{
			get => _user;
			set
			{
				_user = value;
				StateChanged?.Invoke();
			}
		}
	}
}
