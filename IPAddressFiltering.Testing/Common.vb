Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Reflection
Imports IPAddressFiltering.IPAddressFiltering

Namespace IPAddressFiltering.Testing
    Module Common
        Function IsIPAddressAllowed(ByVal attribute As IPAddressFilterAttribute, ByVal ip As String) As Boolean
            Return CBool(GetType(IPAddressFilterAttribute).GetMethod("IsIPAddressAllowed", BindingFlags.NonPublic Or BindingFlags.Instance).Invoke(attribute, New Object() {ip}))
        End Function
    End Module
End Namespace


