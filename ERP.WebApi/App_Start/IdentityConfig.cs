﻿using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using ERP.WebApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ERP.WebApi
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var fromEmail = ConfigurationManager.AppSettings["FromEmail"];
            var fromPassword = ConfigurationManager.AppSettings["FromPassword"];
            var fromDisplayName = ConfigurationManager.AppSettings["FromDisplayName"];
            string smtpServer = ConfigurationManager.AppSettings["SMTPServer"];
            string smtpPort = ConfigurationManager.AppSettings["SMTPPort"];
            var enableSsl = ConfigurationManager.AppSettings["EnableSSL"] == "1";

            MailMessage oMail = new MailMessage
            {
                Body = message.Body,
                IsBodyHtml = true,
                From = new MailAddress(fromEmail, fromDisplayName),
                Priority = MailPriority.Normal,
                Subject = message.Subject
            };
            oMail.To.Add(message.Destination);


            SmtpClient oClient = new SmtpClient
            {
                EnableSsl = enableSsl,
                Port = Convert.ToInt32(smtpPort),
                Host = smtpServer,
                Credentials = new NetworkCredential(fromEmail, fromPassword),
            };

            return oClient.SendMailAsync(oMail);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.

            var token = ConfigurationManager.AppSettings.Get("TwilioToken");
            var sid = ConfigurationManager.AppSettings.Get("TwilioSID");
            var from = ConfigurationManager.AppSettings.Get("FromPhoneNumber");

            TwilioClient.Init(sid,token);
            await MessageResource.CreateAsync(new PhoneNumber(message.Destination), @from: new PhoneNumber(from),
                body: message.Body);

        }

    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
