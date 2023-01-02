# Información a tener en cuenta
## Creación de nuevos usuarios
- Insertar manualmente un nuevo registro a la tabla *afiliados*.
- Realizar la creación del usuario mediante la app, haciendo referencia a los datos recién insertados en la tabla.

## Eliminación de turnos de los pacientes frente a cambios de disponibilidad del médico
#### Disminución de días disponibles de un médico en una sucursal
Cuando un médico **quita** días disponibles en una determinada sucursal, se borran **todos** los turnos que los pacientes tengan con él dicho día en la sucursal en cuestión de manera automática.

#### Eliminación de la disponiblidad de un médico en una sucursal
Idem item anterior, pero en éste caso cuando un médico se **desvincula** de una sucursal, **todos** los turnos que los pacientes tengan con el médico en dicha sucursal se borrarán de manera automática.

#### Disminución del horario disponible de un médico en una sucursal
Idem items anteriores, pero en éste caso cuando un médico **disminuye** el rango horario de un determinado día en una determinada sucursal, **todos** los turnos que los pacientes tengan con el médico en tal sucursal, tal día y por fuera del nuevo rango horario se borrarán de manera automática.

Estos escenarios, para una mayor prolijidad, requieren de algún tipo de aviso a los pacientes que pasaron por las situaciones recién mencionadas notificando que el médico, por alguna u otra razón, ya no está más disponible en una sucursal o en el día/horario en el que habían sacado su turno, por lo que los mismos serán eliminados y se tiene que proceder a sacar otro en caso de necesitarlo.

*Al estar nuestro sistema orientado a la alta, baja y modificación del médico únicamente, dicha notificación a los pacientes queda fuera de la funcionalidad del mismo.*
