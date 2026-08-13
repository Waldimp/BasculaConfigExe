# Novedades de esta versión

Resumen de los cambios de la rama `unificado` frente a la versión que está en producción.

## Para el cliente

El lector de peso ya no hay que abrirlo en cada pesada. Se abre solo al encender la
computadora y se queda tomando el peso cada 5 segundos, con el intervalo configurable.

Deja de ser invisible: muestra una ventana en la esquina inferior derecha con el peso, la
hora de la última lectura y el estado, con botones de Detener y Reanudar. Si algo falla
—cable desconectado, báscula apagada, puerto ocupado— lo avisa en pantalla y sigue
reintentando solo. Antes el programa se cerraba en silencio y el sistema seguía leyendo el
peso anterior sin que nadie se diera cuenta.

En el configurador hay una pantalla nueva, **Automatización**, para elegir si arranca
leyendo solo, cada cuántos segundos lee, y si se abre junto con Windows.

Lo demás funciona igual que siempre: la misma conexión con la báscula y el mismo archivo de
texto con el peso, en el mismo formato, así que del lado del cliente no cambia nada.

## Al instalar

Se instalan por separado: primero el configurador y después el lector.

- **Hay que ejecutar `setup.exe`**, no el ejecutable suelto. Abriéndolo directo funciona
  todo menos el arranque automático con Windows, porque ese depende del acceso directo que
  crea la instalación.
- **El icono junto al reloj queda escondido tras la flechita** la primera vez. Windows lo
  hace con todos los programas nuevos; se arrastra una vez hacia afuera y ya se queda fijo.

## Detalle técnico

| Qué | Dónde |
|---|---|
| Modo de arranque, intervalo y apertura con Windows | `C:\config\Arranque.txt`, `Intervalo.txt`, `InicioWindows.txt` |
| Registro de actividad | `%LocalAppData%\BasculaNET\registro\`, un archivo por día, 30 días de historial |
| Archivo de salida con el peso | La carpeta configurada, sin subcarpeta |

El registro guarda la trama cruda que envía la báscula. Es lo que permite ajustar después
la posición y la cantidad de caracteres del peso sin necesidad de tener el equipo enfrente.

La lógica de conexión y de extracción del peso no se modificó.
