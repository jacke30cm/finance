﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Data;
using Data.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Finance.Models;

namespace Finance
{
    public class EmailService : IIdentityMessageService
    {

        

        public Task SendAsync(IdentityMessage message)
        {

            var uow = new DataWorker();
            var usr = uow.UserRepository.GetSingle(x => x.Email.Equals(message.Destination));
           
            var mailMessage = new MailMessage
            {
                IsBodyHtml = true
            };

            //Subject
            mailMessage.Subject = message.Subject;
            mailMessage.Body = ReturnMailBody(usr.FirstName, usr.LastName, message.Body);
            
            try
            {

                // Use this for localhost
                var smtp = new SmtpClient();

                // Use this for azure. This will still work in localhost mode but we don't want gmail to block us as a spammer
                #region SMTP for Azure
                //var smtp = new SmtpClient("smtp.gmail.com")
                //{
                //    UseDefaultCredentials = false,
                //    Port = 587,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    Credentials = new NetworkCredential("sendservice@wiccon.se", "0f686d17c958880709a49303af662e6b"),
                //    EnableSsl = true
                //};
                #endregion

                //Send to the member's email address
                mailMessage.To.Add(message.Destination);
                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            {

            }
            return Task.FromResult(0);
        }

        public string ReturnMailBody(string firstname, string lastname, string description)
        {
            

            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Path.Combine(HttpRuntime.AppDomainAppPath,"Content/EmailTemplate.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{FirstName}", firstname);
            body = body.Replace("{LastName}", lastname);
            body = body.Replace("{Description}", description);
            body = body.Replace("{Url}", Path.Combine(HttpRuntime.AppDomainAppPath, "Content/Images/karsten.jpg"));

            return body;
        }

    }


  
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<User>(context.Get<DataContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<User>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<User>
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
                    new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<User, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
