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