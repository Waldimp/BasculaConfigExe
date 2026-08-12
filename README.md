# Báscula — Lectura de peso por puerto serial

Sistema de dos aplicaciones VB.NET (WinForms, .NET Framework 4.7.2) que obtienen el peso de una
báscula industrial por puerto serial (RS-232) y lo escriben en un archivo de texto, para que
cualquier sistema externo pueda consumirlo sin necesidad de integrarse con el puerto.

## Las dos aplicaciones

| Proyecto | Rol | Cuándo se ejecuta |
|---|---|---|
| **BasculaConfig** | Configurador. Descubre puertos COM, permite probar la lectura en vivo, y guarda todos los parámetros. | Una sola vez, en la instalación. |
| **BasculaExe** | Automatizador. Lee la configuración, obtiene el peso y lo escribe en el `.txt`. Se cierra solo. | Cada vez que el sistema externo necesita un peso. |

## Cómo se comunican

No hay llamadas directas entre las dos apps. El acoplamiento es a través del **sistema de
archivos**: `BasculaConfig` escribe un archivo `.txt` por cada parámetro en `C:\config\`, y
`BasculaExe` los lee al arrancar.

```
BasculaConfig  ──escribe──>  C:\config\*.txt  ──lee──>  BasculaExe
                                                             │
                                                     escribe │
                                                             v
                                            <RutaConfigurada>\BasculaNET\pesas.txt
                                                             │
                                                             v
                                                  Sistema del cliente
```

### Contrato de configuración (`C:\config\`)

Cada archivo contiene un único valor seguido de `CRLF` (que `BasculaExe` elimina al leer).

**Parámetros del puerto (comunes a ambos modos)**

| Archivo | Contenido | Escrito por |
|---|---|---|
| `modo.txt` | `Continuo` o `Demanda` | `FirstCustomControl` |
| `BaudRate.txt` | p. ej. `9600` | `ConfigurationPort` / `Loader` |
| `DataBits.txt` | p. ej. `8` | `ConfigurationPort` / `Loader` |
| `Parity.txt` | índice del combo `0`–`4` (None, Odd, Even, Mark, Space) | `ConfigurationPort` / `Loader` |
| `HandShake.txt` | índice del combo `0`–`3` (None, XOnXOff, RequestToSend, RequestToSendXOnXOff) | `ConfigurationPort` / `Loader` |

**Modo Continuo** — la báscula transmite sola; se lee con un `Timer`.

| Archivo | Contenido |
|---|---|
| `Continuo_Puerto.txt` | nombre del puerto, p. ej. `COM3` |
| `Continuo_Inicio.txt` | posición (1-based) donde empieza el peso dentro de la trama |
| `Continuo_Ocupar.txt` | cantidad de caracteres que ocupa el peso |
| `Continuo_Ruta.txt` | carpeta base donde se escribirá el `.txt` |

**Modo Demanda** — hay que enviarle un carácter a la báscula para que responda.

| Archivo | Contenido |
|---|---|
| `Demanda_Puerto.txt` | nombre del puerto |
| `Demanda_Inicio.txt` | posición (1-based) donde empieza el peso |
| `Demanda_Ocupar.txt` | cantidad de caracteres del peso |
| `Demanda_Ruta.txt` | carpeta base de salida |
| `Demanda_Caracter.txt` | carácter(es) de solicitud; admite entrada en hexadecimal |

> El peso se extrae con `Mid(trama, Inicio, Ocupar)` — es decir, por **posición fija** dentro de la
> trama, no por parseo del protocolo de la báscula.

### Salida

`<Ruta>\BasculaNET\pesas.txt`, sobrescrito en cada lectura (`CreateTextFile`).

## Flujo interno

### BasculaConfig

`Loader` (splash; siembra valores por defecto de puerto si están vacíos) → `Form1` (shell con menú
lateral) que hospeda cuatro controles de usuario:

- `FirstCustomControl` — selección de modo (Continuo / Demanda).
- `SecondCustomControl` — configuración del modo Continuo, con lectura en vivo para contar caracteres.
- `ThirdCustomControl` — configuración del modo Demanda, con envío del carácter y conversión a hex.
- `ConfigurationPort` — parámetros del puerto (baudios, bits, paridad, handshake).

Cada cambio se guarda **dos veces**: en `My.Settings` (para repoblar la UI al reabrir) y en el
`.txt` de `C:\config\` (para que lo consuma `BasculaExe`).

### BasculaExe

Arranca oculto (`Me.Hide()`), lee todos los `.txt`, configura el `SerialPort` y según el modo:

- **Continuo** → abre el puerto y habilita `tmrTimer`; en cada tick lee el buffer, extrae el peso,
  lo escribe y cierra la aplicación.
- **Demanda** → abre el puerto y envía el carácter de solicitud; el evento `DataReceived` extrae el
  peso, lo escribe y cierra la aplicación.

En la primera ejecución (`My.Settings.abrir <> 1`) abre el explorador y un `SaveFileDialog` para
que el usuario copie el ejecutable a la ubicación que quiera.

## Requisitos

- Visual Studio con soporte para VB.NET y .NET Framework 4.7.2
- **Guna.UI2** (framework de controles WinForms) — usado por `BasculaConfig`
- Permisos de escritura en `C:\config\`

## Compilar

Cada aplicación tiene su propia solución:

```
BasculaConfig\BasculaConfig.sln
BasculaExe\BasculaExe.sln
```

Se mantienen en un solo repositorio porque comparten el contrato de configuración descrito arriba:
cambiar ese contrato exige modificar ambas aplicaciones en el mismo commit.
