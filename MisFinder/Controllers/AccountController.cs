﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MisFinder.Domain.Models;
using MisFinder.Domain.Models.ViewModel;
using MisFinder.Data.Persistence.IRepositories;
using Microsoft.AspNetCore.Authorization;

namespace MisFinder.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        [AllowAnonymous]

        public IActionResult Register () => View();
        
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.UserName);
                if (user == null)
                {
                    user = new ApplicationUser { UserName = model.UserName, Email = model.UserName };
                    var result = await userManager.CreateAsync(user, model.Password);
                    await userManager.AddToRoleAsync(user, "User");
                    if (!result.Succeeded)
                    {
                        foreach (var Error in result.Errors)
                        {
                            ModelState.AddModelError("Password", Error.Description);
                        }
                        return View(model);
                    }
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var ConfirmEmail = Url.Action("ConfirmEmailAddress", "Account",
                        new { token = token, email = user.Email }, Request.Scheme);
                    System.IO.File.WriteAllText("Emailtoken.txt", ConfirmEmail);
                    return RedirectToAction("Success", "Account", new { comment = "Registration Complete,Check your Email for Confirmation" });
                }
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
                    return View("Success", new { comment = "Email Confirmed" });
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

        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
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
                        {if (await userManager.IsInRoleAsync(user, "Admin"))
                            {
                                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                                var ConfirmEmail = Url.Action("ConfirmEmailAddress", "Account",
                                    new { token = token, email = user.Email }, Request.Scheme);
                                System.IO.File.WriteAllText("Emailtoken.txt", ConfirmEmail);
                            }
                            ModelState.AddModelError("", "Email is Not Confirmed");
                            return View();
                        }
                    
                    
                   var result = await signInManager.PasswordSignInAsync(user, model.Password, true, false);
                    if (result.Succeeded)
                    {   //resetting their lockout count 
                        await userManager.ResetAccessFailedCountAsync(user);
                        if (await userManager.IsInRoleAsync(user, "Admin"))
                             return RedirectToAction("Index","Dashboard",new { area = "Admin" });
                        return LocalRedirect(returnUrl ?? "/");
                    }
                    //if user access failed, log it(lockout count) to be able to implement lockout
                    await userManager.AccessFailedAsync(user);
                    if(await userManager.IsLockedOutAsync(user))
                    {
                        //email them that they tried to access their portal with wrong data
                    }
                }
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Invalid User or Password");
            }
            return View(model);

        }
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
                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                        var resetUrl = Url.Action("ResetPassword","Account", 
                            new { token = token, email = email }, Request.Scheme);
                    System.IO.File.WriteAllText("resetlink.txt", resetUrl);
                }
                else
                {
                    //send dem mail that they dont have an account with your company
                    ModelState.AddModelError("", "Invalid Email Address");
                }
                
                return View("Success", new { comment = "Check your mail for Password reset link" });
            }
            return View();

        }
        [HttpGet]
        [AllowAnonymous]

        public IActionResult ResetPassword(string token,string email)
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
                        foreach(var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View();
                    }
                    //if the user is lockedout when he resets his password
                    //we set the lock out end date to now
                    if(await userManager.IsLockedOutAsync(user))
                    {
                        await userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow);
                    }
                    return View("Success", new { comment = "Kindly Login" });
                }
                ModelState.AddModelError("", "Invalid Request");
            }
            return View();
        }
        [AcceptVerbs("Get","Post")]
        [AllowAnonymous]

        public async Task<IActionResult> EmailExist([Bind(Prefix ="UserName")] string email)
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