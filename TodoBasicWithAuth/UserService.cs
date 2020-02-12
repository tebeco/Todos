using System.Collections.Generic;
using System.Security;

namespace Todos
{
    internal class UserService
    {
        private readonly Dictionary<string, (SecureString Password, string[] Claims)> _users;

        public UserService()
        {
            var securePasswordBecauseSecureString = new SecureString();
            securePasswordBecauseSecureString.AppendChar('1');
            securePasswordBecauseSecureString.AppendChar('2');
            securePasswordBecauseSecureString.AppendChar('3');
            securePasswordBecauseSecureString.AppendChar('4');
            securePasswordBecauseSecureString.AppendChar('5');
            securePasswordBecauseSecureString.AppendChar('6');

            _users = new Dictionary<string, (SecureString Password, string[] Claims)>
            {
                ["user"] = (securePasswordBecauseSecureString, new[] { "can_delete", "can_view" }),
            };
        }

        public string[] GetUserClaims(string username)
        {
            return _users[username].Claims;
        }

        public bool IsValid(string username, string password)
        {
            var ultraSecurePassword = new SecureString();
            foreach (char c in password ?? string.Empty)
                ultraSecurePassword.AppendChar(c);


            return _users.TryGetValue(username, out var data) && SecureString.Equals(data.Password, password);
        }
    }
}