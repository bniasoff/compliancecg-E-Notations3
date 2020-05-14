' 
' © 2003 BV Software LLC
' 
Imports System.IO
Imports System.Security
Imports System.Security.Cryptography
Imports System.Text

'=========================================================================
'    
'   Encodes strings using MD5 (one way) Encryption
'    
'=========================================================================
Public Class MD5Encryption

    Public Overloads Function Encode(ByVal message As String) As String

        'The string we wish to encrypt
        Dim strPlainText As String = message

        'The array of bytes that will contain the encrypted value of strPlainText
        Dim hashedDataBytes As Byte()

        'The encoder class used to convert strPlainText to an array of bytes
        Dim encoder As New UTF8Encoding

        'Create an instance of the MD5CryptoServiceProvider class
        Dim md5Hasher As New MD5CryptoServiceProvider

        'Call ComputeHash, passing in the plain-text string as an array of bytes
        'The return value is the encrypted value, as an array of bytes
        hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(strPlainText))

        Return Base64.ConvertToBase64(hashedDataBytes)
    End Function

    Public Overloads Function Encode(ByVal message As String, ByVal salt As String) As String
        'Return Encode(salt & message)
        Return Encode(message)
    End Function
    Public Function Decode(ByVal message As String, ByVal salt As String) As String
        Dim md5Hasher As New MD5CryptoServiceProvider
        Dim butes() As Byte = Base64.ConvertFromBase64(message)
        Dim encoder As New UTF8Encoding
        butes = md5Hasher.ComputeHash(butes)
        Dim result As String = encoder.GetString(butes)
        Return result
    End Function
End Class
