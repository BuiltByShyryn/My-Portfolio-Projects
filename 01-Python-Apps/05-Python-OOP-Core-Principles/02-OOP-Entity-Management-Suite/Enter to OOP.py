#task1
class Book:
    def __init__(self, title, author, genre, year, pages, read_status):
        self.title = title
        self.author = author
        self.genre = genre
        self.year = year
        self.pages = pages
        self.read_status = read_status
    def display_info(self):
        book_info = {
            "Title": self.title,
            "Author": self.author,
            "Genre": self.genre,
            "Year": self.year,
            "Pages": self.pages,
            "Read Status": "Read" if self.read_status else "Not Read"
        }
        for key, value in book_info.items():
            print(f"{key}: {value}")
book1 = Book("1984", "someone", "something", 1949, 328, True)
book1.display_info()
#task2
class Library:
    def __init(self):
        self.books = []
    def add_book(self,book):
        self.books.append(book)
    def display_books(self):
        for book in self.books:
            book.display_info()
            print()
            
library = Library()
library.add_book(book1)
library.add_book(book2)

library.display_books()
#task3
class Pet:
    def __init__(self, name, type, breed):
        self.name= name
        self.type = type
        self.breed = breed 
    def get_info(self):
        return f"{self.name}{self.type}{self.breed}"
    def is_same_type(pet1, pet2):
        return pet1.type == pet2.type
pet1 = Pet("Барсик", "Кошка", "Мейн-кун")
pet2 = Pet("Шарик", "Собака", "Овчарка")
pet3 = Pet("Мурка", "Кошка", "Сиамская")
print(pet1.get_info())
print(pet2.get_info())
print(is_same_type(pet1, pet2))
print(is_same_type(pet1, pet3))
#task4
students_scores = {}
def add_student(name, score):
        students_scores[name] = score
def print_students_above_50():
    for name,score in students_scores.items():
        if score > 50:
            print(f"{name}: {score}")
add_student("someone", 80)
add_student("someone1",45)        
print("someone woh have more than 50:")
print_students_above_50()