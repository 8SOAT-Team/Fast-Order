@echo off
setlocal

:: Verifique se o número de repetições foi passado como argumento
if "%1"=="" (
    echo Por favor, forneça o numero de requisições.
    echo Exemplo: script.bat 10
    exit /b
)

:: Número de requisições
set count=%1

:: Loop para realizar as requisições
for /l %%i in (1,1,%count%) do (
    echo Requisição %%i de %count%
    curl http://localhost:31500
)

endlocal
