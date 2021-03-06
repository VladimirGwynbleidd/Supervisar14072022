Public Class txtUserControl3
    Inherits System.Web.UI.UserControl

    Property isTextBoxLike As Boolean

    '--------------------------------------------------------------
    'Create Delegate to Handle Click event in Default page
    '--------------------------------------------------------------
    Public Delegate Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Event btnPostClk As btnDelete_Click
    Private _SessionID As String

    Private _text1 As String
    Public Property Text1() As String
        Get
            Return _text1
        End Get
        Set(ByVal value As String)
            _text1 = value
            TextBox1.Text = _text1
        End Set
    End Property

    Private _text2 As String
    Public Property Text2() As String
        Get
            Return _text2
        End Get
        Set(ByVal value As String)
            _text2 = value
            TextBox2.Text = _text2
        End Set
    End Property

    Private _isTextBoxRange As Boolean
    Public Property isTextBoxRange() As Boolean
        Get
            Return _isTextBoxRange
        End Get
        Set(ByVal value As Boolean)
            _isTextBoxRange = value
            If _isTextBoxRange Then
                TextBox2.Visible = True
                Label2.Visible = True
                Label1.Text = "Inicio"
                Label2.Text = "Fin"
            Else
                TextBox2.Visible = False
                Label2.Visible = False
            End If


        End Set
    End Property

    Private _dataValueType As Integer
    Public Property DataValueType() As Integer
        Get
            Return Convert.ToInt32(hdnType.Value)
        End Get
        Set(ByVal value As Integer)
            _dataValueType = value
            hdnType.Value = value.ToString()
        End Set
    End Property


    Private _typeOfControl As String
    Public Property TypeOfControl() As String
        Get
            Return _typeOfControl
        End Get
        Set(ByVal value As String)
            _typeOfControl = value
        End Set
    End Property

    Public Property labelText() As String
        Get
            Return Label1.Text
        End Get
        Set(ByVal value As String)
            Label1.Text = value
        End Set
    End Property

    Public Property SessionID() As String
        Get
            Return _SessionID
        End Get
        Set(ByVal value As String)
            _SessionID = value
        End Set
    End Property

    Private _dataValueField As String
    Public Property DataValueField() As String
        Get
            Return _dataValueField
        End Get
        Set(ByVal value As String)
            _dataValueField = value
        End Set
    End Property

    Private _isFixed As Boolean
    Public Property isFixed() As Boolean
        Get
            Return _isFixed
        End Get
        Set(ByVal value As Boolean)
            _isFixed = value
            btnDelete.Visible = Not _isFixed
        End Set
    End Property

    Private _maxLength As Integer
    Public Property maxLength() As Integer
        Get
            Return _maxLength
        End Get
        Set(ByVal value As Integer)
            _maxLength = value
            TextBox1.MaxLength = _maxLength
            TextBox2.MaxLength = _maxLength
        End Set
    End Property

    Public Sub FetchValues()
        _text1 = TextBox1.Text
        _text2 = TextBox2.Text
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        RaiseEvent btnPostClk(sender, e)
    End Sub

    Protected Sub txtBox_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBox1.TextChanged, TextBox2.TextChanged
        FetchValues()
    End Sub
End Class