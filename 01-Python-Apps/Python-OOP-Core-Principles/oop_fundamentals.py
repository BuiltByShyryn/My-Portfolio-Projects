import math 

# --- Task 1: Abstraction & Inheritance ---
class Shape:
    def get_area(self):
        pass 

class Rectangle(Shape):
    def __init__(self, width, height):
        self.width = width
        self.height = height
    def get_area(self):
        return self.width * self.height
    
class Circle(Shape):
    def __init__(self, radius):
        self.radius = radius
    def get_area(self):
        return math.pi * self.radius ** 2

class Triangle(Shape): 
    def __init__(self, base, height):
        self.base = base
        self.height = height
    def get_area(self):
        return 0.5 * self.base * self.height

rectangle = Rectangle(5, 10)
circle = Circle(7)
triangle = Triangle(6, 8)

print(f"Rectangle Area: {rectangle.get_area()}") 
print(f"Circle Area: {circle.get_area()}")              
print(f"Triangle Area: {triangle.get_area()}")    

# --- Task 2: Encapsulation ---
class Person:
    def __init__(self, name, age):
        self.__name = name
        self.__age = age
    def get_name(self):
        return self.__name
    def get_age(self):
        return self.__age
    def set_name(self, name):
        self.__name = name 
    def set_age(self, age):
        if age > 0:
            self.__age = age
        else:
            print("Invalid age")

person = Person("Shyryn", 20) 
print(f"Person Name: {person.get_name()}")
print(f"Person Age: {person.get_age()}")   

# --- Task 3: Inheritance Hierarchies ---
class Clothes:
    def __init__(self, type, color, size):
        self.type = type
        self.color = color
        self.size = size
    def __str__(self):
        return f"{self.type}: {self.color}, Size {self.size}"

class Socks(Clothes):
    def __init__(self, color, size):
        super().__init__("Socks", color, size)

class Tshirt(Clothes):
    def __init__(self, color, size):
        super().__init__("T-shirt", color, size)

wardrobe = []
wardrobe.append(Socks("Red", "M"))
wardrobe.append(Tshirt("Blue", "L"))

print("Wardrobe Contents:")
for item in wardrobe:
    print(item)

# --- Task 4: Logic & State Management ---
class Calculator:
    def __init__(self):
        self.history = []

    def add(self, num1, num2):
        result = num1 + num2 # Fixed logic
        self.history.append(f"{num1} + {num2} = {result}")
        return result

    def subtract(self, num1, num2):
        result = num1 - num2
        self.history.append(f"{num1} - {num2} = {result}")
        return result

    def multiply(self, num1, num2):
        result = num1 * num2
        self.history.append(f"{num1} * {num2} = {result}")
        return result       

    def divide(self, num1, num2):
        if num2 == 0:
            return "Cannot divide by zero"
        result = num1 / num2
        self.history.append(f"{num1} / {num2} = {result}")
        return result

    def show_history(self):
        for operation in self.history:
            print(operation)

calc = Calculator()
calc.add(10, 5)
calc.divide(10, 2)
print("Calculator History:")
calc.show_history()

# --- Task 5: Object Composition & Management ---
class Order:
    def __init__(self, order_number, description, cost, contents):
        self.order_number = order_number
        self.description = description
        self.cost = cost
        self.contents = contents

class OrderManager:
    def __init__(self):
        self.orders = []
    def add_order(self, order):
        self.orders.append(order)
    def get_total_cost(self):
        return sum(order.cost for order in self.orders)

manager = OrderManager()
manager.add_order(Order(1, "Late Night Code", 50, ["Coffee", "Pizza"]))
print(f"Total Order Cost: ${manager.get_total_cost()}")