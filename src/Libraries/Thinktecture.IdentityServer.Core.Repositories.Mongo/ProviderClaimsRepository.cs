using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web.Profile;
using System.Web.Security;
using Thinktecture.IdentityServer.TokenService;

namespace Thinktecture.IdentityServer.Repositories.Mongo
{
    public class ProviderClaimsRepository : IClaimsRepository
    {
        private const string ProfileClaimPrefix = "http://identityserver.thinktecture.com/claims/profileclaims/";

        public virtual IEnumerable<Claim> GetClaims(ClaimsPrincipal principal, RequestDetails requestDetails)
        {
            var userName = principal.Identity.Name;
            var claims = new List<Claim>(from c in principal.Claims select c);

            // email address
            var membership = Membership.FindUsersByName(userName)[userName];
            if (membership != null)
            {
                var email = membership.Email;
                if (!String.IsNullOrEmpty(email))
                {
                    claims.Add(new Claim(ClaimTypes.Email, email));
                }
            }

            // roles
            GetRolesForToken(userName).ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

            // profile claims
            claims.AddRange(GetProfileClaims(userName));

            return claims;
        }

        protected virtual IEnumerable<Claim> GetProfileClaims(string userName)
        {
            var claims = new List<Claim>();

            if (!ProfileManager.Enabled) return claims;
            var profile = ProfileBase.Create(userName, true);
            foreach (SettingsProperty prop in ProfileBase.Properties)
            {
                var value = profile.GetPropertyValue(prop.Name);
                if (value == null) continue;
                if (!string.IsNullOrWhiteSpace(value.ToString()))
                {
                    claims.Add(new Claim(GetProfileClaimType(prop.Name.ToLowerInvariant()), value.ToString()));
                }
            }

            return claims;
        }

        protected virtual string GetProfileClaimType(string propertyName)
        {
            return StandardClaimTypes.Mappings.ContainsKey(propertyName) 
                        ? StandardClaimTypes.Mappings[propertyName] 
                        : string.Format("{0}{1}", ProfileClaimPrefix, propertyName);
        }

        public virtual IEnumerable<string> GetSupportedClaimTypes()
        {
            var claimTypes = new List<string>
            {
                ClaimTypes.Name,
                ClaimTypes.Email,
                ClaimTypes.Role
            };

            if (!ProfileManager.Enabled) return claimTypes;
            foreach (SettingsProperty prop in ProfileBase.Properties)
            {
                claimTypes.Add(GetProfileClaimType(prop.Name.ToLowerInvariant()));
            }

            return claimTypes;
        }

        protected virtual IEnumerable<string> GetRolesForToken(string userName)
        {
            var returnedRoles = new List<string>();

            if (!Roles.Enabled) return returnedRoles;

            var roles = Roles.GetRolesForUser(userName);
            returnedRoles = roles.Where(role => !(role.StartsWith(Constants.Roles.InternalRolesPrefix))).ToList();

            return returnedRoles;
        }
    }
}