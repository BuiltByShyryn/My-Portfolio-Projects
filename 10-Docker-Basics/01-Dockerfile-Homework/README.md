# Dockerfile Homework

This homework demonstrates how to create and run simple Docker images using Dockerfiles.
Each folder contains one Dockerfile based on the official Python image.

## Structure

- `name-image/` prints a name.
- `age-image/` prints an age.
- `city-image/` prints a city.
- `numbers-image/` prints numbers from 1 to 20.
- `screenshots/` contains terminal screenshots showing the build/run results.
- `descriptions.txt` contains short notes about Docker terms used in the homework.

## How to Build and Run

```bash
cd name-image
docker build -t hm-name .
docker run --rm hm-name
```

```bash
cd ../age-image
docker build -t hm-age .
docker run --rm hm-age
```

```bash
cd ../city-image
docker build -t hm-city .
docker run --rm hm-city
```

```bash
cd ../numbers-image
docker build -t hm-numbers .
docker run --rm hm-numbers
```

## Expected Output

- `hm-name`: `My name is Alex`
- `hm-age`: `I am 18 years old`
- `hm-city`: `Astana`
- `hm-numbers`: numbers from `1` to `20`
