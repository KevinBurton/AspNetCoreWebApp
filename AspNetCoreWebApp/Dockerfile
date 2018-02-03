FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build (then throw this image away)
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/out .

# Install R
#RUN sh -c 'echo "deb http://cran.rstudio.com/bin/linux/ubuntu trusty/" >> /etc/apt/sources.list'
#RUN echo "deb http://cran.rstudio.com/bin/linux/ubuntu xenial/" | tee -a /etc/apt/sources.list
#RUN apt-get update
#RUN apt-get -y install gnupg2
#RUN apt-key adv --keyserver keyserver.ubuntu.com --recv-keys E298A3A825C0D65DFD57CBB651716619E084DAB9
#RUN gpg --keyserver keyserver.ubuntu.com --recv-key E084DAB9
#RUN gpg -a --export E084DAB9 | apt-key add -

#RUN apt-get update
# r-base dependency on r-base-core
# r-base-core dependency on
# r-base-core : Depends: libjpeg8 (>= 8c) but it is not installable
#               Depends: libpng12-0 (>= 1.2.13-4) but it is not installable
#               Depends: libreadline6 (>= 6.0) but it is not installable

#RUN apt-get install r-base r-base-core r-recommended r-base-dev

# Set the environment
ENV ASPNETCORE_ENVIRONMENT Development

# Expose the port
EXPOSE 5001

ENTRYPOINT ["dotnet", "AspNetCoreWebApp.dll"]
# docker run --name aspnetapp -p 80:80 -d aspnetcorewebapp:latest
