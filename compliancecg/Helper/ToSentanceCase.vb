Imports System.Globalization
Imports System.Runtime.CompilerServices

Public Module StringExtensions
    ''' <summary>
    ''' This extension function converts a string to TitleCase.
    ''' </summary>
    ''' <param name="inputString">String to convert.</param>
    ''' <returns>Converted string.</returns>
    <Extension()>
    Public Function ToTitleCase(inputString As String) As String
        If (String.IsNullOrEmpty(inputString)) Then
            Return inputString
        End If

        Return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(inputString.ToLower)
    End Function

End Module