﻿'' CDMA DEV TERM
'' Copyright (c) Dillon Graham 2010-2012 Chromableed Studios
'' www.chromableedstudios.com
'' chromableedstudios ( a t ) gmail ( d o t ) com
''     
'' cdmadevterm by ¿k? with help from ajh and jh
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
Imports cdmaDevLib.NvItems.NvItems
Imports cdmaDevLib.Qcdm.Cmd
Public Class CommandFactory

    Public Shared Function GetCommand(nv As NvItems.NvItems) As ICommand
        Return GetCommand(nv, False, New Byte() {})
    End Function
    Public Shared Function GetCommand(nv As NvItems.NvItems, write As Boolean, data() As Byte) As ICommand
        Dim cmd As ICommand
        Dim qc As Qcdm.Cmd = Qcdm.Cmd.DIAG_NV_READ_F
        If write Then
            qc = Qcdm.Cmd.DIAG_NV_WRITE_F
        End If
        Select Case nv
            Case NV_DIR_NUMBER_I
                cmd = New Cmd_NV_DIR_NUMBER_I(qc, _
                                                nv, _
                                                data, _
                                                qc.ToString() & " " & nv.ToString())
            Case NV_MIN1_I
                cmd = New Cmd_NV_MIN1_I(qc, _
                                        nv, _
                                        data, _
                                        qc.ToString() & " " & nv.ToString())
            Case NV_MIN2_I
                cmd = New Cmd_NV_MIN2_I(qc, _
                                        nv, _
                                        data, _
                                        qc.ToString() & " " & nv.ToString())
            Case NV_MEID_I
                cmd = New Cmd_NV_MEID_I(qc, _
                                        nv, _
                                        data, _
                                        qc.ToString() & " " & nv.ToString())
            Case NV_HOME_SID_NID_I
                cmd = New Cmd_NV_HOME_SID_NID_I(qc, _
                                        nv, _
                                        data, _
                                        qc.ToString() & " " & nv.ToString())
            Case NV_LOCK_CODE_I
                cmd = New Cmd_NV_LOCK_CODE_I(qc, _
                                        nv, _
                                        data, _
                                        qc.ToString() & " " & nv.ToString())
            Case NV_SEC_CODE_I
                cmd = New Cmd_NV_SEC_CODE_I(qc, _
                                        nv, _
                                        data, _
                                        qc.ToString() & " " & nv.ToString())
            Case NV_NAM_LOCK_I
                cmd = New Cmd_NV_NAM_LOCK_I(qc, _
                                        nv, _
                                        data, _
                                        qc.ToString() & " " & nv.ToString())
            Case NV_DS_MIP_NUM_PROF_I
                cmd = New Cmd_NV_DS_MIP_NUM_PROF_I(qc, _
                                        nv, _
                                        data, _
                                        qc.ToString() & " " & nv.ToString())
            Case NV_DS_MIP_ENABLE_PROF_I
                cmd = New Cmd_NV_DS_MIP_ENABLE_PROF_I(qc, _
                                        nv, _
                                        data, _
                                        qc.ToString() & " " & nv.ToString())
            Case NV_DS_QCMIP_I
                cmd = New Cmd_NV_DS_QCMIP_I(qc, _
                                        nv, _
                                        data, _
                                        qc.ToString() & " " & nv.ToString())
            Case Else
                cmd = New Cmd_nv(qc, _
                                  nv, _
                                  data, _
                                  qc.ToString() & " " & nv.ToString())
        End Select

        Return cmd
    End Function
    Public Shared Function GetCommand(qc As Qcdm.Cmd) As ICommand
        Dim cmd As ICommand

        Select Case qc
            Case DIAG_ESN_F
                cmd = New Cmd_DIAG_ESN_F(qc, New Byte() {}, qc.ToString)

            Case Else
                cmd = New QcCommand(qc)
        End Select

        Return cmd
    End Function

    Shared Function GetCommand(qc As Qcdm.Cmd, data As Byte()) As ICommand
        Dim cmd As ICommand
        Select Case qc
            Case DIAG_SPC_F
                cmd = New Cmd_DIAG_SPC_F(qc, data, qc.ToString)
            Case Else
                cmd = New Command(qc, data, qc.ToString)
        End Select

        Return cmd
    End Function
    ''TODO: untested entirely
    Shared Function GetCommand(hexStr As String) As ICommand
        Dim cmd As ICommand
        Dim bytes As Byte() = hexStr.ToHexBytes()

        Try
            Dim qc As Qcdm.Cmd = CType(bytes(0), Qcdm.Cmd)
            If qc = DIAG_NV_READ_F Then

                Dim numS As String = bytes(2).ToString("X2") + bytes(1).ToString("X2")
                Dim num = Integer.Parse(numS)
                Dim nv As Qcdm.Cmd = CType(num, NvItems.NvItems)
                cmd = CommandFactory.GetCommand(nv)
            ElseIf qc = DIAG_NV_WRITE_F Then
                Dim numS As String = bytes(2).ToString("X2") + bytes(1).ToString("X2")
                Dim num = Integer.Parse(numS)
                Dim nv As Qcdm.Cmd = CType(num, NvItems.NvItems)
                cmd = CommandFactory.GetCommand(nv, True, hexStr.Substring(6).ToHexBytes())
            Else
                cmd = New Command(hexStr.ToHexBytes, "Raw bytes")
            End If

        Catch ex As Exception
            cmd = New Command(hexStr.Replace(" ", String.Empty).ToHexBytes(), "Raw bytes")

        End Try

        Return cmd
    End Function
End Class
