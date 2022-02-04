# test
Reccojo de otros años diagrmas en PlantUML
No soy muy fan, pero por si acaso.

Necesita el pluging de PlantUML

### Casos de uso de sistema

Mostramos varios posibles casos de uso, pero sólo desarrollamos en este documento enfocamos el **UC1** para mostrar cómo se puede llevar a código fuente, y ver distintas posibilidades de los diagramas UML.

<img src="http://www.plantuml.com/plantuml/proxy?src=https://raw.githubusercontent.com/srlopez/RUP/master/ejemplos/fraccion_completo.md&idx=0&t=5" alt=""/>

<details><summary>Code #0</summary>

```plantuml
@startuml
hide stereotype
skinparam backgroundcolor transparent

skinparam rectangle {
  BackgroundColor WhiteSmoke
}
skinparam usecase {
  BackgroundColor White
  BorderColor DarkSlateGray
  ArrowColor Grey
  
  BorderThickness<<beta>> 1
  BorderStyle<<beta>> dotted
  'BackgroundColor<<beta>> #FFE
  'BorderColor<<beta>> Red
}
skinparam actor {
  BackgroundColor White
  BorderColor DarkSlateGray
  ArrowColor Grey
}
skinparam note {
  BackgroundColor White
  BorderColor DarkSlateGray
}
note "<b>Requisitos Funcionales</b>\n<b>UC1:</b> Sumar f1+f2\n<b>UC2:</b> Multiplicar f1+f2\n<b>UC3:</b> Ranking\n<b>UC4:</b> Consultas\n<b>UC5:</b> Todas la Ops.\n<b>UC6:</b> Operaciones impropias" as n1
note "<b>Requisitos</b> No funcionales <b>Lógicos</b>\n<b>Reglas de Negocio</b>\n<i>Escritos como notas</i>\n<b>RL1:</b> Solo operamos con fracciones propias\n<b>RL2:</b> Y de 8:00 a 15:00" as n2
note "<b>Requisitos</b> No funcionales <b>Técnicos</b>\n<i>Escritos como notas</i>\n<b>RT1:</b> Tiempo de respuesta<1sg\n<b>RT2:</b>Operaciones persistentes" as n3

left to right direction
:User: as cli
rectangle Máquina_Vending as sistema {
  (Sumar\n2 Fracciones\n<b>UC1</b>) as uno 
  (Multiplicar\n2 Fracciones\n<b>UC2</b>) as dos 
  (Ranking\n<b>UC3</b>) as tres
  (Consultar\nFraccion\n<b>UC4</b>) as cuatro
  (Mostrar las\nOperacions\n<b>UC5</b>) as cinco
  (Resultados\nImpropios\n<b>UC6</b>) as seis
  (No Implementado\n<b>UC7</b>) as siete<<beta>>
}

cli -- uno
sistema -- n2
sistema -- n3
cli -- dos
cli -- tres
cli -- cuatro
cli -- cinco
cli -- seis
cli -- siete
uno -- n1
dos -- n1
tres -- n1
cuatro -- n1
cinco -- n1
seis -- n1

@enduml
```
</details>

### Documento de Requistos
**Documento de Requisitos** a rellenar durante la evolución del proyecto.

| ID | Descripción requisito | Implementado en | Invocado desde | Estado | Sprint | Responsable |
| -- | -- | -- | -- | -- | -- | -- |
| UC1 | Suma de Fracciones | Calculadora.java<br>CalculadoraDB.java | Controlador.java|  Done| 
| UC2 | Multiplicación de Fracciones | Calculadora.java<br>CalculadoraDB.java 
| UC3 | Ranking de apariciones  | CalculadoraDB.java | Controlador.java | Done| 
| UC4 | Consulta de Fracciones | CalculadoraDB.java| Controlador.java | Done| 
| UC5 | Todas las Operaciones | CalculadoraDB.java | Controlador.java | Done| 
| UC6 | Resultados Impropias | CalculadoraDB.java | Controlador.java | Done|  
| UC7 | N/A | | | N/A | 
| RL1 | Fracciones propias | Requisitos.java | Controlador.java | Done|  
| RL2 | De 8:00 a 15:00 | Requisitos.java | Controlador.java | Done|   
| RT1 | Tiempo de respuesta de operacion <1sg. | | | Test | 
| RT2 | Operaciones persistentes | repostorios | CalculadoraDB| SQLite<br>File | 
   
   
### Caso de Uso 1 - Completo

