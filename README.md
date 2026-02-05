# FitSammen – Distribueret Holdbookingsystem

Dette projekt er udviklet som 3. semesterprojekt på datamatikeruddannelsen (UCN). Systemet er en fuld-skala løsning til administration og booking af fitnesshold, bygget med fokus på distribueret arkitektur og dataintegritet.

##  Arkitektur
Systemet følger en **N-tier arkitektur** (5-lag), hvilket sikrer en klar separation af ansvarsområder og gør systemet skalerbart:

1. **Webklient (ASP.NET MVC):** Bruges af medlemmer til at booke og afmelde hold.
2. **Desktopklient (WinForms):** Bruges af administratorer til at administrere hold og brugere.
3. **RESTful API (.NET Core):** Central server der håndterer forretningslogik og sikrer ensartet adgang til data.
4. **Data Access Layer (ADO.NET):** Håndterer al kommunikation med databasen.
5. **Database (MSSQL):** Kører i en Docker-container for let udrulning og miljø-konsistens.

##  Tech Stack
* **Sprog:** C# (.NET)
* **API:** RESTful API med JSON-formatering
* **Database:** Microsoft SQL Server (MSSQL) via ADO.NET
* **Sikkerhed:** JWT (JSON Web Tokens), Hashing & Salting (BCrypt/PBKDF2-princip)
* **Containere:** Docker
* **Testing:** xUnit til unit- og integrationstest

#
* **Distribueret Data:** Systemet håndterer kommunikation mellem flere klienttyper og én central server via HTTPS.
* **Concurrency Control:** Implementering af databasetransaktioner for at undgå "overbooking" og sikre ACID-principperne.
* **Sikkerhed:** - Token-baseret autentificering (JWT) beskytter API-endpoints.
  - Beskyttelse mod SQL Injection via parametriserede forespørgsler.
* **Optimistisk Concurrency:** Strategier til at håndtere samtidige opdateringer af holddata.

https://github.com/user-attachments/assets/a99ce3bf-1fe6-434a-a4e1-6aaac359834d

