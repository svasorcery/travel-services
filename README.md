# About

TravelServices is a [Micro-Services Architecture patterns](https://github.com/svasorcery/architectural-patterns/tree/master/distributed/MSA) implementation of [Rail](Rail), Avia, Hotel tickets booking APIs, [Traveler API](Traveler) and [Client applications](Clients), all secured with [Security API](Security).

----------


## Tools

- Install NodeJS: [How to install NodeJS](https://github.com/svasorcery/know-how-to/blob/master/install/node-js.md)

- Install and run MongoDB: [How to install MongoDB](https://github.com/svasorcery/know-how-to/blob/master/install/mongodb-server-on-windows.md)


## Installation

- Clone repo:
```
git clone https://github.com/svasorcery/travel-services.git
```

- Restore dotnet packages:
```cmd
dotnet restore
```

- Restore angular client packages:
```cmd
cd ./travel-services/Clients/HermesSpa/ClientApp
npm install
```

- Get and set pass.rzd.ru credentials
    - [Register (RU)](https://pass.rzd.ru/selfcare/register/ru) | [Register (EN)](https://pass.rzd.ru/selfcare/register/en)
    - Store your login and password in RailAPI's ```appsettings```.


## Seed data

- Successively import data from json files using ```mongoimport```:
```cmd
mongoimport -d kaolin -c countries --jsonArray --mode=upsert --upsertFields=ru --file mongo-countries.json
mongoimport -d kaolin -c stations --jsonArray --mode=upsert --upsertFields=ru --file mongo-stations.json
```
