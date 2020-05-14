'Imports System.Data.Entity

'Public Class Movie
'    Public Property Id As Integer
'    Public Property Title As String
'    Public Property ReleaseDate As DateTime
'End Class

'Public Class Actor
'    Public Property Id As Integer
'    Public Property FirstName As String
'    Public Property LastName As String
'    Public Property DateOfBirth As DateTime
'End Class

'Public Class ChangeLog
'    Public Property Id As Integer
'    Public Property EntityName As String
'    Public Property PropertyName As String
'    Public Property PrimaryKeyValue As String
'    Public Property OldValue As String
'    Public Property NewValue As String
'    Public Property DateChanged As DateTime
'End Class

'Public Class MoviesContext
'    Inherits DbContext

'    Public Overridable Property Movies As DbSet(Of Movie)
'    Public Overridable Property Actors As DbSet(Of Actor)
'    Public Overridable Property ChangeLogs As DbSet(Of ChangeLog)

'    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
'    End Sub

'    Public Overrides Function SaveChanges() As Integer
'        Dim modifiedEntities = ChangeTracker.Entries().Where(Function(p) p.State = EntityState.Modified).ToList()
'        Dim now = DateTime.UtcNow

'        For Each change In modifiedEntities
'            Dim entityName = change.Entity.[GetType]().Name
'            Dim primaryKey = GetPrimaryKeyValue(change)

'            For Each prop In change.OriginalValues.PropertyNames
'                Dim originalValue = change.OriginalValues(prop).ToString()
'                Dim currentValue = change.CurrentValues(prop).ToString()

'                If originalValue <> currentValue Then
'                    Dim log As ChangeLog = New ChangeLog() With {
'                        .EntityName = entityName,
'                        .PrimaryKeyValue = primaryKey.ToString(),
'                        .PropertyName = prop,
'                        .OldValue = originalValue,
'                        .NewValue = currentValue,
'                        .DateChanged = now
'                    }
'                    ChangeLogs.Add(log)
'                End If
'            Next
'        Next

'        Return MyBase.SaveChanges()
'    End Function
'End Class
