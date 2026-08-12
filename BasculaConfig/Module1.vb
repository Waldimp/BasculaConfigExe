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
End Module
