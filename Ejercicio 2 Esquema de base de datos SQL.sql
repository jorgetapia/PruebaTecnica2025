

create database Ejercicio_2_IoT;
go
use Ejercicio_2_IoT
go

--Tabla Dispositivos
create table Dispositivos (
DispositivoID int identity primary key,
Nombre varchar(100) not null,
Tipo varchar(50)  null
)

-- Tabla Eventos
create table Eventos (
EventoID int identity primary key,
DispositivoID int not null,
FechaHora datetime not null default getdate(),
Valor float null,
constraint FK_Eventos_Dispositivos 
    foreign key (DispositivoID) references Dispositivos(DispositivoID)
)



insert into Dispositivos (Nombre, Tipo)
values ('Sensor 1', 'Temperatura'),
    ('Sensor 2', 'Humedad'),
    ('Sensor 3', 'Temperatura'),
	 ('Sensor 4', 'Temperatura')



insert into Eventos (DispositivoID, FechaHora, Valor)
values (1, '2025-01-08 08:30:00',25.5),
    (1, '2025-01-08 09:00:00',26.2),
    (2, '2025-01-08 08:45:00',15.0),
    (2, '2025-01-08 09:15:00',18.5),
    (3, '2025-01-08 08:50:00',22.1),
    (1, '2025-01-08 10:00:00',30.0),
	(1, '2025-01-08 08:30:00',25.5),
    (1, '2025-01-08 09:00:00',26.2),
    (2, '2025-01-08 08:45:00',15.0),
    (2, '2025-01-08 09:15:00',18.5),
    (3, '2025-01-08 08:50:00',22.1),
    (1, '2025-01-08 10:00:00',30.0)


--Consulta para obtener la cantidad de eventos por dispositivo
select d.DispositivoID, d.Nombre, count(e.EventoID) as CantidadEventos
from Dispositivos d
left join Eventos e ON d.DispositivoID = e.DispositivoID
group by d.DispositivoID, d.Nombre

