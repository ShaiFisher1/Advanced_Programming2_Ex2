# Advanced Programming 2 - EX2 - Chats Application: 


1. [About](#About)
2. [Dependencies](#Dependencies)
3. [Instructions](#Instructions)
4. [Authors](#Authors)

## About
We created a web service chat.\
The project has a Registration screen, a Login screen a Chat screen, and a Rating page screen.\
**The project uses react-router and react-bootstrap packages.**
You can find the modules installed in package.json -> dependencies. When running ```npm install``` installations are made according to this file


## Dependencies
* Download Node.js : https://nodejs.org/en/ which includes NPM (a package manager for NODE. Js) and will be installed on your computer when you install Node.js. 
* Clone the repository
* Install dependencies using:
  ```npm install```
* Run using:
  ```npm start```  
* The project uses react-router and react-bootstrap packages.
* Modules installed can be found in package.json -> dependencies
* our site uses microsoft/signal.
* Web-Api service needs to run as well - **Web API folder name: "WebApplication1".**
* Rating page ASP.net needs to run as well - **Rating App folder name: "MyWebApplication"**.
* **The React App is at another repository - in the link: https://github.com/ShaiFisher1/Advanced_Programming_ex2_react_app.**
* The domain of the website is set to deafault at "localhost:7170". If the port needs to be changed, then you need to change the variable - "portNumber" in the file "ServerAddress.js" to the wanted port.
* Remove Migration folder from folder "MyWebApplication" and "WebApplication1" and and Drop table if exists. Then connect by the following commands:\
first command: Add-Migration Init second command: Update-Database
* If the domain of thr rating page needs to be changed - then you need to change the variable "ratingAppLink" at row 14 in the file "LoginItem.js".


## Instructions
* In order to log in, if a user is already registered and found (in the database) you can log in, otherwise, register.
* Running the program leads to the Login screen (run by opening the terminal and then ```npm start```)
* By entering a correct registered user (found in the DB) with username and password, you will be able to log in.\
  Onced logged in you will be navigated to the Chat screen.
* When in the chat screen, the contacts of current user are shown on the left,\
  and by clicking on any one of them you can see the chat of the current user with the selected contact.
* Sending a new message:
  * Sending a new text message is done by typing and clicking the send button
* Adding a new contact:
  * Adding a new contact is done by pressing the logo with the plus sign, and entering an existing (registered as a user in the DB) contact. you will be required to fill the contact's username, nickname, and server address. 

## Authors
Mor Siman Tov and Shai Fisher
