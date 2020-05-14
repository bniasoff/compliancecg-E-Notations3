'Interface ISportUserManager
'    Function FindAsync(ByVal userName As String, ByVal password As String) As Task(Of FrUser)
'    Function SignInAsync(ByVal user As FrUser, ByVal isPersistent As Boolean) As Task
'    Sub SignOut()
'    Function CreateAsync(ByVal user As FrUser, ByVal password As String) As Task(Of IdentityResult)
'End Interface

'Public Class SportUserManager
'    Inherits UserManager(Of FrUser)
'    Implements ISportUserManager

'    Private ReadOnly _userService As IUserService
'    Private ReadOnly _authenticationManager As IAuthenticationManager

'    Public Sub New(ByVal userService As IUserService, ByVal authenticationManager As IAuthenticationManager)
'        MyBase.New(New SportUserStore(Of FrUser)(userService))
'        _userService = userService
'        _authenticationManager = authenticationManager
'    End Sub

'    Public Sub New(ByVal userService As IUserService)
'        MyBase.New(New SportUserStore(Of FrUser)(userService))
'        _userService = userService
'    End Sub

'    Public Shared Function Create(ByVal options As IdentityFactoryOptions(Of SportUserManager), ByVal context As IOwinContext) As SportUserManager
'        Dim manager = New SportUserManager(DependencyResolver.Current.GetService(Of IUserService)())
'        manager.UserValidator = New UserValidator(Of FrUser)(manager) With {
'            .AllowOnlyAlphanumericUserNames = False,
'            .RequireUniqueEmail = False
'        }
'        manager.PasswordValidator = New SportPasswordValidator With {
'            .RequiredLength = 6,
'            .RequireNonLetterOrDigit = True,
'            .RequireDigit = True,
'            .RequireLowercase = True,
'            .RequireUppercase = True
'        }
'        manager.UserLockoutEnabledByDefault = True
'        manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5)
'        manager.MaxFailedAccessAttemptsBeforeLockout = 5
'        manager.RegisterTwoFactorProvider("Phone Code", New PhoneNumberTokenProvider(Of FrUser) With {
'            .MessageFormat = "Your security code is {0}"
'        })
'        manager.RegisterTwoFactorProvider("Email Code", New EmailTokenProvider(Of FrUser) With {
'            .Subject = "Security Code",
'            .BodyFormat = "Your security code is {0}"
'        })
'        Dim dataProtectionProvider = options.DataProtectionProvider

'        If dataProtectionProvider IsNot Nothing Then
'            manager.UserTokenProvider = New DataProtectorTokenProvider(Of FrUser)(dataProtectionProvider.Create("ASP.NET Identity"))
'        End If

'        Return manager
'    End Function

'    Public Overrides Function FindAsync(ByVal userName As String, ByVal password As String) As Task(Of FrUser)
'        Dim task = task(Of FrUser).Run(Function()
'                                           Dim user = _userService.SearchUser(New FrUser With {
'                                               .userName = userName,
'                                               .password = password
'                                           }).FirstOrDefault()
'                                           Return user
'                                       End Function)
'        Return task
'    End Function

'    Public Async Function SignInAsync(ByVal user As FrUser, ByVal isPersistent As Boolean) As Task
'        SignOut()
'        Dim identity = Await MyBase.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie)
'        _authenticationManager.SignIn(New AuthenticationProperties() With {
'            .isPersistent = isPersistent
'        }, identity)
'    End Function

'    Public Overrides Async Function CreateAsync(ByVal user As FrUser, ByVal password As String) As Task(Of IdentityResult)
'        Dim aa As IUserPasswordStore(Of FrUser) = TryCast(Store, IUserPasswordStore(Of FrUser))
'        If CObj(user) Is Nothing Then Return New IdentityResult(String.Format(SystemMessage.NewException, "CreateAsync User Null"))
'        If password Is Nothing Then Return New IdentityResult(String.Format(SystemMessage.NewException, "CreateAsync Password Null"))
'        Dim sssS As IdentityResult = Await Me.UpdatePassword(aa, user, password)
'        Dim s As IdentityResult = Await MyBase.CreateAsync(user)
'        Dim ss = _userService
'        Return Nothing
'    End Function

'    Public Sub SignOut()
'        _authenticationManager.SignOut()
'    End Sub
'End Class



