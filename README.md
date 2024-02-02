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



dotnet new grpc -o Discount.Grpc

sudo apt-get install protobuf-compiler

dotnet add package Grpc.Tools


# Este elemento é o responsável por emitir a classe C# baseada nas intruções do protobuff (discount.proto)
 <ItemGroup>
   <Protobuf Include="Protos\discount.proto" GrpcServices="Server" />
 </ItemGroup>
  
  


sudo -E dotnet dev-certs https -ep /usr/local/share/ca-certificates/aspnet/https.crt --format PEM

dotnet add package Grpc.AspNetCore


#incluir o servico grpc no client (API que vai consumir o Grpc)
<ItemGroup>
    <Protobuf Include="..\..\Discount\Discount.Grpc\Protos\discount.proto" GrpcServices="Client">
      <Link>Protos\discount.proto</Link>
    </Protobuf>
  </ItemGroup>




#Rodar só o PG
docker run -d \
    --name discountdb \
    -e POSTGRES_USER=admin \
    -e POSTGRES_PASSWORD=admin1234 \
    -e POSTGRES_DB=DiscountDb \
    -p 5432:5432 \
    --restart always \
    -v postgres_data:/var/lib/postgresql/data \
    postgres

#rodar só o REDIS
docker run -d \
    --name basketdb \
    --restart always \
    -p 6379:6379 \
    -p 8001:8001 \
    redis/redis-stack
	

docker run -d \
  --name pgadmin \
  -e PGADMIN_DEFAULT_EMAIL=admin@aspnetrun.com \
  -e PGADMIN_DEFAULT_PASSWORD=admin1234 \
  -p 5050:80 \
  -v pgadmin_data:/root/.pgadmin \
  dpage/pgadmin4	