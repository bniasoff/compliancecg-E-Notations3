Imports System
Imports System.Data.Entity
Imports System.Data.Entity.SqlServer.Utilities
Imports System.Linq
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
'Imports Microsoft.AspNet.Identity
'Imports Microsoft.AspNet.Identity.EntityFramework

Namespace Microsoft.AspNet.Identity.EntityFramework
    Public Class RoleStore(Of TRole As {IdentityRole, New})
        Inherits RoleStore(Of TRole, String, IdentityUserRole)
        Implements IQueryableRoleStore(Of TRole)

        Private _disposed As Boolean
        Private _roleStore As EntityStore(Of TRole)

        Public Sub New()
            MyBase.New(New IdentityDbContext())
            DisposeContext = True
        End Sub

        'Public Sub New(ByVal context As DbContext)
        '    MyBase.New(context)
        'End Sub


        Public Sub New(ByVal context As DbContext)
            MyBase.New(context)
            If context Is Nothing Then
                Throw New ArgumentNullException("context")
            End If

            context = context
            _roleStore = New EntityStore(Of TRole)(context)

        End Sub


        Private ReadOnly Property IQueryableRoleStore_Roles As IQueryable(Of TRole) Implements IQueryableRoleStore(Of TRole, String).Roles
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Private Function IRoleStore_CreateAsync(role As TRole) As Task Implements IRoleStore(Of TRole, String).CreateAsync
            Throw New NotImplementedException()
        End Function

        Private Function IRoleStore_UpdateAsync(role As TRole) As Task Implements IRoleStore(Of TRole, String).UpdateAsync
            Throw New NotImplementedException()
        End Function

        Private Function IRoleStore_DeleteAsync(role As TRole) As Task Implements IRoleStore(Of TRole, String).DeleteAsync
            Throw New NotImplementedException()
        End Function

        Private Function IRoleStore_FindByIdAsync(roleId As String) As Task(Of TRole) Implements IRoleStore(Of TRole, String).FindByIdAsync
            Throw New NotImplementedException()
        End Function

        Private Function FindByNameAsync(roleName As String) As Task(Of TRole) Implements IRoleStore(Of TRole, String).FindByNameAsync
            ThrowIfDisposed()
            Return _roleStore.EntitySet.FirstOrDefaultAsync(Function(u) u.Name.ToUpper() = roleName.ToUpper())

            'Throw New NotImplementedException()
        End Function
        Private Sub ThrowIfDisposed()
            If _disposed Then
                Throw New ObjectDisposedException([GetType]().Name)
            End If
        End Sub
#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            ' TODO: uncomment the following line if Finalize() is overridden above.
            ' GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class

    Public Class RoleStore(Of TRole As {IdentityRole(Of TKey, TUserRole), New}, TKey, TUserRole As {IdentityUserRole(Of TKey), New})
        Implements IQueryableRoleStore(Of TRole, TKey)

        Private _disposed As Boolean
        Private _roleStore As EntityStore(Of TRole)

        Public Sub New(ByVal context As DbContext)
            If context Is Nothing Then
                Throw New ArgumentNullException("context")
            End If

            context = context
            _roleStore = New EntityStore(Of TRole)(context)
        End Sub

        Public Property Context As DbContext
        Public Property DisposeContext As Boolean

        Public Function FindByIdAsync(ByVal roleId As TKey) As Task(Of TRole)
            ThrowIfDisposed()
            Return _roleStore.GetByIdAsync(roleId)
        End Function

        Public Function FindByNameAsync(ByVal roleName As String) As Task(Of TRole) Implements IRoleStore(Of TRole, TKey).FindByNameAsync
            ThrowIfDisposed()
            Return _roleStore.EntitySet.FirstOrDefaultAsync(Function(u) u.Name.ToUpper() = roleName.ToUpper())
        End Function

        Public Overridable Async Function CreateAsync(ByVal role As TRole) As Task
            ThrowIfDisposed()

            If role Is Nothing Then
                Throw New ArgumentNullException("role")
            End If

            _roleStore.Create(role)
            Await Context.SaveChangesAsync().WithCurrentCulture()
        End Function

        Public Overridable Async Function DeleteAsync(ByVal role As TRole) As Task
            ThrowIfDisposed()

            If role Is Nothing Then
                Throw New ArgumentNullException("role")
            End If

            _roleStore.Delete(role)
            Await Context.SaveChangesAsync().WithCurrentCulture()
        End Function

        Public Overridable Async Function UpdateAsync(ByVal role As TRole) As Task
            ThrowIfDisposed()

            If role Is Nothing Then
                Throw New ArgumentNullException("role")
            End If

            _roleStore.Update(role)
            Await Context.SaveChangesAsync().WithCurrentCulture()
        End Function

        Public ReadOnly Property Roles As IQueryable(Of TRole)
            Get
                Return _roleStore.EntitySet
            End Get
        End Property

        Private ReadOnly Property IQueryableRoleStore_Roles As IQueryable(Of TRole) Implements IQueryableRoleStore(Of TRole, TKey).Roles
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public Sub Dispose()
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Private Sub ThrowIfDisposed()
            If _disposed Then
                Throw New ObjectDisposedException([GetType]().Name)
            End If
        End Sub

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If DisposeContext AndAlso disposing AndAlso Context IsNot Nothing Then
                Context.Dispose()
            End If

            _disposed = True
            Context = Nothing
            _roleStore = Nothing
        End Sub

        Private Function IRoleStore_CreateAsync(role As TRole) As Task Implements IRoleStore(Of TRole, TKey).CreateAsync
            Throw New NotImplementedException()
        End Function

        Private Function IRoleStore_UpdateAsync(role As TRole) As Task Implements IRoleStore(Of TRole, TKey).UpdateAsync
            Throw New NotImplementedException()
        End Function

        Private Function IRoleStore_DeleteAsync(role As TRole) As Task Implements IRoleStore(Of TRole, TKey).DeleteAsync
            Throw New NotImplementedException()
        End Function

        Private Function IRoleStore_FindByIdAsync(roleId As TKey) As Task(Of TRole) Implements IRoleStore(Of TRole, TKey).FindByIdAsync
            Throw New NotImplementedException()
        End Function

        'Private Function IRoleStore_FindByNameAsync(roleName As String) As Task(Of TRole) Implements IRoleStore(Of TRole, TKey).FindByNameAsync
        '    Throw New NotImplementedException()
        'End Function

        Private Sub IDisposable_Dispose() Implements IDisposable.Dispose
            Throw New NotImplementedException()
        End Sub
    End Class
End Namespace
