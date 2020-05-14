Imports System.Reflection
Imports System.Runtime.CompilerServices

Module PropertyHelper
    <Extension()>
    Function GetPrivatePropertyValue(Of T)(ByVal obj As Object, ByVal propName As String) As T
        If obj Is Nothing Then Throw New ArgumentNullException("obj")
        Dim pi As PropertyInfo = obj.[GetType]().GetProperty(propName, BindingFlags.[Public] Or BindingFlags.NonPublic Or BindingFlags.Instance)
        If pi Is Nothing Then Throw New ArgumentOutOfRangeException("propName", String.Format("Property {0} was not found in Type {1}", propName, obj.[GetType]().FullName))
        Return CType(pi.GetValue(obj, Nothing), T)
    End Function

    <Extension()>
    Function GetPrivateFieldValue(Of T)(ByVal obj As Object, ByVal propName As String) As T
        If obj Is Nothing Then Throw New ArgumentNullException("obj")
        Dim tp As Type = obj.[GetType]()
        Dim fi As FieldInfo = Nothing

        While fi Is Nothing AndAlso tp IsNot Nothing
            fi = tp.GetField(propName, BindingFlags.[Public] Or BindingFlags.NonPublic Or BindingFlags.Instance)
            tp = tp.BaseType
        End While

        If fi Is Nothing Then Throw New ArgumentOutOfRangeException("propName", String.Format("Field {0} was not found in Type {1}", propName, obj.[GetType]().FullName))
        Return CType(fi.GetValue(obj), T)
    End Function

    <Extension()>
    Sub SetPrivatePropertyValue(Of T)(ByVal obj As Object, ByVal propName As String, ByVal val As T)
        Dim tp As Type = obj.[GetType]()
        If tp.GetProperty(propName, BindingFlags.[Public] Or BindingFlags.NonPublic Or BindingFlags.Instance) Is Nothing Then Throw New ArgumentOutOfRangeException("propName", String.Format("Property {0} was not found in Type {1}", propName, obj.[GetType]().FullName))
        tp.InvokeMember(propName, BindingFlags.[Public] Or BindingFlags.NonPublic Or BindingFlags.SetProperty Or BindingFlags.Instance, Nothing, obj, New Object() {val})
    End Sub

    <Extension()>
    Sub SetPrivateFieldValue(Of T)(ByVal obj As Object, ByVal propName As String, ByVal val As T)
        If obj Is Nothing Then Throw New ArgumentNullException("obj")
        Dim tp As Type = obj.[GetType]()
        Dim fi As FieldInfo = Nothing

        While fi Is Nothing AndAlso tp IsNot Nothing
            fi = tp.GetField(propName, BindingFlags.[Public] Or BindingFlags.NonPublic Or BindingFlags.Instance)
            tp = tp.BaseType
        End While

        If fi Is Nothing Then Throw New ArgumentOutOfRangeException("propName", String.Format("Field {0} was not found in Type {1}", propName, obj.[GetType]().FullName))
        fi.SetValue(obj, val)
    End Sub
End Module
