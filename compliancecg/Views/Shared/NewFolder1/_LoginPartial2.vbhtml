@Imports Microsoft.AspNet.Identity
@imports  System.Security.Claims
@Imports compliancecg.Membership

@code
    Dim ClientIDClaim As Claim = Nothing

    Dim Principal As ClaimsPrincipal = TryCast(HttpContext.Current.User, ClaimsPrincipal)
    Dim UserID As String = Nothing
    Dim UserName As String = Nothing
    Dim LastName As String = Nothing
    Dim FirstName As String = Nothing

    If Principal IsNot Nothing Then
        If Principal.Claims IsNot Nothing Then
            Dim Claims = Principal.Claims
            For Each claim As Claim In Principal.Claims
                If claim.Type = "UserID" Then UserID = claim.Value
                If claim.Type = "UserName" Then UserName = claim.Value
                If claim.Type = "LastName" Then LastName = claim.Value
                If claim.Type = "FirstName" Then FirstName = claim.Value
            Next
        End If
    End If


    Dim FullName As String = Nothing
    If FirstName IsNot Nothing Then FullName = FirstName
    If LastName IsNot Nothing Then FullName = FullName + " " + LastName

    If FullName Is Nothing Then If UserName IsNot Nothing Then FullName = UserName


    ' Dim UserAccount As UserAccount = Context.Session("UserAccount")

    'If UserAccount Is Nothing Then
    '    UserAccount = AuthorizationService.GetUser(User.Identity.GetUserId)
    '    If UserAccount IsNot Nothing Then Context.Session("UserAccount") = UserAccount
    'End If

    'If UserAccount IsNot Nothing Then
    '    FullName = UserAccount.FullName
    'End If

End Code