Use case 01: **Sumar Dos Fracciones**  
**ID**: UC01   
**Nombre UC**: *Sumar Dos Fracciones*  
**Descripción**: Calcula la suma de dos fracciones. (Y las persiste)  
**Actor Principal**: El Usuario  
**Actores secundarios**: N/A  
**Precondiciones**: (ej: el usuario está correctamente identificado)
**Postcondiciones**: El sistema registra la operación correctamente.
**Flujo de Eventos:**
- **Flujo Normal o Básico**:
1. El sistema pide una fracción
1. El usuario introduce f1
1. El sistema pide otra fracción
1. El usuario introduce f2
   1. Si no RL1 f1 ir al punto 6
   1. Si no RL1 f2 ir al punto 6
   1. Si no RL2 ir al punto 6
1. El sistema suma f1 y f2 y presenta el resultado
1. El sistema finaliza el UC1
- **Flujo Alternativo**:  
  N/A

> La descripción de un caso de uso **completo** narra un escenario en forma de diálogo entre el _usuario_ y el _sistema_. Se concentra en el flujo principal aunque puede incluir escenarios alternativos, con el objetivo de describir la especificación general del requisito funcional recogido como caso de uso. Ha de incluir, código del UC, Título/Nombre, Descripción, Actores principales y secundarios y Pre y Postcondiciones.


### Diagrama Conceptual del Dominio
N/A

### Diagrama de Clases

Relación de '`Relaciones`' entre Clases (las más habituales).
| | Descripción |
|--|--|
| dependencia | relación de `uso` entre objetos |
| extensión | generalización o especialización<br>Usada para reflejar `herencias`|
| realización | idem que extensión<br>pero para `interfaces` |
| composición | refleja una `parte indispensable` de una clase |
| agregación | refleja una `parte independiente` de una clase |
| asociación | relacción `normal` entre clases<br>Suele tener *nombre*, *dirección* y *cardinalidad* |
|  |  |


<img src="http://www.plantuml.com/plantuml/proxy?src=https://raw.githubusercontent.com/srlopez/RUP/master/ejemplos/fraccion_completo.md&idx=1&t=2" alt=""/>

<details><summary>Code #1</summary>

```plantuml
legend Ejemplos de relaciones UML
left to right direction
'top to bottom direction
@startuml
skinparam monochrome true
skinparam backgroundColor transparent
'skinparam classBackgroundColor transparent
'skinparam handwritten true
skinparam style strictuml
'skinparam defaultFontColor Grey
skinparam class {
  skinparam shadowing false
  BackgroundColor White
  BorderColor Gray
  ' FontName Consolas
  ArrowColor Gray
}

abstract Persona <<abstract>>{
 +nombre
 +edad
 +toString()
}
Class Empleado {
 +sueldo_bruto
 +calcular_sueldo_neto()
 +toString()
}
Class Cliente {
 +dirección_facturación
 +toString()
}
Class Directivo {
 +categoría
 +toString()
}
Class Empresa {
 +facturar()
}
Class CreadorDeNominas {
 +calcularNomina()
}
Interface IFacturar <<interface>> {
 +facturar()
}
IFacturar <|.r. Empresa: <b>extensión</b>\ngeneralización/especialización\nImplementación
Persona <|-r- Cliente: <b>extensión</b>\ngeneralización/especialización\nHerencia
Empleado -r-|> Persona: es
Empleado <|-- Directivo: es
Empresa o-- Cliente: <b>agregación</b>\nParte independiente\nde una composición mayor
Empresa *-- Empleado: <b>composición</b>\nParte indispensable\nde una composición mayor
CreadorDeNominas ..> Empleado: <b>dependencia</b>\nrelación de uso\ny arquitecturas dependientes
Directivo "1" --> "0..*" Empleado: dirige\n<b>asociación</b>\nRelaciónes entre clases\nnormal y que no entra en las otras
@enduml
```
</details>


#### Diagrama Inicial
Se muestra _Fracción_ y _Calculadora_ como los únicos Modelos de Datos (Clases) principales del Dominio base sin introducir la persistencia.


<img src="http://www.plantuml.com/plantuml/proxy?src=https://raw.githubusercontent.com/srlopez/RUP/master/ejemplos/fraccion_completo.md&idx=2&t=2" alt=""/>

<details><summary>Code #2</summary>

```plantuml
@startuml
title <b>Diagrama de Clases</b>\n<i>Modelo del Dominio</i>
left to right direction
'bottom to top direction
skinparam class {
  skinparam monochrome true
  skinparam shadowing false
  BackgroundColor White
  BorderColor Gray
  ' FontName Consolas
  ArrowColor Gray
}
scale 1
hide circle
package aritmetica {

  class Fraccion {
    -int numerador
    -int denominador
  -- Constructores --
    + Fraccion ()
    + Fraccion (n, d)
    + Fraccion (s)
  -- Métodos --
    +String toString()
  }
  class Calculadora {
    +Fraccion suma()
    +Fraccion multiplica()
  } 
}
Fraccion <.. Calculadora: op.f1
Fraccion <.. Calculadora: op.f2
Fraccion <.. Calculadora: resultado
@enduml
```
</details>

