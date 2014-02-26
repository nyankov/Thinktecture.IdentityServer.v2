/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license.txt
 */

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Thinktecture.IdentityServer.Models;

namespace Thinktecture.IdentityServer.Protocols.WSFederation
{
    public class HrdViewModel
    {
        public HrdViewModel(System.IdentityModel.Services.SignInRequestMessage message, IEnumerable<Models.IdentityProvider> idps, RelyingParty rp)
        {
            this.OriginalSigninUrl = message.WriteQueryString();
            this.Providers = idps.Select(x => new HRDIdentityProvider { DisplayName = x.DisplayName, ID = x.Name, IconUrl = x.IconUrl, UseIconAsButton = x.UseIconAsButton }).ToArray();
            Party = new RelyingPartyInfo
            {
                Name = rp.Name,
                Description = rp.Description,
                ImageUrl = rp.ImageUrl
            };
            RelyingPartyDescription = rp.ExtraData1;
        }

        public IEnumerable<HRDIdentityProvider> Providers { get; set; }
        public string OriginalSigninUrl { get; set; }
        [Display(ResourceType = typeof (Resources.WSFederation.HrdViewModel), Name = "RememberHRDSelection")]
        public bool RememberHRDSelection { get; set; }

        public RelyingPartyInfo Party { get; set; }
        public string RelyingPartyDescription { get; set; }
    }

    public class HRDIdentityProvider
    {
        public string ID { get; set; }
        public string DisplayName { get; set; }
        public string IconUrl { get; set; }
        public bool UseIconAsButton { get; set; }
    }

    public class RelyingPartyInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
