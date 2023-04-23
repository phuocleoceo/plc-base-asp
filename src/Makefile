MGR_NAME?=PLCMigration

watch:
	@dotnet watch

add-mig:
	@dotnet ef migrations add $(MGR_NAME) -o Common/Data/Migrations

db-update:
	@dotnet ef database update