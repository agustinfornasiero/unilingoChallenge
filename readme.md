# Unilingo Challenge
[Check Live Site](https://agusforna.devher.online/)

## Comments
- The API is programmed in .Net C#, using SQLite as Database Engine, and Entity Framework Code First.
- The UI is developed in Angular, and styles in CSS.
- YouTube API does not allow you to get the comments from a video, so it's not implementated.
- Some problems with the methods to obtain the audio play, translation and subtitles:
  - Audio Play: The method is programmed, but for a reason of Cors policy, and some problems with "security"
                I couldn't make to reproduce the audio. But it is implemented all the methods and YouTube API integration.
  - Translation to Spanish and Speech: The method is coded, but commented, due to that Google Cloud Translate API, Microsoft Translator API, and IBM API, forced me to pay, and I couldn't get the APIKey to finish the implementation.
  - The application is hosted in a friend's server, also integrated with Docker.

## Running and using the solution
### Stack required
- Visual Studio 2022 (https://visualstudio.microsoft.com/es/vs/)
- Visual Studio Code (https://code.visualstudio.com/)
- Or any IDE of your preference for C# and Angular development.
- Node.js to use npm package administration (https://nodejs.org/en)
- Angular CLI (https://angular.io/cli)
  
### Running application locally
- To run the application locally must be in the correct directory of the web project (e.g C:\\Users\UserName\projects\project-name\src\app).
  Then you have to open in that directory a command prompt. Use the command line or any terminal you want.
- You need to execute this command: 
  ng serve
- Finally, when finishes the deploy, you can open the solution in a Web Browser, use anyone, Firefox, Chrome, etc.
- Generally the port that is listening the UI is http://localhost:4200.


### Executing application with Docker 🐋
Instructions on how-to install and run docker containers are taken as explicit, otherwise you cand check [Docker Docs](https://docs.docker.com/)

For this project an environment file is needed to run this application succesfully on Docker. You need to create a .env file as follows:

```
    APP_ENV = ${APPLICATION_ENVIRONMENT}
    APP_YT_KEY = ${YOUTUBE_AUTHORIZATION_API_KEY}
    APP_DB_STRING = ${Connection string} //Suggestion to use "Data Source=unilingo. db" as it's a straightforward service
    API_PORT = ${API_PORT} //Port where API will be exposed
    UI_PORT = ${UI_PORT} //Port where frontend will be exposed
    UI_API_BASE_URL = ${APPLICATION_ENVIRONMENT} //Final url for exposed API (https://api.domain.com/api)
```

An example as follows

```
    APP_ENV = Development
    APP_YT_KEY = VerySecretKey-ForYoutubeApi
    APP_DB_STRING = "Data Source=unilingo. db"
    API_PORT = 8001
    UI_PORT = 8000
    UI_API_BASE_URL = http://localhost:8001/api
```

And then run the next command on root of the project

```
docker compose --env-file .path-to-env-file.env up --force-recreate --build -d
```
