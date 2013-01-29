''' <summary>
''' Provides a framework for the implenmentation of the IDataListConnection interface.
''' </summary>
''' <remarks>
''' Provides a framework for the implenmentation of the IDataListConnection interface.
''' </remarks>
Public MustInherit Class AbstractGeoDataListConnection
    Implements IGeoDataListConnection

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

    Public MustOverride Function doesLayerExist(ByVal layerName As String) As Boolean Implements IGeoDataListConnection.doesLayerExist

    Public MustOverride Function getDefaultDataNameClauseLookup() As IDataNameClauseLookup Implements IGeoDataListConnection.getDefaultDataNameClauseLookup

    Public MustOverride Function getDetails() As String Implements IGeoDataListConnection.getDetails

    Public MustOverride Function getGeoDataListConnectionType() As Integer Implements IGeoDataListConnection.getGeoDataListConnectionType

    Public MustOverride Function getGeoDataListConnectionTypeDesc() As String Implements IGeoDataListConnection.getGeoDataListConnectionTypeDesc

    Public MustOverride Function getLayerNamesStrings() As List(Of String) Implements IGeoDataListConnection.getLayerNamesStrings

    Public MustOverride Function getLayerDataNamesList(ByRef myDNCL As IDataNameClauseLookup) As List(Of IDataName) Implements IGeoDataListConnection.getLayerDataNamesList

    Public Function getLayerDataNamesList() As List(Of IDataName) Implements IGeoDataListConnection.getLayerDataNamesList
        Return getLayerDataNamesList(getDefaultDataNameClauseLookup())
    End Function

    Public Sub setRecuse(ByVal blnRecuse As Boolean) Implements IGeoDataListConnection.setRecuse
        m_blnRecuse = blnRecuse
    End Sub

    Public Function getRecuse() As Boolean Implements IGeoDataListConnection.getRecuse
        Return m_blnRecuse
    End Function

End Class