#### Diagrama con persistencia

Al añadir la persitencia para cumplir los requisitos de consultas y registro de operaciones,  se modifica el dominio añadiendo _Operación_ como Modelo de Datos a persistir y _CalculadoraDB_ como Modelo que deriva de la _Calculadora_ e interactuará con el motor de persitencia. 

<img src="http://www.plantuml.com/plantuml/proxy?src=https://raw.githubusercontent.com/srlopez/RUP/master/ejemplos/fraccion_completo.md&idx=3&t=2" alt=""/>

<details><summary>Code #3</summary>

```plantuml
@startuml
title <b>Diagrama de Clases II</b>\n<i>Modelo del Dominio con Persistencia</i>
left to right direction
'bottom to top direction
skinparam class {
  skinparam monochrome true
  skinparam shadowing false
  BackgroundColor White
  BorderColor Gray
  ' FontName Consolas
  ArrowColor Gray
}
scale 1
hide circle
package aritmetica {

  class Fraccion {
    -int numerador
    -int denominador
  -- Constructores --
    + Fraccion ()
    + Fraccion (n, d)
    + Fraccion (s)
  -- Métodos --
    +String toString()
  }
  class Operacion {
  Date fh
  -- Métodos --
    +String toString()
  }
  class OperacionTipo<<enum>> {
    SUMA
    MULTIPLICACION
  }
  class Calculadora {
    +Fraccion suma()
    +Fraccion multiplica()
  }
  class CalculadoraDB<<Sistema>> {
    +Fraccion suma()
    +Fraccion multiplica()
    -registrarOperacion()
    +qryOperacionesPor()
    +qryRanking() 
    +qryResultadosImpropios()
    +qryTodaslasOperaciones()
  }
  
}
Fraccion --* Operacion: f1
Fraccion --* Operacion: f2
Fraccion --* Operacion: resultado
OperacionTipo --* Operacion: tipo
CalculadoraDB --|> Calculadora
Fraccion <.. Calculadora: op.f1
Fraccion <.. Calculadora: op.f2
Fraccion <.. Calculadora: resultado
Operacion <.. CalculadoraDB : persiste

@enduml
```
</details>

#### Diagrama de Arquitectura

Diagrama de Clases de Arquitectura de la aplicación. Es distinto del diagrama de clases del Modelo del Dominio. Este se centra en mostrar la Arquitectura de la aplicación, mostrarndo las clases que mecánicamente llevaran la información de la BD a la pantalla.   
En este caso se aplican los Patrones `MVC` y `Fachada` (CalculadoraDB) al Sistema.
Y Clase de `Acceso a Datos` con `Interface`, mostrando dos implementaciones.

<img src="http://www.plantuml.com/plantuml/proxy?src=https://raw.githubusercontent.com/srlopez/RUP/master/ejemplos/fraccion_completo.md&idx=4&t=2" alt=""/>

<details><summary>Code #4</summary>

```plantuml
@startuml
title <b>Diagrama de Clases</b>\n<i>Arquitectura de la Aplicación</i>
left to right direction
skinparam class {
  skinparam monochrome true
  skinparam shadowing false
  BackgroundColor White
  BorderColor Gray
  ' FontName Consolas
  ArrowColor Gray
}
scale 1
hide circle

package aritmetica {
  class Calculadora {
    +Fraccion suma()
    +Fraccion multiplica()
  }
  class CalculadoraDB<<Sistema>> {
    +Fraccion suma()
    +Fraccion multiplica()
    -registrarOperacion()
    +qryOperacionesPor()
    +qryRanking() 
    +qryResultadosImpropios()
    +qryTodaslasOperaciones()
  }
}
package ui {

  class CtrlTerminal{
  -- Métodos --
    +void run()
    +void useCase1()
    +void useCase2()
    +void useCase3()
    +void useCase4()
    +void useCase5()
    +void useCase6()
  }

  class ViewTerminal{
  -- Métodos --
    - String leerFraccionString()
    +Fraccion leerFraccion()
    +void mostrarResultado()
    +int mostrarMenu()
  }
}

package persistencia {
    class OperacionesSQLite{ 
      -dbname 
    }
    class OperacionesMem{ 
      -filename 
    }

    class IOperacionesDAO
    {
    +cmdRegistrarOperacion(op)
    +qryOperacionesPor(f)
    +qryRanking() 
    +qryResultadosImpropios()
    +qryTodasLasOperaciones() 
    }
}
IOperacionesDAO <.. CalculadoraDB : repositorio
CalculadoraDB --|> Calculadora
CtrlTerminal ..> CalculadoraDB: sistema 
CtrlTerminal ..> ViewTerminal: vista
OperacionesSQLite --|> IOperacionesDAO
OperacionesMem --|> IOperacionesDAO
@enduml
```
</details>


