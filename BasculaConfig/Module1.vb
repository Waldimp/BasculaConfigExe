Module Module1
    Public Continuo_Nombre_Puerto As String
    Public Continuo_Inicio As String
    Public Continuo_Recorrer As String
    Public Continuo_rutaUser As String

    Public Demanda_Nombre_Puerto As String
    Public Demanda_Inicio As String
    Public Demanda_Recorrer As String
    Public Demanda_rutaUser As String
    Public Demanda_caracter As String


    Public Activo As Boolean
    Public tiempo As Integer
    Public abrir As Boolean

    Public Modo As String

    ''' <summary>
    ''' Traduce una falla al abrir el puerto en un mensaje que el usuario entienda.
    '''
    ''' El caso más frecuente es que el lector de báscula esté corriendo y tenga el
    ''' puerto tomado: un puerto serial solo admite un programa a la vez, así que hay
    ''' que detener el lector antes de poder probar la conexión desde aquí.
    ''' </summary>
    Public Function MensajeDePuerto(ex As Exception, nombrePuerto As String) As String
        Dim puerto As String = If(String.IsNullOrEmpty(nombrePuerto), "el puerto", nombrePuerto)

        If TypeOf ex Is UnauthorizedAccessException Then
            Return "El puerto " & puerto & " está siendo usado por otro programa." & vbCrLf & vbCrLf &
                   "Si el lector de báscula está abierto, muéstralo desde el icono junto al reloj " &
                   "y presiona Detener. Después vuelve a intentar aquí."
        End If

        If TypeOf ex Is IO.IOException Then
            Return "No se encuentra el puerto " & puerto & "." & vbCrLf & vbCrLf &
                   "Revisa que el cable esté conectado y que la báscula esté encendida."
        End If

        If TypeOf ex Is ArgumentException Then
            Return "El puerto " & puerto & " ya no existe en esta computadora." & vbCrLf & vbCrLf &
                   "Presiona Determinar puertos para volver a buscarlos."
        End If

        Return "No se pudo abrir el puerto " & puerto & "." & vbCrLf & vbCrLf & ex.Message
    End Function
    ''' <summary>
    ''' Devuelve los puertos seriales que reporta Windows, sin duplicados y ordenados
    ''' por número.
    '''
    ''' El orden importa: GetPortNames los entrega como estén en el registro, así que
    ''' COM10 puede aparecer antes que COM2 y la lista queda confusa.
    ''' </summary>
    Public Function PuertosDelSistema() As List(Of String)
        Dim puertos As New List(Of String)

        Try
            For Each nombre As String In IO.Ports.SerialPort.GetPortNames()
                Dim limpio As String = If(nombre Is Nothing, "", nombre.Trim())
                If limpio <> "" AndAlso Not puertos.Contains(limpio, StringComparer.OrdinalIgnoreCase) Then
                    puertos.Add(limpio)
                End If
            Next
        Catch ex As Exception
            ' Si Windows no puede entregar la lista, se devuelve vacía en lugar de fallar.
        End Try

        puertos.Sort(Function(uno As String, otro As String)
                         Dim a As Integer = NumeroDePuerto(uno)
                         Dim b As Integer = NumeroDePuerto(otro)
                         If a <> b Then Return a.CompareTo(b)
                         Return String.Compare(uno, otro, StringComparison.OrdinalIgnoreCase)
                     End Function)

        Return puertos
    End Function

    Private Function NumeroDePuerto(nombre As String) As Integer
        Dim digitos As New Text.StringBuilder()
        For Each caracter As Char In nombre
            If Char.IsDigit(caracter) Then digitos.Append(caracter)
        Next

        Dim numero As Integer
        If Integer.TryParse(digitos.ToString(), numero) Then Return numero
        Return Integer.MaxValue
    End Function
End Module
