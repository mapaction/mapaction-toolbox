Imports System.IO

Public Class LayerFileFinder

    Private m_dInfoSymbRoot As DirectoryInfo

    Sub New(ByRef dInfoSymbRoot As DirectoryInfo)
        m_dInfoSymbRoot = dInfoSymbRoot
    End Sub

    '<symbolsroot>\<datacategory>\<datatheme>\<source>_<datatype>.lyr
    '<symbolsroot>\<datacategory>\<datatheme>_<datatype>.lyr
    '<symbolsroot>\<datacategory>_<datatype>.lyr
    '
    'A background layer and/or label expression file will be searched for in the following order:
    '
    '<symbolsroot>\<datacategory>\<datatheme>\<source>_<datatype>_bg.lyr
    '<symbolsroot>\<datacategory>\<datatheme>_<datatype>_bg.lyr
    '<symbolsroot>\<datacategory>_<datatype>_bg.lyr
    '
    Private Function findLyrFile(ByVal strDataType As String, _
                                    ByVal strDataCat As String, _
                                    ByVal strDataTheme As String, _
                                    ByVal strSource As String, _
                                    ByVal blnGetBackgroundLyrs As Boolean) As FileInfo

        Dim lstStrTestPaths As New List(Of String)

        Dim strAryParams(8) As String
        Dim fInfoTest As FileInfo = Nothing
        Dim fInfoResult As FileInfo = Nothing

        If m_dInfoSymbRoot Is Nothing Then
            Throw New ArgumentException("Cannot search for layer files until the root directory has been saved")
        Else
            strAryParams(0) = m_dInfoSymbRoot.FullName
            strAryParams(1) = Path.DirectorySeparatorChar
            strAryParams(2) = ".lyr"
            If blnGetBackgroundLyrs Then
                strAryParams(3) = "_bg"
            Else
                strAryParams(3) = String.Empty
            End If

            strAryParams(4) = strDataType
            strAryParams(5) = strDataCat
            strAryParams(6) = strDataTheme

            If strSource Is Nothing Then
                strAryParams(7) = String.Empty
            Else
                strAryParams(7) = strSource
            End If

            'A background layer and/or label expression file will be searched for in the following order:
            '
            '<symbolsroot>\<datacategory>\<datatheme>\<source>_<datatype>_bg.lyr
            lstStrTestPaths.Add(String.Format("{0}{1}{5}{1}{6}{1}{7}_{4}{3}{2}", strAryParams))

            '<symbolsroot>\<datacategory>\<datatheme>_<datatype>_bg.lyr
            lstStrTestPaths.Add(String.Format("{0}{1}{5}{1}{6}_{4}{3}{2}", strAryParams))

            '<symbolsroot>\<datacategory>_<datatype>_bg.lyr
            lstStrTestPaths.Add(String.Format("{0}{1}{5}_{4}{3}{2}", strAryParams))

            For Each strFilePath In lstStrTestPaths
                'MsgBox("test path : " & strFilePath)

                If fInfoResult Is Nothing Then
                    fInfoTest = New FileInfo(strFilePath)

                    If fInfoTest.Exists() Then
                        fInfoResult = fInfoTest
                    End If
                End If
            Next

            ''
            If fInfoResult Is Nothing Then
                Throw New FileNotFoundException()
            End If
        End If

        Return fInfoResult
    End Function

    Private Function findLyrFile(ByVal strDataType As String, _
                                    ByVal strDataCat As String, _
                                    ByVal strDataTheme As String) As FileInfo
        Return findLyrFile(strDataType, strDataCat, strDataTheme, Nothing, False)
    End Function

    Private Function findLyrFile(ByVal strDataType As String, _
                                  ByVal strDataCat As String, _
                                  ByVal strDataTheme As String, _
                                  ByVal blnGetBackgroundLyrs As Boolean) As FileInfo
        Return findLyrFile(strDataType, strDataCat, strDataTheme, Nothing, blnGetBackgroundLyrs)
    End Function

    Private Function findLyrFile(ByVal strDataType As String, _
                                   ByVal strDataCat As String, _
                                   ByVal strDataTheme As String, _
                                   ByVal strSource As String) As FileInfo
        Return findLyrFile(strDataType, strDataCat, strDataTheme, strSource, False)
    End Function

End Class
