Imports System.IO

''' <summary>
''' Activa o desactiva que el lector de báscula se abra solo al encender la computadora.
'''
''' El configurador no puede apuntar al ejecutable del lector directamente: al instalarse
''' con ClickOnce, éste queda en una ruta de %LocalAppData%\Apps\2.0\ que cambia cada vez
''' que se vuelve a publicar. Lo que sí se mantiene estable es el acceso directo
''' .appref-ms que ClickOnce deja en el menú Inicio, así que ese es el que se copia a la
''' carpeta de Inicio de Windows.
'''
''' No requiere permisos de administrador, y el usuario puede desactivarlo también desde
''' el Administrador de tareas, en la pestaña Inicio.
''' </summary>
Module InicioLector

    ''' <summary>Nombre del producto del lector, tal como aparece en el menú Inicio.</summary>
    Private Const NombreDelLector As String = "BasculaExe"

    Private ReadOnly Property CarpetaDeInicio As String
        Get
            Return Environment.GetFolderPath(Environment.SpecialFolder.Startup)
        End Get
    End Property

    Private ReadOnly Property AccesoDirectoEnInicio As String
        Get
            Return Path.Combine(CarpetaDeInicio, NombreDelLector & ".appref-ms")
        End Get
    End Property

    ''' <summary>Indica si el lector ya está configurado para abrirse con Windows.</summary>
    Public Function EstaActivado() As Boolean
        Try
            Return File.Exists(AccesoDirectoEnInicio)
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Activa el arranque con Windows. Devuelve True si quedó activado, y en
    ''' <paramref name="mensaje"/> deja una explicación para mostrarle al usuario.
    ''' </summary>
    Public Function Activar(ByRef mensaje As String) As Boolean
        Try
            If EstaActivado() Then
                mensaje = "El lector ya se abre al encender la computadora."
                Return True
            End If

            Dim origen As String = BuscarAccesoDirectoDelLector()
            If String.IsNullOrEmpty(origen) Then
                mensaje = "No se encontró el lector de báscula instalado en esta computadora. " &
                          "Instálalo primero y vuelve a marcar esta casilla."
                Return False
            End If

            File.Copy(origen, AccesoDirectoEnInicio, True)
            mensaje = "Listo. El lector se abrirá al encender la computadora."
            Return True

        Catch ex As Exception
            mensaje = "No se pudo activar. Revisa que tengas permisos sobre la carpeta de Inicio."
            Return False
        End Try
    End Function

    ''' <summary>Desactiva el arranque con Windows quitando el acceso directo.</summary>
    Public Function Desactivar(ByRef mensaje As String) As Boolean
        Try
            If File.Exists(AccesoDirectoEnInicio) Then File.Delete(AccesoDirectoEnInicio)
            mensaje = "El lector ya no se abrirá solo. Tendrás que abrirlo a mano."
            Return True

        Catch ex As Exception
            mensaje = "No se pudo desactivar. Cierra otras ventanas y prueba de nuevo."
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Busca el acceso directo del lector en el menú Inicio.
    '''
    ''' Se busca por nombre de archivo en lugar de armar la ruta con una fórmula porque
    ''' Windows cambia el nombre de la carpeta del publicador; por ejemplo, con la
    ''' empresa "HP Inc." la carpeta real queda como "HP Inc", sin el punto final.
    ''' </summary>
    Private Function BuscarAccesoDirectoDelLector() As String
        Dim buscado As String = NombreDelLector & ".appref-ms"

        For Each raiz As Environment.SpecialFolder In New Environment.SpecialFolder() _
                {Environment.SpecialFolder.Programs, Environment.SpecialFolder.CommonPrograms}
            Try
                Dim carpeta As String = Environment.GetFolderPath(raiz)
                If String.IsNullOrEmpty(carpeta) OrElse Not Directory.Exists(carpeta) Then Continue For

                For Each encontrado As String In Directory.GetFiles(carpeta, "*.appref-ms", SearchOption.AllDirectories)
                    If String.Equals(Path.GetFileName(encontrado), buscado, StringComparison.OrdinalIgnoreCase) Then
                        Return encontrado
                    End If
                Next
            Catch
                ' Si una rama del menú Inicio no se puede leer, se sigue con la otra.
            End Try
        Next

        Return ""
    End Function

End Module
