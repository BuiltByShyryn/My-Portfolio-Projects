#task1
import json
user_input = input("Enter smth in json formate")
data = json.loads(user_input)
for key, value in data.items():
    print(f"{key}: {value}")
#task2
import requests
api_key = "98aae91b730052d016899a654cb87213"
city = input("Enter the name of ur city ")
try:
    url = f"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={api_key}&units=metric&lang=ru"

    response = requests.get(url)
    if response.status_code == 200:
        data = response.json()
        temperature = data['main']['temp']
        weather_description = data['weather'][0]['description']
        print(f"Тtemp {city}: {temperature}°C")
        print(f"weather {weather_description}")
    else:
        print("no")
except requests.exceptions.HTTPError as http_err:
    print(f"error  {http_err}")
except requests.exceptions.RequestException as req_err:
    print(f"Error  {req_err}")
except KeyError:
    print("Error.")
except Exception as err:
    print(f"error {err}")        
#task 3 
import requests
api_key = "98aae91b730052d016899a654cb87213"
print("Choose one of em:")
print("1. Celsius (metric)")
print("2. Fahrenheit (imperial)")
print("3. Kelvin (standard)")
choice = input("enter ur answer 1/2/3")
try:
    if choice == "1":
        unit = "metric"  
    elif choice == "2":
        unit = "imperial"  
    elif choice == "3":
        unit = "standard"  
    else:
        unit = "metric"
        
    city = input("Enter the name of ur city ")

    url = f"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={api_key}&units={unit}&lang=ru"

    response = requests.get(url)

    if response.status_code == 200:
        data = response.json()
        temperature = data['main']['temp']
        weather_description = data['weather'][0]['description']
        if unit == "metric":
            unit_display = "°C"  
        elif unit == "imperial":
            unit_display = "°F"  
        else:
            unit_display = "K"
        print(f"Тtemp {city}: {temperature}{unit_display}")
        print(f"weather {weather_description}")
    else:
        print("no")
except requests.exceptions.HTTPError as http_err:
    print(f"error{http_err}")
except requests.exceptions.RequestException as req_err:
    print(f"Error {req_err}")
except KeyError:
    print("Error")
except Exception as err:
    print(f"error {err}")       
#task4
import requests
url = "https://v2.jokeapi.dev/joke/Any?blacklistFlags=nsfw,religious,political,racist,sexist,explicit"
response = requests.get(url)
if response.status_code == 200:
    data = response.json()
    if data['type'] == 'single':
        joke = data['joke']
        print(f"joke: {joke}")
    elif data['type'] =='twopart':
        setup = data['setup']
        delivery = data['delivery']
        print(f"joke:")
        print(setup)
        print(delivery)
else:
    print("no")
#task5 
import requests
def choose_category():
    print("Выберите категорию шутки:")
    print("1. Programming")
    print("2. Misc")
    print("3. Pun")
    print("4. Spooky")
    print("5. Christmas")
    
    choice = input("Введите номер категории")
    categories = {
        "1": "Programming",
        "2": "Misc",
        "3": "Pun",
        "4": "Spooky",
        "5": "Christmas"
        }
    return categories.get(choice)
category = choose_category()
try:
    url = f"https://v2.jokeapi.dev/joke/{category}?blacklistFlags=nsfw,religious,political,racist,sexist,explicit"
    response = requests.get(url)
    if response.status_code ==200:
        data = response.json()
        if data['type'] == 'single':
            joke = data['joke']
            print(f"joke: {joke}")
        elif data['type'] =='twopart':
            setup = data['setup']
            delivery = data['delivery']
            print(f"joke:")
            print(setup)
            print(delivery)
    else:
        print("no") 
except requests.exceptions.HTTPError as http_err:
    print(f"error{http_err}")
except requests.exceptions.RequestException as req_err:
    print(f"Error {req_err}")
except KeyError:
    print("Error")
except Exception as err:
    print(f"error {err}")  