# NCBASoapAPITest
To Test this sample application
## Download Docker 
Download docker and run the following commands

###
    1. docker pull mcr.microsoft.com/mssql/server:2022-latest   
    2.  docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=secret123@##" \
               -p 1433:1433 --name sql1 --hostname sql1 \
            -d \
            mcr.microsoft.com/mssql/server:2022-latest
## Run Using DockerFile

    1. docker build --tag 'countryInformation-0-beta' .

    2. docker run --detach 'countryInformation-0-beta'