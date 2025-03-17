CREATE DATABASE ProyectoAdo;

USE ProyectoAdo;

CREATE TABLE Users (
 id INT IDENTITY(1000,1) PRIMARY KEY,
 username VARCHAR(50) UNIQUE NOT NULL,
 password VARCHAR(MAX) NOT NULL,
 email VARCHAR(100) UNIQUE NULL,
 createdDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE TaskCategories (
 id INT IDENTITY(1000,1) PRIMARY KEY,
 name VARCHAR(100) UNIQUE NOT NULL,
 description TEXT NULL,
 creationDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE TaskStatuses (
 id INT IDENTITY(1,1) PRIMARY KEY,
 statusName VARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE Tasks (
 id INT IDENTITY(1000,1) PRIMARY KEY,
 task_name VARCHAR(255) NOT NULL,
 task_description TEXT NOT NULL,
 categoryId INT NOT NULL,
 userId INT NOT NULL,
 statusId INT NOT NULL,
 creationDate DATETIME DEFAULT GETDATE(),
 dueDate DATE NULL,
 FOREIGN KEY (categoryId) REFERENCES TaskCategories(id) ON DELETE CASCADE,
 FOREIGN KEY (userId) REFERENCES Users(id) ON DELETE CASCADE,
 FOREIGN KEY (statusId) REFERENCES TaskStatuses(id) ON DELETE CASCADE
);

ALTER TABLE TaskCategories
ADD userId INT;

ALTER TABLE TaskCategories
ADD CONSTRAINT FK_TaskCategories_Users FOREIGN KEY (userId) REFERENCES Users(id);

Select * FROM TaskCategories

DELETE FROM TaskCategories;

DROP TABLE TaskCategories;



insert into Users (username, password, email) Values ('Admin', '1234', 'admin@gmail.com');
insert into Users (username, password, email) Values ('Luis', '1234', 'luis@gmail.com');
insert into Users (username, password, email) Values ('Andres', '1234', 'andres@gmail.com');



INSERT INTO TaskStatuses (statusName) VALUES ('To Do');
INSERT INTO TaskStatuses (statusName) VALUES ('In Progress');
INSERT INTO TaskStatuses (statusName) VALUES ('Completed');
INSERT INTO TaskStatuses (statusName) VALUES ('On Hold');
INSERT INTO TaskStatuses (statusName) VALUES ('Cancelled');
INSERT INTO TaskStatuses (statusName) VALUES ('Postponed');

INSERT INTO TaskCategories (name, description) VALUES ('Work', 'Tasks related to work or professional projects');
INSERT INTO TaskCategories (name, description) VALUES ('Personal', 'Tasks related to personal goals or chores');
INSERT INTO TaskCategories (name, description) VALUES ('Shopping', 'Tasks related to shopping lists or purchasing items');
INSERT INTO TaskCategories (name, description) VALUES ('Health', 'Tasks related to health and wellness');
INSERT INTO TaskCategories (name, description) VALUES ('Education', 'Tasks related to learning and educational activities');
INSERT INTO TaskCategories (name, description) VALUES ('Hobbies', 'Tasks related to hobbies and leisure activities');



-- Stored Procedures

-- Add Tasks

CREATE PROCEDURE spAddTask
    @task_name VARCHAR(255),
    @task_description TEXT,
    @categoryId INT,
    @userId INT,
    @statusId INT,
    @dueDate DATE = NULL
AS
BEGIN
    INSERT INTO Tasks (task_name, task_description, categoryId, userId, statusId, dueDate)
    VALUES (@task_name, @task_description, @categoryId, @userId, @statusId, @dueDate)
    
    SELECT SCOPE_IDENTITY() AS 'ID'
END
GO


--Display Tasks

CREATE PROCEDURE spDisplayTasks
AS
BEGIN
    SELECT 
        t.id,
        t.task_name,
        t.task_description,
        t.categoryId,
        tc.name AS category_name,
        t.userId,
        u.username,
        t.statusId,
        ts.statusName,
        t.creationDate,
        t.dueDate
    FROM 
        Tasks t
        INNER JOIN TaskCategories tc ON t.categoryId = tc.id
        INNER JOIN Users u ON t.userId = u.id
        INNER JOIN TaskStatuses ts ON t.statusId = ts.id
    ORDER BY 
        t.dueDate, t.task_name
END
GO

-- Display Tasks by user exec spDisplayTasksByUser 1000

CREATE PROCEDURE spDisplayTasksByUser
    @userId INT
AS
BEGIN
    SELECT 
        t.id,
        t.task_name,
        t.task_description,
        t.categoryId,
        tc.name AS category_name,
        t.userId,
        u.username,
        t.statusId,
        ts.statusName,
        t.creationDate,
        t.dueDate
    FROM 
        Tasks t
        INNER JOIN TaskCategories tc ON t.categoryId = tc.id
        INNER JOIN Users u ON t.userId = u.id
        INNER JOIN TaskStatuses ts ON t.statusId = ts.id
    WHERE 
        t.userId = @userId
    ORDER BY 
        t.dueDate, t.task_name
END
GO


-- Update Tasks

CREATE PROCEDURE spUpdateTask
    @id INT,
    @task_name VARCHAR(255),
    @task_description TEXT,
    @categoryId INT,
    @statusId INT,
    @dueDate DATE = NULL
AS
BEGIN
    UPDATE Tasks
    SET task_name = @task_name,
        task_description = @task_description,
        categoryId = @categoryId,
        statusId = @statusId,
        dueDate = @dueDate
    WHERE id = @id
    
    RETURN @@ROWCOUNT
END
GO



-- Delete Tasks

CREATE PROCEDURE spDeleteTask
    @id INT
AS
BEGIN
    DELETE FROM Tasks
    WHERE id = @id
    
    RETURN @@ROWCOUNT
END
GO

-- Categories


-- Procedimiento almacenado para INSERTAR una categoría con userId

CREATE OR ALTER PROCEDURE spAddTaskCategory
    @name VARCHAR(100),
    @description TEXT,
    @userId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        INSERT INTO TaskCategories (name, description, userId)
        VALUES (@name, @description, @userId);
        
        -- Devolver el ID y la fecha de creación de la categoría recién insertada
        SELECT id, name, description, creationDate, userId
        FROM TaskCategories
        WHERE id = SCOPE_IDENTITY();
    END TRY
    BEGIN CATCH
        -- Verificar si es un error de clave única (nombre duplicado para el mismo usuario)
        IF ERROR_NUMBER() = 2627 OR ERROR_NUMBER() = 2601
            THROW 50000, 'A category with this name already exists for this user.', 1;
        ELSE
            THROW;
    END CATCH
END
GO

-- Procedimiento almacenado para ACTUALIZAR una categoría existente
CREATE OR ALTER PROCEDURE spEditTaskCategory
    @id INT,
    @name VARCHAR(100),
    @description TEXT,
    @userId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        UPDATE TaskCategories
        SET name = @name,
            description = @description
        WHERE id = @id AND userId = @userId;
        
        -- Verificar si se realizó la actualización
        IF @@ROWCOUNT = 0
            THROW 50001, 'Categoría no encontrada o no pertenece al usuario actual.', 1;
            
        -- Devolver la categoría actualizada
        SELECT id, name, description, creationDate, userId
        FROM TaskCategories
        WHERE id = @id;
    END TRY
    BEGIN CATCH
        -- Verificar si es un error de clave única (nombre duplicado para el mismo usuario)
        IF ERROR_NUMBER() = 2627 OR ERROR_NUMBER() = 2601
            THROW 50000, 'Ya existe una categoría con ese nombre para este usuario.', 1;
        ELSE
            THROW;
    END CATCH
END
GO



-- Procedimiento almacenado para ELIMINAR una categoría


CREATE OR ALTER PROCEDURE spDeleteTaskCategory
    @id INT,
    @userId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM TaskCategories WHERE id = @id AND userId = @userId;
    
    -- Verificar si se realizó la eliminación
    IF @@ROWCOUNT = 0
        THROW 50001, 'Categoría no encontrada o no pertenece al usuario actual.', 1;
END
GO

-- Procedimiento almacenado para OBTENER todas las categorías de un usuario
CREATE OR ALTER PROCEDURE spDisplayAllTaskCategoriesByUser
    @userId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT id, name, description, creationDate
    FROM TaskCategories
    WHERE userId = @userId
    ORDER BY name;
END
GO

-- Procedimiento almacenado para OBTENER una categoría por ID y verificar que pertenezca al usuario


CREATE OR ALTER PROCEDURE spGetTaskCategoryById
    @id INT,
    @userId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT id, name, description, creationDate
    FROM TaskCategories
    WHERE id = @id AND userId = @userId;
    
    -- Verificar si se encontró la categoría
    IF @@ROWCOUNT = 0
        THROW 50001, 'Categoría no encontrada o no pertenece al usuario actual.', 1;
END
GO

-- Procedimiento almacenado para cargar categorías en un ComboBox

CREATE PROCEDURE spGetCategoriesForComboBox
    @userId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT id, name
    FROM TaskCategories
    WHERE userId = @userId
    ORDER BY name;
END
GO


-- Procedimiento almacenado Usuarios

CREATE PROCEDURE spGetUserIdByLogin
    @UserOrEmail NVARCHAR(100),
    @Password NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Busca el usuario por username o email y verifica la contraseña
    SELECT id 
    FROM Users 
    WHERE (username = @UserOrEmail OR email = @UserOrEmail) 
      AND password = @Password; -- Asegúrate de que la contraseña esté encriptada
END

SELECT * FROM TaskCategories

EXEC spGetCategoriesForComboBox @userId = 1000; -- Cambia 1 por un userId válido

EXEC sp_readerrorlog;



-- Procedimiento para obtener todos los estados de tareas
CREATE PROCEDURE spGetAllTaskStatuses
AS
BEGIN
    SELECT id, statusName 
    FROM TaskStatuses
    ORDER BY id
END
GO

-- Procedimiento para obtener un estado por su ID
CREATE PROCEDURE spGetTaskStatusById
    @id INT
AS
BEGIN
    SELECT id, statusName
    FROM TaskStatuses
    WHERE id = @id
END
GO

-----------------------------------------------------------------------

-- Procedimiento almacenado para INSERTAR una categoría con userId
CREATE OR ALTER PROCEDURE spAddTaskCategory
    @name VARCHAR(100),
    @description TEXT,
    @userId INT
AS
BEGIN
    SET NOCOUNT OFF; -- Cambiado a OFF para devolver el conteo de filas
    
    BEGIN TRY
        INSERT INTO TaskCategories (name, description, userId)
        VALUES (@name, @description, @userId);
        
        -- El contador de filas se devolverá automáticamente
    END TRY
    BEGIN CATCH
        -- Verificar si es un error de clave única (nombre duplicado para el mismo usuario)
        IF ERROR_NUMBER() = 2627 OR ERROR_NUMBER() = 2601
            THROW 50000, 'A category with this name already exists for this user.', 1;
        ELSE
            THROW;
    END CATCH
END
GO

-- Procedimiento almacenado para ACTUALIZAR una categoría existente
CREATE OR ALTER PROCEDURE spEditTaskCategory
    @id INT,
    @name VARCHAR(100),
    @description TEXT,
    @userId INT
AS
BEGIN
    SET NOCOUNT OFF; -- Cambiado a OFF para devolver el conteo de filas
    
    BEGIN TRY
        UPDATE TaskCategories
        SET name = @name,
            description = @description
        WHERE id = @id AND userId = @userId;
        
        -- El contador de filas se devolverá automáticamente
        -- Verificar si se realizó la actualización
        IF @@ROWCOUNT = 0
            THROW 50001, 'Categoría no encontrada o no pertenece al usuario actual.', 1;
    END TRY
    BEGIN CATCH
        -- Verificar si es un error de clave única (nombre duplicado para el mismo usuario)
        IF ERROR_NUMBER() = 2627 OR ERROR_NUMBER() = 2601
            THROW 50000, 'Ya existe una categoría con ese nombre para este usuario.', 1;
        ELSE
            THROW;
    END CATCH
END
GO

-- Procedimiento almacenado para ELIMINAR una categoría
CREATE OR ALTER PROCEDURE spDeleteTaskCategory
    @id INT,
    @userId INT
AS
BEGIN
    SET NOCOUNT OFF; -- Cambiado a OFF para devolver el conteo de filas
    
    DELETE FROM TaskCategories WHERE id = @id AND userId = @userId;
    
    -- El contador de filas se devolverá automáticamente
    -- Verificar si se realizó la eliminación
    IF @@ROWCOUNT = 0
        THROW 50001, 'Categoría no encontrada o no pertenece al usuario actual.', 1;
END
GO