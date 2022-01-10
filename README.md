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
WebAPI which allows to simulate battleships game between two AI

## Features
* Watch every move of AI
* Create players, insert ships, shoot ships
* Write/Read everything into/from .json file

## Endpoints
* /CreatePlayer/{playername} - allows to create a new player with empty board, and saves it (board) to .json file
* /GetBoard/{playername} - allows to recieve player board in a .json format
* /InsertShip/{playername}/{shipLength}/{direction} - allows to insert a new ship with specified length and direction to player's board, and saves updated board to .json file
* /InsertStartingShips/{playername} - allows to insert a basic set of ships (5,4,3,2,2,1,1) to player's board, and saves updated board to .json file
* /Fire/{shooterName}/{targetName} - allows to shoot other player's board in totally random way (but 'AI' remembers previous missfires and hit ships) - <b>WIP</b>

## Technologies
* ASP.NET Core WebAPI
* Json.NET
* Swagger

## Screenshots
<p align="center">
 <img src="./battleshipsapi.jpg" alt="Screenshot from Swagger with BattleshipsGameAPI"/>
</p>
