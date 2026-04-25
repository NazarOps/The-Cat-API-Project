Functional requirements:

- Users should be able to register an account
- Users should be able to log in
- Upon login, users will receive an auth token after successful login
- users should be able to create their own cats
- users should be able to assign their created cats to existing breeds and breedfacts from thecatapi
- users should be able to make their own breed facts
- the application should have a frontend website 
- users should be able to browse a public list of cats with breed facts
- users' created account information should be stored in a self hosted PostgreSQL database. 
  Passwords should be hashed with salt and never stored in plain text
- The API should protect certain endpoints using JWT authentication

Non Functional Requirements:

- Website should be easily accessible
- Website should be responsive and load fast
- The API should handle errors and return appropriate status codes
- App should be secure from common vunerabilities (e.g., SQL injection)
- System should be easily maintainable and have a clear structure
