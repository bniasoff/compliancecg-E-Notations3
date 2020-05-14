Imports System
Imports System.Text

Class RandomNumberSample
    Private Shared Sub Main(ByVal args As String())
        Dim random As Random = New Random()
        Dim num As Integer = random.[Next]()
        Dim randomLessThan100 As Integer = random.[Next](100)
        Console.WriteLine(randomLessThan100)
        Dim randomBetween100And500 As Integer = random.[Next](100, 500)
        Console.WriteLine(randomBetween100And500)
        Dim generator As PasswordGenerator = New PasswordGenerator()
        Dim rand As Integer = generator.RandomNumber(5, 100)
        Console.WriteLine($"Random number between 5 and 100 is {rand}")
        Dim str As String = generator.RandomString(10, False)
        Console.WriteLine($"Random string of 10 chars is {str}")
        Dim pass As String = generator.RandomPassword()
        Console.WriteLine($"Random password {pass}")
        Console.ReadKey()
    End Sub
End Class

Public Class PasswordGenerator
    Public Shared Function RandomNumber(ByVal min As Integer, ByVal max As Integer) As Integer
        Dim random As Random = New Random()
        Return random.[Next](min, max)
    End Function

    Public Shared Function RandomString(ByVal size As Integer, ByVal lowerCase As Boolean) As String
        Dim builder As StringBuilder = New StringBuilder()
        Dim random As Random = New Random()
        Dim ch As Char

        For i As Integer = 0 To size - 1
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)))
            builder.Append(ch)
        Next

        If lowerCase Then Return builder.ToString().ToLower()
        Return builder.ToString()
    End Function

    Public Shared Function RandomPassword(ByVal Optional size As Integer = 0, ByVal Optional size2 As Integer = 0) As String
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append(RandomString(size, True))
        builder.Append(RandomNumber(10, 99))
        builder.Append(RandomString(size2, False))
        Return builder.ToString()
    End Function

    Private Shared Function CreateRandomPassword(ByVal Optional length As Integer = 15) As String
        Dim validChars As String = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-"
        Dim random As Random = New Random()
        Dim chars As Char() = New Char(length - 1) {}

        For i As Integer = 0 To length - 1
            chars(i) = validChars(random.[Next](0, validChars.Length))
        Next

        Return New String(chars)
    End Function
End Class



Class PasswordGenerator2
    'Private Shared Sub Main(ByVal args As String())
    '    Console.WriteLine(CreateRandomPassword())
    '    Console.WriteLine(CreateRandomPassword(10))
    '    Console.WriteLine(CreateRandomPassword(30))
    '    Console.WriteLine(CreateRandomPasswordWithRandomLength())
    '    Console.ReadKey()
    'End Sub

    Private Shared Function CreateRandomPassword(ByVal Optional length As Integer = 15) As String
        Dim validChars As String = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-"
        Dim random As Random = New Random()
        Dim chars As Char() = New Char(length - 1) {}

        For i As Integer = 0 To length - 1
            chars(i) = validChars(random.[Next](0, validChars.Length))
        Next

        Return New String(chars)
    End Function

    Private Shared Function CreateRandomPasswordWithRandomLength() As String
        Dim validChars As String = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-"
        Dim random As Random = New Random()
        Dim size As Integer = random.[Next](8, validChars.Length)
        Dim chars As Char() = New Char(size - 1) {}

        For i As Integer = 0 To size - 1
            chars(i) = validChars(random.[Next](0, validChars.Length))
        Next

        Return New String(chars)
    End Function
End Class





'Public Class Password

'    Public Function RandomString(ByVal size As Integer, ByVal lowerCase As Boolean) As String
'        Dim builder As StringBuilder = New StringBuilder()
'        Dim random As Random = New Random()
'        Dim ch As Char

'        For i As Integer = 0 To size - 1
'            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)))
'            builder.Append(ch)
'        Next

'        If lowerCase Then Return builder.ToString().ToLower()
'        Return builder.ToString()
'    End Function
'    Public Function RandomPassword(ByVal Optional size As Integer = 0) As String
'        Dim builder As StringBuilder = New StringBuilder()
'        builder.Append(RandomString(4, True))
'        builder.Append(RandomNumber(1000, 9999))
'        builder.Append(RandomString(2, False))
'        Return builder.ToString()
'    End Function
'End Class



