# AuthenticationService

## How To Run It In Docker (The "May Not Work" Way) :

1 - Download Docker Desktop & All Its Dependiencies (WSL and All ...) and Run it:

https://docs.docker.com/desktop/windows/install/

2 - Clone the Repository Into An Empty Directory :

````
git clone https://github.com/4Annee/OES-Authentication-Service.git
`````

3 - Enter The Repo Directory

````
cd {{Repo Directory Name}}
// Try Adding This
docker run -it microsoft/dotnet:latest
`````

4 - Execute The Following Command :

````
docker build -f AuthenticationService/Dockerfile --force-rm -t userservice .
`````

5 - Launch The Container Using The Command :

````

docker run -d -p 8080:80 --name userservice userservice
````

6 - Verify if it's Running By Launching the Command :

````
docker ps
````

7 - Go To The Browser And Check The URL 

```
localhost:8080/
````


## How To Run (The "Will Surely Work Way"):

1 - Install Visual Studio 2022 with The Required Workloads :

.net web dev workload and all related workloads

2 - Open The Solution File

3 - Choose The Docker Run Option (Drop down right to run)

or 

`````
cd AuthenticationService // make sure you are in the same folder as program.cs
dotnet watch run
`````

## Test These :

`````
/// After Cloning The Repo : and Going to the folder with the "Dockerfile" file
docker build -f Dockerfile ..
/// 
`````
