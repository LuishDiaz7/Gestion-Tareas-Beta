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

DROP TABLE Tasks;

insert into Users (username, password, email) Values ('Admin', '1234', 'admin@gmail.com');

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




