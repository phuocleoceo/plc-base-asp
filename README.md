## PLC - BASE - ASP

## Certificate Command
```sh
openssl genpkey -algorithm RSA -out pri.key
openssl req -new -key pri.key -out pub.csr
openssl x509 -req -in pub.csr -signkey pri.key -out pub.crt
```

## Docker Command

```sh
docker build --no-cache --platform=linux/amd64 -t phuocleoceo/plc-base .
docker push phuocleoceo/plc-base
docker run -d -p {{port}}:80 --name test-plc phuocleoceo/plc-base
```

## Local Command

```sh
dotnet restore
dotnet tool restore
dotnet run / dotnet watch
```

## EFCore Code First

```sh
dotnet tool install dotnet-ef
dotnet ef migrations add {migration-name} -o Common/Data/Migrations
dotnet ef database update
```

## Cshapier Formatter Tool

```sh
dotnet tool install csharpier
dotnet csharpier .
```

## Husky Hooks:

```sh
dotnet tool install Husky
dotnet husky install
dotnet husky run
```
