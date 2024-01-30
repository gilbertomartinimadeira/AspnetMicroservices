# AspnetMicroservices
AspnetMicroservices

dotnet new solution -o Catalog
dotnet new webapi -n Catalog.API
dotnet sln add Catalog.API.csproj

docker pull mongo

docker run -d -p 27017:27017 --name shopping-mongo mongo

docker exec -it <containerid> /bin/bash

mongosh

use CatalogDb

db.createCollection('Products')


db.Products.insertMany(
			[
			    {
			        "Name": "Asus Laptop",
			        "Category": "Computers",
			        "Summary": "Summary",
			        "Description": "Description",
			        "ImageFile": "ImageFile",
			        "Price": 54.93
			    },
			    {
			        "Name": "HP Laptop",
			        "Category": "Computers",
			        "Summary": "Summary",
			        "Description": "Description",
			        "ImageFile": "ImageFile",
			        "Price": 88.93
			    }
			])

db.Products.find({'Price':{$gt:60}}).pretty()            

db.Products.remove({})

deleteOne
deleteMany

nuget pagackes:

	MongoDB.Driver
	Swashbuckle.AspNetcore





docker build -t gilmartmd/catalog_api .

docker run -p 8080:80 gilmartmd/catalog_api

docker compose up -d

docker container stop $(docker container list -aq)

#Segunda API

dotnet new webapi --use-controllers -o Basket.API

dotnet sln add Services/Basket/Basket.API/Basket.API.csproj

docker run -p 6379:6379 -p 8001:8001 --name aspnetrun-redis redis/redis-stack

docker exec -it <cid> /bin/bash

cd /data && redis-cli

ping

dotnet add package Microsoft.Extensions.Caching.StackExchangeRedis --version 8.0.1

dotnet add package Newtonsoft.Json --version 13.0.3


dotnet new webapi --use-controllers -o Discount.API

dotnet add package Npgsql --version 8.0.1

dotnet add package Dapper --version 2.1.28







