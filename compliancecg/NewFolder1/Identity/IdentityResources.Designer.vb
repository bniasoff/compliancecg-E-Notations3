﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Class IdentityResources
        
        Private Shared resourceMan As Global.System.Resources.ResourceManager
        
        Private Shared resourceCulture As Global.System.Globalization.CultureInfo
        
        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>  _
        Friend Sub New()
            MyBase.New
        End Sub
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("compliancecg.IdentityResources", GetType(IdentityResources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property

        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>
        Friend Shared Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property

        Friend Shared ReadOnly Property DbValidationFailed As String
            Get
                Return ResourceManager.GetString("DbValidationFailed", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property DuplicateEmail As String
            Get
                Return ResourceManager.GetString("DuplicateEmail", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property DuplicateUserName As String
            Get
                Return ResourceManager.GetString("DuplicateUserName", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property EntityFailedValidation As String
            Get
                Return ResourceManager.GetString("EntityFailedValidation", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property ExternalLoginExists As String
            Get
                Return ResourceManager.GetString("ExternalLoginExists", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property IdentityV1SchemaError As String
            Get
                Return ResourceManager.GetString("IdentityV1SchemaError", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property IncorrectType As String
            Get
                Return ResourceManager.GetString("IncorrectType", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property PropertyCannotBeEmpty As String
            Get
                Return ResourceManager.GetString("PropertyCannotBeEmpty", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property RoleAlreadyExists As String
            Get
                Return ResourceManager.GetString("RoleAlreadyExists", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property RoleIsNotEmpty As String
            Get
                Return ResourceManager.GetString("RoleIsNotEmpty", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property RoleNotFound As String
            Get
                Return ResourceManager.GetString("RoleNotFound", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property UserAlreadyInRole As String
            Get
                Return ResourceManager.GetString("UserAlreadyInRole", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property UserIdNotFound As String
            Get
                Return ResourceManager.GetString("UserIdNotFound", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property UserLoginAlreadyExists As String
            Get
                Return ResourceManager.GetString("UserLoginAlreadyExists", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property UserNameNotFound As String
            Get
                Return ResourceManager.GetString("UserNameNotFound", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property UserNotInRole As String
            Get
                Return ResourceManager.GetString("UserNotInRole", resourceCulture)
            End Get
        End Property

        Friend Shared ReadOnly Property ValueCannotBeNullOrEmpty As String
            Get
                Return ResourceManager.GetString("ValueCannotBeNullOrEmpty", resourceCulture)
            End Get
        End Property
    End Class
End Namespace
