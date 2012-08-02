Imports System.IO
Public Class Form1
    Const failmessage = "You failed, please try the junction tool again"
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'GoTo doexe
        
        Dim first As String = ""
        Dim second As String = ""
        FolderBrowserDialog1.Description = "Please select the existing folder"
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            first = FolderBrowserDialog1.SelectedPath
            FolderBrowserDialog1.Reset()
            FolderBrowserDialog1.Description = "Please select the destination folder"
            If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                second = FolderBrowserDialog1.SelectedPath
                    If Directory.Exists(second) Then
                        Directory.Delete(second, True)
                    End If
                Else
                    MsgBox(failmessage)
                    Throw New Exception("Failure")
                End If
            Else
                MsgBox(failmessage)
                Throw New Exception("Failure")
            End If
            Dim junctionpath As String = ""
            OpenFileDialog1.Title = "Please locate the junction application"
            If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                junctionpath = OpenFileDialog1.FileName
            Else
                Throw New Exception("Failure")
            End If
doexe:
            Dim args As String = """" & second & """ """ & first & """"
            Dim pi As New ProcessStartInfo("cmd.exe", "/C junction.exe " & args) '(junctionpath, args)
            Dim output As String = ""
            Dim p As New Process
            pi.UseShellExecute = False
            pi.WorkingDirectory = junctionpath.Replace("junction.exe", "")
            pi.RedirectStandardOutput = True
            p.StartInfo = pi


            If p.Start() Then
                output = p.StandardOutput.ReadToEnd()
                p.WaitForExit()
                MsgBox(output)
            Else
                MsgBox("Failure Starting")
            End If
            Application.Exit()
        Catch ex As Exception
            MsgBox(ex.ToString())
            Application.Exit()
        End Try
    End Sub
End Class
