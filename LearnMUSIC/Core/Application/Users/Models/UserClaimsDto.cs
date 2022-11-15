using System.Collections.Generic;
using System.Security.Claims;

namespace LearnMUSIC.Core.Application.Users.Models
{
    public class UserClaimsDto
    {
        public long UserGuid { get; set; }

        public string DisplayName { get; set; }

        public string EmailAddress { get; set; }

        public List<Claim> Claims { get; set; }

        public UserClaimsDto()
        {
            this.Claims = new List<Claim>();
        }
    }
}
