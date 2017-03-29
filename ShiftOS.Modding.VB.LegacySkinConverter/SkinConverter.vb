Imports System.Drawing
Imports ShiftOS.Engine

<Launcher("Legacy Skin Converter", True, "al_skin_loader", "Customization")>
<RequiresUpgrade("skinning")>
Public Class SkinConverter
    Implements IShiftOSWindow

    Private Property IShiftOSWindow_Location As Point Implements IShiftOSWindow.Location
        Get
            Return Me.Size
        End Get
        Set(value As Point)
            Me.Size = value
        End Set
    End Property

    Private Property IShiftOSWindow_Size As Size Implements IShiftOSWindow.Size
        Get
            Return Me.Size
        End Get
        Set(value As Size)
            Me.Size = value
        End Set
    End Property

    Private Sub btnin_Click(sender As Object, e As EventArgs) Handles btnin.Click
        FileSkimmerBackend.GetFile(New String() {".skn"}, FileOpenerStyle.Open, New Action(Of String)(Sub(path As String)
                                                                                                          txtin.Text = path
                                                                                                      End Sub))
    End Sub

    Private Sub btnout_Click(sender As Object, e As EventArgs) Handles btnout.Click
        FileSkimmerBackend.GetFile(New String() {".skn"}, FileOpenerStyle.Save, New Action(Of String)(Sub(path As String)
                                                                                                          txtout.Text = path
                                                                                                      End Sub))

    End Sub

    Private Sub btnconvert_Click(sender As Object, e As EventArgs) Handles btnconvert.Click
        If String.IsNullOrWhiteSpace(txtin.Text) Then
            Infobox.Show("No input", "Please select a legacy skin file as your input.")
            Return
        End If
        If String.IsNullOrWhiteSpace(txtout.Text) Then
            Infobox.Show("No output", "Please select an output file path to place the converted skin.")
            Return
        End If


        TerminalBackend.InvokeCommand("skinning.convert{in:""" + txtin.Text + """,out:""" + txtout.Text + """}")

    End Sub

    Public Sub OnLoad() Implements IShiftOSWindow.OnLoad

    End Sub

    Public Sub OnSkinLoad() Implements IShiftOSWindow.OnSkinLoad
    End Sub

    Public Function OnUnload() As Boolean Implements IShiftOSWindow.OnUnload
        Return True
    End Function

    Public Sub OnUpgrade() Implements IShiftOSWindow.OnUpgrade
    End Sub
End Class
