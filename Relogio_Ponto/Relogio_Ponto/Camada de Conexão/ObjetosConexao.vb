﻿Imports System.Data.SqlClient
Module ObjetosConexao
    Public ObjCon As New SqlConnection
    Public Dr As SqlDataReader
    Public Da As SqlDataAdapter
    Public Comando As New SqlCommand

    Public Id_log As Integer
End Module