## Diagrama de secuencia

### Versión básica:  
> Mostramos el ejemplo más sencillo. Un escenario con un único flujo principal. Sin escenarios alternativos y a continuación el código que podría ser el guión del caso de uso dentro del Controlador.
> Tampoco se muestran la aplicación de las Reglas de Negocio.

<img src="http://www.plantuml.com/plantuml/proxy?src=https://raw.githubusercontent.com/srlopez/RUP/master/ejemplos/fraccion_completo.md&idx=5&t=2" alt=""/>

<details><summary>Code #5</summary>

```plantuml
@startuml
title <b>Sumar Dos Fracciones</b>\n<i>Diagrama de secuencia - UseCase1</i>
skinparam monochrome true
' skinparam handwritten true
' skinparam defaultFontName Comic Sans MS
' skinparam classArrowFontName Arial

autonumber "[0]"
hide footbox

actor Usuario as u
boundary Vista as v
control Controlador as c 
participant "Calculadora\n<<Sistema>>" as s

'group Comprar Producto
c -> v: leerFraccion
v -[#LightGrey]> u: "Indica una fracción (0/1): "
u -[#LightGrey]> v: Fraccion (f1)
v -> c: Fraccion (f1)
c -> v: leerFraccion
v -[#LightGrey]> u: "Indica una fracción (0/1): "
u -[#LightGrey]> v: Fraccion (f2)
v -> c: Fraccion (f2)
c -> s: suma(f1,f2)
s -> c: Fraccion (result)
c -> v: mostrarResultado(result)
v -[#LightGrey]> u: "Suma :" (result)

'end
@enduml
```
</details>


El código en el controlador:
```java
  public void useCase1() {
      // Punto de Entrada al Caso de Uso #1 
      // Indicando el número de mensaje en el diagrama 
      Fraccion f1 = viewTerminal.leerFraccion(); // 1..4
      Fraccion f2 = viewTerminal.leerFraccion(); // 5..8
      Fraccion result = sistema.suma(f1, f2); // 9..10
      viewTerminal.mostrarResultado(result); // 11
  }
```


### Versión con una caja de `loop` y `alt` 
>Versión pedagógica para mostrar alternativas de cómo se puede modelar un diagrama de secuencia mostrando un ciclo de repetición, y las alternativas secuencias en caso de escenarios distintos. 

`Loop` para indicar un ciclo. Se describe la condición de salida.
`Alt` para indicar una condición _IF_, y se describen las condiciones que escenifican las opciones.

El **Ciclo** mostraría como se repiten los mensajes entre los Participantes mientras se mantiene la condición. En este ejemplo la condición es que las fracciones f1 y f2 sean distintas.  
Mediante IF(`Alt`) mostramos como se podría modelar la verificación d euna regla de negocio sencilla y como se pueden escoger entre mensajes diferentes (`Enhorabuena` o `Inténtalo de nuevo`) dada una condición.

<img src="http://www.plantuml.com/plantuml/proxy?src=https://raw.githubusercontent.com/srlopez/RUP/master/ejemplos/fraccion_completo.md&idx=6&t=2" alt=""/>

<details><summary>Code #6</summary>

```plantuml
@startuml
title <b>Sumar Dos Fracciones</b>\n<i>Diagrama de secuencia - UseCase1</i>
skinparam monochrome true
' skinparam handwritten true
' skinparam defaultFontName Comic Sans MS
' skinparam classArrowFontName Arial

autonumber "[0]"
hide footbox

actor Usuario as u
boundary Vista as v
control Controlador as c 
participant "Calculadora\n<<Sistema>>" as s

'group Comprar Producto
c -> v: leerFraccion
v -> u: "Indica una fracción (0/1): "
u -> v: Fraccion (f1)
v -> c: Fraccion (f1)
loop mientras que f1==f2
  c -> v: leerFraccion
  v -> u: "Indica una fracción (0/1): "
  u -> v: Fraccion (f2)
  v -> c: Fraccion (f2)
end
alt NO RL1 or NO RL2
c -> c: RL1 (f1)
c -> c: RL1 (f2)
c -> c: RL2
note right
Verificamos las Reglas de Negocio
Si no se cumple alguna -> Fin UC
end note
end
c -> s: suma(f1,f2)
s -> c: Fraccion (result)
c -> v: mostrarResultado(result)
v -> u: "Suma :" (result)
alt result == "1/1"
  c -> v: muestraMensajeEnhorabuena
  v -> u: "Enhorabuena has sumado la unidad"
else result != "1/1"
  c -> v: muestraMensajePruebaOtraVez
  v -> u: "Inténtalo otra vez"
end

'end
@enduml
```
</details>

