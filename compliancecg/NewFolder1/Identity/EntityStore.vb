Imports System.Data.Entity
Imports System.Linq
Imports System.Threading.Tasks

Namespace Microsoft.AspNet.Identity.EntityFramework
    Friend Class EntityStore(Of TEntity As Class)
        Public Sub New(ByVal context As DbContext)
            context = context
            DbEntitySet = context.[Set](Of TEntity)()
        End Sub

        Public Property Context As DbContext

        Public ReadOnly Property EntitySet As IQueryable(Of TEntity)
            Get
                Return DbEntitySet
            End Get
        End Property

        Public Property DbEntitySet As DbSet(Of TEntity)

        Public Overridable Function GetByIdAsync(ByVal id As Object) As Task(Of TEntity)
            Return DbEntitySet.FindAsync(id)
        End Function

        Public Sub Create(ByVal entity As TEntity)
            DbEntitySet.Add(entity)
        End Sub

        Public Sub Delete(ByVal entity As TEntity)
            DbEntitySet.Remove(entity)
        End Sub

        Public Overridable Sub Update(ByVal entity As TEntity)
            If entity IsNot Nothing Then
                Context.Entry(entity).State = EntityState.Modified
            End If
        End Sub
    End Class
End Namespace
