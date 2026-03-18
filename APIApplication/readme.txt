Como usar esse arquivo:
Limpeza inicial: Antes de testar o Compose, pare os containers que você criou manualmente para não dar conflito de portas:
docker rm -f api-container sql_server_container
Subir tudo: No terminal, dentro da pasta do arquivo .yml, rode:
powershell
docker-compose up -d

Restaurar o .bak: Como é uma máquina nova (ou ambiente limpo), você precisará copiar o seu arquivo .bak para dentro do novo container do SQL e fazer o restore uma única vez:
docker cp seu_arquivo.bak sql_server_container:/var/opt/mssql/data/