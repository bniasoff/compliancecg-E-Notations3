Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports System
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Runtime.CompilerServices

Namespace Api.Extensions
    Public Enum TokenValidity
        VALID
        INVALID
        INVALID_EXPIRED
        [ERROR]
    End Enum

    Module UserManagerExtensions
        <Extension()>
        Async Function IsResetPasswordTokenValid(Of TUser As {Class, IUser(Of TKey)}, TKey As IEquatable(Of TKey))(ByVal manager As UserManager(Of TUser, TKey), ByVal user As TUser, ByVal token As String) As Threading.Tasks.Task(Of TokenValidity)
            Dim ResetPasswordTokenValid = Await IsTokenValid(manager, user, "ResetPassword", token)
            Return ResetPasswordTokenValid
        End Function

        <Extension()>
        Async Function IsTokenValid(Of TUser As {Class, IUser(Of TKey)}, TKey As IEquatable(Of TKey))(ByVal manager As UserManager(Of TUser, TKey), ByVal user As TUser, ByVal purpose As String, ByVal token As String) As Threading.Tasks.Task(Of TokenValidity)
            Dim tokenProvider As DataProtectorTokenProvider(Of TUser, TKey) = Nothing
            Dim tokenProvider2 = TryCast(manager.UserTokenProvider, DataProtectorTokenProvider(Of TUser, TKey))

            Try
                ' If Not TryCast(manager.UserTokenProvider, DataProtectorTokenProvider(Of TUser, TKey)) IsNot Nothing Then Return TokenValidity.ERROR
                'Dim unprotectedData = tokenProvider2.Protector.Unprotect(Convert.FromBase64String(token))
                Dim unprotectedData = Convert.FromBase64String(token)
                Dim ms = New MemoryStream(unprotectedData)

                Using reader = ms.CreateReader()
                    Dim creationTime = reader.ReadDateTimeOffset()
                    Dim expirationTime = creationTime + tokenProvider2.TokenLifespan
                    Dim userId = reader.ReadString()

                    If Not String.Equals(userId, Convert.ToString(user.Id, CultureInfo.InvariantCulture)) Then
                        Return TokenValidity.INVALID
                    End If

                    Dim purp = reader.ReadString()

                    If Not String.Equals(purp, purpose) Then
                        Return TokenValidity.INVALID
                    End If

                    Dim stamp = reader.ReadString()

                    If reader.PeekChar() <> -1 Then
                        Return TokenValidity.INVALID
                    End If

                    Dim expectedStamp = ""

                    If manager.SupportsUserSecurityStamp Then
                        expectedStamp = Await manager.GetSecurityStampAsync(user.Id)
                    End If

                    If Not String.Equals(stamp, expectedStamp) Then Return TokenValidity.INVALID

                    If expirationTime < DateTimeOffset.UtcNow Then
                        Return TokenValidity.INVALID_EXPIRED
                    End If

                    Return TokenValidity.VALID
                End Using

            Catch ex As Exception
            End Try

            Return TokenValidity.INVALID
        End Function

        'Private Class CSharpImpl
        '    <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
        '    Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
        '        target = value
        '        Return value
        '    End Function
        'End Class
    End Module

    Friend Module StreamExtensions
        Friend ReadOnly DefaultEncoding As Encoding = New UTF8Encoding(False, True)

        <Extension()>
        Function CreateReader(ByVal stream As Stream) As BinaryReader
            Return New BinaryReader(stream, DefaultEncoding, True)
        End Function

        <Extension()>
        Function CreateWriter(ByVal stream As Stream) As BinaryWriter
            Return New BinaryWriter(stream, DefaultEncoding, True)
        End Function

        <Extension()>
        Function ReadDateTimeOffset(ByVal reader As BinaryReader) As DateTimeOffset
            Return New DateTimeOffset(reader.ReadInt64(), TimeSpan.Zero)
        End Function

        <Extension()>
        Sub Write(ByVal writer As BinaryWriter, ByVal value As DateTimeOffset)
            writer.Write(value.UtcTicks)
        End Sub

        Private Class CSharpImpl
            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Module
End Namespace
