# RinkuCoppel
Ejercicio Técnico para Programador Senior

El Sistema permite registrar los datos de los empleados, asi como las entregas que realicen y se puede consultar un Reporte de los Pagos y Retenciones.

El presente repositorio contiene el Sistema para Cinematográfica Rinku.

Organización del Sistema:

MenuPrincipal: Contiene el Menu de opciones del Sistema.

FormEmpleados: Permite dar de Alta a un Empleado, asi como Modificar sus Datos.

FormEntrega: Permite Agregar las Entregas realizadas por el Empleado en cada mes, asi como Modificar la información de las Entregas del empleado.

FormReporte: Permite Consultar la Información mensual de cada Empleado, mostrando: Horas Trabajadas, Pago Total por Entregas, Pago Total por Bonos, Retenciones, Vales y Sueldo Total.

## Empezando

Estas instrucciones le permitirán obtener una copia del proyecto en funcionamiento en su máquina local.

### Prerequisitos 

- Obtener una copia de este repositorio.
- Se requiere tener instalado Microsoft SQL Server Express Edition 2016 64Bits o tener instalado el Microsoft SQL Server Express LocalDB.
- Se requiere Restaurar la BD llamada RinkuCoppel, el archivo de respaldo esta en la carpeta Respaldo_BD.
- Se requiere tener instalado Visual Studio 2017 o 2019, así como haber compilado y ejecutado el proyecto correspondiente.

### Cadena de Conexión

La cadena de conexión esta en el archivo clsConDat.cs que se encuentra en la carpeta Clase, la ruta es: Clase\clsConDat.cs
"server=(localdb)\\MSSQLLocalDB; database=RinkuCoppel ; integrated security = true"

La BD fue creada en MSSQLLocalDB.
Si es necesario cambie el nombre del Servidor por el que tenga en uso.

### Base de Datos

En la carpeta Respaldo_BD se encuentran los siguientes archivos:
- RinkuCoppel.bak (Archivo de Respaldo de la BD).
- script_Generar_RinkuCoppel.sql (Script para crear la BD).

### Ejecución del Sistema sin utilizar el IDE

- La BD debe de estar restaurada.
- Ejecute el archivo "RinkuCoppel.exe" que se encuentra en la carpeta bin\Debug

### Capturas de Pantalla

En la carpeta Capturas_de_Pantalla se encuentran las capturas del Sistema que muestran las pantallas en funcionamiento, asi como una de la estrucutura de la BD.

## Desarrollado con

- Sistema Operativo de desarrollo	    Microsoft Windows 10
- Motor de base de datos                Microsoft SQL Server Express Edition 2016 64Bits
- IDE									Microsoft Visual Studio Profesional 2019
- Lenguaje de Programación				C#
- Microsoft .NET Framework              4.5
- Tipo de Aplicación					Windows Form (Escritorio)

## Arquitectura

Cliente - Servidor

## Versiones

Se uso [GitHub] (https://github.com/) para el control de versiones.
El link del Repositorio es (https://github.com/sasalgh/RinkuCoppel)

## Autor
* *L.I. Sandro Sarlis López*