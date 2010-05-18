''' <summary>
''' An Exception indicating at an error occured whilst attempting to rename
''' the underlying data store.
''' </summary>
''' <remarks>
''' An Exception indicating at an error occured whilst attempting to rename
''' the underlying data store.
''' 
''' Provides a convenence function to get hold of a reference to the offending 
''' IDataname object.
''' </remarks>
Public Class RenamingDataException
    Inherits Exception

    Private m_dnOffendingName As IDataName

    Protected Friend Sub New(ByVal strMessage As String, ByRef dnOffendingName As IDataName)
        MyBase.New(strMessage)
        m_dnOffendingName = dnOffendingName
    End Sub

    Protected Friend Sub New(ByRef dnOffendingName As IDataName)
        'todo add a constant with a meaningful default string here
        'eg. "Unable to rename Feature Class: " & myNameStr
        'MyBase.New(strMessage)
        m_dnOffendingName = dnOffendingName
    End Sub

    ''' <summary>
    ''' Returns the IDataName which could not be renamed.
    ''' </summary>
    ''' <returns>The IDataName which could not be renamed.</returns>
    ''' <remarks>
    ''' Returns the IDataName which could not be renamed
    ''' </remarks>
    Public Function getDataName() As IDataName
        Return m_dnOffendingName
    End Function

End Class
