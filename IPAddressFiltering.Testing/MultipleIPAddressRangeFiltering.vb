Imports System
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports IPAddressFiltering.IPAddressFiltering

Namespace IPAddressFiltering.Testing
    <TestClass>
    Public Class MultipleIPAddressRangeFiltering
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
            Dim attribute As IPAddressFilterAttribute = New IPAddressFilterAttribute(New IPAddressRange() {New IPAddressRange("94.123.252.5", "94.130.252.100"), New IPAddressRange("94.201.252.5", "94.201.252.90"), New IPAddressRange("94.201.242.1", "94.201.242.101"), New IPAddressRange("34.201.232.5", "54.201.242.200")}, action)
            Return Common.IsIPAddressAllowed(attribute, requestIP)
        End Function
    End Class
End Namespace
