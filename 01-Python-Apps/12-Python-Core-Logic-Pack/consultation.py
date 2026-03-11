#task1
user = input("input something")
for i in user:
    print(i)
#task2
fruits = [apple,banana,fruit, something, yeah]
index = 1 
for i in fruits:
    print(f"{index}. {i}")
    index+=1
#task3
n = int(input("Put the number"))
if n % 2 == 0:
    print("its even")
else:
    print("ots odd")
#task4
number = int(input("Put the number"))
for i in range(1.11):
     print(f"{number} * {i} = {number * i}")
#task5
moveis = {
   "some movies with raitng and all"
}
def add_movie(title, director, year, rating):
    new_movie = {
        "title": title,
        "director": director,
        "year": year,
        "rating": rating
    }
    movies.append(new_movie) 
add_movie(somemovie)
#task6
products = {
    "Яблоки": 10,
    "Бананы": 3,
    "Молоко": 7,
    "Хлеб": 2,
    "Яйца": 12
}

def five(products):
    for product, quantity in products.items():
        if quantity > 5:
            print(f"{product}: {quantity} шт.")
            
five(products)