Imports System.IO

''' <summary>
''' Pantalla donde se define cómo trabaja el lector de peso: si se abre al encender la
''' computadora, si empieza a leer solo, y cada cuántos segundos toma una lectura.
'''
''' Igual que el resto del configurador, cada cambio se guarda al momento en su archivo
''' de C:\config, que es de donde el lector toma la configuración.
''' </summary>
Public Class AutomatizacionControl

    Private Const CarpetaConfiguracion As String = "C:\config"
    Private Const IntervaloPorDefecto As Integer = 5

    ''' <summary>
    ''' Evita guardar mientras se están repoblando los controles al abrir la pantalla.
    '''
    ''' Nace en True a propósito. Los valores que el diseñador asigna a la casilla, al
    ''' botón de opción y al intervalo se establecen dentro de InitializeComponent, que
    ''' corre antes de Load, y ahí los manejadores ya están enganchados. Si la bandera
    ''' empezara en False, con solo abrir la pantalla se dispararían los eventos y se
    ''' guardaría configuración que el usuario nunca tocó: llegó a reactivar el arranque
    ''' con Windows aunque lo hubieran desmarcado antes.
    ''' </summary>
    Private _cargando As Boolean = True

    Private Sub AutomatizacionControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _cargando = True

        Try
            If Not Directory.Exists(CarpetaConfiguracion) Then MkDir(CarpetaConfiguracion)

            ' De fábrica el lector empieza a leer apenas se abre; solo queda esperando al
            ' operativo si se eligió el modo manual expresamente.
            Dim arranque As String = LeerTexto("Arranque.txt")
            If String.IsNullOrEmpty(arranque) Then
                rdbEmpezarSolo.Checked = True
                Guardar("Arranque.txt", "Automatico")
            ElseIf String.Equals(arranque, "Manual", StringComparison.OrdinalIgnoreCase) Then
                rdbEsperarOperativo.Checked = True
            Else
                rdbEmpezarSolo.Checked = True
            End If

            Dim intervalo As Integer
            If Not Integer.TryParse(LeerTexto("Intervalo.txt"), intervalo) Then intervalo = IntervaloPorDefecto
            If intervalo < nudIntervalo.Minimum Then intervalo = CInt(nudIntervalo.Minimum)
            If intervalo > nudIntervalo.Maximum Then intervalo = CInt(nudIntervalo.Maximum)
            nudIntervalo.Value = intervalo

            AplicarValorPorDefectoDeArranque()
            chkInicioWindows.Checked = InicioLector.EstaActivado()

        Catch ex As Exception
            MostrarResultado("No se pudo leer la configuración guardada.", True)
        Finally
            _cargando = False
            ' El mensaje solo debe aparecer cuando el usuario cambia algo, no al abrir.
            lblResultado.Text = ""
        End Try
    End Sub

    ''' <summary>
    ''' De fábrica, el lector se abre al encender la computadora. La primera vez que se
    ''' entra a esta pantalla se deja aplicado, y a partir de ahí manda lo que el usuario
    ''' haya elegido: si lo desmarcó a propósito, no se vuelve a activar solo.
    ''' </summary>
    Private Sub AplicarValorPorDefectoDeArranque()
        ' El intervalo se siembra aparte: puede faltar aunque el arranque ya esté
        ' definido, por ejemplo en una instalación hecha con la versión anterior.
        If Not File.Exists(Path.Combine(CarpetaConfiguracion, "Intervalo.txt")) Then
            Guardar("Intervalo.txt", IntervaloPorDefecto.ToString())
        End If

        If File.Exists(Path.Combine(CarpetaConfiguracion, "InicioWindows.txt")) Then Return

        Dim mensaje As String = ""
        InicioLector.Activar(mensaje)
        Guardar("InicioWindows.txt", "Si")
    End Sub

    Private Sub rdbEmpezarSolo_CheckedChanged(sender As Object, e As EventArgs) _
        Handles rdbEmpezarSolo.CheckedChanged, rdbEsperarOperativo.CheckedChanged

        If _cargando Then Return
        ' Los dos botones comparten manejador: solo se guarda cuando uno queda marcado,
        ' para no escribir dos veces por cada cambio.
        Dim boton As RadioButton = TryCast(sender, RadioButton)
        If boton Is Nothing OrElse Not boton.Checked Then Return

        If Guardar("Arranque.txt", If(rdbEmpezarSolo.Checked, "Automatico", "Manual")) Then
            If rdbEmpezarSolo.Checked Then
                MostrarResultado("El lector empezará a leer apenas se abra.", False)
            Else
                MostrarResultado("El lector esperará a que el operativo presione Iniciar.", False)
            End If
        End If
    End Sub

    Private Sub nudIntervalo_ValueChanged(sender As Object, e As EventArgs) Handles nudIntervalo.ValueChanged
        If _cargando Then Return

        Dim segundos As Integer = CInt(nudIntervalo.Value)
        If Guardar("Intervalo.txt", segundos.ToString()) Then
            MostrarResultado("Se tomará una lectura cada " & segundos & " segundos.", False)
        End If
    End Sub

    Private Sub chkInicioWindows_CheckedChanged(sender As Object, e As EventArgs) Handles chkInicioWindows.CheckedChanged
        If _cargando Then Return

        Dim mensaje As String = ""
        Dim correcto As Boolean

        If chkInicioWindows.Checked Then
            correcto = InicioLector.Activar(mensaje)
        Else
            correcto = InicioLector.Desactivar(mensaje)
        End If

        MostrarResultado(mensaje, Not correcto)

        If Not correcto Then
            ' Si no se pudo aplicar, la casilla vuelve a reflejar la realidad en lugar
            ' de dejar al usuario creyendo que quedó configurado.
            _cargando = True
            chkInicioWindows.Checked = InicioLector.EstaActivado()
            _cargando = False
        End If
    End Sub

    Private Function Guardar(nombreArchivo As String, contenido As String) As Boolean
        Try
            If Not Directory.Exists(CarpetaConfiguracion) Then MkDir(CarpetaConfiguracion)
            File.WriteAllText(Path.Combine(CarpetaConfiguracion, nombreArchivo),
                              contenido & vbCrLf, System.Text.Encoding.ASCII)
            Return True

        Catch ex As Exception
            MostrarResultado("No se pudo guardar la configuración en " & CarpetaConfiguracion & ".", True)
            Return False
        End Try
    End Function

    Private Function LeerTexto(nombreArchivo As String) As String
        Dim ruta As String = Path.Combine(CarpetaConfiguracion, nombreArchivo)
        Try
            If Not File.Exists(ruta) Then Return ""
            Return File.ReadAllText(ruta).Replace(vbCrLf, "").Replace(vbLf, "").Trim()
        Catch
            Return ""
        End Try
    End Function

    Private Sub MostrarResultado(mensaje As String, esProblema As Boolean)
        lblResultado.Text = mensaje
        If esProblema Then
            lblResultado.ForeColor = Color.FromArgb(178, 8, 55)
        Else
            lblResultado.ForeColor = Color.Gray
        End If
    End Sub

End Class
