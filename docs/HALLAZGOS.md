# Informe de hallazgos

Registro de deuda técnica detectada en la revisión del código existente.

**Criterio rector:** el sistema está en producción y es funcional, y **no hay básculas disponibles
para probar**. Por lo tanto la columna más importante de este informe no es la severidad, sino
**cómo se verifica** el arreglo. Un hallazgo grave que solo se puede validar con hardware real es
más riesgoso de tocar que un hallazgo medio verificable por inspección.

## Cómo leer las tablas

| Columna | Significado |
|---|---|
| **Severidad** | Impacto si el problema se manifiesta. |
| **Riesgo de arreglar** | Probabilidad de romper algo que hoy funciona. |
| **Verificación** | 🟢 Por inspección o prueba de escritorio · 🟡 Con puerto serial virtual (com0com) · 🔴 Requiere báscula física |
| **Decisión** | Pendiente / **Sí** / **No** / Diferido. Se llena al revisar cada punto. |

---

## Grupo A — No tocan la comunicación serial

Verificables sin hardware. Es donde conviene concentrar el esfuerzo.

| # | Hallazgo | Severidad | Riesgo de arreglar | Verificación | Decisión |
|---|---|---|---|---|---|
| A1 | `BasculaConfig.vbproj` referencia `Guna.UI2.dll` desde `..\..\..\..\Downloads\...`. El proyecto no compila en otra máquina ni en un clon del repo. | Bloqueante | Muy bajo | 🟢 | Pendiente |
| A2 | Ruta acumulativa en modo Demanda: `Form1_Load` deja `ruta = <base>\BasculaNET` y `DataReceived` hace `ruta = ruta + "\pesas.txt"`. Si `DataReceived` dispara dos veces → `...\pesas.txt\pesas.txt`. | Alta | Muy bajo | 🟢 | Pendiente |
| A3 | `C:\config\` hardcodeado, creado con `MkDir` en la raíz de C:. Requiere permisos elevados con UAC estándar. Debería ser `%ProgramData%\BasculaNET\`. | Media | Medio (rompe instalaciones existentes si no se migra) | 🟢 | Pendiente |
| A4 | 14 archivos `.txt` sueltos, uno por parámetro, sin transaccionalidad. Si `BasculaExe` arranca mientras el configurador guarda, lee configuración mezclada. Un `.txt` faltante lanza excepción en `ReadAllText`. | Media | Medio | 🟢 | Pendiente |
| A5 | Excepciones silenciadas: `Catch` vacíos en `BasculaExe`, y **ningún** `Try` en `tmrTimer_Tick` ni en `DataReceived`. Sin log. Al correr oculto, una falla es invisible. | Alta | Muy bajo (solo añade) | 🟢 | Pendiente |
| A6 | No hay log de ninguna clase. Imposible diagnosticar en sitio del cliente. | Alta | Nulo (solo añade) | 🟢 | Pendiente |
| A7 | `Scripting.FileSystemObject` vía COM/late binding en lugar de `File.WriteAllText`. Depende del runtime de WSH y no libera el handle si hay excepción. | Media | Bajo | 🟢 | Pendiente |
| A8 | `HandShake.txt` (escrito por el configurador) vs `Handshake.txt` (leído por el ejecutable). Funciona solo porque NTFS ignora mayúsculas. | Baja | Muy bajo | 🟢 | Pendiente |
| A9 | Paridad y Handshake se persisten como **índice del ComboBox**, no como valor. Coincide con los enums de `System.IO.Ports` por casualidad; reordenar los ítems corrompe la configuración en silencio. | Media | Bajo | 🟢 | Pendiente |
| A10 | `Demanda_Ruta` leída y nunca usada en `DataReceived` (líneas 62–64). Código muerto. | Baja | Nulo | 🟢 | Pendiente |
| A11 | `Option Strict Off`. Ej.: `StrBufferIn.Length > Continuo_Inicio` compara Integer con String vía conversión implícita. | Baja | Alto (activarlo revela decenas de errores) | 🟢 | Pendiente |
| A12 | Primera ejecución de `BasculaExe` abre el explorador y un `SaveFileDialog` para autocopiarse. Ajeno a la función de pesar y confuso para el operativo. | Baja | Bajo | 🟢 | Pendiente |
| A13 | La lógica de lectura está **triplicada**: `SecondCustomControl`, `ThirdCustomControl` y `BasculaExe/Form1`. Las copias ya divergen (A2 existe solo en una). | Alta | Medio | 🟢 si se **mueve** sin reescribir | Pendiente |

---

## Grupo B — Tocan la comunicación serial

Aquí es donde hay que ser conservador. Salvo B1 y B2, conviene diferir.

| # | Hallazgo | Severidad | Riesgo de arreglar | Verificación | Decisión |
|---|---|---|---|---|---|
| B1 | **`ReadTimeout` y `WriteTimeout` no se configuran nunca** → ambos quedan en `InfiniteTimeout` (-1). En modo Demanda con handshake `RequestToSend`, si el dispositivo no afirma CTS, `WriteLine` **bloquea para siempre**. Hoy pasa desapercibido porque el proceso es efímero; en una rutina continua cuelga la aplicación. | Alta | Bajo (solo añade un límite) | 🟡 | Pendiente |
| B2 | `CheckForIllegalCrossThreadCalls = False`. `DataReceived` corre en un hilo del pool y escribe en `Label.Text` sin `Invoke`. En un proceso efímero es casi inocuo; en una rutina de horas termina corrompiendo el estado de la UI. | Alta si hay rutina continua | Medio | 🟡 | Pendiente |
| B3 | **No hay validación del peso.** No se verifica que sea numérico ni que la lectura sea estable. `Mid()` corta a ciegas y lo que salga se escribe en `pesas.txt`. | Alta | Medio | 🔴 | Pendiente |
| B4 | **No se maneja la trama partida.** `ReadExisting` devuelve el contenido del buffer en ese instante, que en serial suele ser media trama. No hay acumulación ni delimitador (`NewLine`/STX/ETX). La guarda `buffer.Length > Inicio` verifica que haya algo *antes* del inicio, no que existan `Ocupar` caracteres *después*. | Alta | Alto | 🔴 | Pendiente |
| B5 | La desconexión física de un adaptador USB-serial deja el objeto `SerialPort` de .NET Framework en estado inconsistente; `Close()` puede bloquearse. La mitigación conocida es **desechar el objeto y crear uno nuevo** en cada reconexión, no reutilizarlo. Relevante solo si se implementa reconexión automática. | Alta si hay rutina continua | Medio | 🟡 | Pendiente |
| B6 | `tmrTimer` sin guarda de reentrada. Si el trabajo de un tick dura más que el intervalo, los ticks se solapan. | Media | Bajo | 🟢 | Pendiente |
| B7 | `pesas.txt` se sobrescribe sin marca de tiempo. El sistema del cliente no puede distinguir un peso actual de uno de hace ocho horas. Se vuelve crítico si la rutina corre desatendida. | Alta | Bajo (campo adicional) | 🟢 | Pendiente |

---

## Notas de verificación sin hardware

Para lo marcado 🟡 se puede levantar un par de puertos COM virtuales enlazados
(**com0com**, null-modem virtual) y emular la báscula desde un script que responda con tramas
capturadas. Eso cubre: timeouts, reconexión, reentrada del timer y cruce de hilos — sin báscula.

Lo marcado 🔴 (B3, B4) depende de conocer las tramas reales de cada modelo de báscula. Si en algún
momento se pueden capturar tramas crudas en sitio y guardarlas, esos dos hallazgos pasan a 🟡.
**Registrar la trama cruda en el log (A6) es el habilitador de todo lo demás.**
