## PLC - BASE - ASP

## Docker command:
- docker build --no-cache --platform=linux/amd64 -t phuocleoceo/plc-base .
- docker push phuocleoceo/plc-base
- docker run -d -p {{port}}:80 --name test-plc phuocleoceo/plc-base

## Run Command:
- dotnet run / dotnet watch