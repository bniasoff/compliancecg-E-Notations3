'Imports System.IO
'Imports System.Runtime.CompilerServices
'Imports Microsoft.AspNet.Identity
'Imports Microsoft.AspNet.Identity.Owin

'Module UserManagerExtensions
'    <Extension()>
'    Function IsTokenExpired(Of TUser As {Class, IUser(Of TKey)}, TKey As IEquatable(Of TKey))(ByVal manager As UserManager(Of TUser, TKey), ByVal user As TUser, ByVal token As String) As Boolean
'        Try
'            Dim tokenProvider = TryCast(manager.UserTokenProvider, DataProtectorTokenProvider(Of TUser, TKey))
'            If tokenProvider Is Nothing Then Return False
'            Dim unprotectedData = tokenProvider.Protector.Unprotect(Convert.FromBase64String(token))
'            Dim ms = New MemoryStream(unprotectedData)

'            Using reader = ms.CreateReader()
'                Dim creationTime = reader.ReadDateTimeOffset()
'                Dim expirationTime = creationTime + tokenProvider.TokenLifespan

'                If expirationTime < DateTimeOffset.UtcNow Then
'                    Return True
'                End If

'                Return False
'            End Using

'        Catch
'        End Try

'        Return True
'    End Function
'End Module


'Friend Module StreamExtensions
'    Friend ReadOnly DefaultEncoding As Encoding = New UTF8Encoding(False, True)

'    <Extension()>
'    Function CreateReader(ByVal stream As Stream) As BinaryReader
'        Return New BinaryReader(stream, DefaultEncoding, True)
'    End Function

'    <Extension()>
'    Function CreateWriter(ByVal stream As Stream) As BinaryWriter
'        Return New BinaryWriter(stream, DefaultEncoding, True)
'    End Function

'    <Extension()>
'    Function ReadDateTimeOffset(ByVal reader As BinaryReader) As DateTimeOffset
'        Return New DateTimeOffset(reader.ReadInt64(), TimeSpan.Zero)
'    End Function

'    <Extension()>
'    Sub Write(ByVal writer As BinaryWriter, ByVal value As DateTimeOffset)
'        writer.Write(value.UtcTicks)
'    End Sub
'End Module
