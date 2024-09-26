@echo off
setlocal enabledelayedexpansion

rem Define la extensión que deseas eliminar (por ejemplo, .txt)
set "extension=.meta"

rem Define el directorio raíz donde se iniciará la búsqueda
set "directorio=."

rem Confirma con el usuario antes de borrar
echo Vas a eliminar todos los archivos con extensión %extension% en el directorio %directorio% y sus subdirectorios.
set /p "confirmar=¿Estás seguro? (S/N): "

if /I "!confirmar!" neq "S" (
    echo Operación cancelada.
    exit /b
)

rem Recorrer todos los archivos con la extensión especificada y eliminarlos
for /r "%directorio%" %%f in (*%extension%) do (
    echo Eliminando: "%%f"
    del /f /q "%%f"
)

echo Proceso completado.
pause
