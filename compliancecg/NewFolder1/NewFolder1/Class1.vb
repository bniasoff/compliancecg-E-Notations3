Public Class ApplicationSignInManager
    Inherits SignInManager(Of ApplicationUser, String)

    Public Sub New(ByVal userManager As ApplicationUserManager, ByVal authenticationManager As IAuthenticationManager)
        MyBase.New(userManager, authenticationManager)
    End Sub

    Public Overrides Function CreateUserIdentityAsync(ByVal user As ApplicationUser) As Task(Of ClaimsIdentity)
        Return user.GenerateUserIdentityAsync(CType(MyBase.UserManager, ApplicationUserManager))
    End Function

    Public Overrides Async Function PasswordSignInAsync(ByVal userEmail As String, ByVal password As String, ByVal isPersistent As Boolean, ByVal shouldLockout As Boolean) As Task(Of SignInStatus)
        Dim signInStatu As SignInStatus

        If Me.UserManager IsNot Nothing Then
            Dim userAwaiter As Task(Of ApplicationUser) = Me.UserManager.FindByEmailAsync(userEmail)
            Dim tUser As ApplicationUser = Await userAwaiter

            If tUser IsNot Nothing Then
                Dim cultureAwaiter1 As Task(Of Boolean) = Me.UserManager.IsLockedOutAsync(tUser.Id)

                If Not Await cultureAwaiter1 Then
                    Dim cultureAwaiter2 As Task(Of Boolean) = Me.UserManager.CheckPasswordAsync(tUser, password)

                    If Not Await cultureAwaiter2 Then

                        If shouldLockout Then
                            Dim cultureAwaiter3 As Task(Of IdentityResult) = Me.UserManager.AccessFailedAsync(tUser.Id)
                            Await cultureAwaiter3
                            Dim cultureAwaiter4 As Task(Of Boolean) = Me.UserManager.IsLockedOutAsync(tUser.Id)

                            If Await cultureAwaiter4 Then
                                signInStatu = SignInStatus.LockedOut
                                Return signInStatu
                            End If
                        End If

                        signInStatu = SignInStatus.Failure
                    Else
                        Dim cultureAwaiter5 As Task(Of IdentityResult) = Me.UserManager.ResetAccessFailedCountAsync(tUser.Id)
                        Await cultureAwaiter5
                        Dim cultureAwaiter6 As Task(Of SignInStatus) = Me.SignInOrTwoFactor(tUser, isPersistent)
                        signInStatu = Await cultureAwaiter6
                    End If
                Else
                    signInStatu = SignInStatus.LockedOut
                End If
            Else
                signInStatu = SignInStatus.Failure
            End If
        Else
            signInStatu = SignInStatus.Failure
        End If

        Return signInStatu
    End Function

    Private Async Function SignInOrTwoFactor(ByVal user As ApplicationUser, ByVal isPersistent As Boolean) As Task(Of SignInStatus)
        Dim signInStatu As SignInStatus
        Dim str As String = Convert.ToString(user.Id)
        Dim cultureAwaiter As Task(Of Boolean) = Me.UserManager.GetTwoFactorEnabledAsync(user.Id)

        If Await cultureAwaiter Then
            Dim providerAwaiter As Task(Of IList(Of String)) = Me.UserManager.GetValidTwoFactorProvidersAsync(user.Id)
            Dim listProviders As IList(Of String) = Await providerAwaiter

            If listProviders.Count > 0 Then
                Dim cultureAwaiter2 As Task(Of Boolean) = AuthenticationManagerExtensions.TwoFactorBrowserRememberedAsync(Me.AuthenticationManager, str)

                If Not Await cultureAwaiter2 Then
                    Dim claimsIdentity As ClaimsIdentity = New ClaimsIdentity("TwoFactorCookie")
                    claimsIdentity.AddClaim(New Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", str))
                    Me.AuthenticationManager.SignIn(New ClaimsIdentity() {claimsIdentity})
                    signInStatu = SignInStatus.RequiresVerification
                    Return signInStatu
                End If
            End If
        End If

        Dim cultureAwaiter3 As Task = Me.SignInAsync(user, isPersistent, False)
        Await cultureAwaiter3
        signInStatu = SignInStatus.Success
        Return signInStatu
    End Function

    Public Shared Function Create(ByVal options As IdentityFactoryOptions(Of ApplicationSignInManager), ByVal context As IOwinContext) As ApplicationSignInManager
        Return New ApplicationSignInManager(context.GetUserManager(Of ApplicationUserManager)(), context.Authentication)
    End Function
End Class
