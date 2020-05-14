Imports IPAddressFiltering.IPAddressFiltering
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace IPAddressFiltering.Testing
    <TestClass>
    Public Class MultipleIPAddressFiltering
        <TestMethod>
        Public Sub TestMultipleIPRestrictMatch()
            Assert.AreEqual(Of Boolean)(False, CheckIPAddress("94.201.252.25", IPAddressFilteringAction.Restrict))
        End Sub

        <TestMethod>
        Public Sub TestMultipleIPRestrictNoMatch()
            Assert.AreEqual(Of Boolean)(True, CheckIPAddress("94.201.252.100", IPAddressFilteringAction.Restrict))
        End Sub

        <TestMethod>
        Public Sub TestMultipleIPAllowMatch()
            Assert.AreEqual(Of Boolean)(True, CheckIPAddress("94.201.252.25", IPAddressFilteringAction.Allow))
        End Sub

        <TestMethod>
        Public Sub TestMultipleIPAllowNoMatch()
            Assert.AreEqual(Of Boolean)(False, CheckIPAddress("94.201.252.100", IPAddressFilteringAction.Allow))
        End Sub

        Private Function CheckIPAddress(ByVal requestIP As String, ByVal action As IPAddressFilteringAction) As Boolean
            Dim attribute As IPAddressFilterAttribute = New IPAddressFilterAttribute(New String() {"94.201.252.21", "94.201.252.22", "94.201.252.23", "94.201.252.24", "94.201.252.25", "94.201.252.26", "94.201.252.27"}, action)
            Return Common.IsIPAddressAllowed(attribute, requestIP)
        End Function
    End Class
End Namespace