''Public Class CustomUserValidator(Of TUser As {Class, Microsoft.AspNet.Identity.IUser})
''    Inherits IIdentityValidator(Of TUser)

''    Private ReadOnly _userManager As UserManager(Of TUser)

''    Public Sub New(ByVal manager As UserManager(Of TUser))
''        _userManager = manager
''    End Sub

''    Public Async Function ValidateAsync(ByVal user As TUser) As Task(Of IdentityResult)
''        Dim errors = New List(Of String)()

''        If _userManager IsNot Nothing Then
''            Dim existingAccount = Await _userManager.FindByNameAsync(user.UserName)
''            If existingAccount IsNot Nothing AndAlso existingAccount.Id <> user.Id Then errors.Add("User name already in use ...")
''        End If

''        Return If(errors.Any(), IdentityResult.Failed(errors.ToArray()), IdentityResult.Success)
''    End Function
''End Class

''Public Overridable Function CreateAsync(ByVal user As User) As Task
''    Using _conn = New SqlConnection(_dbConn)
''        Dim result = _conn.ExecuteAsync("users_UserCreate", New With {Key
''            .UserId = user.Id, Key
''            .UserName = user.UserName, Key
''            .PasswordHash = user.PasswordHash, Key
''            .SecurityStamp = user.SecurityStamp
''        }, commandType:=CommandType.StoredProcedure).ConfigureAwait(True)
''        Return Task.FromResult(result)
''    End Using
''End Function




'Class SurroundingClass
'    Public Function CreateAsync(ByVal user As ApplicationUser) As Task
'        If user Is Nothing Then Throw New ArgumentNullException("user")
'        Dim u = getUser(user)
'        userRepository.Add(u)
'        Return userRepository.SaveChangesAsync()
'    End Function

'    Private Function getUser(ByVal applicationUser As ApplicationUser) As IdentityPoco.User
'        If applicationUser Is Nothing Then Return Nothing
'        Dim user = New IdentityPoco.User()
'        populateUser(user, applicationUser)
'        Return user
'    End Function

'    Private Sub populateUser(ByVal user As IdentityPoco.User, ByVal ApplicationUser As ApplicationUser)
'        user.UserId = ApplicationUser.Id
'        user.UserName = ApplicationUser.UserName
'        user.PasswordHash = ApplicationUser.PasswordHash
'        user.SecurityStamp = ApplicationUser.SecurityStamp
'    End Sub

'    Public Function FindByNameAsync(ByVal userName As String) As Task(Of ApplicationUser)
'        Dim user = userRepository.FindByUserName(userName)
'        Dim applicationUser = getApplicationUser(user)
'        Return Task.FromResult(Of ApplicationUser)(applicationUser)
'    End Function

'    Private Function getApplicationUser(ByVal user As IdentityPoco.User) As ApplicationUser
'        If user Is Nothing Then Return Nothing
'        Dim ApplicationUser = New ApplicationUser()
'        populateApplicationUser(ApplicationUser, user)
'        Return ApplicationUser
'    End Function

'    Private Sub populateApplicationUser(ByVal ApplicationUser As ApplicationUser, ByVal user As IdentityPoco.User)
'        ApplicationUser.Id = user.UserId
'        ApplicationUser.UserName = user.UserName
'        ApplicationUser.PasswordHash = user.PasswordHash
'        ApplicationUser.SecurityStamp = user.SecurityStamp
'    End Sub
'End Class




'Class SurroundingClass
'    Public Async Function Register(ByVal model As RegisterViewModel) As Task(Of ActionResult)
'        If ModelState.IsValid Then
'            Dim user = New ApplicationUser With {
'                .UserName = model.Email,
'                .Email = model.Email
'            }
'            Dim appDbContext = HttpContext.GetOwinContext().[Get](Of ApplicationDbContext)()

'            Using context = New MyEntities()

'                Using transaction = appDbContext.Database.BeginTransaction()

'                    Try
'                        Dim DataModel = New UserMaster()
'                        DataModel.Gender = model.Gender.ToString()
'                        DataModel.Name = String.Empty
'                        Dim result = Await UserManager.CreateAsync(user, model.Password)

