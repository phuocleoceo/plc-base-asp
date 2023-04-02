## PLC - BASE - ASP

## Docker Command:
- docker build --no-cache --platform=linux/amd64 -t phuocleoceo/plc-base .
- docker push phuocleoceo/plc-base
- docker run -d -p {{port}}:80 --name test-plc phuocleoceo/plc-base

## Run Command:
- dotnet run / dotnet watch

## EFCore Code first:
- dotnet-ef migrations add {migration-name} -o Common/Data/Migrations
- dotnet-ef database update