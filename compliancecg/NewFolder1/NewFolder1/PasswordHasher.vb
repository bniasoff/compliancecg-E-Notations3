Imports System
Imports System.Runtime.CompilerServices
Imports System.Security.Cryptography
Imports Microsoft.AspNetCore.Cryptography.KeyDerivation
Imports Microsoft.Extensions.Options
Imports System.Runtime.InteropServices

Namespace Microsoft.AspNetCore.Identity
    Public Class PasswordHasher(Of TUser As Class)
        Inherits IPasswordHasher(Of TUser)

        Private ReadOnly _compatibilityMode As PasswordHasherCompatibilityMode
        Private ReadOnly _iterCount As Integer
        Private ReadOnly _rng As RandomNumberGenerator

        Public Sub New(ByVal Optional optionsAccessor As IOptions(Of PasswordHasherOptions) = Nothing)
            Dim options = If(optionsAccessor?.Value, New PasswordHasherOptions())
            _compatibilityMode = options.CompatibilityMode

            Select Case _compatibilityMode
                Case PasswordHasherCompatibilityMode.IdentityV2
                Case PasswordHasherCompatibilityMode.IdentityV3
                    _iterCount = options.IterationCount

                    If _iterCount < 1 Then
                        Throw New InvalidOperationException(Resources.InvalidPasswordHasherIterationCount)
                    End If

                Case Else
                    Throw New InvalidOperationException(Resources.InvalidPasswordHasherCompatibilityMode)
            End Select

            _rng = options.Rng
        End Sub

        <MethodImpl(MethodImplOptions.NoInlining Or MethodImplOptions.NoOptimization)>
        Private Shared Function ByteArraysEqual(ByVal a As Byte(), ByVal b As Byte()) As Boolean
            If a Is Nothing AndAlso b Is Nothing Then
                Return True
            End If

            If a Is Nothing OrElse b Is Nothing OrElse a.Length <> b.Length Then
                Return False
            End If

            Dim areSame = True

            For i = 0 To a.Length - 1
                areSame = areSame And (a(i) = b(i))
            Next

            Return areSame
        End Function

        Public Overridable Function HashPassword(ByVal user As TUser, ByVal password As String) As String
            If password Is Nothing Then
                Throw New ArgumentNullException(NameOf(password))
            End If

            If _compatibilityMode = PasswordHasherCompatibilityMode.IdentityV2 Then
                Return Convert.ToBase64String(HashPasswordV2(password, _rng))
            Else
                Return Convert.ToBase64String(HashPasswordV3(password, _rng))
            End If
        End Function

        Private Shared Function HashPasswordV2(ByVal password As String, ByVal rng As RandomNumberGenerator) As Byte()
            Const Pbkdf2Prf As KeyDerivationPrf = KeyDerivationPrf.HMACSHA1
            Const Pbkdf2IterCount As Integer = 1000
            Const Pbkdf2SubkeyLength As Integer = 256 / 8
            Const SaltSize As Integer = 128 / 8
            Dim salt As Byte() = New Byte(15) {}
            rng.GetBytes(salt)
            Dim subkey As Byte() = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength)
            Dim outputBytes = New Byte(48) {}
            outputBytes(0) = &H0
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize)
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength)
            Return outputBytes
        End Function

        Private Function HashPasswordV3(ByVal password As String, ByVal rng As RandomNumberGenerator) As Byte()
            Return HashPasswordV3(password, rng, prf:=KeyDerivationPrf.HMACSHA256, iterCount:=_iterCount, saltSize:=128 / 8, numBytesRequested:=256 / 8)
        End Function

        Private Shared Function HashPasswordV3(ByVal password As String, ByVal rng As RandomNumberGenerator, ByVal prf As KeyDerivationPrf, ByVal iterCount As Integer, ByVal saltSize As Integer, ByVal numBytesRequested As Integer) As Byte()
            Dim salt As Byte() = New Byte(saltSize - 1) {}
            rng.GetBytes(salt)
            Dim subkey As Byte() = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, numBytesRequested)
            Dim outputBytes = New Byte(13 + salt.Length + subkey.Length - 1) {}
            outputBytes(0) = &H1
            WriteNetworkByteOrder(outputBytes, 1, CUInt(prf))
            WriteNetworkByteOrder(outputBytes, 5, CUInt(iterCount))
            WriteNetworkByteOrder(outputBytes, 9, CUInt(saltSize))
            Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length)
            Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, subkey.Length)
            Return outputBytes
        End Function

        Private Shared Function ReadNetworkByteOrder(ByVal buffer As Byte(), ByVal offset As Integer) As UInteger
                        ''' Cannot convert ReturnStatementSyntax, System.ArgumentOutOfRangeException: Exception of type 'System.ArgumentOutOfRangeException' was thrown.
''' Parameter name: op
''' Actual value was LeftShiftExpression.
'''    at ICSharpCode.CodeConverter.Util.VBUtil.GetExpressionOperatorTokenKind(SyntaxKind op)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitBinaryExpression(BinaryExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitBinaryExpression(BinaryExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitBinaryExpression(BinaryExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitBinaryExpression(BinaryExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitBinaryExpression(BinaryExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitBinaryExpression(BinaryExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitBinaryExpression(BinaryExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitBinaryExpression(BinaryExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.VisitReturnStatement(ReturnStatementSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.Syntax.ReturnStatementSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)
''' 
''' Input: 
'''             return ((uint)(buffer[offset + 0]) << 24)
                | ((uint)(buffer[offset + 1]) << 16)
                | ((uint)(buffer[offset + 2]) << 8)
                | ((uint)(buffer[offset + 3]));

''' 
        End Function

        Public Overridable Function VerifyHashedPassword(ByVal user As TUser, ByVal hashedPassword As String, ByVal providedPassword As String) As PasswordVerificationResult
            If hashedPassword Is Nothing Then
                Throw New ArgumentNullException(NameOf(hashedPassword))
            End If

            If providedPassword Is Nothing Then
                Throw New ArgumentNullException(NameOf(providedPassword))
            End If

            Dim decodedHashedPassword As Byte() = Convert.FromBase64String(hashedPassword)

            If decodedHashedPassword.Length = 0 Then
                Return PasswordVerificationResult.Failed
            End If

            Select Case decodedHashedPassword(0)
                Case &H0

                    If VerifyHashedPasswordV2(decodedHashedPassword, providedPassword) Then
                        Return If((_compatibilityMode = PasswordHasherCompatibilityMode.IdentityV3), PasswordVerificationResult.SuccessRehashNeeded, PasswordVerificationResult.Success)
                    Else
                        Return PasswordVerificationResult.Failed
                    End If

                Case &H1
                    Dim embeddedIterCount As Integer

                    If VerifyHashedPasswordV3(decodedHashedPassword, providedPassword, embeddedIterCount) Then
                        Return If((embeddedIterCount < _iterCount), PasswordVerificationResult.SuccessRehashNeeded, PasswordVerificationResult.Success)
                    Else
                        Return PasswordVerificationResult.Failed
                    End If

                Case Else
                    Return PasswordVerificationResult.Failed
            End Select
        End Function

        Private Shared Function VerifyHashedPasswordV2(ByVal hashedPassword As Byte(), ByVal password As String) As Boolean
            Const Pbkdf2Prf As KeyDerivationPrf = KeyDerivationPrf.HMACSHA1
            Const Pbkdf2IterCount As Integer = 1000
            Const Pbkdf2SubkeyLength As Integer = 256 / 8
            Const SaltSize As Integer = 128 / 8

            If hashedPassword.Length <> 1 + SaltSize + Pbkdf2SubkeyLength Then
                Return False
            End If

            Dim salt As Byte() = New Byte(15) {}
            Buffer.BlockCopy(hashedPassword, 1, salt, 0, salt.Length)
            Dim expectedSubkey As Byte() = New Byte(31) {}
            Buffer.BlockCopy(hashedPassword, 1 + salt.Length, expectedSubkey, 0, expectedSubkey.Length)
            Dim actualSubkey As Byte() = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength)
            Return ByteArraysEqual(actualSubkey, expectedSubkey)
        End Function

        Private Shared Function VerifyHashedPasswordV3(ByVal hashedPassword As Byte(), ByVal password As String, <Out> ByRef iterCount As Integer) As Boolean
            iterCount = Nothing

            Try
                Dim prf As KeyDerivationPrf = CType(ReadNetworkByteOrder(hashedPassword, 1), KeyDerivationPrf)
                iterCount = CInt(ReadNetworkByteOrder(hashedPassword, 5))
                Dim saltLength As Integer = CInt(ReadNetworkByteOrder(hashedPassword, 9))

                If saltLength < 128 / 8 Then
                    Return False
                End If

                Dim salt As Byte() = New Byte(saltLength - 1) {}
                Buffer.BlockCopy(hashedPassword, 13, salt, 0, salt.Length)
                Dim subkeyLength As Integer = hashedPassword.Length - 13 - salt.Length

                If subkeyLength < 128 / 8 Then
                    Return False
                End If

                Dim expectedSubkey As Byte() = New Byte(subkeyLength - 1) {}
                Buffer.BlockCopy(hashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length)
                Dim actualSubkey As Byte() = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, subkeyLength)
                Return ByteArraysEqual(actualSubkey, expectedSubkey)
            Catch
                Return False
            End Try
        End Function

        Private Shared Sub WriteNetworkByteOrder(ByVal buffer As Byte(), ByVal offset As Integer, ByVal value As UInteger)
            ''' Cannot convert ExpressionStatementSyntax, System.ArgumentOutOfRangeException: Exception of type 'System.ArgumentOutOfRangeException' was thrown.
            ''' Parameter name: op
            ''' Actual value was RightShiftExpression.
            '''    at ICSharpCode.CodeConverter.Util.VBUtil.GetExpressionOperatorTokenKind(SyntaxKind op)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitBinaryExpression(BinaryExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitBinaryExpression(BinaryExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitCastExpression(CastExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.CastExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitCastExpression(CastExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.CastExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.MakeAssignmentStatement(AssignmentExpressionSyntax node)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitAssignmentExpression(AssignmentExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitAssignmentExpression(AssignmentExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.ConvertSingleExpression(ExpressionSyntax node)
            '''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.VisitExpressionStatement(ExpressionStatementSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionStatementSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)
            ''' 
            ''' Input: 
            '''             buffer[offset + 0] = (byte)(value >> 24);

            ''' 
            ''' Cannot convert ExpressionStatementSyntax, System.ArgumentOutOfRangeException: Exception of type 'System.ArgumentOutOfRangeException' was thrown.
            ''' Parameter name: op
            ''' Actual value was RightShiftExpression.
            '''    at ICSharpCode.CodeConverter.Util.VBUtil.GetExpressionOperatorTokenKind(SyntaxKind op)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitBinaryExpression(BinaryExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitBinaryExpression(BinaryExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitCastExpression(CastExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.CastExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitCastExpression(CastExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.CastExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.MakeAssignmentStatement(AssignmentExpressionSyntax node)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitAssignmentExpression(AssignmentExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitAssignmentExpression(AssignmentExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.ConvertSingleExpression(ExpressionSyntax node)
            '''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.VisitExpressionStatement(ExpressionStatementSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionStatementSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)
            ''' 
            ''' Input: 
            '''             buffer[offset + 1] = (byte)(value >> 16);

            ''' 
            ''' Cannot convert ExpressionStatementSyntax, System.ArgumentOutOfRangeException: Exception of type 'System.ArgumentOutOfRangeException' was thrown.
            ''' Parameter name: op
            ''' Actual value was RightShiftExpression.
            '''    at ICSharpCode.CodeConverter.Util.VBUtil.GetExpressionOperatorTokenKind(SyntaxKind op)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitBinaryExpression(BinaryExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitBinaryExpression(BinaryExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitCastExpression(CastExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.CastExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitCastExpression(CastExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.CastExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.MakeAssignmentStatement(AssignmentExpressionSyntax node)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitAssignmentExpression(AssignmentExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitAssignmentExpression(AssignmentExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.ConvertSingleExpression(ExpressionSyntax node)
            '''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.VisitExpressionStatement(ExpressionStatementSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionStatementSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)
            ''' 
            ''' Input: 
            '''             buffer[offset + 2] = (byte)(value >> 8);

            ''' 
            ''' Cannot convert ExpressionStatementSyntax, System.ArgumentOutOfRangeException: Exception of type 'System.ArgumentOutOfRangeException' was thrown.
            ''' Parameter name: op
            ''' Actual value was RightShiftExpression.
            '''    at ICSharpCode.CodeConverter.Util.VBUtil.GetExpressionOperatorTokenKind(SyntaxKind op)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitBinaryExpression(BinaryExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitBinaryExpression(BinaryExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitCastExpression(CastExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.CastExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitCastExpression(CastExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.CastExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.MakeAssignmentStatement(AssignmentExpressionSyntax node)
            '''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitAssignmentExpression(AssignmentExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.VisitAssignmentExpression(AssignmentExpressionSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.ConvertSingleExpression(ExpressionSyntax node)
            '''    at ICSharpCode.CodeConverter.VB.MethodBodyVisitor.VisitExpressionStatement(ExpressionStatementSyntax node)
            '''    at Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionStatementSyntax.Accept[TResult](CSharpSyntaxVisitor`1 visitor)
            '''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
            '''    at ICSharpCode.CodeConverter.VB.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)
            ''' 
            ''' Input: 
            '''             buffer[offset + 3] = (byte)(value >> 0);

            ''' 
        End Sub
    End Class
End Namespace
