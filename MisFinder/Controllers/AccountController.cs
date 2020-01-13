using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MisFinder.Data.Notification.Email;
using MisFinder.Domain.Models;
using MisFinder.Domain.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MisFinder.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailNotifier emailNotifier;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IEmailNotifier emailNotifier)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailNotifier = emailNotifier;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.UserName.ToUpper());
                if (user == null)
                {
                    user = new ApplicationUser { UserName = model.UserName, Email = model.UserName, FirstName = model.FirstName, LastName = model.LastName };
                    var result = await userManager.CreateAsync(user, model.Password);
                    //user = await userManager.FindByEmailAsync(model.UserName);
                    if (!result.Succeeded)
                    {
                        foreach (var Error in result.Errors)
                        {
                            ModelState.AddModelError("Password", Error.Description);
                        }
                        return View(model);
                    }
                    await userManager.AddToRoleAsync(user, "User");
                    await userManager.AddClaimAsync(user, new Claim("firstname", model.FirstName));

                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var ConfirmEmail = Url.Action("ConfirmEmailAddress", "Account",
                        new { token = token, email = user.Email }, Request.Scheme);
                    var message = new Dictionary<string, string>
                    {
                        { "FName",$"{model.FirstName}" },
                        {"EmailLink", $"{ConfirmEmail}" }
                    };
                    await emailNotifier.SendEmailAsync(model.UserName, "Email Confirmation", message, "EmailConfirmation");
                    System.IO.File.WriteAllText("Emailtoken.txt", ConfirmEmail);

                    var msg = "Swal.fire({position: 'top-end',icon: 'success',title: 'Registration Complete, Check your Email for Confirmation', showConfirmButton: false, timer: 3500})";
                    TempData["message"] = $"<script type='text/javascript'> {msg} </script>";
                    ViewBag.Trial = $"<script type='text/javascript'> {msg} </script>";

                    //   TempData["message"] = "Registration Complete, Check your Email for Confirmation";
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Account already Exist");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmailAddress(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    //   ViewBag.Comment = "Email Confirmed, Login ";
                    var msg = "Swal.fire({position: 'top-end',icon: 'success',title: 'Mail Confirmation Was Successful,Proceed to Login', showConfirmButton: false, timer: 3500})";
                    TempData["message"] = $"<script type='text/javascript'> {msg} </script>";
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.UserName);
                //checking if user exist and is not locked out
                if (user != null && !await userManager.IsLockedOutAsync(user))
                {
                    await signInManager.SignOutAsync();

                    //Making Sure the Email is Confirmed before User sign in.
                    if (!await userManager.IsEmailConfirmedAsync(user))
                    {
                        if (await userManager.IsInRoleAsync(user, "SystemAdmin"))
                        {
                            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                            var ConfirmEmail = Url.Action("ConfirmEmailAddress", "Account",
                            new { token = token, email = user.Email }, Request.Scheme);
                            var message = new Dictionary<string, string>
                            {
                               { "FName",$"{user.FirstName}" },
                               {"EmailLink", $"{ConfirmEmail}" }
                            };
                            await emailNotifier.SendEmailAsync(model.UserName, "Email Confirmation", message, "EmailConfirmation");
                            //System.IO.File.WriteAllText("Emailtoken.txt", ConfirmEmail);
                        }
                        ModelState.AddModelError("", "Email is Not Confirmed");
                        return View();
                    }
                    if (user.IsBlackListed)
                    {
                        ModelState.AddModelError("", "Account has been Suspended  ");
                        return View();
                    }

                    var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {   //resetting their lockout count
                        await userManager.ResetAccessFailedCountAsync(user);
                        if (await userManager.IsInRoleAsync(user, "Admin"))
                            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                        return LocalRedirect(returnUrl ?? "/");
                    }
                    //if user access failed, log it(lockout count) to be able to implement lockout
                    await userManager.AccessFailedAsync(user);
                    if (await userManager.IsLockedOutAsync(user))
                    {
                        //email them that they tried to access their portal with wrong data
                    }
                }
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Invalid User or Password");
            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            //request a redirct to external login provider
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        //public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        //{
        //}

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([Required][EmailAddress] string email)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user == null || !(await userManager.IsEmailConfirmedAsync(user)))
                {
                    //refactor this to not reveal that th eemail doesnt eexist
                    ModelState.AddModelError("", "Invalid Email Address");
                    return View();
                    // or send dem mail that they dont have an account with your company
                }
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var resetUrl = Url.Action("ResetPassword", "Account", new { token = token, email = email }, Request.Scheme);
                var message = new Dictionary<string, string>
                    {
                        { "FName",$"{user.FirstName}" },
                        {"EmailLink", $"{resetUrl}" }
                    };
                await emailNotifier.SendEmailAsync(user.UserName, "Reset Password", message, "forgotpassword");
                // System.IO.File.WriteAllText("resetlink.txt", resetUrl);
                //  ViewBag.Comment = "Check your mail for Password reset link ";
                //refactor this later
                var msg = "Swal.fire({position: 'top-end',icon: 'success',title: 'Check your mail for Password reset link', showConfirmButton: false, timer: 3500})";
                TempData["message"] = $"<script type='text/javascript'> {msg} </script>";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            return View(new ResetViewModel { Token = token, Email = email });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View();
                    }
                    //if the user is lockedout when he resets his password
                    //we set the lock out end date to now
                    if (await userManager.IsLockedOutAsync(user))
                    {
                        await userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow);
                    }
                    ViewBag.Comment = "Kindly Go ahead to Login";
                    var msg = "Swal.fire({position: 'top-end',icon: 'success',title: 'Password Reset Successful. Kindly Login ', showConfirmButton: false, timer: 3500})";
                    TempData["message"] = $"<script type='text/javascript'> {msg} </script>";

                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("", "Invalid Request");
            }
            return View();
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> EmailExist([Bind(Prefix = "UserName")] string email)
        {
            var userEmail = await userManager.FindByEmailAsync(email);
            return userEmail != null ? Json(true) : Json("Invalid UserName or Password");
        }

        public IActionResult Success(string comment)
        {
            ViewBag.Comment = comment;
            return View();
        }
    }
}