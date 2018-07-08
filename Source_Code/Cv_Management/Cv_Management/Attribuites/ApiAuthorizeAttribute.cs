﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Cv_Management.Entities.Context;
using Cv_Management.ViewModel.User;
using JWT;
using JWT.Serializers;
using Newtonsoft.Json;

namespace Cv_Management.Attribuites
{
    public class ApiAuthorizeAttribute:AuthorizeAttribute
    {
        public readonly DbCvManagementContext DbSet;
        public ApiAuthorizeAttribute()
        {
            DbSet= new DbCvManagementContext();
        }

        public override void OnAuthorization( HttpActionContext actionContext)
        {
            var token = actionContext.Request.Headers.Authorization.Parameter;
            const string secret = "dkjfjfosaijfskfjslfjaoijovocjosjfofjs";
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                var json = decoder.Decode(token, secret, verify: true);
                var acount = JsonConvert.DeserializeObject<AcountViewModel>(json);
              //  var user = DbSet.Users.Any(c=>c.)

            }
            catch (TokenExpiredException e)
            {
                Console.WriteLine("Token has expried");
               
            }
        }
    }
}