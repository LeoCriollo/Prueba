using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using Microsoft.Owin.Security;
using OfficeOpenXml;

using DoleEcIntranet.Models;
using System.DirectoryServices.AccountManagement;
using DoleEcIntranet.Tools;

namespace DoleEcIntranet.Controllers
{
    [DoleEcIntranetAuthorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            LoginViewModel model =new LoginViewModel();
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            //1.- Recuperamos el usuario para saber si ya esta ingresado
            ApplicationUser userExist = await UserManager.FindByNameAsync(model.Username);

            //Si Usuario existe 
            if (userExist != null)
            {
                //Si usuario tiene el password en null
                //es un usuario Directorio Activo
                if (string.IsNullOrEmpty(userExist.PasswordHash))
                {
                    //Verificar que este en el Directorio Activo
                    var isAutenticateAD = AuthenticateAD(model.Username, model.Password);
                    //Si existe Logonearse sin PASSWORD
                    if (isAutenticateAD)
                    {
                        await SignInAsync(userExist, model.RememberMe);
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Acceso Incorrecto");
                        return View(model);

                    }

                }
                else
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, change to shouldLockout: true
                    var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, shouldLockout: false);

                    switch (result)
                    {
                        case SignInStatus.Success:
                            return RedirectToLocal(returnUrl);
                        case SignInStatus.LockedOut:
                            return View("Lockout");
                        case SignInStatus.RequiresVerification:
                            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                        case SignInStatus.Failure:
                        default:
                            ModelState.AddModelError("", "Acceso Incorrecto");
                            return View(model);
                    }
                }
            }
            else
            {
                var isAutenticate = AuthenticateAD(model.Username, model.Password);
                if (isAutenticate)
                {
                    InfoUserADModel infouserad = InfoUserAD(model.Username);
                    if (infouserad != null)
                    {
                        //Seteamos el Usuario del AD
                        var userAD = new ApplicationUser
                        {
                            UserName = model.Username,
                            Email = infouserad.EmailAddress                          

                            
                        };

                        //Creamos el usario sin password
                        var result = UserManager.Create(userAD);
                        //Seteamos el ROl inicial
                        await UserManager.AddToRoleAsync(userAD.Id, Tools.Enum.RolUser);
                        await UserManager.AddClaimAsync(userAD.Id, new Claim(Tools.Enum.ClaimCodigoEmpleado, infouserad.EmployeeId));

                        //Autenticarse
                        await SignInAsync(userAD, model.RememberMe);
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Usuario en AD no tiene Info");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Acceso Incorrecto");
                    return View(model);

                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isPersistent"></param>
        /// <returns></returns>
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        /// <summary>
        /// Validar si el usuario existe en el directorio activo
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool AuthenticateAD(string username, string password)
        {
            bool aud = false;
            using (var context = new PrincipalContext(ContextType.Domain, System.Configuration.ConfigurationManager.AppSettings.Get("DomainAD")))
            {
                aud = context.ValidateCredentials(username, password);
            }

            if (!aud)
            {
                using (var context = new PrincipalContext(ContextType.Domain, System.Configuration.ConfigurationManager.AppSettings.Get("DomainAD_2")))
                {
                    aud = context.ValidateCredentials(username, password);
                }
            }

            return aud;
        }

        /// <summary>
        /// Obtener informacion del usuario cuando se registra desde el AD
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private InfoUserADModel InfoUserAD(string username)
        {
            InfoUserADModel infoUser1 = null;
            using (var context = new PrincipalContext(ContextType.Domain, System.Configuration.ConfigurationManager.AppSettings.Get("DomainAD")))
            {
                UserPrincipal user = UserPrincipal.FindByIdentity(context, username);
                if (user != null)
                {
                    infoUser1 = new InfoUserADModel()
                    {
                        EmployeeId = user.EmployeeId,
                        EmailAddress = user.EmailAddress,
                        NameEmployee = user.DisplayName

                    };
                    return infoUser1;

                }
            }

            if (infoUser1 == null)
            {
                using (var context = new PrincipalContext(ContextType.Domain, System.Configuration.ConfigurationManager.AppSettings.Get("DomainAD_2")))
                {
                    UserPrincipal user = UserPrincipal.FindByIdentity(context, username);
                    if (user != null)
                    {
                        infoUser1 = new InfoUserADModel()
                        {
                            EmployeeId = user.EmployeeId,
                            EmailAddress = user.EmailAddress,
                            NameEmployee = user.DisplayName

                        };
                        return infoUser1;

                    }
                }

            }

            return null;

        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
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

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
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
        // GET: /Account/Register
        //[AllowAnonymous]
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdmin)]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        //[AllowAnonymous]
        [ValidateAntiForgeryToken]
        [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdmin)]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                //Validar que no exista en el Directorio Activo
                if (InfoUserAD(model.Username) == null)
                {

                    var user = new ApplicationUser { UserName = model.Username };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await UserManager.AddToRoleAsync(user.Id, Tools.Enum.RolUser);
                        await UserManager.AddClaimAsync(user.Id, new Claim(Tools.Enum.ClaimCodigoEmpleado, model.EmployeeId.ToString()));
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", "Home");
                    }
                    AddErrors(result);
                }
                else
                {
                    ModelState.AddModelError("", "Username ya existe en el AD");

                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


       // [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdmin)]
        [AllowAnonymous]
        public ActionResult CargarUsuarios()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
       // [DoleEcIntranetAuthorize(Roles = Tools.Enum.RolAdmin)]
        public async Task<ActionResult> CargarUsuarios(LoadExcel model)
        {

            if (ModelState.IsValid)
            {
                

                using (ExcelPackage excelPackage = new ExcelPackage(model.file.InputStream))
                {
                    ExcelWorkbook excelWorkBook = excelPackage.Workbook;
                    ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets.First();

                    var start = excelWorksheet.Dimension.Start;
                    var end = excelWorksheet.Dimension.End;

                    for (int row = start.Row + 1; row <= end.Row; row++)
                    {

                        int codigoEmplado = int.Parse(excelWorksheet.Cells[row, 1].Value.ToString());
                        string nombreCompleto = "";

                        if (excelWorksheet.Cells[row, 3].Value != null)
                            nombreCompleto = excelWorksheet.Cells[row, 2].Value.ToString().TrimEnd() + " " + excelWorksheet.Cells[row, 3].Value.ToString().TrimEnd();
                        else
                            nombreCompleto = excelWorksheet.Cells[row, 2].Value.ToString().TrimEnd();


                        //crear username
                        string[] username1 = excelWorksheet.Cells[row, 2].Value.ToString().TrimEnd().Split(new char[0]);
                        string userName = "";

                        if(username1.Length > 1)
                        {
                            if (excelWorksheet.Cells[row, 3].Value != null)
                                userName = excelWorksheet.Cells[row, 3].Value.ToString().TrimEnd().Substring(0, 1) + username1[0] + username1[1].Substring(0, 1);
                            else
                                userName = username1[0] + username1[1].Substring(0, 1);

                        }
                        else
                        {
                            if (excelWorksheet.Cells[row, 3].Value != null)
                                userName = excelWorksheet.Cells[row, 3].Value.ToString().TrimEnd().Substring(0, 1) + username1[0];
                            else
                                userName = username1[0] + "1";

                        }

                        string passWord = "Dole.2019";


                        ApplicationUser userExist = await UserManager.FindByNameAsync(userName);
                        InfoUserADModel infouserad = InfoUserAD(userName);
                        if (userExist == null && infouserad == null)
                        {


                            var user = new ApplicationUser { UserName = userName.ToLower() };
                            var result = await UserManager.CreateAsync(user, passWord);
                            if (result.Succeeded)
                            {
                                await UserManager.AddToRoleAsync(user.Id, Tools.Enum.RolUser);
                                await UserManager.AddClaimAsync(user.Id, new Claim(Tools.Enum.ClaimCodigoEmpleado, codigoEmplado.ToString()));
                                await UserManager.AddClaimAsync(user.Id, new Claim(Tools.Enum.ClaimNombreEmpleado, nombreCompleto));

                                excelWorksheet.Cells[row, 5].Value = userName;

                            }
                        }
                        else
                        {
                            excelWorksheet.Cells[row, 10].Value = "UserName ya existe en la Base o en el AD: " + userName; 

                        }

                    }

                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    return File(excelPackage.GetAsByteArray(), contentType, "Carga_Masiva_Usuaarios" + ".xlsx"); //Name File
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(int userId, string code)
        {
            if (userId == default(int) || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
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
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
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
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
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
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == default(int))
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
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
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
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
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
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

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
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
    }
}