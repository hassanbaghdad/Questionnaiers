

Imports System
Imports System.Data
Imports System.String
Imports System.Data.DataTable
Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.Win32
Imports System.Runtime.InteropServices


Public Class MainForm
    Dim regKey As RegistryKey
    Dim sql As String
    Dim ds As New DataSet
    Dim da As SqlDataAdapter
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand
    Dim mydatatable As DataTable
    Private FixHeight As Integer   '= 1024 'default height
    Private FixWidth As Integer    '= 768 'default width


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim f As New dash
        f.TopLevel = False
        f.Dock = DockStyle.Fill
        Me.Panel2.Controls.Clear()
        Me.Panel2.Controls.Add(f)
        f.Show()
       
    End Sub

    Private Sub StressTestingButton_Click(sender As Object, e As EventArgs)
        If StressTestingPanel.Visible = False Then
            StressTestingPanel.Visible = True
            RiskScopePanel.Visible = False
        Else
            RiskScopePanel.Visible = False
            StressTestingPanel.Visible = False
        End If
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub




    Public Sub ChangeColorsub(sender)

        Dim btn As Button = CType(sender, Button)

        For Each c As Button In StressTestingPanel.Controls.OfType(Of Button)()

            If btn.Name <> c.Name Then
                c.BackColor = Color.FromArgb(35, 32, 39)
               
            Else
                c.BackColor = Color.Gray

            End If
        Next
        For Each c As Button In RiskScopePanel.Controls.OfType(Of Button)()

            If btn.Name <> c.Name Then

                c.BackColor = Color.FromArgb(35, 32, 39)

            Else
                c.BackColor = Color.Gray

            End If
        Next
    End Sub
    Public Sub ChangeColorI(sender)

        Dim btn As Button = CType(sender, Button)

        For Each c As Button In Panel1.Controls.OfType(Of Button)()

            If btn.Name <> c.Name Then
                If btn.Name <> "Button12" Then
                    c.ForeColor = Color.White
                    c.BackColor = Color.Black
                End If
            Else
                c.BackColor = Color.RoyalBlue
                c.ForeColor = Color.White
            End If
        Next

    End Sub
  

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Me.Close()
    End Sub


    Private Sub RiskScopePanel_Paint(sender As Object, e As PaintEventArgs) Handles RiskScopePanel.Paint

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        ChangeColorsub(sender)


    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        ChangeColorsub(sender)
        ChangeColorsub(sender)
        Dim f As New ReporTRisk
        f.TopLevel = False
        f.Dock = DockStyle.Fill
        Me.Panel2.Controls.Clear()
        Me.Panel2.Controls.Add(f)
        f.Show()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        ChangeColorsub(sender)





    End Sub

  

    Private Sub Button9_Click(sender As Object, e As EventArgs)
        ChangeColorsub(sender)


    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs)
        ChangeColorsub(sender)




    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs)
        ChangeColorsub(sender)

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs)




    End Sub

   

    Private Sub Button8_Click(sender As Object, e As EventArgs)
        ChangeColorsub(sender)


    End Sub

    Private Sub CheckUser()


    End Sub
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
 
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
    Private Sub frmProgramma_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If MessageBox.Show("Do you want to close the application?", "close", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            End

        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button15_Click_1(sender As Object, e As EventArgs)

    End Sub

 



    Private Sub Button20_Click(sender As Object, e As EventArgs)
        ChangeColorsub(sender)


    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs)
        ChangeColorsub(sender)

    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        ChangeColorsub(sender)
        Dim f As New ReporTRisk
        f.TopLevel = False
        f.Dock = DockStyle.Fill
        Me.Panel2.Controls.Clear()
        Me.Panel2.Controls.Add(f)
        f.Show()
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        ChangeColorsub(sender)
        Dim f As New Questionnaire
        f.TopLevel = False
        f.Dock = DockStyle.Fill
        Me.Panel2.Controls.Clear()
        Me.Panel2.Controls.Add(f)
        f.Show()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        ChangeColorsub(sender)
        Dim f As New dash
        f.TopLevel = False
        f.Dock = DockStyle.Fill
        Me.Panel2.Controls.Clear()
        Me.Panel2.Controls.Add(f)
        f.Show()
    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click
        ChangeColorsub(sender)
        Dim f As New users
        f.TopLevel = False
        f.Dock = DockStyle.Fill
        Me.Panel2.Controls.Clear()
        Me.Panel2.Controls.Add(f)
        f.Show()
    End Sub
End Class
