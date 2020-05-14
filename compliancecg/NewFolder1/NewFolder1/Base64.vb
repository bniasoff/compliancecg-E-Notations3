
Public NotInheritable Class Base64

	Private Sub New()

	End Sub

	Public Shared Function ConvertToBase64(ByVal inputBytes() As Byte) As String
		Dim sOutput As String
		sOutput = Convert.ToBase64String(inputBytes, 0, inputBytes.Length)
		Return sOutput
	End Function

	Public Shared Function ConvertFromBase64(ByVal inputCharacters As String) As Byte()
		Dim arrOutput() As Byte
		arrOutput = Convert.FromBase64String(inputCharacters)
		Return arrOutput
	End Function

End Class
