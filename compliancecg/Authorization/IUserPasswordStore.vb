'// Copyright (c) Microsoft Corporation, Inc. All rights reserved.
'// Licensed under the MIT License, Version 2.0. See License.txt in the project root for license information.

Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity

Namespace Microsoft.AspNet.Identity

    '/// <summary>
    '///     Stores a user's password hash
    '/// </summary>
    '/// <typeparam name="TUser"></typeparam>
    'Public Interface IUserPasswordStore<TUser> : IUserPasswordStore<TUser, string> where TUser : Class, IUser<string>

    '/// <summary>
    '///     Stores a user's password hash
    '/// </summary>
    '/// <typeparam name="TUser"></typeparam>
    '/// <typeparam name="TKey"></typeparam>
    'Public Interface IUserPasswordStore(Of ApplicationUser As IUserPasswordStore(Of ApplicationUser, Integer) where ApplicationUser : Class, ApplicationUser(Of integer)
    Public Interface IUserPasswordStore(Of ApplicationUser As {Class, IUser(Of TKey)}, In TKey)
        Inherits IUserStore(Of ApplicationUser, TKey), IDisposable

        '/// <summary>
        '///     Set the user password ~hash~
        '/// </summary>
        '/// <param name="user"></param>
        '/// <param name="passwordHash"></param>
        '/// <returns></returns>
        'Task SetPasswordHashAsync(TUser user, string passwordHash);
        Function SetPasswordHashAsync(ByVal user As ApplicationUser, ByVal passwordHash As String) As Task



        '/// <summary>
        '///     Get the user password hash
        '/// </summary>
        '/// <param name="user"></param>
        '/// <returns></returns>
        'Task<string> GetPasswordHashAsync(TUser user);
        Function GetPasswordHashAsync(ByVal user As ApplicationUser) As Task(Of String)


        '/// <summary>
        '///     Returns true if a user has a password set
        '/// </summary>
        '/// <param name="user"></param>
        '/// <returns></returns>
        'Task<bool> HasPasswordAsync(TUser user);
        Function HasPasswordAsync(ByVal user As ApplicationUser) As Task(Of Boolean)

    End Interface
End Namespace