'                        If result.Succeeded Then
'                            Await Me.UserManager.AddToRoleAsync(user.Id, model.Role.ToString())
'                            Me.AddUser(DataModel, context)
'                            transaction.Commit()
'                            Return View("DisplayEmail")
'                        End If

'                        AddErrors(result)
'                  Catch ex As Exception
'logger.Error(ex)
'                        transaction.Rollback()
'                        Return Nothing
'                    End Try
'                End Using
'            End Using
'        End If

'        Return View(model)
'    End Function

'    Public Function AddUser(ByVal _addUser As UserMaster, ByVal _context As MyEntities) As Integer
'        _context.UserMaster.Add(_addUser)
'        _context.SaveChanges()
'        Return 0
'    End Function
'End Class

Imports System.Web.UI.WebControls
Imports DevExpress.Web
Imports DevExpress.Web.ASPxRichEdit


Module RibbonCustomizationDemoHelper
        Function GetCustomRibbonTab(ByVal isExtenernalRibbon As Boolean) As RibbonTab
            Dim ribbonTab As RibbonTab = New RibbonTab("Home")

            If isExtenernalRibbon Then
                ribbonTab.Groups.AddRange(New RibbonGroup() {GetCommonGroup(), GetFontGroup(isExtenernalRibbon), GetViewGroup()})
            Else
                ribbonTab.Groups.AddRange(New RibbonGroup() {GetCommonGroup(), GetUndoGroup(), GetFontGroup(isExtenernalRibbon), GetPagesGroup(), GetViewGroup()})
            End If

            Return ribbonTab
        End Function

        Private Function GetCommonGroup() As RERFileCommonGroup
            Dim commonGroup = New RERFileCommonGroup()
            commonGroup.Items.AddRange(New RibbonItemBase() {New RERNewCommand(RibbonItemSize.Small) With {
                .Text = "New Document",
                .ToolTip = "Ctrl + N"
            }, New RERPrintCommand(RibbonItemSize.Small) With {
                .Text = "Print Document",
                .ToolTip = "Ctrl + P"
            }})
            Return commonGroup
        End Function

        Private Function GetUndoGroup() As RERUndoGroup
            Dim undoGroup = New RERUndoGroup()
            undoGroup.Items.AddRange(New RibbonItemBase() {New RERUndoCommand(RibbonItemSize.Large) With {
                .Text = "Undo",
                .ToolTip = "Ctrl + Z"
            }, New RERRedoCommand(RibbonItemSize.Large) With {
                .Text = "Redo",
                .ToolTip = "Ctrl + Y"
            }})
            Return undoGroup
        End Function

        Private Function GetFontGroup(ByVal isExtenernalRibbon As Boolean) As RERFontGroup
            Dim fontGroup = New RERFontGroup() With {
                .ShowDialogBoxLauncher = isExtenernalRibbon
            }
            fontGroup.Items.AddRange(New RibbonItemBase() {PrepareComboBoxCommand(New RERFontNameCommand()), PrepareComboBoxCommand(New RERFontSizeCommand()), New RERFontBoldCommand(RibbonItemSize.Large) With {
                .Text = "Bold",
                .ToolTip = "Ctrl + B"
            }, New RERFontItalicCommand(RibbonItemSize.Large) With {
                .Text = "Italic",
                .ToolTip = "Ctrl + I"
            }})
            Return fontGroup
        End Function

        Private Function PrepareComboBoxCommand(ByVal command As RERComboBoxCommandBase) As RERComboBoxCommandBase
            command.FillItems()
            command.PropertiesComboBox.Width = Unit.Pixel(100)
            Return command
        End Function

        Private Function GetPagesGroup() As RERPagesGroup
            Dim pagesGroup = New RERPagesGroup()
            pagesGroup.Items.Add(New RibbonItemBase() {New RERPageMarginsCommand(), New RERChangeSectionPageOrientationCommand(), New RERChangeSectionPaperKindCommand()})
            Return pagesGroup
        End Function

        Private Function GetViewGroup() As RERViewGroup
            Dim viewGroup = New RERViewGroup()
            viewGroup.Items.AddRange(New RibbonItemBase() {New RERToggleShowHorizontalRulerCommand(), New RERToggleFullScreenCommand() With {
                .ToolTip = "F11"
            }})
            Return viewGroup
        End Function
    End Module
