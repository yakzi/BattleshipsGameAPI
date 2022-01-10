<h1 align="center">
 BattleshipsGameAPI
</h1>

## Table of contents
* [Introduction](#introduction)
* [Endpoints](#Endpoints)
* [Features](#Features)
* [Technologies](#technologies)
* [Screenshots](#screenshots)

## Introduction
<h3>Project is still in development!</h3>
WebAPI which allows to simulate battleships game between two AI

## Features
* Create players, insert ships, shoot ships
* Watch every move of AI
* Write/Read everything into/from .json file

## Endpoints
* <b>/CreatePlayer/{playername}</b> - allows to create a new player with empty board, and saves it (board) to .json file
* <b>/GetBoard/{playername}</b> - allows to recieve player board in a .json format
* <b>/InsertShip/{playername}/{shipLength}/{direction}</b> - allows to insert a new ship with specified length and direction to player's board, and saves updated board to .json file
* <b>/InsertStartingShips/{playername}</b> - allows to insert a basic set of ships (5,4,3,2,2,1,1) to player's board, and saves updated board to .json file
* <b>/Fire/{shooterName}/{targetName}</b> - allows to shoot other player's board in totally random way (but 'AI' remembers previous missfires and hit ships), and saves updated board to .json file - <b>WIP</b>

## Technologies
* ASP.NET Core WebAPI
* Json.NET
* Swagger

## Screenshots
<p align="center">
 <img src="./battleshipsapi.jpg" alt="Screenshot from Swagger with BattleshipsGameAPI"/>
</p>
