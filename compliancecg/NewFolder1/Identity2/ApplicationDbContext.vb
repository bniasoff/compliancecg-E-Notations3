Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity
Imports System.ComponentModel.DataAnnotations
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.ModelConfiguration
Imports System.Data.Entity.ModelConfiguration.Configuration
Imports System.Data.Entity.Validation
Imports System.Globalization
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports System.Data.Entity.Infrastructure.Annotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace AspNetExtendingIdentityRoles.Models
    Public Class ApplicationDbContext
        Inherits IdentityDbContext(Of ApplicationUser)

        Public Overridable Overloads Property Roles As IDbSet(Of ApplicationRole)

        'Public Sub New()
        '    MyBase.New("DefaultConnection")
        'End Sub
        Public Sub New()
            MyBase.New("IdentityConnection")
        End Sub

        'Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
        '    If modelBuilder Is Nothing Then
        '        Throw New ArgumentNullException("modelBuilder")
        '    End If

        '    modelBuilder.Entity(Of IdentityUser)().ToTable("AspNetUsers")

        '    Dim table As EntityTypeConfiguration(Of ApplicationUser) = modelBuilder.Entity(Of ApplicationUser)().ToTable("AspNetUsers")
        '    table.[Property](Function(ByVal u As ApplicationUser) u.UserName).IsRequired()
        '    modelBuilder.Entity(Of ApplicationUser)().HasMany(Of IdentityUserRole)(Function(ByVal u As ApplicationUser) u.Roles)
        '    modelBuilder.Entity(Of IdentityUserRole)().HasKey(Function(ByVal r As IdentityUserRole) New With {.UserId = r.UserId, .RoleId = r.RoleId}).ToTable("AspNetUserRoles")

        '    Dim entityTypeConfiguration As EntityTypeConfiguration(Of IdentityUserLogin) = modelBuilder.Entity(Of IdentityUserLogin)().HasKey(Function(ByVal l As IdentityUserLogin) New With {.UserId = l.UserId, .LoginProvider = l.LoginProvider, .ProviderKey = l.ProviderKey}).ToTable("AspNetUserLogins")
        '    entityTypeConfiguration.HasRequired(Of IdentityUser)(Function(ByVal u As IdentityUserLogin) u.User)

        '    Dim table1 As EntityTypeConfiguration(Of IdentityUserClaim) = modelBuilder.Entity(Of IdentityUserClaim)().ToTable("AspNetUserClaims")
        '    table1.HasRequired(Of IdentityUser)(Function(ByVal u As IdentityUserClaim) u.)
        '    modelBuilder.Entity(Of IdentityRole)().ToTable("AspNetRoles")

        '    Dim entityTypeConfiguration1 As EntityTypeConfiguration(Of ApplicationRole) = modelBuilder.Entity(Of ApplicationRole)().ToTable("AspNetRoles")
        '    entityTypeConfiguration1.[Property](Function(ByVal r As ApplicationRole) r.Name).IsRequired()
        'End Sub

        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            If modelBuilder Is Nothing Then
                Throw New ArgumentNullException("modelBuilder")
            End If
            Dim user = modelBuilder.Entity(Of IdentityUser)().ToTable("AspNetUsers")


            'Dim user = modelBuilder.Entity(Of TUser)().ToTable("AspNetUsers")
            user.HasMany(Function(u) u.Roles).WithRequired().HasForeignKey(Function(ur) ur.UserId)
            user.HasMany(Function(u) u.Claims).WithRequired().HasForeignKey(Function(uc) uc.UserId)
            user.HasMany(Function(u) u.Logins).WithRequired().HasForeignKey(Function(ul) ul.UserId)
            user.[Property](Function(u) u.UserName).IsRequired().HasMaxLength(256).HasColumnAnnotation("Index", New IndexAnnotation(New IndexAttribute("UserNameIndex") With {.IsUnique = True}))
            user.[Property](Function(u) u.Email).HasMaxLength(256)

            modelBuilder.Entity(Of IdentityUserRole)().HasKey(Function(r) New With {r.UserId, r.RoleId}).ToTable("AspNetUserRoles")
            modelBuilder.Entity(Of IdentityUserLogin)().HasKey(Function(l) New With {l.LoginProvider, l.ProviderKey, l.UserId}).ToTable("AspNetUserLogins")

            modelBuilder.Entity(Of IdentityUserClaim)().ToTable("AspNetUserClaims")
            Dim role = modelBuilder.Entity(Of IdentityUserRole)().ToTable("AspNetRoles")
            role.[Property](Function(r) r.RoleId).IsRequired().HasMaxLength(256).HasColumnAnnotation("Index", New IndexAnnotation(New IndexAttribute("RoleNameIndex") With {.IsUnique = True}))
            ' role.HasMany(Function(r) r.UserId).WithRequired().HasForeignKey(Function(ur) ur.RoleId)


        End Sub

        Public Shared Function Create(options As IdentityFactoryOptions(Of ApplicationUserManager), context As IOwinContext) As ApplicationUserManager
            Dim manager = New ApplicationUserManager(New UserStore(Of ApplicationUser)(context.Get(Of ApplicationDbContext)()))

            ' Configure validation logic for usernames
            manager.UserValidator = New UserValidator(Of ApplicationUser)(manager) With {
                .AllowOnlyAlphanumericUserNames = False,
                .RequireUniqueEmail = True
            }

            ' Configure validation logic for passwords
            manager.PasswordValidator = New PasswordValidator With {
                .RequiredLength = 6,
                .RequireNonLetterOrDigit = True,
                .RequireDigit = True,
                .RequireLowercase = True,
                .RequireUppercase = True
            }

            ' Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = True
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5)
            manager.MaxFailedAccessAttemptsBeforeLockout = 5

            ' Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            ' You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", New PhoneNumberTokenProvider(Of ApplicationUser) With {
                                              .MessageFormat = "Your security code is {0}"
                                          })
            manager.RegisterTwoFactorProvider("Email Code", New EmailTokenProvider(Of ApplicationUser) With {
                                              .Subject = "Security Code",
                                              .BodyFormat = "Your security code is {0}"
                                              })
            manager.EmailService = New EmailService()
            manager.SmsService = New SmsService()
            Dim dataProtectionProvider = options.DataProtectionProvider
            If (dataProtectionProvider IsNot Nothing) Then
                manager.UserTokenProvider = New DataProtectorTokenProvider(Of ApplicationUser)(dataProtectionProvider.Create("ASP.NET Identity"))
            End If

            Return manager
        End Function
    End Class
End Namespace
