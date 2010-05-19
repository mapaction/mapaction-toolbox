Imports System.IO

''' <summary>
''' Provides a framework for the implenmentation of the IDataListConnection interface.
''' </summary>
''' <remarks>
''' Provides a framework for the implenmentation of the IDataListConnection interface.
''' </remarks>
Public MustInherit Class AbstractDataListConnection
    Implements IDataListConnection

    Private m_blnRecuse As Boolean

    ''' <summary>
    ''' The Constructor.
    ''' </summary>
    ''' <param name="blnRecuse">The default Recusive behaviour, if there is a heirachircal 
    ''' structure to the underlying DataList. TRUE = the heirachy should be recused.
    ''' FALSE = the heirachy should not be recused.
    ''' </param>
    ''' <remarks>
    ''' The Constructor. 
    ''' 
    ''' The constructor is declared Protected to discourage implenmenters from
    ''' making their constructors public. Use the related Factory class instead.
    ''' </remarks>
    Protected Sub New(ByVal blnRecuse As Boolean)
        m_blnRecuse = blnRecuse
    End Sub

    ''' <summary>
    ''' The Constructor. Sets default Recusive behaviour to TRUE.
    ''' </summary>
    ''' <remarks>
    ''' The Constructor. Sets default Recusive behaviour to TRUE.
    ''' 
    ''' The constructor is declared Protected to discourage implenmenters from
    ''' making their constructors public. Use the related Factory class instead.
    ''' </remarks>
    Protected Sub New()
        m_blnRecuse = True
    End Sub

    Public MustOverride Function doesLayerExist(ByVal layerName As String) As Boolean Implements IDataListConnection.doesLayerExist

    Public MustOverride Function getDefaultDataNameClauseLookup() As IDataNameClauseLookup Implements IDataListConnection.getDefaultDataNameClauseLookup

    Public MustOverride Function getDetails() As String Implements IDataListConnection.getDetails

    Public MustOverride Function getpath() As FileInfo Implements IDataListConnection.getPath

    Public MustOverride Function isheirachical() As Boolean Implements IDataListConnection.isHeirachical

    Public MustOverride Function getDataListConnectionType() As dnListType Implements IDataListConnection.getDataListConnectionType

    Public MustOverride Function getDataListConnectionTypeDesc() As String Implements IDataListConnection.getDataListConnectionTypeDesc

    Public MustOverride Function getLayerNamesStrings() As List(Of String) Implements IDataListConnection.getLayerNamesStrings

    Public MustOverride Function getLayerDataNamesList(ByRef myDNCL As IDataNameClauseLookup) As List(Of IDataName) Implements IDataListConnection.getLayerDataNamesList

    Public Function getLayerDataNamesList() As List(Of IDataName) Implements IDataListConnection.getLayerDataNamesList
        Return getLayerDataNamesList(getDefaultDataNameClauseLookup())
    End Function

    Public Sub setRecuse(ByVal blnRecuse As Boolean) Implements IDataListConnection.setRecuse
        m_blnRecuse = blnRecuse
    End Sub

    Public Function getRecuse() As Boolean Implements IDataListConnection.getRecuse
        Return m_blnRecuse
    End Function

End Class
