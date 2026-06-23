# Dockerized Flask Practice App

This project is a beginner Docker practice app built with Python and Flask. It shows how to package a small web application into a Docker image and run it inside a container.

The app contains several simple routes, HTML responses, a random number page, a student page, and a Dockerfile that prepares the Flask environment.

## Project Structure

```text
02-Flask-Docker-App/
  Dockerfile
  main.py
  requirements.txt
```

## What This Project Demonstrates

- Creating a Flask web application.
- Creating multiple Flask routes.
- Installing Python dependencies with `requirements.txt`.
- Writing a Dockerfile for a Python project.
- Building a Docker image.
- Running a Flask app inside a Docker container.
- Mapping container port `5000` to the local machine.

## Dockerfile Explanation

```dockerfile
FROM python:3.14.1
```

Uses the official Python image as the base image.

```dockerfile
WORKDIR /app
```

Creates and switches to the `/app` folder inside the container.

```dockerfile
COPY requirements.txt .
```

Copies the dependency file into the container.

```dockerfile
RUN pip install -r requirements.txt
```

Installs Flask and other dependencies listed in `requirements.txt`.

```dockerfile
COPY . .
```

Copies the project files into the container.

```dockerfile
CMD ["python", "main.py"]
```

Runs the Flask application when the container starts.

## How to Build

Run this command inside the project folder:

```bash
docker build -t flask-practice-app .
```

## How to Run

```bash
docker run --rm -p 5000:5000 flask-practice-app
```

Then open this URL in the browser:

```text
http://localhost:5000
```

## Available Routes

| Route | Description |
| --- | --- |
| `/` | Home page with a greeting |
| `/about` | About page |
| `/contact` | Contact information page |
| `/help` | Help page |
| `/student` | Student information page |
| `/topic` | List of practice topics |
| `/datetime` | Shows the current date and time |
| `/rand` | Shows a random number from 1 to 100 |

## Technologies Used

- Python
- Flask
- Docker

## Expected Result

After building and running the Docker container, the Flask web application becomes available at `http://localhost:5000`. The project demonstrates the basics of containerizing a Python web app with Docker.
