﻿using ERP.Web;
using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Net;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;
using ERP.Models.IdentityModels.ViewModels;
using ERP.Models.ModelMapers;
using ERP.Models.WebModels;
using ERP.Models.WebViewModels;
using ERP.Service.Identity;
using ERP.Web;
using ERP.Web.Resources.HR;
using ExternalLoginConfirmationViewModel = ERP.Models.IdentityModels.ViewModels.ExternalLoginConfirmationViewModel;
using ForgotPasswordViewModel = ERP.Models.IdentityModels.ViewModels.ForgotPasswordViewModel;
using LoginViewModel = ERP.Models.IdentityModels.ViewModels.LoginViewModel;
using RegisterViewModel = ERP.Models.IdentityModels.ViewModels.RegisterViewModel;
using ResetPasswordViewModel = ERP.Models.IdentityModels.ViewModels.ResetPasswordViewModel;
using SendCodeViewModel = ERP.Models.IdentityModels.ViewModels.SendCodeViewModel;
using VerifyCodeViewModel = ERP.Models.IdentityModels.ViewModels.VerifyCodeViewModel;

namespace IdentitySample.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        #region Private

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private IAspNetUserService AspNetUserService;


        /// <summary>
        /// Set User Permission
        /// </summary>
        private void SetUserPermissions(string userEmail)
        {
            try
            {
                AspNetUser userResult = UserManager.FindByEmail(userEmail);
                IList<AspNetRole> roles = userResult.AspNetRoles.ToList();
                
                //string[] userPermissions = userRights.Select(user => user.Menu.PermissionKey).ToArray();
                //Session["UserPermissionSet"] = userPermissions;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        private void SetCultureInfo(string userId)
        {
        }
        #endregion

        #region Constructor

        public AccountController(IAspNetUserService aspNetUserService)
        {
            this.AspNetUserService = aspNetUserService;
        }

        #endregion

        #region Public

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>(); }
            private set { _roleManager = value; }
        }

        #region Change Password
        public ActionResult ChangePassword()
        {
            return View();
        }
        // POST: /Account/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                //return RedirectToAction("Index", new { Message = IdentitySample.Controllers.ManageController.ManageMessageId.ChangePasswordSuccess });
                //return RedirectToAction("Index", "Dashboard");
                ViewBag.MessageVM = new MessageViewModel {Message = "Password has been updated.", IsUpdated = true};

                return View();
            }
            else
            {
                ViewBag.MessageVM = new MessageViewModel { Message = "Incorrect old Password", IsError = true };
            }
            AddErrors(result);
            return View(model);
        }
        private async Task SetExternalProperties(ClaimsIdentity identity)
        {
            // get external claims captured in Startup.ConfigureAuth
            ClaimsIdentity ext = await AuthenticationManager.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);

            if (ext != null)
            {
                var ignoreClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";
                // add external claims to identity
                foreach (var c in ext.Claims)
                {
                    if (!c.Type.StartsWith(ignoreClaim))
                        if (!identity.HasClaim(c.Type, c.Value))
                            identity.AddClaim(c);
                }
            }
        }
        private async Task SignInAsync(AspNetUser user, bool isPersistent)
        {
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            //AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            //var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            //await SetExternalProperties(identity);

            //AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);

            //await SaveAccessToken(user, identity);
        }
        #endregion

        #region Login & Logoff
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.MessageVM = TempData["message"] as MessageViewModel;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }

        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                    {
                        ModelState.AddModelError("", "Email not confirmed");
                        return View(model);
                    }
                    else
                    {
                        if (user.LockoutEnabled)
                        {
                            ModelState.AddModelError("", "User Is locked, Please contact admin to unlock the user");
                            return View(model);
                        }
                        else
                        {
                            var role = user.AspNetRoles.FirstOrDefault();
                            if (role.Id == Utility.MemberRoleId)
                            {
                                
                            }
                        }
                    }
                }
                // This doen't count login failures towards lockout only two factor authentication
                // To enable password failures to trigger lockout, change to shouldLockout: true
                var result =
                    await
                        SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                            shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        {
                            SetUserPermissions(user.Email);
                            SetCultureInfo(user.Id);
                                           var role = user.AspNetRoles.FirstOrDefault();
                            //if (role.Id == Utility.MemberRoleId)
                            //{
                            //    return RedirectToAction("Index", "Home");
                            //}
                            if (string.IsNullOrEmpty(returnUrl))
                                return RedirectToAction("Index", "Dashboard");
                            return RedirectToLocal(returnUrl);
                        }
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");

            }
        }
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
        #endregion

        #region Register
        //
        // GET: /Account/Register
        //[SiteAuthorize(PermissionKey = "UserCreate")]
        public ActionResult Create(string userName)
        {
           // AspNetUsersViewModel Result = new AspNetUsersViewModel();
            AspNetUsersViewModel Result = new AspNetUsersViewModel();
            Result.AspNetUserModel = new AspNetUserModel();
            Result.Roles = RoleManager.Roles.Where(r => r.Name != "Admin").ToList(); 
         
            if (!string.IsNullOrEmpty(userName))
            {
                AspNetUser userToEdit = UserManager.FindByName(userName);
                Result.AspNetUserModel = userToEdit.CreateFrom();
                return View(Result);
            }
             return View(Result);
        }


           [Authorize(Roles = "Admin")]
        //[SiteAuthorize(PermissionKey = "User")]
        public ActionResult Users()
        {
            //if (Session["UserID"] == null)
            //{
            //    return RedirectToAction("Login");
            //}

            var users = AspNetUserService.GetAllUsers();
            List <AspNetUserModel>  oUsers= users.Select(x => x.CreateFrom()).ToList();
        

            //List<AspNetUser> oList = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().Users.ToList();
            //var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>());
            //var roleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
           // TempData["message"] = new MessageViewModel { Message = "Employee has been Added" ,IsSaved = true };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            //UserViewModel oVM = new UserViewModel();
            //oVM.Data = new List<SystemUser>();
            //foreach (var item in oList)
            //{
            //    if (item.Employee > 0)
            //    {
            //        oVM.Data.Add(new SystemUser
            //        {
            //            EmailConfirmed = item.EmailConfirmed,
            //            Email = item.Email,
            //            FirstName = item.Employee.EmployeeNameE,
            //            KeyId = item.Id,
            //            Role = roleManager.FindById(item.AspNetRoles.ToList()[0].Id).Name,
            //            Username = item.UserName
            //        });
            //    }
            //}
            return View(oUsers);
        }


        //
        // POST: /Account/Create
          
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AspNetUsersViewModel model)
        {
            model.AspNetUserModel.UserName = model.AspNetUserModel.Email;
            #region Update
            if (!string.IsNullOrEmpty(model.AspNetUserModel.Id))
            {
                //Means Update

                // Get role
                var roleName = RoleManager.FindById(model.AspNetUserModel.RoleId).Name;
                AspNetUser userResult = UserManager.FindById(model.AspNetUserModel.Id);
                string userrRoleID = userResult.AspNetRoles.ToList()[0].Id;
                string userRoleName = RoleManager.FindById(userrRoleID).Name;

                // Check if role has been changed
               /************** DISABLING CHANGE ROLE IMPLEMENTATION/ UNCOMMENT TO RUN 
               if (userrRoleID != model.AspNetUserModel.RoleId)
                {
                    // Update User Role
                    UserManager.RemoveFromRole(model.AspNetUserModel.Id, userRoleName);
                    UserManager.AddToRole(model.AspNetUserModel.Id, roleName);
                    TempData["message"] = new MessageViewModel { Message = "Role has been updated", IsUpdated = true };
                }************************/
                // Password Reset
                if (!String.IsNullOrEmpty(model.AspNetUserModel.Password))
                {
                    var token = await UserManager.GeneratePasswordResetTokenAsync(model.AspNetUserModel.Id);
                    var resetPwdResults = await UserManager.ResetPasswordAsync(model.AspNetUserModel.Id, token, model.AspNetUserModel.Password);

                    if (resetPwdResults.Succeeded)
                    {
                        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                        if (user != null)
                        {
                            await SignInAsync(user, isPersistent: false);
                        }
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Account.UpdatePass,
                            IsUpdated = true
                        };
                    }
                }
                // Get user by UserId to Update User
                AspNetUser userToUpdate = UserManager.FindById(model.AspNetUserModel.Id);
                //if (userToUpdate.Email != model.AspNetUserModel.Email)
                //{
                    
                    if (userToUpdate != null)
                    {
                        userToUpdate.UpdateUserTo(model.AspNetUserModel);
                    }
                    var updateUserResult = await UserManager.UpdateAsync(userToUpdate);
                    if (updateUserResult.Succeeded)
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = "User has been Updated",
                            IsUpdated = true
                        };
                    }
                //}

                return RedirectToAction("Users");
            }
            #endregion
            // Add new User
            if (ModelState.IsValid)
            {
                // TODO:Check # of Users that Admin can create
                var user = new AspNetUser
                {
                    UserName = model.AspNetUserModel.UserName,
                    Email = model.AspNetUserModel.Email,
                    Address = model.AspNetUserModel.Address, 
                    Telephone = model.AspNetUserModel.Telephone,FirstName = model.AspNetUserModel.FirstName,LastName = model.AspNetUserModel.LastName, LockoutEnabled = false
                };
                user.EmailConfirmed = true;
                if (!String.IsNullOrEmpty(model.AspNetUserModel.Password))
                {
                    var result = await UserManager.CreateAsync(user, model.AspNetUserModel.Password);
                    if (result.Succeeded)
                    {
                        //Setting role
                        var roleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
                        var roleName = roleManager.FindById(model.AspNetUserModel.RoleId).Name;
                        UserManager.AddToRole(user.Id, roleName);
                        // Add User Preferences for Dashboards Widgets

                        TempData["message"] = new MessageViewModel
                        {
                            Message = "Employee has been created",
                            IsSaved = true
                        };
                        return RedirectToAction("Users");
                    }
                    else
                    {
                        var resultStr = "";
                        if (result.Errors.Count() > 0)
                            resultStr = result.Errors.ToList()[0].ToString();
                        TempData["message"] = new MessageViewModel
                        {
                            Message = resultStr,
                            IsError = true
                        };
                        ViewBag.MessageVM = TempData["message"] as MessageViewModel;
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            model.Roles = HttpContext.GetOwinContext().Get<ApplicationRoleManager>().Roles.ToList();
            //TempData["message"] = new MessageViewModel { Message = ERP.Web.Resources.HR.Account.ChkFields, IsError = true };
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Subscribe()
        {
            SignupViewModel signupViewModel = new SignupViewModel();
            return View(signupViewModel);
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Subscribe(SignupViewModel signupViewModel)
        {
            // Add new User
                // Check if User already exists
                var usernames = AspNetUserService.GetAllUsers().Select(x => x.UserName);
                if (usernames.Contains(signupViewModel.UserName))
                {
                    // it means username is already taken
                    TempData["message"] = new MessageViewModel { Message = Account.EmpError, IsError = true };
                    return View(signupViewModel);
                }

                var user = new AspNetUser { UserName = signupViewModel.UserName, Email = signupViewModel.Email };
                user.EmailConfirmed = true;
                if (!String.IsNullOrEmpty(signupViewModel.Password))
                {
                    var result = await UserManager.CreateAsync(user, signupViewModel.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index","Home");
                    }
                }
            return View(signupViewModel);
        }
        #endregion

        #region External Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider,
                Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation",
                        new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model,
            string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new AspNetUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }
        #endregion

        #region Manage
        public ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); }
            private set { _signInManager = value; }
        }

        //
        // POST: /Account/Login


        public ActionResult Error()
        {
            return View("Error");
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            var user = await UserManager.FindByIdAsync(await SignInManager.GetVerifiedUserIdAsync());
            if (user != null)
            {
                ViewBag.Status = "For DEMO purposes the current " + provider + " code is: " +
                                 await UserManager.GenerateTwoFactorTokenAsync(user.Id, provider);
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result =
                await
                    SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: false,
                        rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            try
            {
                if (userId == null || code == null)
                {
                    return View("Error");
                }
                var result = await UserManager.ConfirmEmailAsync(userId, code);
                //return View(result.Succeeded ? "ConfirmEmail" : "Error");
                return RedirectToAction("Login");
            }
            catch (Exception)
            {

                return RedirectToAction("Login");
            }
        }

        //
        // GET: /Account/`Password
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    ModelState.AddModelError("", "Email not found.");
                    // Don't reveal that the user does not exist or is not confirmed
                    return View(model);
                }

                var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code },
                    protocol: Request.Url.Scheme);
                await
                    UserManager.SendEmailAsync(user.Email, "Reset Password",
                        "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
                ViewBag.Link = callbackUrl;
                return RedirectToAction("Login");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("Login", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                TempData["message"] = new MessageViewModel { Message = "Password has been updated.", IsUpdated = true };
                return RedirectToAction("Login", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin


        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions =
                userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl });
        }

        #endregion

        #region Profile Work
        [Authorize]
        public ActionResult Profile()
        {
            //AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            //var ProfileViewModel = new ProfileViewModel
            //{
            //    Email = result.Email,
            //    UserName = result.UserName,
            //    Address = result.Address,
            //    ImageName = (result.ImageName != null && result.ImageName != string.Empty) ? result.ImageName : string.Empty,
            //    ImagePath = ConfigurationManager.AppSettings["ProfileImage"].ToString() + result.ImageName
            //};
            //ViewBag.FilePath = ConfigurationManager.AppSettings["ProfileImage"] + ProfileViewModel.ImageName;//Server.MapPath
            //ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            AspNetUserModel ProfileViewModel = new AspNetUserModel();
            ProfileViewModel=AspNetUserService.FindById(User.Identity.GetUserId()).CreateFrom();
            return View(ProfileViewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Profile(AspNetUserModel profileViewModel)
        {
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());

            //Updating Data
            try
            {
                result.FirstName = profileViewModel.FirstName;
                result.LastName = profileViewModel.LastName;
                result.Telephone = profileViewModel.Telephone;
                result.Address = profileViewModel.Address;
                var updationResult = UserManager.Update(result);
                updateSessionValues(result);
                TempData["message"] = new MessageViewModel { Message = "Profile has been updated", IsUpdated = true };
            }
            catch (Exception e)
            {
            }
            return RedirectToAction("Profile");
        }

        public ActionResult UploadUserPhoto()
        {
            HttpPostedFileBase userPhoto = Request.Files[0];
            try
            {
                AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
                string savedFileName = "";
                //Save image to Folder
                if ((userPhoto != null))
                {
                    var filename = userPhoto.FileName;
                    var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["ProfileImage"]);
                    savedFileName = Path.Combine(filePathOriginal, filename);
                    userPhoto.SaveAs(savedFileName);
                    result.ImageName = filename;
                    var updationResult = UserManager.Update(result);
                }
            }
            catch (Exception exp)
            {
                return Json(new { response = "Failed to upload. Error: " + exp.Message, status = (int)HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { filename = userPhoto.FileName, size = userPhoto.ContentLength / 1024 + "KB", response = "Successfully uploaded!", status = (int)HttpStatusCode.OK }, JsonRequestBehavior.AllowGet);
        }
        #endregion

      

        #region Pricing
        [AllowAnonymous]
        public ActionResult Pricing()
        {

            return View();
            
        
        }

        //[AllowAnonymous]
        //public ActionResult SignUp()
        //{
        //    return RedirectToAction("Pricing");
        //}

        [AllowAnonymous]
        public ActionResult SignUp()
        {
            try
            {


                //if (!string.IsNullOrEmpty(vkpy))
                //{

                //    //means we need to go direct to payment
                //    AspNetUser oModel = UserManager.FindByEmail(vkpy);
                //    if (oModel != null)
                //        return PreparePayPalPayment(oModel);

                //}

                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("SignUp");
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> SignUp(AspNetUserModel oModel)
        {
            oModel.UserName = oModel.Email;
            Utility oUtility = new Utility();
            oModel.RoleId = Utility.MemberRoleId;
            oModel.RoleName = Utility.MemberRoleName;
            var user = new AspNetUser
            {
                UserName = oModel.UserName,
                Email = oModel.Email,
                Address = oModel.Address,
                Telephone = oModel.Telephone,
                FirstName = oModel.FirstName,
                LastName = oModel.LastName,
                UserComments = string.Empty,
                LockoutEnabled = false
            };

                user.EmailConfirmed = true;
                if (!String.IsNullOrEmpty(oModel.Password))
                {
                    var result = await UserManager.CreateAsync(user, oModel.Password);
                    if (result.Succeeded)
                    {
                        //Setting role
                        var roleManager = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
                        var roleName = roleManager.FindById(oModel.RoleId).Name;
                        UserManager.AddToRole(user.Id, roleName);

                        //return PreparePayPalPayment(user);


                        // Add User Preferences for Dashboards Widgets

                        TempData["message"] = new MessageViewModel { Message = "Please check your email for Confirmation", IsSaved = true };
                        return RedirectToAction("Index","Home");
                    }
                    
                }


            return View(oModel);

        }
        #endregion
        private async Task SaveAccessToken(AspNetUser user, ClaimsIdentity identity)
        {
            var userclaims = await UserManager.GetClaimsAsync(user.Id);

            foreach (var at in (
                from claims in identity.Claims
                where claims.Type.EndsWith("access_token")
                select new Claim(claims.Type, claims.Value, claims.ValueType, claims.Issuer)))
            {

                if (!userclaims.Contains(at))
                {
                    await UserManager.AddClaimAsync(user.Id, at);
                }
            }
        }

        #endregion

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion
        private void updateSessionValues(AspNetUser user)
        {
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            string role = HttpContext.GetOwinContext().Get<ApplicationRoleManager>().FindById(result.AspNetRoles.ToList()[0].Id).Name;
            //Session["FullName"] = result.Employee.EmployeeFirstName + " " + result.Employee.EmployeeLastName;
            Session["UserID"] = result.Id;
            Session["RoleName"] = role;
        }
    }
}