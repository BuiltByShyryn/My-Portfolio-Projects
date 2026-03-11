import json
import requests
title = input("Enter the name of the film")
apikey = "real api key that i dont have;d"
key = key
url = f"http://www.omdbapi.com/?t={title}&apikey={key}" 
try:
    response = requests.get(url)
    if response.status_code == 200:
        data= requests.json()
    if data.get("Response") == "True":
            print(f"Title: {data['Title']}")
            print(f"Year: {data['Year']}")
            print(f"Genre: {data['Genre']}")
            print(f"IMDb Rating: {data['imdbRating']}")
    else:
            print(f"Error")
except requests.exceptions.RequestException as e:
    print(f"error with the request:{e}")
    
    