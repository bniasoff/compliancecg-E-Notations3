Imports System
Imports System.Data.Entity.Validation
Imports System.Linq
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Xunit

Namespace Identity.Test
    Public Class RolesTest
        '<Fact>
        'Public Sub ManagerPublicNullCheckTest()
        '    ExceptionHelper.ThrowsArgumentNull(Function() New RoleValidator(Of IdentityRole)(Nothing), "manager")
        '    ExceptionHelper.ThrowsArgumentNull(Function() New RoleManager(Of IdentityRole)(Nothing), "store")
        '    Dim manager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)())
        '    ExceptionHelper.ThrowsArgumentNull(Function() CSharpImpl.__Assign(manager.RoleValidator, Nothing), "value")
        '    ExceptionHelper.ThrowsArgumentNull(Function() New RoleManager(Of IdentityRole)(Nothing), "store")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() manager.RoleValidator.ValidateAsync(Nothing)), "item")
        '    ExceptionHelper.ThrowsArgumentNull(Function() manager.Create(Nothing), "role")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() manager.UpdateAsync(Nothing)), "role")
        '    ExceptionHelper.ThrowsArgumentNull(Function() manager.Update(Nothing), "role")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() manager.RoleExistsAsync(Nothing)), "roleName")
        '    ExceptionHelper.ThrowsArgumentNull(Function() manager.RoleExists(Nothing), "roleName")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() manager.FindByNameAsync(Nothing)), "roleName")
        '    ExceptionHelper.ThrowsArgumentNull(Function() manager.FindByName(Nothing), "roleName")
        'End Sub

        '<Fact>
        'Public Sub RoleManagerMethodsThrowWhenDisposedTest()
        '    Dim manager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)())
        '    manager.Dispose()
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.CreateAsync(Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() manager.Create(Nothing))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.UpdateAsync(Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() manager.Update(Nothing))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.DeleteAsync(Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() manager.Delete(Nothing))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.FindByIdAsync(Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() manager.FindById(Nothing))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() manager.FindByNameAsync(Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() manager.FindByName(Nothing))
        'End Sub

        '<Fact>
        'Public Sub RoleStoreMethodsThrowWhenDisposedTest()
        '    Dim store = New RoleStore(Of IdentityRole)()
        '    store.Dispose()
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.CreateAsync(Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.UpdateAsync(Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.DeleteAsync(Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.FindByIdAsync(Nothing)))
        '    Assert.Throws(Of ObjectDisposedException)(Function() AsyncHelper.RunSync(Function() store.FindByNameAsync(Nothing)))
        'End Sub

        '<Fact>
        'Public Sub RoleManagerSyncMethodsThrowWhenManagerNullTest()
        '    Dim manager As RoleManager(Of IdentityRole) = Nothing
        '    ExceptionHelper.ThrowsArgumentNull(Function() manager.Create(Nothing), "manager")
        '    ExceptionHelper.ThrowsArgumentNull(Function() manager.Update(Nothing), "manager")
        '    ExceptionHelper.ThrowsArgumentNull(Function() manager.Delete(Nothing), "manager")
        '    ExceptionHelper.ThrowsArgumentNull(Function() manager.FindById(Nothing), "manager")
        '    ExceptionHelper.ThrowsArgumentNull(Function() manager.FindByName(Nothing), "manager")
        '    ExceptionHelper.ThrowsArgumentNull(Function() manager.RoleExists(Nothing), "manager")
        'End Sub

        '<Fact>
        'Public Sub RoleStorePublicNullCheckTest()
        '    ExceptionHelper.ThrowsArgumentNull(Function() New RoleStore(Of IdentityRole)(Nothing), "context")
        '    Dim store = New RoleStore(Of IdentityRole)()
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.CreateAsync(Nothing)), "role")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.UpdateAsync(Nothing)), "role")
        '    ExceptionHelper.ThrowsArgumentNull(Function() AsyncHelper.RunSync(Function() store.DeleteAsync(Nothing)), "role")
        'End Sub

        '<Fact>
        'Public Sub RolesQueryableFailWhenStoreNotImplementedTest()
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim manager = New RoleManager(Of IdentityRole)(New NoopRoleStore())
        '    Assert.Throws(Of NotSupportedException)(Function() manager.Roles.Count())
        'End Sub

        '<Fact>
        'Public Async Function CreateRoleTest() As Task
        '    Dim manager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(UnitTestHelper.CreateDefaultDb()))
        '    Dim role = New IdentityRole("create")
        '    Assert.[False](Await manager.RoleExistsAsync(role.Name))
        '    UnitTestHelper.IsSuccess(Await manager.CreateAsync(role))
        '    Assert.[True](Await manager.RoleExistsAsync(role.Name))
        'End Function

        '<Fact>
        'Public Async Function BadValidatorBlocksCreateTest() As Task
        '    Dim manager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)())
        '    manager.RoleValidator = New AlwaysBadValidator(Of IdentityRole)()
        '    UnitTestHelper.IsFailure(Await manager.CreateAsync(New IdentityRole("blocked")), AlwaysBadValidator(Of IdentityRole).ErrorMessage)
        'End Function

        '<Fact>
        'Public Async Function BadValidatorBlocksAllUpdatesTest() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim manager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(db))
        '    Dim role = New IdentityRole("poorguy")
        '    UnitTestHelper.IsSuccess(Await manager.CreateAsync(role))
        '    Dim [error] = AlwaysBadValidator(Of IdentityRole).ErrorMessage
        '    manager.RoleValidator = New AlwaysBadValidator(Of IdentityRole)()
        '    UnitTestHelper.IsFailure(Await manager.UpdateAsync(role), [error])
        'End Function

        '<Fact>
        'Public Async Function DeleteRoleTest() As Task
        '    Dim manager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(UnitTestHelper.CreateDefaultDb()))
        '    Dim role = New IdentityRole("delete")
        '    Assert.[False](Await manager.RoleExistsAsync(role.Name))
        '    UnitTestHelper.IsSuccess(Await manager.CreateAsync(role))
        '    UnitTestHelper.IsSuccess(Await manager.DeleteAsync(role))
        '    Assert.[False](Await manager.RoleExistsAsync(role.Name))
        'End Function

        '<Fact>
        'Public Sub DeleteRoleSyncTest()
        '    Dim manager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(UnitTestHelper.CreateDefaultDb()))
        '    Dim role = New IdentityRole("delete")
        '    Assert.[False](manager.RoleExists(role.Name))
        '    UnitTestHelper.IsSuccess(manager.Create(role))
        '    UnitTestHelper.IsSuccess(manager.Delete(role))
        '    Assert.[False](manager.RoleExists(role.Name))
        'End Sub

        '<Fact>
        'Public Sub DeleteFailWithUnknownRoleTest()
        '    Dim manager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(UnitTestHelper.CreateDefaultDb()))
        '    Assert.Throws(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.DeleteAsync(New IdentityRole("bogus"))))
        'End Sub

        '<Fact>
        'Public Async Function RoleFindByIdTest() As Task
        '    Dim manager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(UnitTestHelper.CreateDefaultDb()))
        '    Dim role = New IdentityRole("FindById")
        '    Assert.Null(Await manager.FindByIdAsync(role.Id))
        '    UnitTestHelper.IsSuccess(Await manager.CreateAsync(role))
        '    Assert.Equal(role, Await manager.FindByIdAsync(role.Id))
        'End Function

        '<Fact>
        'Public Sub RoleFindByIdSyncTest()
        '    Dim manager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(UnitTestHelper.CreateDefaultDb()))
        '    Dim role = New IdentityRole("FindById")
        '    Assert.Null(manager.FindById(role.Id))
        '    UnitTestHelper.IsSuccess(manager.Create(role))
        '    Assert.Equal(role, manager.FindById(role.Id))
        'End Sub

        '<Fact>
        'Public Async Function RoleFindByNameTest() As Task
        '    Dim manager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(UnitTestHelper.CreateDefaultDb()))
        '    Dim role = New IdentityRole("FindByName")
        '    Assert.Null(Await manager.FindByNameAsync(role.Name))
        '    Assert.[False](Await manager.RoleExistsAsync(role.Name))
        '    UnitTestHelper.IsSuccess(Await manager.CreateAsync(role))
        '    Assert.Equal(role, Await manager.FindByNameAsync(role.Name))
        'End Function

        '<Fact>
        'Public Sub RoleFindByNameSyncTest()
        '    Dim manager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(UnitTestHelper.CreateDefaultDb()))
        '    Dim role = New IdentityRole("FindByName")
        '    Assert.[False](manager.RoleExists(role.Name))
        '    UnitTestHelper.IsSuccess(manager.Create(role))
        '    Assert.Equal(role, manager.FindByName(role.Name))
        'End Sub

        '<Fact>
        'Public Async Function UpdateRoleNameTest() As Task
        '    Dim manager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(UnitTestHelper.CreateDefaultDb()))
        '    Dim role = New IdentityRole("update")
        '    Assert.[False](Await manager.RoleExistsAsync(role.Name))
        '    UnitTestHelper.IsSuccess(Await manager.CreateAsync(role))
        '    Assert.[True](Await manager.RoleExistsAsync(role.Name))
        '    role.Name = "Changed"
        '    UnitTestHelper.IsSuccess(Await manager.UpdateAsync(role))
        '    Assert.[False](Await manager.RoleExistsAsync("update"))
        '    Assert.Equal(role, Await manager.FindByNameAsync(role.Name))
        'End Function

        '<Fact>
        'Public Sub UpdateRoleNameSyncTest()
        '    Dim manager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(UnitTestHelper.CreateDefaultDb()))
        '    Dim role = New IdentityRole("update")
        '    Assert.[False](manager.RoleExists(role.Name))
        '    UnitTestHelper.IsSuccess(manager.Create(role))
        '    Assert.[True](manager.RoleExists(role.Name))
        '    role.Name = "Changed"
        '    UnitTestHelper.IsSuccess(manager.Update(role))
        '    Assert.[False](manager.RoleExists("update"))
        '    Assert.Equal(role, manager.FindByName(role.Name))
        'End Sub

        '<Fact>
        'Public Async Function QuerableRolesTest() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim manager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(db))
        '    Dim roles As IdentityRole() = {New IdentityRole("r1"), New IdentityRole("r2"), New IdentityRole("r3"), New IdentityRole("r4")}

        '    For Each r As IdentityRole In roles
        '        UnitTestHelper.IsSuccess(Await manager.CreateAsync(r))
        '    Next

        '    Assert.Equal(roles.Length, manager.Roles.Count())
        '    Dim r1 = manager.Roles.FirstOrDefault(Function(r) r.Name = "r1")
        '    Assert.Equal(roles(0), r1)
        'End Function

        '<Fact>
        'Public Async Function DeleteRoleNonEmptySucceedsTest() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim userMgr = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
        '    Dim roleMgr = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(db))
        '    Dim role = New IdentityRole("deleteNonEmpty")
        '    Assert.[False](Await roleMgr.RoleExistsAsync(role.Name))
        '    UnitTestHelper.IsSuccess(Await roleMgr.CreateAsync(role))
        '    Dim user = New IdentityUser("t")
        '    UnitTestHelper.IsSuccess(Await userMgr.CreateAsync(user))
        '    UnitTestHelper.IsSuccess(Await userMgr.AddToRoleAsync(user.Id, role.Name))
        '    UnitTestHelper.IsSuccess(Await roleMgr.DeleteAsync(role))
        '    Assert.[False](Await roleMgr.RoleExistsAsync(role.Name))
        '    Dim roles = Await userMgr.GetRolesAsync(user.Id)
        '    Assert.Equal(0, roles.Count())
        'End Function

        '<Fact>
        'Public Async Function DeleteUserRemovesFromRoleTest() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim userMgr = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
        '    Dim roleMgr = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(db))
        '    Dim role = New IdentityRole("deleteNonEmpty")
        '    Assert.[False](Await roleMgr.RoleExistsAsync(role.Name))
        '    UnitTestHelper.IsSuccess(Await roleMgr.CreateAsync(role))
        '    Dim user = New IdentityUser("t")
        '    UnitTestHelper.IsSuccess(Await userMgr.CreateAsync(user))
        '    UnitTestHelper.IsSuccess(Await userMgr.AddToRoleAsync(user.Id, role.Name))
        '    UnitTestHelper.IsSuccess(Await userMgr.DeleteAsync(user))
        '    role = roleMgr.FindById(role.Id)
        '    Assert.Equal(0, role.Users.Count())
        'End Function

        '<Fact>
        'Public Async Function DeleteRoleUnknownFailsTest() As Task
        '    Dim manager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(UnitTestHelper.CreateDefaultDb()))
        '    Dim role = New IdentityRole("bogus")
        '    Assert.[False](Await manager.RoleExistsAsync(role.Name))
        '    Assert.Throws(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.DeleteAsync(role)))
        'End Function

        '<Fact>
        'Public Async Function CreateRoleFailsIfExistsTest() As Task
        '    Dim manager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(UnitTestHelper.CreateDefaultDb()))
        '    Dim role = New IdentityRole("dupeRole")
        '    Assert.[False](Await manager.RoleExistsAsync(role.Name))
        '    UnitTestHelper.IsSuccess(Await manager.CreateAsync(role))
        '    Assert.[True](Await manager.RoleExistsAsync(role.Name))
        '    Dim role2 = New IdentityRole("dupeRole")
        '    UnitTestHelper.IsFailure(Await manager.CreateAsync(role2))
        'End Function

        '<Fact>
        'Public Async Function CreateDuplicateRoleAtStoreFailsTest() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim store = New RoleStore(Of IdentityRole)(db)
        '    Dim role = New IdentityRole("dupeRole")
        '    Await store.CreateAsync(role)
        '    db.SaveChanges()
        '    Dim role2 = New IdentityRole("dupeRole")
        '    Assert.Throws(Of DbEntityValidationException)(Function() AsyncHelper.RunSync(Function() store.CreateAsync(role2)))
        'End Function

        '  <Fact>
        Public Async Function AddUserToRoleTest() As Task
            Dim manager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(UnitTestHelper.CreateDefaultDb()))
            Dim roleManager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(UnitTestHelper.CreateDefaultDb()))
            Dim role = New IdentityRole("addUserTest")

            Await roleManager.CreateAsync(role)
            Dim users As IdentityUser() = {New IdentityUser("1"), New IdentityUser("2"), New IdentityUser("3"), New IdentityUser("4")}

            For Each u As IdentityUser In users
                Await manager.CreateAsync(u)
                Await manager.AddToRoleAsync(u.Id, role.Name)
                Dim Cnt1 = u.Roles.Where(Function(ur) ur.RoleId = role.Id).Count
                Await manager.IsInRoleAsync(u.Id, role.Name)
            Next


            '    UnitTestHelper.IsSuccess(Await roleManager.CreateAsync(role))
            '    Dim users As IdentityUser() = {New IdentityUser("1"), New IdentityUser("2"), New IdentityUser("3"), New IdentityUser("4")}

            '    For Each u As IdentityUser In users
            '        UnitTestHelper.IsSuccess(Await manager.CreateAsync(u))
            '        UnitTestHelper.IsSuccess(Await manager.AddToRoleAsync(u.Id, role.Name))
            '        Assert.Equal(1, u.Roles.Count(Function(ur) ur.RoleId = role.Id))
            '        Assert.[True](Await manager.IsInRoleAsync(u.Id, role.Name))
            '    Next
        End Function

        '<Fact>
        'Public Async Function GetRolesForUserTest() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim userManager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
        '    Dim roleManager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(db))
        '    Dim users As IdentityUser() = {New IdentityUser("u1"), New IdentityUser("u2"), New IdentityUser("u3"), New IdentityUser("u4")}
        '    Dim roles As IdentityRole() = {New IdentityRole("r1"), New IdentityRole("r2"), New IdentityRole("r3"), New IdentityRole("r4")}

        '    For Each u In users
        '        UnitTestHelper.IsSuccess(Await userManager.CreateAsync(u))
        '    Next

        '    For Each r In roles
        '        UnitTestHelper.IsSuccess(Await roleManager.CreateAsync(r))

        '        For Each u In users
        '            UnitTestHelper.IsSuccess(Await userManager.AddToRoleAsync(u.Id, r.Name))
        '            Assert.[True](Await userManager.IsInRoleAsync(u.Id, r.Name))
        '        Next

        '        Assert.Equal(users.Length, r.Users.Count())
        '    Next

        '    For Each u In users
        '        Dim rs = Await userManager.GetRolesAsync(u.Id)
        '        Assert.Equal(roles.Length, rs.Count)

        '        For Each r As IdentityRole In roles
        '            Assert.[True](rs.Any(Function(role) role = r.Name))
        '        Next
        '    Next
        'End Function

        '<Fact>
        'Public Sub GetRolesForUserSyncTest()
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim userManager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
        '    Dim roleManager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(db))
        '    Dim users As IdentityUser() = {New IdentityUser("u1"), New IdentityUser("u2"), New IdentityUser("u3"), New IdentityUser("u4")}
        '    Dim roles As IdentityRole() = {New IdentityRole("r1"), New IdentityRole("r2"), New IdentityRole("r3"), New IdentityRole("r4")}

        '    For Each u In users
        '        UnitTestHelper.IsSuccess(userManager.Create(u))
        '    Next

        '    For Each r In roles
        '        UnitTestHelper.IsSuccess(roleManager.Create(r))

        '        For Each u In users
        '            UnitTestHelper.IsSuccess(userManager.AddToRole(u.Id, r.Name))
        '            Assert.[True](userManager.IsInRole(u.Id, r.Name))
        '        Next
        '    Next

        '    For Each u In users
        '        Dim rs = userManager.GetRoles(u.Id)
        '        Assert.Equal(roles.Length, rs.Count)

        '        For Each r In roles
        '            Assert.[True](rs.Any(Function(role) role = r.Name))
        '        Next
        '    Next
        'End Sub

        '<Fact>
        'Public Async Function RemoveUserFromRoleWithMultipleRoles() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim userManager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
        '    Dim roleManager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(UnitTestHelper.CreateDefaultDb()))
        '    Dim user = New IdentityUser("MultiRoleUser")
        '    UnitTestHelper.IsSuccess(Await userManager.CreateAsync(user))
        '    Dim roles As IdentityRole() = {New IdentityRole("r1"), New IdentityRole("r2"), New IdentityRole("r3"), New IdentityRole("r4")}

        '    For Each r In roles
        '        UnitTestHelper.IsSuccess(Await roleManager.CreateAsync(r))
        '        UnitTestHelper.IsSuccess(Await userManager.AddToRoleAsync(user.Id, r.Name))
        '        Assert.Equal(1, user.Roles.Count(Function(ur) ur.RoleId = r.Id))
        '        Assert.[True](Await userManager.IsInRoleAsync(user.Id, r.Name))
        '    Next

        '    UnitTestHelper.IsSuccess(Await userManager.RemoveFromRoleAsync(user.Id, roles(2).Name))
        '    Assert.Equal(0, user.Roles.Count(Function(ur) ur.RoleId = roles(2).Id))
        '    Assert.[False](Await userManager.IsInRoleAsync(user.Id, roles(2).Name))
        'End Function

        '<Fact>
        'Public Async Function RemoveUserFromRoleTest() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim userManager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
        '    Dim roleManager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(UnitTestHelper.CreateDefaultDb()))
        '    Dim users As IdentityUser() = {New IdentityUser("1"), New IdentityUser("2"), New IdentityUser("3"), New IdentityUser("4")}

        '    For Each u In users
        '        UnitTestHelper.IsSuccess(Await userManager.CreateAsync(u, "password"))
        '    Next

        '    Dim r = New IdentityRole("r1")
        '    UnitTestHelper.IsSuccess(Await roleManager.CreateAsync(r))

        '    For Each u In users
        '        UnitTestHelper.IsSuccess(Await userManager.AddToRoleAsync(u.Id, r.Name))
        '        Assert.[True](Await userManager.IsInRoleAsync(u.Id, r.Name))
        '    Next

        '    For Each u In users
        '        UnitTestHelper.IsSuccess(Await userManager.RemoveFromRoleAsync(u.Id, r.Name))
        '        Assert.Equal(0, u.Roles.Count(Function(ur) ur.RoleId = r.Id))
        '        Assert.[False](Await userManager.IsInRoleAsync(u.Id, r.Name))
        '    Next

        '    Assert.Equal(0, db.[Set](Of IdentityUserRole)().Count())
        'End Function

        '<Fact>
        'Public Sub RemoveUserFromRoleSyncTest()
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim store = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
        '    Dim roleManager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(UnitTestHelper.CreateDefaultDb()))
        '    Dim users As IdentityUser() = {New IdentityUser("1"), New IdentityUser("2"), New IdentityUser("3"), New IdentityUser("4")}

        '    For Each u In users
        '        UnitTestHelper.IsSuccess(store.Create(u, "password"))
        '    Next

        '    Dim r = New IdentityRole("r1")
        '    UnitTestHelper.IsSuccess(roleManager.Create(r))

        '    For Each u In users
        '        UnitTestHelper.IsSuccess(store.AddToRole(u.Id, r.Name))
        '        Assert.[True](store.IsInRole(u.Id, r.Name))
        '    Next

        '    For Each u In users
        '        UnitTestHelper.IsSuccess(store.RemoveFromRole(u.Id, r.Name))
        '        Assert.[False](store.IsInRole(u.Id, r.Name))
        '    Next

        '    Assert.Equal(0, db.[Set](Of IdentityUserRole)().Count())
        'End Sub

        '<Fact>
        'Public Async Function RemoveUserFromBogusRolesFails() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim userManager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
        '    Dim user = New IdentityUser("1")
        '    UnitTestHelper.IsSuccess(Await userManager.CreateAsync(user))
        '    UnitTestHelper.IsFailure(Await userManager.RemoveFromRolesAsync(user.Id, "bogus"), "User is not in role.")
        'End Function

        '<Fact>
        'Public Async Function AddToBogusRolesFails() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim userManager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
        '    Dim user = New IdentityUser("1")
        '    UnitTestHelper.IsSuccess(Await userManager.CreateAsync(user))
        '    Assert.Throws(Of InvalidOperationException)(Function() userManager.AddToRoles(user.Id, "bogus"))
        'End Function

        '<Fact>
        'Public Async Function AddToDupeRolesFails() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim userManager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
        '    Dim user = New IdentityUser("1")
        '    Dim roleManager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(UnitTestHelper.CreateDefaultDb()))
        '    Dim roles As IdentityRole() = {New IdentityRole("r1"), New IdentityRole("r2"), New IdentityRole("r3"), New IdentityRole("r4")}

        '    For Each r In roles
        '        UnitTestHelper.IsSuccess(Await roleManager.CreateAsync(r))
        '    Next

        '    UnitTestHelper.IsSuccess(Await userManager.CreateAsync(user))
        '    UnitTestHelper.IsSuccess(Await userManager.AddToRolesAsync(user.Id, "r1", "r2", "r3", "r4"))
        '    UnitTestHelper.IsFailure(Await userManager.AddToRolesAsync(user.Id, "r1", "r2", "r3", "r4"), "User already in role.")
        'End Function

        '<Fact>
        'Public Async Function RemoveUserFromRolesTest() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim userManager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
        '    Dim roleManager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(UnitTestHelper.CreateDefaultDb()))
        '    Dim user = New IdentityUser("1")
        '    Dim roles As IdentityRole() = {New IdentityRole("r1"), New IdentityRole("r2"), New IdentityRole("r3"), New IdentityRole("r4")}

        '    For Each r In roles
        '        UnitTestHelper.IsSuccess(Await roleManager.CreateAsync(r))
        '    Next

        '    UnitTestHelper.IsSuccess(Await userManager.CreateAsync(user))
        '    UnitTestHelper.IsSuccess(Await userManager.AddToRolesAsync(user.Id, roles.[Select](Function(ro) ro.Name).ToArray()))
        '    Assert.Equal(roles.Count(), db.[Set](Of IdentityUserRole)().Count())
        '    Assert.[True](Await userManager.IsInRoleAsync(user.Id, "r1"))
        '    Assert.[True](Await userManager.IsInRoleAsync(user.Id, "r2"))
        '    Assert.[True](Await userManager.IsInRoleAsync(user.Id, "r3"))
        '    Assert.[True](Await userManager.IsInRoleAsync(user.Id, "r4"))
        '    Dim rs = Await userManager.GetRolesAsync(user.Id)
        '    UnitTestHelper.IsSuccess(Await userManager.RemoveFromRolesAsync(user.Id, "r1", "r3"))
        '    rs = Await userManager.GetRolesAsync(user.Id)
        '    Assert.[False](Await userManager.IsInRoleAsync(user.Id, "r1"))
        '    Assert.[False](Await userManager.IsInRoleAsync(user.Id, "r3"))
        '    Assert.[True](Await userManager.IsInRoleAsync(user.Id, "r2"))
        '    Assert.[True](Await userManager.IsInRoleAsync(user.Id, "r4"))
        '    UnitTestHelper.IsSuccess(Await userManager.RemoveFromRolesAsync(user.Id, "r2", "r4"))
        '    Assert.[False](Await userManager.IsInRoleAsync(user.Id, "r2"))
        '    Assert.[False](Await userManager.IsInRoleAsync(user.Id, "r4"))
        '    Assert.Equal(0, db.[Set](Of IdentityUserRole)().Count())
        'End Function

        '<Fact>
        'Public Sub RemoveUserFromRolesSync()
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim userManager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
        '    Dim roleManager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(UnitTestHelper.CreateDefaultDb()))
        '    Dim user = New IdentityUser("1")
        '    Dim roles As IdentityRole() = {New IdentityRole("r1"), New IdentityRole("r2"), New IdentityRole("r3"), New IdentityRole("r4")}

        '    For Each r In roles
        '        UnitTestHelper.IsSuccess(roleManager.Create(r))
        '    Next

        '    UnitTestHelper.IsSuccess(userManager.Create(user))
        '    UnitTestHelper.IsSuccess(userManager.AddToRoles(user.Id, roles.[Select](Function(ro) ro.Name).ToArray()))
        '    Assert.Equal(roles.Count(), db.[Set](Of IdentityUserRole)().Count())
        '    Assert.[True](userManager.IsInRole(user.Id, "r1"))
        '    Assert.[True](userManager.IsInRole(user.Id, "r2"))
        '    Assert.[True](userManager.IsInRole(user.Id, "r3"))
        '    Assert.[True](userManager.IsInRole(user.Id, "r4"))
        '    Dim rs = userManager.GetRoles(user.Id)
        '    UnitTestHelper.IsSuccess(userManager.RemoveFromRoles(user.Id, "r1", "r3"))
        '    rs = userManager.GetRoles(user.Id)
        '    Assert.[False](userManager.IsInRole(user.Id, "r1"))
        '    Assert.[False](userManager.IsInRole(user.Id, "r3"))
        '    Assert.[True](userManager.IsInRole(user.Id, "r2"))
        '    Assert.[True](userManager.IsInRole(user.Id, "r4"))
        '    UnitTestHelper.IsSuccess(userManager.RemoveFromRoles(user.Id, "r2", "r4"))
        '    Assert.[False](userManager.IsInRole(user.Id, "r2"))
        '    Assert.[False](userManager.IsInRole(user.Id, "r4"))
        '    Assert.Equal(0, db.[Set](Of IdentityUserRole)().Count())
        'End Sub

        '<Fact>
        'Public Async Function UnknownRoleThrowsTest() As Task
        '    Dim manager = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(UnitTestHelper.CreateDefaultDb()))
        '    Dim u = New IdentityUser("u1")
        '    UnitTestHelper.IsSuccess(Await manager.CreateAsync(u))
        '    Assert.Throws(Of InvalidOperationException)(Function() AsyncHelper.RunSync(Function() manager.AddToRoleAsync(u.Id, "bogus")))
        'End Function

        '<Fact>
        'Public Async Function RemoveUserNotInRoleFailsTest() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim userMgr = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
        '    Dim roleMgr = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(db))
        '    Dim role = New IdentityRole("addUserDupeTest")
        '    Dim user = New IdentityUser("user1")
        '    UnitTestHelper.IsSuccess(Await userMgr.CreateAsync(user))
        '    UnitTestHelper.IsSuccess(Await roleMgr.CreateAsync(role))
        '    Dim result = Await userMgr.RemoveFromRoleAsync(user.Id, role.Name)
        '    UnitTestHelper.IsFailure(result, "User is not in role.")
        'End Function

        '<Fact>
        'Public Async Function AddUserToRoleFailsIfAlreadyInRoleTest() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim userMgr = New UserManager(Of IdentityUser)(New UserStore(Of IdentityUser)(db))
        '    Dim roleMgr = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(db))
        '    Dim role = New IdentityRole("addUserDupeTest")
        '    Dim user = New IdentityUser("user1")
        '    UnitTestHelper.IsSuccess(Await userMgr.CreateAsync(user))
        '    UnitTestHelper.IsSuccess(Await roleMgr.CreateAsync(role))
        '    UnitTestHelper.IsSuccess(Await userMgr.AddToRoleAsync(user.Id, role.Name))
        '    Assert.[True](Await userMgr.IsInRoleAsync(user.Id, role.Name))
        '    UnitTestHelper.IsFailure(Await userMgr.AddToRoleAsync(user.Id, role.Name), "User already in role.")
        'End Function

        '<Fact>
        'Public Async Function FindRoleByNameWithManagerTest() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim roleMgr = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(db))
        '    Dim role = New IdentityRole("findRoleByNameTest")
        '    UnitTestHelper.IsSuccess(Await roleMgr.CreateAsync(role))
        '    Assert.Equal(role.Id, (Await roleMgr.FindByNameAsync(role.Name)).Id)
        'End Function

        '<Fact>
        'Public Async Function FindRoleWithManagerTest() As Task
        '    Dim db = UnitTestHelper.CreateDefaultDb()
        '    Dim roleMgr = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(db))
        '    Dim role = New IdentityRole("findRoleTest")
        '    UnitTestHelper.IsSuccess(Await roleMgr.CreateAsync(role))
        '    Assert.Equal(role.Name, (Await roleMgr.FindByIdAsync(role.Id)).Name)
        'End Function

        'Public Class NoopRoleStore
        '    Inherits IRoleStore(Of IdentityRole)

        '    Public Function CreateAsync(ByVal role As IdentityRole) As Task
        '        Throw New NotImplementedException()
        '    End Function

        '    Public Function UpdateAsync(ByVal role As IdentityRole) As Task
        '        Throw New NotImplementedException()
        '    End Function

        '    Public Function DeleteAsync(ByVal role As IdentityRole) As Task
        '        Throw New NotImplementedException()
        '    End Function

        '    Public Function FindByIdAsync(ByVal roleId As String) As Task(Of IdentityRole)
        '        Throw New NotImplementedException()
        '    End Function

        '    Public Function FindByNameAsync(ByVal roleName As String) As Task(Of IdentityRole)
        '        Throw New NotImplementedException()
        '    End Function

        '    Public Sub Dispose()
        '    End Sub

        '    Private Class CSharpImpl
        '        <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
        '        Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
        '            target = value
        '            Return value
        '        End Function
        '    End Class
        'End Class

        'Private Class CSharpImpl
        '    <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
        '    Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
        '        target = value
        '        Return value
        '    End Function
        'End Class
    End Class
End Namespace
