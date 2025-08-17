# Makefile para MinhaAPI
# Este arquivo facilita a execução de comandos do projeto .NET

# Comandos principais
.PHONY: run build clean restore test watch help

# Executa o projeto
run:
	dotnet run &
	sleep 3
	xdg-open http://localhost:5224/swagger

# Compila o projeto
build:
	dotnet build

# Limpa os arquivos de build
clean:
	dotnet clean

# Restaura as dependências
restore:
	dotnet restore

# Executa os testes (se existirem)
test:
	dotnet test

# Executa o projeto em modo watch (recompila automaticamente quando há mudanças)
watch:
	dotnet watch run

# Para processos que possam estar usando a porta padrão
kill-port:
	@echo "Parando processos na porta 5224..."
	@lsof -ti:5224 | xargs -r kill
	@echo "Processos parados."

# Mostra informações sobre o projeto
info:
	dotnet --info

# Lista os packages instalados
list-packages:
	dotnet list package

# Ajuda - mostra os comandos disponíveis
help:
	@echo "Comandos disponíveis:"
	@echo "  make run          - Executa o projeto"
	@echo "  make build        - Compila o projeto"
	@echo "  make clean        - Limpa os arquivos de build"
	@echo "  make restore      - Restaura as dependências"
	@echo "  make test         - Executa os testes"
	@echo "  make watch        - Executa em modo watch"
	@echo "  make run-port     - Executa na porta 5000"
	@echo "  make kill-port    - Para processos na porta 5224"
	@echo "  make info         - Mostra informações do .NET"
	@echo "  make list-packages- Lista packages instalados"
	@echo "  make help         - Mostra esta ajuda"

# Comando padrão quando apenas 'make' é executado
.DEFAULT_GOAL := help
