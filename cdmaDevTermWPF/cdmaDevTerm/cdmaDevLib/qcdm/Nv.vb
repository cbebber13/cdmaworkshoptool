﻿'' cdmaDevTerm
'' Copyright (c) Dillon Graham 2010-2013 Chromableed Studios
'' www.chromableedstudios.com
''     
'' cdmadevterm by ¿k? with help from ajh and jh and many others
''
'' this was originally developed as a test framework, before many 
'' things about qcdm(and programming) were understood by the author
'' please forgive some code that should never have seen the light of day ;)
''
''-------------------------------------------------------------------------------------------------------------
'' CDMA DEV TERM is released AS-IS without any warranty of any thing, blah blah blah, under the GPL v3 licence
'' check out the GPL v3 for details
'' http://www.gnu.org/copyleft/gpl.html
''-------------------------------------------------------------------------------------------------------------

''this class is mostly used for display purpose
''check out cmd_nv for the 'actual' nv class
Public Class Nv
    Property DisplayData As String
    Property Data As String
    Property DataHex As String
    Property Item As String

    Sub New(_nvItem As NvItems.NvItems, _Data As String, _DataHex As String)
        Data = _Data
        DisplayData = _Data
        DataHex = _DataHex
        Item = _nvItem.ToString
    End Sub
End Class
