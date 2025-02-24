﻿Imports System.Data.SqlClient

Public Class TB_Login
    Inherits Conexao

    Private Chave As String = "SEN"

    Public exito As Boolean

    Public Lusuario As List(Of String)
    Public LSenha As List(Of String)
    Public LId_Log As List(Of Integer)

    Public Usuario As String
    Public Senha As String
    Public Id_Log As Integer

    'Sub de Insert de dados na tabela
    Public Sub Insert(ByVal VstrSQL As String)
        Try
            Open()
            Comando = New SqlCommand(VstrSQL, ObjCon)
            Comando.ExecuteNonQuery()
            Me.exito = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.exito = False
        Finally
            Close()
        End Try
    End Sub

    'Sub de Select de dados na tabela
    Public Sub SelectValues(ByVal VstrSQL As String)
        Me.Id_Log = 0
        Me.Usuario = ""
        Me.Senha = ""


        Try
            Open()
            Comando = New SqlCommand(VstrSQL, ObjCon)
            Dr = Comando.ExecuteReader
            If Dr.Read() = True Then
                Me.Id_Log = Dr("ID_LOG")
                Me.Usuario = Dr("USUARIO_LOG")
                Me.Senha = Decrypt(Dr("SENHA_LOG"))
                Me.exito = True
            Else
                Me.Id_Log = 0
                Me.Usuario = ""
                Me.Senha = ""
                Me.exito = False
            End If

            ObjetosConexao.Id_log = Me.Id_Log
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.exito = False
        Finally
            Dr.Close()
            Close()
        End Try

    End Sub

    'Sub para desencriptar senha
    Private Function Decrypt(ByVal text As String) As String
        Dim Key As List(Of String) = New List(Of String)
        Dim Pwd As String = ""
        Dim Ascc As Integer

        For x = 1 To Len(Chave)
            Key.Add(Asc(Mid(Chave, x, 1)))
        Next x

        Dim j As Integer
        For i As Integer = 0 To text.Length - 1
            j = IIf(j + 1 >= Len(Chave), 1, j + 1)
            Ascc = Asc(text.Substring(i, 1)) - Key(j - 1)
            If Ascc < 0 Then Ascc = Ascc + 255
            Pwd = Pwd & Chr(Ascc)
        Next i
        Return Pwd
    End Function

End Class
