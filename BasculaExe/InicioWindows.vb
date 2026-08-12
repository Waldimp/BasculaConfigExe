Imports System.IO
Imports System.Deployment.Application

''' <summary>
''' Activa o desactiva que el lector se abra solo al encender la computadora, colocando
''' un acceso directo en la carpeta de Inicio del usuario.
'''
''' Detalle importante con ClickOnce: el ejecutable instalado vive en una ruta de
''' %LocalAppData%\Apps\2.0\ que cambia cada vez que se vuelve a publicar la aplicación.
''' Apuntar el arranque a esa ruta se rompería en la primera actualización. Por eso se
''' copia el acceso directo .appref-ms que genera ClickOnce en el menú Inicio, que sí
''' sobrevive a las republicaciones porque siempre resuelve a la versión instalada.
'''
''' No requiere permisos de administrador y el usuario puede desactivarlo desde el
''' Administrador de tareas, pestaña Inicio.
''' </summary>
Module InicioWindows

    ''' <summary>Carpeta de Inicio del usuario actual.</summary>
    Public ReadOnly Property CarpetaDeInicio As String
        Get
            Return Environment.GetFolderPath(Environment.SpecialFolder.Startup)
        End Get
    End Property

    Private ReadOnly Property NombreDelProducto As String
        Get
            Dim nombre As String = Application.ProductName
            If String.IsNullOrEmpty(nombre) Then nombre = "BasculaExe"
            Return nombre
        End Get
    End Property

    ''' <summary>Ruta del acceso directo que esta aplicación coloca en la carpeta de Inicio.</summary>
    Private ReadOnly Property AccesoDirectoEnInicio As String
        Get
            Dim extension As String = If(EstaInstaladoConClickOnce(), ".appref-ms", ".lnk")
            Return Path.Combine(CarpetaDeInicio, NombreDelProducto & extension)
        End Get
    End Property

    Public Function EstaInstaladoConClickOnce() As Boolean
        Try
            Return ApplicationDeployment.IsNetworkDeployed
        Catch
            Return False
        End Try
    End Function

    ''' <summary>Indica si el lector está configurado para abrirse al encender la computadora.</summary>
    Public Function EstaActivado() As Boolean
        Try
            Return File.Exists(AccesoDirectoEnInicio)
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Activa el arranque con Windows. Devuelve True si quedó activado; si algo falla,
    ''' <paramref name="motivo"/> explica en lenguaje natural qué pasó.
    ''' </summary>
    Public Function Activar(ByRef motivo As String) As Boolean
        motivo = ""
        Try
            If EstaActivado() Then Return True

            If EstaInstaladoConClickOnce() Then
                Dim origen As String = BuscarAccesoDirectoDeClickOnce()
                If String.IsNullOrEmpty(origen) Then
                    motivo = "No se encontró el acceso directo de la aplicación en el menú Inicio. " &
                             "Vuelve a instalarla y prueba otra vez."
                    Registro.Advertencia("No se localizó el .appref-ms de " & NombreDelProducto)
                    Return False
                End If

                File.Copy(origen, AccesoDirectoEnInicio, True)
                Registro.Informacion("Arranque con Windows activado (copiado desde " & origen & ")")
            Else
                CrearAccesoDirectoAlEjecutable(AccesoDirectoEnInicio)
                Registro.Informacion("Arranque con Windows activado (acceso directo al ejecutable)")
            End If

            Return EstaActivado()

        Catch ex As Exception
            motivo = "No se pudo activar el arranque automático. Revisa que tengas permisos sobre la carpeta de Inicio."
            Registro.Error_("Fallo al activar el arranque con Windows", ex)
            Return False
        End Try
    End Function

    ''' <summary>Desactiva el arranque con Windows quitando el acceso directo.</summary>
    Public Function Desactivar(ByRef motivo As String) As Boolean
        motivo = ""
        Try
            If File.Exists(AccesoDirectoEnInicio) Then
                File.Delete(AccesoDirectoEnInicio)
                Registro.Informacion("Arranque con Windows desactivado")
            End If
            Return True

        Catch ex As Exception
            motivo = "No se pudo desactivar el arranque automático. Cierra otras ventanas y prueba de nuevo."
            Registro.Error_("Fallo al desactivar el arranque con Windows", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Busca el acceso directo .appref-ms que ClickOnce dejó en el menú Inicio.
    '''
    ''' Se busca por nombre en lugar de armar la ruta con una fórmula porque Windows
    ''' modifica el nombre de la carpeta del publicador: con AssemblyCompany "HP Inc."
    ''' la carpeta real queda como "HP Inc", sin el punto final.
    ''' </summary>
    Private Function BuscarAccesoDirectoDeClickOnce() As String
        Dim nombreBuscado As String = NombreDelProducto & ".appref-ms"

        For Each raiz As Environment.SpecialFolder In New Environment.SpecialFolder() _
                {Environment.SpecialFolder.Programs, Environment.SpecialFolder.CommonPrograms}
            Try
                Dim carpeta As String = Environment.GetFolderPath(raiz)
                If String.IsNullOrEmpty(carpeta) OrElse Not Directory.Exists(carpeta) Then Continue For

                For Each encontrado As String In Directory.GetFiles(carpeta, "*.appref-ms", SearchOption.AllDirectories)
                    If String.Equals(Path.GetFileName(encontrado), nombreBuscado, StringComparison.OrdinalIgnoreCase) Then
                        Return encontrado
                    End If
                Next
            Catch ex As Exception
                Registro.Advertencia("No se pudo revisar el menú Inicio: " & ex.Message)
            End Try
        Next

        Return ""
    End Function

    ''' <summary>
    ''' Crea un acceso directo tradicional al ejecutable. Se usa cuando la aplicación no
    ''' está instalada con ClickOnce, por ejemplo al ejecutarla desde Visual Studio.
    ''' </summary>
    Private Sub CrearAccesoDirectoAlEjecutable(rutaDestino As String)
        Dim shell As Object = CreateObject("WScript.Shell")
        Dim acceso As Object = shell.CreateShortcut(rutaDestino)
        acceso.TargetPath = Application.ExecutablePath
        acceso.WorkingDirectory = Path.GetDirectoryName(Application.ExecutablePath)
        acceso.Description = "Lector de báscula"
        acceso.Save()
    End Sub

End Module
