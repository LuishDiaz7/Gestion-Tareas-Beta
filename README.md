# Gestion-Tareas-Beta
Task Manager is a university project designed to help users efficiently organize, track, and manage their daily tasks. It provides features such as task creation, categorization, deadlines, and status updates to enhance productivity and time management.

**Class Documentation**  

**Class: Tasks**  

**Purpose:**  
The `Tasks` class represents user tasks and provides methods for managing them. It enables CRUD (Create, Read, Update, Delete) operations on tasks, as well as retrieving tasks associated with a specific user.

**Main Properties:**
- **Id:** Unique identifier of the task.
- **TaskName:** Name of the task.
- **TaskDescription:** Detailed description of the task.
- **CategoryId:** Identifier of the category to which the task belongs.
- **UserId:** Identifier of the user who owns the task.
- **StatusId:** Identifier of the current status of the task (e.g., pending, completed).
- **CreationDate:** Date when the task was created.
- **DueDate:** Deadline for completing the task. It can be null if there is no deadline.

**Main Methods:**
- **AddTask():** Adds a new task to the database. Returns the ID of the added task.  
  - **Usage:** `int taskId = task.AddTask();`  
  - **Exception:** Throws an exception if an error occurs while adding the task.

- **EditTask():** Updates an existing task in the database. Returns `true` if the task was successfully updated.  
  - **Usage:** `bool success = task.EditTask();`  
  - **Exception:** Throws an exception if an error occurs while updating the task.

- **DeleteTask():** Deletes a task from the database. Returns `true` if the task was successfully deleted.  
  - **Usage:** `bool success = task.DeleteTask();`  
  - **Exception:** Throws an exception if an error occurs while deleting the task.

- **DisplayTasksByUser(int userId):** Retrieves all tasks associated with a specific user. Returns a `DataTable` containing the user's tasks.  
  - **Usage:** `DataTable tasks = Tasks.DisplayTasksByUser(userId);`  
  - **Exception:** Throws an exception if an error occurs while retrieving the tasks.
  
**Class: Categories**  

**Purpose:**  
The `Categories` class represents task categories and provides methods for managing them. It enables CRUD (Create, Read, Update, Delete) operations on categories, as well as loading categories into a `ComboBox` for selection in the user interface.

**Main Properties:**
- **Id:** Unique identifier of the category.
- **Name:** Name of the category.
- **Description:** Description of the category.
- **CreationDate:** Date when the category was created.
- **UserId:** Identifier of the user who owns the category.

**Main Methods:**
- **AddCategory():** Adds a new category to the database. Returns the number of rows affected by the operation.  
  - **Usage:** `int rowsAffected = category.AddCategory();`  
  - **Exception:** Throws an exception if an error occurs while adding the category.

- **GetCategories():** Retrieves all categories associated with the current user. Returns a `DataTable` containing the categories.  
  - **Usage:** `DataTable categories = category.GetCategories();`  
  - **Exception:** Throws an exception if an error occurs while retrieving the categories.

- **LoadCategoriesComboBox(ComboBox cmb):** Loads the user's categories into a `ComboBox`. If no categories are found, adds an "Uncategorized" option.  
  - **Usage:** `category.LoadCategoriesComboBox(comboBox);`  
  - **Exception:** Throws an exception if an error occurs while loading the categories.

- **DeleteCategory():** Deletes a category from the database. Returns the number of rows affected by the operation.  
  - **Usage:** `int rowsAffected = category.DeleteCategory();`  
  - **Exception:** Throws an exception if an error occurs while deleting the category.

- **EditCategory():** Updates the information of an existing category. Returns the number of rows affected by the operation.  
  - **Usage:** `int rowsAffected = category.EditCategory();`  
  - **Exception:** Throws an exception if an error occurs while editing the category.

**Class: Users**  

**Purpose:**  
The `Users` class handles user-related operations, specifically authentication. It provides a method to retrieve a user's ID based on their username or email and password.

**Main Methods:**
- **GetUserId(string userOrEmail, string password):**  
  - **Description:** Retrieves the ID of a user based on their username or email and password. This method is useful for authenticating users in the system.
  - **Parameters:**  
    - **userOrEmail:** Username or email of the user.  
    - **password:** Password of the user.  
  - **Return:**  
    - **int:** The user's ID if the credentials are valid. Returns `-1` if no user is found with the provided credentials.  
  - **Exception:**  
    - **Exception:** Throws an exception if an error occurs while executing the database query.

**Class: Statuses**

**Purpose:**  
The `Statuses` class represents task statuses (e.g., "Pending," "In Progress," "Completed") and provides a method to load these statuses into a `ComboBox`. This makes it easier to select a task's status within the user interface.

**Main Methods:**
- **ConfigureStatusesComboBox(ComboBox cmb):**  
  - **Description:** Configures a `ComboBox` with the task statuses retrieved from the database. This method is useful for dynamically loading statuses into a `ComboBox` in the user interface.
  - **Parameters:**  
    - **cmb:** The `ComboBox` to be configured with the task statuses.  
  - **Exception:**  
    - **Exception:** Thrown if no task statuses are found in the database or if an error occurs while loading the data into the `ComboBox`.

**Class: DatabaseHelper**

**Purpose:**  
The `DatabaseHelper` class provides utility methods for managing database connections. Its primary function is to create and return an instance of `SqlConnection` configured with the appropriate connection string, simplifying database interaction across the project.

**Main Properties:**
- **ConnectionString:** The connection string used to establish communication with the database. This property is static and read-only, ensuring the connection string remains consistent throughout the application.

**Main Methods:**
- **CreateConnection():**  
  - **Description:** Creates a new instance of `SqlConnection` using the configured connection string. This method is useful for centralizing database connection creation and avoiding code repetition.  
  - **Return:**  
    - **SqlConnection:** An `SqlConnection` object ready to be used.


