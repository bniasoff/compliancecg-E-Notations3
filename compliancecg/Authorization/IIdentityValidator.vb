
Imports System.Threading.Tasks

Namespace Microsoft.AspNet.Identity

    '/// <summary>
    '///     Used to validate an item
    '/// </summary>
    '/// <typeparam name="T"></typeparam>
    Public Interface IIdentityValidator(Of T)

        '/// <summary>
        '///     Validate the item
        '/// </summary>
        '/// <param name="item"></param>
        '/// <returns></returns>
        Function ValidateAsync(ByVal item As T) As Task(Of IdentityResult)
    End Interface
End Namespace