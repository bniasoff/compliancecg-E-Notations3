
Imports CCGData
Imports CCGData.CCGData

Public Class AuthorizationService


    Public Shared Function GetUser(ByVal username As String) As UserAccount
        Try
            Dim DataRepository As New DataRepository
            Dim UserAccount As New UserAccount

            If username IsNot Nothing Then
                Dim User = DataRepository.GetUser(username)
                If User IsNot Nothing Then
                    UserAccount.Email = User.Email
                    UserAccount.PasswordHash = User.PasswordHash
                    UserAccount.UserID = User.Email
                    UserAccount.UserName = User.Email
                    UserAccount.LastName = User.LastName
                    UserAccount.FirstName = User.FirstName
                End If
            Else
                UserAccount = Nothing
            End If
            'Dim userInfo As bvc_UserAccount_ByName_s_Result = SellerCloudData2.GetUser(username)
            'result = ConvertUserDataTableToUserAccount(userInfo)

            Return UserAccount
        Catch ex As Exception

        End Try
    End Function

    'Public Shared Function GetUser(ByVal ClientId As Integer, ByVal UserName As String) As bvc_Client_User_byName_s_Result
    '    'Dim userInfo As bvc_Client_User_byName_s_Result = SellerCloudData2.GetUser(ClientId, UserName)
    '    Return userInfo
    'End Function

    'Public Shared Function DoPasswordsMatch(ByVal trialpassword As String, ByVal matchUser As UserAccount) As Boolean
    '    Dim ret As Boolean = False
    '    Dim crypto As New MD5Encryption
    '    If crypto.Encode(trialpassword, matchUser.Salt).Equals(matchUser.Password) = True Then
    '        ret = True
    '    End If
    '    crypto = Nothing
    '    Return ret
    'End Function


    'Private Shared Function ConvertUserDataTableToUserAccount(ByVal userInfo As bvc_UserAccount_ByName_s_Result) As UserAccount

    '    Try


    '        Dim result As UserAccount = Nothing
    '        If Not userInfo Is Nothing Then
    '            result = New UserAccount

    '            With userInfo
    '                result.Id = userInfo.UserName
    '                result.UserName = userInfo.UserName
    '                result.UserID = userInfo.ID

    '                result.Password = userInfo.Password
    '                result.FirstName = userInfo.FirstName
    '                result.LastName = userInfo.LastName
    '                result.Salt = userInfo.Salt
    '                If userInfo.taxexempt = -1 Or userInfo.taxexempt = 1 Then
    '                    result.TaxExempt = True
    '                Else
    '                    result.TaxExempt = userInfo.taxexempt
    '                End If

    '                result.Email = userInfo.Email
    '                result.CreationDate = userInfo.CreationDate
    '                result.LastLoginDate = userInfo.LastLoginDate
    '                result.PasswordHint = userInfo.PasswordHint
    '                result.Comment = userInfo.Comment
    '                result.PricingLevel = userInfo.PricingLevel
    '                ' If .IsNull("AddressBook = False Then result.Addresses.FromXml( userInfo.AddressBook)


    '                result.UserType = SellerCloudData.UserType.WEB_USER
    '                If Not userInfo.UserType Is Nothing Then result.UserType = userInfo.UserType
    '                If Not userInfo.UserSource Is Nothing Then result.UserSource = userInfo.UserSource

    '                If Not userInfo.eBayFeedbackScore Is Nothing Then result.eBayFeedbackScore = userInfo.eBayFeedbackScore
    '                If Not userInfo.eBayIDVerified Is Nothing Then result.eBayIDVerified = userInfo.eBayIDVerified
    '                If Not userInfo.eBayGoodStanding Is Nothing Then result.eBayGoodStanding = userInfo.eBayGoodStanding
    '                If Not userInfo.eBayEIASToken Is Nothing Then result.eBayEIASToken = userInfo.eBayEIASToken
    '                If Not userInfo.eBayFeedbackRatingStar Is Nothing Then result.eBayFeedbackRatingStar = userInfo.eBayFeedbackRatingStar

    '                If Not userInfo.eBayNewUser Is Nothing Then result.eBayNewUser = userInfo.eBayNewUser
    '                If Not userInfo.eBayPayPalAccountLevel Is Nothing Then result.eBayPayPalAccountLevel = userInfo.eBayPayPalAccountLevel
    '                If Not userInfo.eBayPayPalAccountStatus Is Nothing Then result.eBayPayPalAccountStatus = userInfo.eBayPayPalAccountStatus
    '                If Not userInfo.eBayPayPalAccountType Is Nothing Then result.eBayPayPalAccountType = userInfo.eBayPayPalAccountType
    '                If Not userInfo.eBayPositiveFeedbackPercent Is Nothing Then result.eBayPositiveFeedbackPercent = userInfo.eBayPositiveFeedbackPercent

    '                If Not userInfo.eBayRegistrationDate Is Nothing Then result.eBayRegistrationDate = userInfo.eBayRegistrationDate
    '                If Not userInfo.eBayUserID Is Nothing Then result.eBayUserID = userInfo.eBayUserID
    '                If Not userInfo.eBayUserIDLastChanged Is Nothing Then result.eBayUserIDLastChanged = userInfo.eBayUserIDLastChanged
    '                If Not userInfo.eBayUserIDChanged Is Nothing Then result.eBayUserIDChanged = userInfo.eBayUserIDChanged
    '                If Not userInfo.eBayUserStatus Is Nothing Then result.eBayUserStatus = userInfo.eBayUserStatus

    '                If Not userInfo.PaypalAccountId Is Nothing Then result.PaypalAccountID = userInfo.PaypalAccountId
    '                If Not userInfo.PaypalAccountVerified Is Nothing Then result.PaypalAccountVerified = userInfo.PaypalAccountVerified

    '                If Not userInfo.PaypalLastName Is Nothing Then result.PaypalLastName = userInfo.PaypalLastName
    '                If Not userInfo.PaypalFirstName Is Nothing Then result.PaypalFirstName = userInfo.PaypalFirstName

    '                'If Not  userInfo.CreditCardType Is Then result.CreditCardType =  userInfo.CreditCardType
    '                'If Not  userInfo.CreditCardNumber Is Then result.CreditCardNumber =  userInfo.CreditCardNumber
    '                'If Not  userInfo.CreditCardExpYear Is Then result.CreditCardExpYear =  userInfo.CreditCardExpYear
    '                'If Not  userInfo.CreditCardNameOnCard Is Then result.CreditCardNameOnCard =  userInfo.CreditCardNameOnCard
    '                'If Not  userInfo.CreditCardCVVCode Is Then result.CreditCardCVVCode =  userInfo.CreditCardCVVCode
    '                'If Not  userInfo.CreditCardExpMonth Is Then result.CreditCardExpMonth =  userInfo.CreditCardExpMonth

    '                If Not userInfo.CompanyID Is Nothing Then result.CompanyID = userInfo.CompanyID
    '                If Not userInfo.Unsubscribed Is Nothing Then result.Unsubscribed = userInfo.Unsubscribed
    '                If Not userInfo.Optin Is Nothing Then result.Optin = userInfo.Optin
    '                If Not userInfo.OptinDate Is Nothing Then result.OptinDate = userInfo.OptinDate
    '                If Not userInfo.LastSellerCompanyID Is Nothing Then result.LastSellerCompanyID = userInfo.LastSellerCompanyID
    '                If Not userInfo.ClientID Is Nothing Then result.ClientID = userInfo.ClientID

    '                If Not userInfo.ActivationCode Is Nothing Then result.ActivationCode = userInfo.ActivationCode
    '                If Not userInfo.AccountActivated Is Nothing Then result.AccountActivated = userInfo.AccountActivated
    '                If Not userInfo.AccountActivatedOn Is Nothing Then result.AccountActivatedOn = userInfo.AccountActivatedOn
    '                If Not userInfo.IsWholeSaleUser Is Nothing Then result.IsWholeSaleUser = userInfo.IsWholeSaleUser
    '                If Not userInfo.LastLoginFromIP Is Nothing Then result.LastLoginFromIP = userInfo.LastLoginFromIP
    '                If Not userInfo.CurrentLoginDate Is Nothing Then result.CurrentLoginDate = userInfo.CurrentLoginDate
    '                If Not userInfo.ImportSourceUniqueID Is Nothing Then result.ImportSourceUniqueID = userInfo.ImportSourceUniqueID

    '                If Not userInfo.AccountLocked Is Nothing Then result.AccountLocked = userInfo.AccountLocked
    '                If Not userInfo.AccountLockedUntil Is Nothing Then result.AccountLockedUntil = userInfo.AccountLockedUntil
    '                If Not userInfo.AccountLockedReason Is Nothing Then result.AccountLockedReason = userInfo.AccountLockedReason
    '                If Not userInfo.PasswordLastChangedOn Is Nothing Then result.PasswordLastChangedOn = userInfo.PasswordLastChangedOn

    '                'If result.CreditCardNumber <> "" Then
    '                '    Dim crypto As New Encryption.TripleDesEncryption
    '                '    result.CreditCardNumber = crypto.Decode(result.CreditCardNumber)
    '                '    crypto = Nothing
    '                'End If
    '            End With
    '        End If

    '        Return result
    '    Catch ex As Exception

    '    End Try
    'End Function




End Class
