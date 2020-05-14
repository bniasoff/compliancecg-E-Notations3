Imports System.Runtime.Serialization
Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Configuration
Imports System
Imports System.Data.Entity.Infrastructure
Imports System.Reflection
Imports CCGData.CCGData
Imports CCGData.Enums



Namespace CCGData


    <Serializable>
    Partial Public Class Facility
        Public Property Roles As String
    End Class


    <Serializable>
    Partial Public Class User
        Public Property Roles As String
        Public Property FacilityName As String
        Public Property FacilityInActive As Boolean
    End Class
    <Serializable>
    Partial Public Class CCGDataEntities
        Inherits DbContext

        Public Sub New(ConnectionString As String)
            MyBase.New(ConnectionString)
            ' MyBase.Configuration.ProxyCreationEnabled = False
        End Sub




        'Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)
        '    ' modelBuilder.HasDefaultSchema("QBOnline")
        '    MyBase.OnModelCreating(modelBuilder)
        'End Sub

        'Public Function GetConfig() As Integer
        '    Dim modelBuilder As New DbModelBuilder
        '    modelBuilder.Build(Me.Database.Connection)
        '    '  modelBuilder.Properties(Of String).Configure(Function(p) p.HasColumnName("StudentID"))
        'End Function


        'Public Overrides Function SaveChanges() As Integer
        '    Try


        '        Me.ChangeTracker.DetectChanges()


        '        'If TypeOf Entry() Is IAuditEntity Then
        '        '    If (CType(Entry(), IAuditEntity)).CreateDate._._._ Then
        '        '    End If
        '        'End If


        '        Dim added = Me.ChangeTracker.Entries().Where(Function(t) t.State = EntityState.Added).[Select](Function(t) t.Entity).ToArray()
        '        'For Each newEntity In added
        '        '    UpdateCompanyID(newEntity)
        '        '    UpdateDateOfEntry(newEntity)

        '        '    'If TypeOf newEntity Is ITrack Then
        '        '    '    Dim track = TryCast(newEntity, ITrack)
        '        '    '    track.CreatedDate = DateTime.Now
        '        '    '    track.CreatedBy = UserId
        '        '    'End If
        '        'Next

        '        'Dim modified = Me.ChangeTracker.Entries().Where(Function(t) t.State = EntityState.Modified).[Select](Function(t) t.Entity).ToArray()
        '        'For Each modEntity In modified
        '        '    UpdateCompanyID(modEntity)
        '        '    UpdateDateOfEntry(modEntity)
        '        '    'If TypeOf modEntity Is ITrack Then
        '        '    '    Dim track = TryCast(modEntity, ITrack)
        '        '    '    track.ModifiedDate = DateTime.Now
        '        '    '    track.ModifiedBy = UserId
        '        '    'End If
        '        'Next

        '        Return MyBase.SaveChanges()
        '  Catch ex As Exception
        'logger.Error(ex)

        '    End Try
        'End Function


    End Class

    Public Class EmailMessage
        <DataMember()>
        Public Property FromEmail As String
        <DataMember()>
        Public Property ToEmail As String
        <DataMember()>
        Public Property CC As New List(Of String)
        <DataMember()>
        Public Property Attachments As New List(Of String)
        <DataMember()>
        Public Property Subject As String
        <DataMember()>
        Public Property Message As String
        <DataMember()>
        Public Property EmailType As EmailType
        <DataMember()>
        Public Property IsBodyHtml As Boolean

    End Class

    'Public Class EmailMessage2

    '    <DataMember()>
    '    Public Property Message As String
    '    <DataMember()>
    '    Public Property FromEmail As String
    '    <DataMember()>
    '    Public Property EmailAddress As String

    '    <DataMember()>
    '    Public Property Sent As Boolean = False
    '    'Public Property Attachment As FileInfo
    '    <DataMember()>
    '    Public Property Attachments As New List(Of String)
    '    <DataMember()>
    '    Public Property Subject As String
    '    <DataMember()>
    '    Public Property CC As New List(Of String)

    '    <DataMember()>
    '    Public Property EmailTo As Integer
    '    <DataMember()>
    '    Public ErrorMessage As String
    'End Class


    <DataContract()>
    Public Class FaultError
        <DataMember()>
        Public Property Issue As String
        <DataMember()>
        Public Property Details As String
        <DataMember()>
        Public Property ID As Integer
    End Class
End Namespace