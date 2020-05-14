Imports System.Net
Imports System.Net.Sockets
Imports System.Runtime.CompilerServices

Namespace IPAddressFiltering
    Module IPAddressExtensions
        <Extension()>
        Function IsInRange(ByVal address As IPAddress, ByVal start As IPAddress, ByVal [end] As IPAddress) As Boolean
            Dim addressFamily As AddressFamily = start.AddressFamily
            Dim lowerBytes As Byte() = start.GetAddressBytes()
            Dim upperBytes As Byte() = [end].GetAddressBytes()

            If address.AddressFamily <> addressFamily Then
                Return False
            End If

            Dim addressBytes As Byte() = address.GetAddressBytes()
            Dim lowerBoundary As Boolean = True, upperBoundary As Boolean = True
            Dim i As Integer = 0

            While i < lowerBytes.Length AndAlso (lowerBoundary OrElse upperBoundary)

                If (lowerBoundary AndAlso addressBytes(i) < lowerBytes(i)) OrElse (upperBoundary AndAlso addressBytes(i) > upperBytes(i)) Then
                    Return False
                End If

                lowerBoundary = lowerBoundary And (addressBytes(i) = lowerBytes(i))
                upperBoundary = upperBoundary And (addressBytes(i) = upperBytes(i))
                i += 1
            End While

            Return True
        End Function
    End Module
End Namespace
