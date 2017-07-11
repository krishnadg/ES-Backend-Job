
FROM microsoft/dotnet:1.1.2-sdk
LABEL Name=esjob Version=0.0.1 
COPY . /usr/share/dotnet/sdk/esjob
WORKDIR /usr/share/dotnet/sdk/esjob
RUN dotnet restore
RUN dotnet build
ENTRYPOINT dotnet run --project ESClassLib/ESClassLib.csproj
