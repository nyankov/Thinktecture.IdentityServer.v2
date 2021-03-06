﻿/*
 * Copyright (c) Dominick Baier.  All rights reserved.
 * see license.txt
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Thinktecture.IdentityModel;
using Thinktecture.IdentityServer.Models;
using Thinktecture.IdentityServer.Repositories.Mongo.EntityModel;
using Thinktecture.IdentityServer.Repositories.Mongo.EntityModel.Configuration;
using Client = Thinktecture.IdentityServer.Repositories.Mongo.EntityModel.Client;
using CodeToken = Thinktecture.IdentityServer.Repositories.Mongo.EntityModel.CodeToken;
using IdentityProvider = Thinktecture.IdentityServer.Repositories.Mongo.EntityModel.IdentityProvider;
using StoredGrant = Thinktecture.IdentityServer.Repositories.Mongo.EntityModel.StoredGrant;

namespace Thinktecture.IdentityServer.Repositories.Mongo
{
    internal static class Mappings
    {
        #region GlobalConfiguration
        public static Models.Configuration.GlobalConfiguration ToDomainModel(this GlobalConfiguration entity)
        {
            return new Models.Configuration.GlobalConfiguration
            {
                SiteName = entity.SiteName,
                IssuerUri = entity.IssuerUri,
                IssuerContactEmail = entity.IssuerContactEmail,
                DefaultHttpTokenType = entity.DefaultHttpTokenType,
                DefaultWSTokenType = entity.DefaultWSTokenType,
                DefaultTokenLifetime = entity.DefaultTokenLifetime,
                MaximumTokenLifetime = entity.MaximumTokenLifetime,
                EnableClientCertificateAuthentication = entity.EnableClientCertificateAuthentication,
                EnforceUsersGroupMembership = entity.EnforceUsersGroupMembership,
                HttpPort = entity.HttpPort,
                DisableSSL = entity.DisableSSL,
                HttpsPort = entity.HttpsPort,
                PublicHostName = entity.PublicHostName,
                RequireEncryption = entity.RequireEncryption,
                RequireRelyingPartyRegistration = entity.RequireRelyingPartyRegistration,
                SsoCookieLifetime = entity.SsoCookieLifetime
            };
        }

        public static GlobalConfiguration ToEntity(this Models.Configuration.GlobalConfiguration model)
        {
            return new GlobalConfiguration
            {
                SiteName = model.SiteName,
                IssuerUri = model.IssuerUri,
                IssuerContactEmail = model.IssuerContactEmail,
                DefaultHttpTokenType = model.DefaultHttpTokenType,
                DefaultWSTokenType = model.DefaultWSTokenType,
                DefaultTokenLifetime = model.DefaultTokenLifetime,
                MaximumTokenLifetime = model.MaximumTokenLifetime,
                EnableClientCertificateAuthentication = model.EnableClientCertificateAuthentication,
                EnforceUsersGroupMembership = model.EnforceUsersGroupMembership,
                HttpPort = model.HttpPort,
                DisableSSL = model.DisableSSL,
                HttpsPort = model.HttpsPort,
                PublicHostName = model.PublicHostName,
                RequireEncryption = model.RequireEncryption,
                RequireRelyingPartyRegistration = model.RequireRelyingPartyRegistration,
                SsoCookieLifetime = model.SsoCookieLifetime
            };
        }
        #endregion

        #region WSFederationConfiguration
        public static Models.Configuration.WSFederationConfiguration ToDomainModel(this WSFederationConfiguration entity)
        {
            return new Models.Configuration.WSFederationConfiguration
            {
                AllowReplyTo = entity.AllowReplyTo,
                EnableAuthentication = entity.EnableAuthentication,
                Enabled = entity.Enabled,
                EnableFederation = entity.EnableFederation,
                EnableHrd = entity.EnableHrd,
                RequireReplyToWithinRealm = entity.RequireReplyToWithinRealm,
                RequireSslForReplyTo = entity.RequireSslForReplyTo
            };
        }

        public static WSFederationConfiguration ToEntity(this Models.Configuration.WSFederationConfiguration model)
        {
            return new WSFederationConfiguration
            {
                AllowReplyTo = model.AllowReplyTo,
                EnableAuthentication = model.EnableAuthentication,
                Enabled = model.Enabled,
                EnableFederation = model.EnableFederation,
                EnableHrd = model.EnableHrd,
                RequireReplyToWithinRealm = model.RequireReplyToWithinRealm,
                RequireSslForReplyTo = model.RequireSslForReplyTo
            };
        }
        #endregion

        #region KeyMaterialConfiguration
        public static Models.Configuration.KeyMaterialConfiguration ToDomainModel(this KeyMaterialConfiguration entity)
        {
            var model = new Models.Configuration.KeyMaterialConfiguration();
            if (entity == null)
            {
                return model;
            }

            if (!string.IsNullOrWhiteSpace(entity.SigningCertificateName))
            {
                var cert = X509.LocalMachine.My.SubjectDistinguishedName.Find(entity.SigningCertificateName, false).FirstOrDefault();

                if (cert == null)
                {
                    throw new InvalidOperationException(string.Format(Resources.Mappings.SigningCertificateNotFoundException, entity.SigningCertificateName));
                }

                model.SigningCertificate = cert;
            }

            if (!string.IsNullOrWhiteSpace(entity.DecryptionCertificateName))
            {
                var cert = X509.LocalMachine.My.SubjectDistinguishedName.Find(entity.DecryptionCertificateName, false).FirstOrDefault();

                if (cert == null)
                {
                    throw new InvalidOperationException(string.Format(Resources.Mappings.DecryptionCertificateNotFoundException, entity.DecryptionCertificateName));
                }

                model.DecryptionCertificate = cert;
            }
            else
            {
                model.DecryptionCertificate = null;
            }

            model.SymmetricSigningKey = entity.SymmetricSigningKey;

            return model;
        }

        public static KeyMaterialConfiguration ToEntity(this Models.Configuration.KeyMaterialConfiguration model)
        {
            var entity = new KeyMaterialConfiguration();

            if (model.SigningCertificate != null)
            {
                entity.SigningCertificateName = model.SigningCertificate.Subject;
            }

            entity.DecryptionCertificateName = model.DecryptionCertificate != null ? model.DecryptionCertificate.Subject : null;

            entity.SymmetricSigningKey = model.SymmetricSigningKey;

            return entity;
        }
        #endregion

        #region WSTrustConfiguration
        public static Models.Configuration.WSTrustConfiguration ToDomainModel(this WSTrustConfiguration entity)
        {
            return new Models.Configuration.WSTrustConfiguration
            {
                EnableClientCertificateAuthentication = entity.EnableClientCertificateAuthentication,
                Enabled = entity.Enabled,
                EnableDelegation = entity.EnableDelegation,
                EnableFederatedAuthentication = entity.EnableFederatedAuthentication,
                EnableMessageSecurity = entity.EnableMessageSecurity,
                EnableMixedModeSecurity = entity.EnableMixedModeSecurity
            };
        }

        public static WSTrustConfiguration ToEntity(this Models.Configuration.WSTrustConfiguration model)
        {
            return new WSTrustConfiguration
            {
                EnableClientCertificateAuthentication = model.EnableClientCertificateAuthentication,
                Enabled = model.Enabled,
                EnableDelegation = model.EnableDelegation,
                EnableFederatedAuthentication = model.EnableFederatedAuthentication,
                EnableMessageSecurity = model.EnableMessageSecurity,
                EnableMixedModeSecurity = model.EnableMixedModeSecurity
            };
        }
        #endregion

        #region FederationMetadataConfiguration
        public static Models.Configuration.FederationMetadataConfiguration ToDomainModel(this FederationMetadataConfiguration entity)
        {
            return new Models.Configuration.FederationMetadataConfiguration
            {
                Enabled = entity.Enabled
            };
        }

        public static FederationMetadataConfiguration ToEntity(this Models.Configuration.FederationMetadataConfiguration model)
        {
            return new FederationMetadataConfiguration
            {
                Enabled = model.Enabled
            };
        }
        #endregion

        #region OAuth2Configuration
        public static Models.Configuration.OAuth2Configuration ToDomainModel(this OAuth2Configuration entity)
        {
            return new Models.Configuration.OAuth2Configuration
            {
                Enabled = entity.Enabled,
                EnableImplicitFlow = entity.EnableImplicitFlow,
                EnableResourceOwnerFlow = entity.EnableResourceOwnerFlow,
                EnableConsent = entity.EnableConsent,
                EnableCodeFlow = entity.EnableCodeFlow
            };
        }

        public static OAuth2Configuration ToEntity(this Models.Configuration.OAuth2Configuration model)
        {
            return new OAuth2Configuration
            {
                Enabled = model.Enabled,
                EnableImplicitFlow = model.EnableImplicitFlow,
                EnableResourceOwnerFlow = model.EnableResourceOwnerFlow,
                EnableConsent = model.EnableConsent,
                EnableCodeFlow = model.EnableCodeFlow
            };
        }
        #endregion

        #region AdfsIntegrationConfiguration
        public static Models.Configuration.AdfsIntegrationConfiguration ToDomainModel(this AdfsIntegrationConfiguration entity)
        {
            var value = new Models.Configuration.AdfsIntegrationConfiguration
            {
                Enabled = entity.Enabled,
                UsernameAuthenticationEnabled = entity.UsernameAuthenticationEnabled,
                SamlAuthenticationEnabled = entity.SamlAuthenticationEnabled,
                JwtAuthenticationEnabled = entity.JwtAuthenticationEnabled,
                PassThruAuthenticationToken = entity.PassThruAuthenticationToken,
                AuthenticationTokenLifetime = entity.AuthenticationTokenLifetime,
                UserNameAuthenticationEndpoint = entity.UserNameAuthenticationEndpoint,
                FederationEndpoint = entity.FederationEndpoint,
                IssuerThumbprint = entity.IssuerThumbprint,
                IssuerUri = entity.IssuerUri,
                EncryptionCertificate = !String.IsNullOrWhiteSpace(entity.EncryptionCertificate)
                    ? new X509Certificate2(Convert.FromBase64String(entity.EncryptionCertificate))
                    : null
            };

            return value;
        }

        public static AdfsIntegrationConfiguration ToEntity(this Models.Configuration.AdfsIntegrationConfiguration model)
        {
            var value = new AdfsIntegrationConfiguration
            {
                Enabled = model.Enabled,
                UsernameAuthenticationEnabled = model.UsernameAuthenticationEnabled,
                SamlAuthenticationEnabled = model.SamlAuthenticationEnabled,
                JwtAuthenticationEnabled = model.JwtAuthenticationEnabled,
                PassThruAuthenticationToken = model.PassThruAuthenticationToken,
                AuthenticationTokenLifetime = model.AuthenticationTokenLifetime,
                UserNameAuthenticationEndpoint = model.UserNameAuthenticationEndpoint,
                FederationEndpoint = model.FederationEndpoint,
                IssuerThumbprint = model.IssuerThumbprint,
                IssuerUri = model.IssuerUri,
                EncryptionCertificate =  model.EncryptionCertificate != null
                                                ? Convert.ToBase64String(model.EncryptionCertificate.RawData)
                                                : null
            };

            return value;
        }
        #endregion

        #region SimpleHttpConfiguration
        public static Models.Configuration.SimpleHttpConfiguration ToDomainModel(this SimpleHttpConfiguration entity)
        {
            return new Models.Configuration.SimpleHttpConfiguration
            {
                Enabled = entity.Enabled
            };
        }

        public static SimpleHttpConfiguration ToEntity(this Models.Configuration.SimpleHttpConfiguration model)
        {
            return new SimpleHttpConfiguration
            {
                Enabled = model.Enabled
            };
        }
        #endregion

        #region DiagnosticsConfiguration
        public static Models.Configuration.DiagnosticsConfiguration ToDomainModel(this DiagnosticsConfiguration entity)
        {
            return new Models.Configuration.DiagnosticsConfiguration
            {
                EnableFederationMessageTracing = entity.EnableFederationMessageTracing
            };
        }

        public static DiagnosticsConfiguration ToEntity(this Models.Configuration.DiagnosticsConfiguration model)
        {
            return new DiagnosticsConfiguration
            {
                EnableFederationMessageTracing = model.EnableFederationMessageTracing
            };
        }
        #endregion

        #region Relying Party
        public static RelyingParty ToDomainModel(this RelyingParties rpEntity)
        {
            var rp = new RelyingParty
            {
                Id = rpEntity.Id.ToString(CultureInfo.InvariantCulture),
                Name = rpEntity.Name,
                Enabled = rpEntity.Enabled,
                TokenType = rpEntity.TokenType,
                TokenLifeTime = rpEntity.TokenLifeTime,
                Realm = new Uri(rpEntity.Realm),
                ExtraData1 = rpEntity.ExtraData1,
                ExtraData2 = rpEntity.ExtraData2,
                ExtraData3 = rpEntity.ExtraData3,
                Description = rpEntity.Description,
                ImageUrl = rpEntity.ImageUrl
            };

            if (!string.IsNullOrWhiteSpace(rpEntity.ReplyTo))
            {
                rp.ReplyTo = new Uri(rpEntity.ReplyTo);
            }

            if (!string.IsNullOrWhiteSpace(rpEntity.EncryptingCertificate))
            {
                rp.EncryptingCertificate = new X509Certificate2(Convert.FromBase64String(rpEntity.EncryptingCertificate));
            }

            if (!string.IsNullOrWhiteSpace(rpEntity.SymmetricSigningKey))
            {
                rp.SymmetricSigningKey = Convert.FromBase64String(rpEntity.SymmetricSigningKey);
            }

            return rp;
        }

        public static RelyingParties ToEntity(this RelyingParty relyingParty)
        {
            var rpEntity = new RelyingParties
            {
                Name = relyingParty.Name,
                Enabled = relyingParty.Enabled,
                Realm = relyingParty.Realm.AbsoluteUri,
                TokenType = relyingParty.TokenType,
                TokenLifeTime = relyingParty.TokenLifeTime,
                ExtraData1 = relyingParty.ExtraData1,
                ExtraData2 = relyingParty.ExtraData2,
                ExtraData3 = relyingParty.ExtraData3,
                Description = relyingParty.Description,
                ImageUrl = relyingParty.ImageUrl
            };

            if (!string.IsNullOrEmpty(relyingParty.Id))
            {
                rpEntity.Id = relyingParty.Id;
            }

            if (relyingParty.ReplyTo != null)
            {
                rpEntity.ReplyTo = relyingParty.ReplyTo.AbsoluteUri;
            }

            if (relyingParty.EncryptingCertificate != null)
            {
                rpEntity.EncryptingCertificate = Convert.ToBase64String(relyingParty.EncryptingCertificate.RawData);
            }

            if (relyingParty.SymmetricSigningKey != null && relyingParty.SymmetricSigningKey.Length != 0)
            {
                rpEntity.SymmetricSigningKey = Convert.ToBase64String(relyingParty.SymmetricSigningKey);
            }

            return rpEntity;
        }

        public static IEnumerable<RelyingParty> ToDomainModel(this List<RelyingParties> relyingParties)
        {
            return
                (from rp in relyingParties
                 select new RelyingParty
                 {
                     Id = rp.Id.ToString(CultureInfo.InvariantCulture),
                     Name = rp.Name,
                     Realm = new Uri(rp.Realm),
                     Enabled = rp.Enabled,
                     Description = rp.Description,
                     ImageUrl = rp.ImageUrl
                 }).ToList();
        }
        #endregion

        #region Client Certificates
        public static List<ClientCertificate> ToDomainModel(this List<ClientCertificates> entities)
        {
            return
                (from entity in entities
                 select new ClientCertificate
                 {
                     UserName = entity.UserName,
                     Thumbprint = entity.Thumbprint,
                     Description = entity.Description
                 }
                ).ToList();
        }
        #endregion

        #region Clients
        public static Models.Client ToDomainModel(this Client client)
        {
            return new Models.Client
            {
                ID = client.Id,
                ClientId = client.ClientId,
                //ClientSecret = client.ClientSecret,
                HasClientSecret = !String.IsNullOrWhiteSpace(client.ClientSecret),
                Description = client.Description,
                Name = client.Name,
                RedirectUri = client.RedirectUri != null ? new Uri(client.RedirectUri) : null,
                AllowRefreshToken = client.AllowRefreshToken,
                AllowCodeFlow = client.AllowCodeFlow,
                AllowImplicitFlow = client.AllowImplicitFlow,
                AllowResourceOwnerFlow = client.AllowResourceOwnerFlow
            };
        }
        public static void UpdateEntity(this Models.Client client, Client target)
        {
            target.Id = client.ID;
            target.ClientId = client.ClientId;
            if (!String.IsNullOrWhiteSpace(client.ClientSecret))
            {
                target.ClientSecret = Helper.CryptoHelper.HashPassword(client.ClientSecret);
            }
            target.Description = client.Description;
            target.Name = client.Name;
            target.RedirectUri = client.RedirectUri != null ? client.RedirectUri.AbsoluteUri : null;
            target.AllowRefreshToken = client.AllowRefreshToken;
            target.AllowResourceOwnerFlow = client.AllowResourceOwnerFlow;
            target.AllowImplicitFlow = client.AllowImplicitFlow;
            target.AllowCodeFlow = client.AllowCodeFlow;
        }
        #endregion

        #region Delegation
        public static List<DelegationSetting> ToDomainModel(this List<Delegation> entities)
        {
            return
                (from entity in entities
                 select new DelegationSetting
                 {
                     UserName = entity.UserName,
                     Realm = new Uri(entity.Realm),
                     Description = entity.Description
                 }
                ).ToList();
        }
        #endregion

        #region IdentityProvider
        public static List<Models.IdentityProvider> ToDomainModel(this List<IdentityProvider> idps)
        {
            return new List<Models.IdentityProvider>(
                from idp in idps
                select new Models.IdentityProvider
                {
                    ID = idp.Id,
                    Name = idp.Name,
                    Enabled = idp.Enabled,
                    DisplayName = idp.DisplayName,
                    Type = (IdentityProviderTypes)idp.Type,
                    ShowInHrdSelection = idp.ShowInHrdSelection,
                    WSFederationEndpoint = idp.WSFederationEndpoint,
                    IssuerThumbprint = idp.IssuerThumbprint,
                    ClientID = idp.ClientID,
                    ClientSecret = idp.ClientSecret,
                    ProviderType = (OAuth2ProviderTypes?)idp.OAuth2ProviderType,
                    IconUrl = idp.IconUrl,
                    UseIconAsButton = idp.UseIconAsButton
                });
        }

        public static Models.IdentityProvider ToDomainModel(this IdentityProvider idp)
        {
            if (idp == null)
            {
                return null;
            }

            return new Models.IdentityProvider
            {
                ID = idp.Id,
                Name = idp.Name,
                Enabled = idp.Enabled,
                DisplayName = idp.DisplayName,
                ShowInHrdSelection = idp.ShowInHrdSelection,
                Type = (IdentityProviderTypes)idp.Type,
                WSFederationEndpoint = idp.WSFederationEndpoint,
                IssuerThumbprint = idp.IssuerThumbprint,
                ClientID = idp.ClientID,
                ClientSecret = idp.ClientSecret,
                ProviderType = (OAuth2ProviderTypes?)idp.OAuth2ProviderType,
                IconUrl = idp.IconUrl,
                UseIconAsButton = idp.UseIconAsButton
            };
        }

        public static IdentityProvider ToEntity(this Models.IdentityProvider idp)
        {
            if (idp == null)
            {
                return null;
            }

            var entity = new IdentityProvider();
            idp.UpdateEntity(entity);
            return entity;
        }

        public static void UpdateEntity(this Models.IdentityProvider idp, IdentityProvider entity)
        {
            if (idp == null || entity == null)
            {
                return;
            }

            if (!idp.ID.Equals("0"))
                entity.Id = idp.ID;
            entity.Name = idp.Name;
            entity.Enabled = idp.Enabled;
            entity.ShowInHrdSelection = idp.ShowInHrdSelection;
            entity.DisplayName = idp.DisplayName;
            entity.Type = (int)idp.Type;
            entity.WSFederationEndpoint = idp.WSFederationEndpoint;
            entity.IssuerThumbprint = idp.IssuerThumbprint;
            entity.ClientID = idp.ClientID;
            entity.ClientSecret = idp.ClientSecret;
            entity.OAuth2ProviderType = (int?)idp.ProviderType;
            entity.IconUrl = idp.IconUrl;
            entity.UseIconAsButton = idp.UseIconAsButton;
        }

        #endregion

        #region StoredGrant
        public static StoredGrant ToEntityModel(this Models.StoredGrant grant)
        {
            return new StoredGrant
            {
                ClientId = grant.ClientId,
                Created = grant.Created,
                Expiration = grant.Expiration,
                GrantId = grant.GrantId,
                GrantType = (int)grant.GrantType,
                RedirectUri = grant.RedirectUri,
                RefreshTokenExpiration = grant.RefreshTokenExpiration,
                Scopes = grant.Scopes,
                Subject = grant.Subject
            };
        }

        public static Models.StoredGrant ToDomainModel(this StoredGrant grant)
        {
            return new Models.StoredGrant
            {
                ClientId = grant.ClientId,
                Created = grant.Created,
                Expiration = grant.Expiration,
                GrantId = grant.GrantId,
                GrantType = (StoredGrantType)grant.GrantType,
                RedirectUri = grant.RedirectUri,
                RefreshTokenExpiration = grant.RefreshTokenExpiration,
                Scopes = grant.Scopes,
                Subject = grant.Subject
            };
        }
        #endregion

        #region CodeToken
        public static Models.CodeToken ToDomainModel(this CodeToken token)
        {
            return new Models.CodeToken
            {
                ClientId = token.ClientId,
                Scope = token.Scope,
                UserName = token.UserName,
                Code = token.Code,
                Type = (CodeTokenType)token.Type,
                TimeStamp = token.TimeStamp
            };
        }
        #endregion

        #region OpenIdConnectConfiguration
        public static Models.Configuration.OpenIdConnectConfiguration ToDomainModel(this OpenIdConnectConfiguration entity)
        {
            var value = new Models.Configuration.OpenIdConnectConfiguration
            {
                Enabled = entity.Enabled,
            };

            return value;
        }

        public static OpenIdConnectConfiguration ToEntity(this Models.Configuration.OpenIdConnectConfiguration model)
        {
            var value = new OpenIdConnectConfiguration
            {
                Enabled = model.Enabled,
            };

            return value;
        }
#endregion    

        #region OpenIdConnectClients
        public static OpenIdConnectClient ToDomainModel(this OpenIdConnectClientEntity client)
        {
            var ret = new OpenIdConnectClient
            {
                ClientId = client.ClientId,
                AccessTokenLifetime = client.AccessTokenLifetime, 
                AllowRefreshToken = client.AllowRefreshToken,
                ClientSecretType = client.ClientSecretType, 
                Flow = client.Flow, 
                Name = client.Name,
                RefreshTokenLifetime = client.RefreshTokenLifetime, 
                RequireConsent = client.RequireConsent, 
            };

            if (client.RedirectUris != null)
            {
                ret.RedirectUris =
                    (from item in client.RedirectUris
                     select item.RedirectUri).ToArray();
            }
            else
            {
                ret.RedirectUris = new string[0];
            }
            
            return ret;
        }

        public static void UpdateEntity(this OpenIdConnectClient client, OpenIdConnectClientEntity target)
        {
            target.ClientId = client.ClientId;
            target.AccessTokenLifetime = client.AccessTokenLifetime;
            target.AllowRefreshToken = client.AllowRefreshToken;
            target.ClientSecretType = client.ClientSecretType;
            target.Flow = client.Flow;
            target.Name = client.Name;
            target.RefreshTokenLifetime = client.RefreshTokenLifetime;
            target.RequireConsent = client.RequireConsent;
            
            if (!String.IsNullOrWhiteSpace(client.ClientSecret))
            {
                target.ClientSecret = Helper.CryptoHelper.HashPassword(client.ClientSecret);
            }

            if (client.RedirectUris != null)
            {
                var urlsToRemove = target.RedirectUris.Where(x => !client.RedirectUris.Contains(x.RedirectUri)).ToArray();
                foreach (var remove in urlsToRemove)
                {
                    target.RedirectUris.Remove(remove);
                }
            }

            if (client.RedirectUris == null) return;

            var urlsToAdd = target.RedirectUris != null ?
                client.RedirectUris.Where(x => target.RedirectUris.All(y => y.RedirectUri != x)).ToArray() :
                client.RedirectUris;
            foreach (var add in urlsToAdd)
            {
                target.RedirectUris.Add(new OpenIdConnectClientRedirectUri { RedirectUri = add });
            }
        }
        #endregion
    }
}
