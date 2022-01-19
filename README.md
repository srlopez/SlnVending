# Máquina Vending
<!--
Nomenclatura PlantUML
https://crashedmind.github.io/PlantUMLHitchhikersGuide/index.html

VSCODE settings.json
PlantUML Server:
https://www.plantuml.com/plantuml
-->

## Fabricamos Máquinas de Vending.

Nuestras máquinas son susceptibles de ser configuradas de formas distintas.  
Adminten tipos de productos distintos como son `Golosinas`, `Refrescos` y artículos de `Parafarmacia`.   
Las golosinas tienen `peso` en gramos, los refrescos tienen `capacidad` en centilitros.  Además los refrescos pueden ser `Dietéticos`.

La matriz de productos puede ser del tamaño que el cliente desee.

## Operativa
**La forma de operar es la siguiente:**  
El usuario se presenta ante la máquina y puede elegir entre `Consultar los productos` o `Adquirir` alguno. Si el que se presenta es el reponedor se le da la opción de `Administrar` la máquina.

### Administración
**La administración** de la máquina consiste en un tareas diarias como:
- Recaudar el `cajón de monedas`, y 
- Rellenar cada canal de la `caja de cambios` de `2€`/`1€`/`0,50€`/`0,20€`/`0,10€` con `10 monedas`, (no usamos `0,05€`), y
- completar los productos hasta `5 unidades`. 

y otras tareas como:
- Obtener un informe, y
- Apagar la máquina.

Para entrar en modo administrativo se activa un sistema de seguridad, configurado según desee el cliente. Y para ello ofrecemos dos tipos de `control de Seguridad`:  
- A.- mediante PIN'es prefijados, y
- B.- mediante on PIN validado por algoritmo secreto. 

**Reposición y recaudación:**  
RELLENAMOS TODOS LOS artículos, y la CAJA de cambios hasta sus máximos permitidos.
Retiramos el cajón de monedas sobrantes.

**Informe:**  
Mostrarnos el importe y el número de ventas totales.
Y el estado de la Caja.

**Apagar máquina:**  
Nos permitirá apagar la máquina, pero antes deberá darnos la opción de mostrar **Informe**

### Adquisición
La operativa normal de **adquirir un producto** es como sigue:

El cliente indicará la `posición` del producto que quiere. Si no hay producto disponible en esa posición se avisará con un mensaje, en caso contrario se mostrará el importe que deberá introducir.

Los productos de ParaFarmacia deben pueden ser adquiridos pero necesitan `identificación` del usuario (se evitará que se identifique si desea realizar más compras).  

A continuación el usuario deberá introducir su importe en monedas, en un `máximo de 5 monedas`. (tú decides cómo se introduce el importe).

El importe del pago será validado, y si `no es conforme` se devuelve el importe, y se cancela la operación.

Durante el proceso de pago, las monedas se reintegran en los `canales de cambio` hasta el máximo permitido, y el resto van al cajón.
El `cambio` será con el menor número de monedas posibles (opcional: evitando las monedas de 2€).

**Mostrar matriz de productos:** 
En modo USER mostrará la etiqueta y el precio.
En modo ADMIN, añadirá las unidades disponibles de cada producto.
El tipo de artículo debe ser expuesto al usuario.
Si el refresco es pequeño (menor de 60cc) también se representa. 

### Persitencia
Estudiar la persitencia
Capa de Datos
