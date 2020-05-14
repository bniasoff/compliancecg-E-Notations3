Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Common
Imports System.Data.Entity
Imports System.Data.Entity.Core.Objects
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Infrastructure.Annotations
Imports System.Data.Entity.Validation
Imports System.Data.SqlClient
Imports System.Diagnostics.CodeAnalysis
Imports System.Globalization
Imports System.Linq
Imports compliancecg.My.Resources

Namespace Microsoft.AspNet.Identity.EntityFramework
    Public Class IdentityDbContext
        Inherits IdentityDbContext(Of IdentityUser, IdentityRole, String, IdentityUserLogin, IdentityUserRole, IdentityUserClaim)
        Public Sub New()
            Me.New("DefaultConnection")
        End Sub
        'Public Sub New()
        '    Me.New("IdentityEntities")
        'End Sub

        Public Sub New(ByVal nameOrConnectionString As String)
            MyBase.New(nameOrConnectionString)
        End Sub

        Public Sub New(ByVal existingConnection As DbConnection, ByVal model As DbCompiledModel, ByVal contextOwnsConnection As Boolean)
            MyBase.New(existingConnection, model, contextOwnsConnection)
        End Sub

        Public Sub New(ByVal model As DbCompiledModel)
            MyBase.New(model)
        End Sub

        Public Sub New(ByVal existingConnection As DbConnection, ByVal contextOwnsConnection As Boolean)
            MyBase.New(existingConnection, contextOwnsConnection)
        End Sub

        Public Sub New(ByVal nameOrConnectionString As String, ByVal model As DbCompiledModel)
            MyBase.New(nameOrConnectionString, model)
        End Sub
    End Class

    Public Class IdentityDbContext(Of TUser As IdentityUser)
        Inherits IdentityDbContext(Of TUser, IdentityRole, String, IdentityUserLogin, IdentityUserRole, IdentityUserClaim)

        Public Sub New()
            Me.New("DefaultConnection")
        End Sub

        Public Sub New(ByVal nameOrConnectionString As String)
            Me.New(nameOrConnectionString, True)
        End Sub

        Public Sub New(ByVal nameOrConnectionString As String, ByVal throwIfV1Schema As Boolean)
            MyBase.New(nameOrConnectionString)

            If throwIfV1Schema AndAlso IsIdentityV1Schema(Me) Then
                Throw New InvalidOperationException(IdentityResources.IdentityV1SchemaError)
            End If
        End Sub

        Public Sub New(ByVal existingConnection As DbConnection, ByVal model As DbCompiledModel, ByVal contextOwnsConnection As Boolean)
            MyBase.New(existingConnection, model, contextOwnsConnection)
        End Sub

        Public Sub New(ByVal model As DbCompiledModel)
            MyBase.New(model)
        End Sub

        Public Sub New(ByVal existingConnection As DbConnection, ByVal contextOwnsConnection As Boolean)
            MyBase.New(existingConnection, contextOwnsConnection)
        End Sub

        Public Sub New(ByVal nameOrConnectionString As String, ByVal model As DbCompiledModel)
            MyBase.New(nameOrConnectionString, model)
        End Sub

        Friend Shared Function IsIdentityV1Schema(ByVal db As DbContext) As Boolean
            Dim originalConnection = TryCast(db.Database.Connection, SqlConnection)

            If originalConnection Is Nothing Then
                Return False
            End If

            If db.Database.Exists() Then

                Using tempConnection = New SqlConnection(originalConnection.ConnectionString)
                    tempConnection.Open()
                    Return VerifyColumns(tempConnection, "AspNetUsers", "Id", "UserName", "PasswordHash", "SecurityStamp", "Discriminator") AndAlso VerifyColumns(tempConnection, "AspNetRoles", "Id", "Name") AndAlso VerifyColumns(tempConnection, "AspNetUserRoles", "UserId", "RoleId") AndAlso VerifyColumns(tempConnection, "AspNetUserClaims", "Id", "ClaimType", "ClaimValue", "User_Id") AndAlso VerifyColumns(tempConnection, "AspNetUserLogins", "UserId", "ProviderKey", "LoginProvider")
                End Using
            End If

            Return False
        End Function

        <SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification:="Reviewed")>
        Friend Shared Function VerifyColumns(ByVal conn As SqlConnection, ByVal table As String, ParamArray columns As String()) As Boolean
            Dim tableColumns = New List(Of String)()

            Using command = New SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=@Table", conn)
                command.Parameters.Add(New SqlParameter("Table", table))

                Using reader = command.ExecuteReader()

                    While reader.Read()
                        tableColumns.Add(reader.GetString(0))
                    End While
                End Using
            End Using

            Return columns.All(AddressOf tableColumns.Contains)
        End Function
    End Class

    Public Class IdentityDbContext(Of TUser As IdentityUser(Of TKey, TUserLogin, TUserRole, TUserClaim), TRole As IdentityRole(Of TKey, TUserRole), TKey, TUserLogin As IdentityUserLogin(Of TKey), TUserRole As IdentityUserRole(Of TKey), TUserClaim As IdentityUserClaim(Of TKey))
        Inherits DbContext

        Public Sub New()
            Me.New("DefaultConnection")
        End Sub

        Public Sub New(ByVal nameOrConnectionString As String)
            MyBase.New(nameOrConnectionString)
        End Sub

        Public Sub New(ByVal existingConnection As DbConnection, ByVal model As DbCompiledModel, ByVal contextOwnsConnection As Boolean)
            MyBase.New(existingConnection, model, contextOwnsConnection)
        End Sub

        Public Sub New(ByVal model As DbCompiledModel)
            MyBase.New(model)
        End Sub

        Public Sub New(ByVal existingConnection As DbConnection, ByVal contextOwnsConnection As Boolean)
            MyBase.New(existingConnection, contextOwnsConnection)
        End Sub

        Public Sub New(ByVal nameOrConnectionString As String, ByVal model As DbCompiledModel)
            MyBase.New(nameOrConnectionString, model)
        End Sub

        Public Overridable Property Users As IDbSet(Of TUser)
        Public Overridable Property Roles As IDbSet(Of TRole)
        Public Property RequireUniqueEmail As Boolean

        Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
            If modelBuilder Is Nothing Then
                Throw New ArgumentNullException("modelBuilder")
            End If

            Dim user = modelBuilder.Entity(Of TUser)().ToTable("AspNetUsers")
            user.HasMany(Function(u) u.Roles).WithRequired().HasForeignKey(Function(ur) ur.UserId)
            '     user.HasMany(Function(u) u.Claims).WithRequired().HasForeignKey(Function(uc) uc.UserId)
            user.HasMany(Function(u) u.Logins).WithRequired().HasForeignKey(Function(ul) ul.UserId)
            user.[Property](Function(u) u.UserName).IsRequired().HasMaxLength(256).HasColumnAnnotation("Index", New IndexAnnotation(New IndexAttribute("UserNameIndex") With {.IsUnique = True}))
            user.[Property](Function(u) u.Email).HasMaxLength(256)
            modelBuilder.Entity(Of TUserRole)().HasKey(Function(r) New With {r.UserId, r.RoleId}).ToTable("AspNetUserRoles")
            modelBuilder.Entity(Of TUserLogin)().HasKey(Function(l) New With {l.LoginProvider, l.ProviderKey, l.UserId}).ToTable("AspNetUserLogins")
            'modelBuilder.Entity(Of TUserClaim)().ToTable("AspNetUserClaims")
            Dim role = modelBuilder.Entity(Of TRole)().ToTable("AspNetRoles")
            role.[Property](Function(r) r.Name).IsRequired().HasMaxLength(256).HasColumnAnnotation("Index", New IndexAnnotation(New IndexAttribute("RoleNameIndex") With {.IsUnique = True}))
            role.HasMany(Function(r) r.Users).WithRequired().HasForeignKey(Function(ur) ur.RoleId)
        End Sub

        Protected Overrides Function ValidateEntity(ByVal entityEntry As DbEntityEntry, ByVal items As IDictionary(Of Object, Object)) As DbEntityValidationResult
            If entityEntry IsNot Nothing AndAlso entityEntry.State = EntityState.Added Then
                Dim errors = New List(Of DbValidationError)()
                Dim user = TryCast(entityEntry.Entity, TUser)

                If user IsNot Nothing Then

                    If Users.Any(Function(u) String.Equals(u.UserName, user.UserName)) Then
                        errors.Add(New DbValidationError("User", String.Format(CultureInfo.CurrentCulture, IdentityResources.DuplicateUserName, user.UserName)))
                    End If

                    If RequireUniqueEmail AndAlso Users.Any(Function(u) String.Equals(u.Email, user.Email)) Then
                        errors.Add(New DbValidationError("User", String.Format(CultureInfo.CurrentCulture, IdentityResources.DuplicateEmail, user.Email)))
                    End If
                Else
                    Dim role = TryCast(entityEntry.Entity, TRole)

                    If role IsNot Nothing AndAlso Roles.Any(Function(r) String.Equals(r.Name, role.Name)) Then
                        errors.Add(New DbValidationError("Role", String.Format(CultureInfo.CurrentCulture, IdentityResources.RoleAlreadyExists, role.Name)))
                    End If
                End If

                If errors.Any() Then
                    Return New DbEntityValidationResult(entityEntry, errors)
                End If
            End If

            Return MyBase.ValidateEntity(entityEntry, items)
        End Function
    End Class
End Namespace
