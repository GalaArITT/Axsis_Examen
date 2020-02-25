create database BD_CRUD_Axsis 
----
go
use BD_CRUD_Axsis
go
create table usuarios_axsis
(
	id int identity primary key ,
	correo varchar(25),
	usuario varchar(25),
	contrasena varchar(25),
	estatus bit,
	sexo varchar(2),
	fecha_creacion datetime2
)
--Insertar directo usuario de prueba
insert into usuarios_axsis values ('correo@correo.com','Galaviz19','Contraseña-1234',1,'H',GETDATE(),'Contraseña-1234')
go
alter table usuarios_axsis
alter column contrasena nvarchar(250)
go
---SP para mostrar 
alter proc sp_mostrarDatos as
begin 
	select * from usuarios_axsis
end
go
--SP Para INACTIVAR
create proc sp_inactivarUsuario @id int as
begin 
	update usuarios_axsis
	set estatus = 0 
	where id = @id
end

