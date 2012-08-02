Imports System.IO
Public Class Form1
    Const failmessage = "You failed, please try the junction tool again"
    Dim first As String = ""
    Dim second As String = ""
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        FolderBrowserDialog1.Description = "Please select the existing folder"
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            first = FolderBrowserDialog1.SelectedPath
            Button2.Enabled = True
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            second = FolderBrowserDialog1.SelectedPath
            Button3.Enabled = True
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        Dim junctionpath As String = ""
        OpenFileDialog1.Title = "Please locate the junction application"
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            junctionpath = OpenFileDialog1.FileName
            Dim args As String = """" & second & """ """ & first & """"
            Dim pi As New ProcessStartInfo("cmd.exe", "/C junction.exe " & args) '(junctionpath, args)
            Dim output As String = ""
            Dim p As New Process
            pi.UseShellExecute = False
            pi.WorkingDirectory = junctionpath.Replace("junction.exe", "")
            pi.RedirectStandardOutput = True
            p.StartInfo = pi
            If Directory.Exists(second) Then
                Directory.Delete(second, True)
            End If
            If p.Start() Then
                output = p.StandardOutput.ReadToEnd()
                p.WaitForExit()
                MsgBox(output)
            Else
                MsgBox("Failure Starting")
            End If
            
        End If
        
    End Sub
End Class
