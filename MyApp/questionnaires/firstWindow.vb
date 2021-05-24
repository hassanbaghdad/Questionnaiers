Public Class firstWindow

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim screen_depart_ctrl As New screen_departs_ctrl
        Panel1.Controls.Add(screen_depart_ctrl)


    End Sub
End Class