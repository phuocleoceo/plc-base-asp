## PLC - BASE - ASP

## Docker command:
- docker build --no-cache --platform=linux/amd64 -t phuocleoceo/plc-base .
- docker push phuocleoceo/plc-base
- docker run -d -p 7133:80 --name test-plc phuocleoceo/plc-base