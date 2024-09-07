# Usar a imagem base para SQL Server
FROM mcr.microsoft.com/mssql/server:2019-latest

# Expor a porta padrão do SQL Server
EXPOSE 1433

# Copiar arquivos de migrações e datasets para um diretório no contêiner
COPY ./RestWithASPNET/db/migrations/ /var/opt/mssql/migrations/
COPY ./RestWithASPNET/db/dataset/ /var/opt/mssql/dataset/

# Copiar o script de inicialização para um diretório onde o SQL Server possa executá-lo
COPY ./RestWithASPNET/ci/init_database.sh /usr/src/app/init_database.sh

# Garantir que o script de inicialização tenha permissões de execução
RUN chmod +x /usr/src/app/init_database.sh

# Definir a variável de ambiente para a senha do SA
ENV SA_PASSWORD=YourStrong!Passw0rd

# Definir a variável de ambiente para aceitar licenças
ENV ACCEPT_EULA=Y

# Comando para inicializar o SQL Server e executar o script de inicialização
ENTRYPOINT ["/bin/bash", "/usr/src/app/init_database.sh"]
