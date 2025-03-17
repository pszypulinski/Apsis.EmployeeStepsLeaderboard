# Employee Steps Leaderboard API

## Overview

The Employee Steps Leaderboard API is designed to facilitate a company-wide steps tracking application, allowing teams of employees to log and compare their step counts. This API supports team creation, member management, and step tracking.

## API Version

### v1

## Endpoints

### Teams

#### Create a Team

POST `/teams`

- Request Body: `{ "name": "string" }`

- Responses:

  - `201 Created`: `{ "teamId": int }`

  - `400 Bad Request`: `{ "type": "string", "title": "string", "status": int, "detail": "string", "instance": "string" }`

  - `500 Internal Server Error`

#### Delete a Team

DELETE `/teams/{id}`

- Path Parameters:

  - `id (int)`: Team ID

- Responses:

  - `204 No Content`

  - `400 Bad Request`

  - `500 Internal Server Error`

### Team Members

#### Create a Team Member

POST `/teams/{teamId}/members`

- Path Parameters:

  - `teamId (int)`: Team ID

- Request Body: `{ "name": "string" }`

- Responses:

  - `201 Created`: `{ "memberId": int }`

  - `400 Bad Request`

  - `500 Internal Server Error`

#### Delete a Team Member

DELETE `/teams/{teamId}/members/{id}`

- Path Parameters:

  - `teamId (int)`: Team ID

  - `id (int)`: Member ID

- Responses:

  - `204 No Content`

  - `400 Bad Request`

  - `500 Internal Server Error`

### Step Tracking

#### Retrieve All Teams' Step Counts

GET `/teams/steps`

- Responses:

  - `200 OK`: `[ { "teamId": int, "steps": int } ]`

  - `404 Not Found`

  - `500 Internal Server Error`

#### Retrieve a Team's Step Count

GET `/teams/{teamId}/steps`

- Path Parameters:

  - `teamId (int)`: Team ID

- Responses:

  - `200 OK`: `{ "teamId": int, "steps": int }`

  - `400 Bad Request`

  - `404 Not Found`

  - `500 Internal Server Error`

#### Retrieve All Team Members' Step Counts

GET `/teams/{teamId}/members/steps`

- Path Parameters:

  - `teamId (int)`: Team ID

- Responses:

  - `200 OK`: `[ { "memberId": int, "steps": int } ]`

  - `400 Bad Request`

  - `404 Not Found`

  - `500 Internal Server Error`

#### Add Steps for a Team Member

POST `/teams/{teamId}/members/{memberId}/steps`

- Path Parameters:

  - `teamId (int)`: Team ID

  - `memberId (int)`: Member ID

- Request Body: `{ "steps": int }`

- Responses:

  - `201 Created`: `{ "stepsIncrementId": int }`

  - `400 Bad Request`

  - `500 Internal Server Error`

#### Delete Step Increments for a Team Member

DELETE `/teams/{teamId}/members/{memberId}/steps`

- Path Parameters:

  - `teamId (int)`: Team ID

  - `memberId (int)`: Member ID

- Responses:

  - `204 No Content`

  - `400 Bad Request`

  - `500 Internal Server Error`
