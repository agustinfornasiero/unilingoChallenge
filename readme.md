## Executing application

An environment file is needed to run this application succesfully on Docker. You need t ocreate a .env file as follows:

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