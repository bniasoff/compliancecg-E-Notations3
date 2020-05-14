Imports System
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports IPAddressFiltering.IPAddressFiltering

Namespace IPAddressFiltering.Testing
    <TestClass>
    Public Class SingleIPAddressFiltering
        <TestMethod>
        Public Sub TestSingleIPRestrictMatch()
            Assert.AreEqual(Of Boolean)(False, CheckIPAddress("94.201.252.25", IPAddressFilteringAction.Restrict))
        End Sub

        <TestMethod>
        Public Sub TestSingleIPRestrictNoMatch()
            Assert.AreEqual(Of Boolean)(True, CheckIPAddress("94.201.252.100", IPAddressFilteringAction.Restrict))
        End Sub

        <TestMethod>
        Public Sub TestSingleIPAllowMatch()
            Assert.AreEqual(Of Boolean)(True, CheckIPAddress("94.201.252.25", IPAddressFilteringAction.Allow))
        End Sub

        <TestMethod>
        Public Sub TestSingleIPAllowNoMatch()
            Assert.AreEqual(Of Boolean)(False, CheckIPAddress("94.201.252.100", IPAddressFilteringAction.Allow))
        End Sub

        Private Function CheckIPAddress(ByVal requestIP As String, ByVal action As IPAddressFilteringAction) As Boolean
            Dim attribute As IPAddressFilterAttribute = New IPAddressFilterAttribute("94.201.252.25", action)
            Return Common.IsIPAddressAllowed(attribute, requestIP)
        End Function
    End Class
End Namespace
