Imports System
Imports System.Runtime.CompilerServices
Imports System.Security.Cryptography
Imports Microsoft.AspNet.Identity

Namespace Microsoft.AspNet.Identity
    Friend Module Crypto
        Private Const PBKDF2IterCount As Integer = 1000
        Private Const PBKDF2SubkeyLength As Integer = 256 / 8
        Private Const SaltSize As Integer = 128 / 8

        Function HashPassword(ByVal password As String) As String
            If password Is Nothing Then
                Throw New ArgumentNullException("password")
            End If

            Dim salt As Byte()
            Dim subkey As Byte()

            Using deriveBytes = New Rfc2898DeriveBytes(password, SaltSize, PBKDF2IterCount)
                salt = deriveBytes.Salt
                subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength)
            End Using

            Dim outputBytes = New Byte(48) {}
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize)
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, PBKDF2SubkeyLength)
            Return Convert.ToBase64String(outputBytes)
        End Function

        Function VerifyHashedPassword(ByVal hashedPassword As String, ByVal password As String) As Boolean
            If hashedPassword Is Nothing Then
                Return False
            End If

            If password Is Nothing Then
                Throw New ArgumentNullException("password")
            End If

            Dim hashedPasswordBytes = Convert.FromBase64String(hashedPassword)

            If hashedPasswordBytes.Length <> (1 + SaltSize + PBKDF2SubkeyLength) OrElse hashedPasswordBytes(0) <> &H0 Then
                Return False
            End If

            Dim salt = New Byte(15) {}
            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, SaltSize)
            Dim storedSubkey = New Byte(31) {}
            Buffer.BlockCopy(hashedPasswordBytes, 1 + SaltSize, storedSubkey, 0, PBKDF2SubkeyLength)
            Dim generatedSubkey As Byte()

            Using deriveBytes = New Rfc2898DeriveBytes(password, salt, PBKDF2IterCount)
                generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength)
            End Using

            Return ByteArraysEqual(storedSubkey, generatedSubkey)
        End Function

        <MethodImpl(MethodImplOptions.NoOptimization)>
        Private Function ByteArraysEqual(ByVal a As Byte(), ByVal b As Byte()) As Boolean
            If ReferenceEquals(a, b) Then
                Return True
            End If

            If a Is Nothing OrElse b Is Nothing OrElse a.Length <> b.Length Then
                Return False
            End If

            Dim areSame = True

            For i = 0 To a.Length - 1
                areSame = areSame And (a(i) = b(i))
            Next

            Return areSame
        End Function
    End Module

    Public Class CustomPasswordHasher
        Implements IPasswordHasher
        Private Const PBKDF2IterCount As Integer = 1000
        Private Const PBKDF2SubkeyLength As Integer = 256 / 8
        Private Const SaltSize As Integer = 128 / 8
        Public Function HashPassword(ByVal password As String) As String Implements IPasswordHasher.HashPassword
            If password Is Nothing Then
                Throw New ArgumentNullException("password")
            End If

            Dim salt As Byte()
            Dim subkey As Byte()

            Using deriveBytes = New Rfc2898DeriveBytes(password, SaltSize, PBKDF2IterCount)
                salt = deriveBytes.Salt
                subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength)
            End Using

            Dim outputBytes = New Byte(48) {}
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize)
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, PBKDF2SubkeyLength)
            Return Convert.ToBase64String(outputBytes)

            ' Return password
        End Function

        Public Function VerifyHashedPassword(ByVal hashedPassword As String, ByVal providedPassword As String) As PasswordVerificationResult Implements IPasswordHasher.VerifyHashedPassword
            If hashedPassword Is Nothing Then
                Return False
            End If

            If providedPassword Is Nothing Then
                Throw New ArgumentNullException("password")
            End If

            Dim hashedPasswordBytes = Convert.FromBase64String(hashedPassword)

            If hashedPasswordBytes.Length <> (1 + SaltSize + PBKDF2SubkeyLength) OrElse hashedPasswordBytes(0) <> &H0 Then
                Return False
            End If

            Dim salt = New Byte(15) {}
            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, SaltSize)
            Dim storedSubkey = New Byte(31) {}
            Buffer.BlockCopy(hashedPasswordBytes, 1 + SaltSize, storedSubkey, 0, PBKDF2SubkeyLength)
            Dim generatedSubkey As Byte()

            Using deriveBytes = New Rfc2898DeriveBytes(providedPassword, salt, PBKDF2IterCount)
                generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength)
            End Using

            Return ByteArraysEqual(storedSubkey, generatedSubkey)


            '  Return If(hashedPassword = providedPassword, PasswordVerificationResult.Success, PasswordVerificationResult.Failed)
        End Function


        <MethodImpl(MethodImplOptions.NoOptimization)>
        Private Function ByteArraysEqual(ByVal a As Byte(), ByVal b As Byte()) As Boolean
            If ReferenceEquals(a, b) Then
                Return True
            End If

            If a Is Nothing OrElse b Is Nothing OrElse a.Length <> b.Length Then
                Return False
            End If

            Dim areSame = True

            For i = 0 To a.Length - 1
                areSame = areSame And (a(i) = b(i))
            Next

            Return areSame
        End Function

    End Class


End Namespace
