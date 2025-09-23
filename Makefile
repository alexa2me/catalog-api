# Makefile for MyAPI
# This file makes it easier to run .NET project commands

# Main commands
.PHONY: run build clean restore test watch kill-port info list-packages help

# Runs the project
run:
	dotnet run

# Runs the project in watch mode (automatically recompiles on changes)
watch:
	dotnet watch run

# Builds the project
build:
	dotnet build

# Cleans build files
clean:
	dotnet clean

# Restores dependencies
restore:
	dotnet restore

# Runs tests (if any)
test:
	dotnet test

# Create migrations script
add-migration:
	dotnet ef migrations add ${name}

# Remove created script
remove-migration:
	dotnet ef migrations remove ${name}

# Applies migrations to the database
update-database:
	dotnet ef database update

# Stops processes that may be using the default port
kill-port:
	@echo "Stopping processes on port 5101..."
	@lsof -ti:5101 | xargs -r kill
	@echo "Processes stopped."

# Shows project information
info:
	dotnet --info

# Lists installed packages
list-packages:
	dotnet list package

# Help - shows available commands
help:
	@echo "Available commands:"
	@echo "  make run           - Runs the project"
	@echo "  make build         - Builds the project"
	@echo "  make clean         - Cleans build files"
	@echo "  make restore       - Restores dependencies"
	@echo "  make test          - Runs tests"
	@echo "  make watch         - Runs in watch mode"
	@echo "  make kill-port     - Stops processes on port 5101"
	@echo "  make info          - Shows .NET information"
	@echo "  make list-packages - Lists installed packages"
	@echo "  make help          - Shows this help"

# Default command when only 'make' is executed
.DEFAULT_GOAL := help
