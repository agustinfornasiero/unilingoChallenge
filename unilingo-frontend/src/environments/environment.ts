import { EnvironmentDef } from "./environment.interface";

export const environment : EnvironmentDef = {
  production: false,
  settings: { 
    apiUrl: 'http://localhost:7207/api', 
    debug: true
  }  
};