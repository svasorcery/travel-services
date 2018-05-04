# About

----------


## Tools

- Install and run MongoDB: [How to install MongoDB](https://github.com/svasorcery/know-how-to/blob/master/installs/install-mongodb.md)

- Run ```mongoimport``` to import data:
```cmd
mongoimport -d kaolin -c countries --jsonArray --mode=upsert --upsertFields=ru --file mongo-countries.json
mongoimport -d kaolin -c stations --jsonArray --mode=upsert --upsertFields=ru --file mongo-stations.json
```

## Installation

- Clone repo:
```
https://github.com/svasorcery/travel-services.git
```

- Go to the Rail folder and run ```cmd``` with comand:
```cmd
dotnet restore
```

- Go to the Clients/HermesSpa folder and run:
```cmd
dotnet restore
```

- Then go to the Clients/HermesSpa/ClientApp and run:
```cmd
npm install
```

- Get and set pass.rzd.ru credentials
    - [Register (RU)](https://pass.rzd.ru/selfcare/register/ru) | [Register (EN)](https://pass.rzd.ru/selfcare/register/en)
    - Store your login and password in RailAPI's ```appsettings```.
