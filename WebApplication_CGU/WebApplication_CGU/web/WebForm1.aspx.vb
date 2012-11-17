Imports System.Data.OleDb
Imports System.IO

Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        get_html5()

        If Request.Params("action") = "select" Then
            Response.AddHeader("Content-Type", "application/json")
            Response.Write(database_read(Request.Params("t")))
            Response.End()
        Else

        End If


    End Sub
    Protected Function get_html5()

        Dim radio_sex As String = Request.Params.Item("radio_sex")

        Dim Birthday_year As String = Request.Params.Item("Birthday_year")
        Dim Birthday_month As String = Request.Params.Item("Birthday_month")
        Dim Birthday_day As String = Request.Params.Item("Birthday_day")

        Dim Age As String = Request.Params.Item("Age")

        Dim KPS As String = Request.Params.Item("KPS")

        Dim Cancer_diagnosis As String = Request.Params.Item("Cancer_diagnosis")

        Dim Cancer_diagnosis_year As String = Request.Params.Item("Cancer_diagnosis_year")
        Dim Cancer_diagnosis_month As String = Request.Params.Item("Cancer_diagnosis_month")
        Dim Cancer_diagnosis_day As String = Request.Params.Item("Cancer_diagnosis_day")

        Dim Stage_of_disease As String = Request.Params.Item("Stage_of_disease")

        Dim radio_Pain As String = Request.Params.Item("radio_Pain")

        Dim radio_Pain_Hospitalized As String = Request.Params.Item("radio_Pain_Hospitalized")

        Dim radio_Anti_cancer_treatment As String = Request.Params.Item("radio_Anti_cancer_treatment")

        Dim radio_Anti_cancer_treatment_year As String = Request.Params.Item("radio_Anti_cancer_treatment_year")
        Dim radio_Anti_cancer_treatment_month As String = Request.Params.Item("radio_Anti_cancer_treatment_month")
        Dim radio_Anti_cancer_treatment_day As String = Request.Params.Item("radio_Anti_cancer_treatment_day")

        Dim Comorbid_conditions As String = Request.Params.Item("Comorbid_conditions")

        Dim Inpatient_unit As String = Request.Params.Item("Inpatient_unit")

        Dim Cause_of_hospitalization As String = Request.Params.Item("Cause_of_hospitalization")

        Dim Hospital_stay_year_Before As String = Request.Params.Item("Hospital_stay_year_Before")
        Dim Hospital_stay_month_Before As String = Request.Params.Item("Hospital_stay_month_Before")
        Dim Hospital_stay_day_Before As String = Request.Params.Item("Hospital_stay_day_Before")

        Dim Hospital_stay_year_After As String = Request.Params.Item("Hospital_stay_year_After")
        Dim Hospital_stay_month_After As String = Request.Params.Item("Hospital_stay_month_After")
        Dim Hospital_stay_day_After As String = Request.Params.Item("Hospital_stay_day_After")

        Dim Hospice_care_reasons As String = Request.Params.Item("Hospice_care_reasons")


        Dim Accept_hospice_time_year_Before As String = Request.Params.Item("Accept_hospice_time_year_Before")
        Dim Accept_hospice_time_month_Before As String = Request.Params.Item("Accept_hospice_time_month_Before")
        Dim Accept_hospice_time_day_Before As String = Request.Params.Item("Accept_hospice_time_day_Before")

        Dim Accept_hospice_time_year_After As String = Request.Params.Item("Accept_hospice_time_year_After")
        Dim Accept_hospice_time_month_After As String = Request.Params.Item("Accept_hospice_time_month_After")
        Dim Accept_hospice_time_day_After As String = Request.Params.Item("Accept_hospice_time_day_After")



        'Response.Write("check: " + radio_sex + Birthday_year + Birthday_month + Birthday_day & "<br />")

    End Function

    Protected Function database_read(ByVal table As String) As String

        Dim FileName As String = "codes.mdb"
        Dim DataSource As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\" & FileName
        Dim connDbStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataSource
        Dim conn As OleDbConnection = New OleDbConnection(connDbStr)
        conn.Open()

        'read
        Dim SQLCommand As String = "select * from " + table
        Dim da As OleDbDataAdapter = New OleDbDataAdapter(SQLCommand, conn)
        Dim ds As New DataSet
        da.Fill(ds, table)
        Dim c As Integer = ds.Tables(0).Rows.Count
        Dim resp(c) As String

        For i = 0 To c - 1
            resp(i) = ds.Tables(0).Rows(i).Item(1).ToString().Replace("""", "\""").Replace(vbNewLine, "\n")
        Next

        conn.Close()

        Return "{""id"": """ + table + """, ""option"":[""" + String.Join(""",""", resp) + """]}"
    End Function

    Protected Sub database_Write(ByVal table1 As String)

        Dim FileName As String = "codes.mdb"
        Dim DataSource As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\" & FileName
        Dim connDbStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataSource
        Dim conn As OleDbConnection = New OleDbConnection(connDbStr)
        conn.Open()

        'write

        Dim strInsert As String = " INSERT INTO " + table1 + "(" + "?,?,?,?,?" + ")" + "VALUES(" + "data" + ")"
        Dim inst As OleDbCommand = New OleDbCommand(strInsert, conn)
        inst.ExecuteNonQuery()

        conn.Close()

    End Sub

End Class