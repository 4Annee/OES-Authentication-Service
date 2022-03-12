# AuthenticationService

## How To Run It In Docker :

1 - Download Docker Desktop & All Its Dependiencies (WSL and All ...) and Run it:

https://docs.docker.com/desktop/windows/install/

2 - Clone the Repository Into An Empty Directory :

````
git clone https://github.com/4Annee/OES-Authentication-Service.git
`````

3 - Enter The Repo Directory

````
cd {{Repo Directory Name}}
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
