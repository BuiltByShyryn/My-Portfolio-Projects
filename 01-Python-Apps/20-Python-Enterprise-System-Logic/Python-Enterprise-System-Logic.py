#task1
class Employee:
    def __init__(self, __name,__position, __salary):
        self.__name = __name 
        self.__position = __position
        self.__salary = __salary
    def get_name(self, __name):
        return self.__name
    def get_position (self,__position):
        return self.__position
    def get_salary(self,__salary):
        return self.__salary
    def set_name(self, new_name):
        self.__name = new_name
    def set_position(self,new_position):
        self.__position = new_position
    def set_salary(self,new_salary):
        self.__salary = new_salary
class Manager(Employee):
    
    def __init__(self,__name,__position, __salary):
        super().__init__(__name,__position, __salary)
        self.employers = []
    def add_employee(self,employee):
        self.employers.append(employee)
    def average(self):
        total = sum(emp.get_salary() for emp in self.employers)
        return total/len(self.employers)
#task2
class Product:
    def __init__(self, name, description, price):
        self.name = name
        self.description = description
        self.price = price
        
class Food(Product):
    def __init__(self, name, description, price):
        super().__init__(name,description, price)
        
class Clothing(Product):
    def __init__(self, name, description, price):
        super().__init__(name,description, price)
class ShoppingCart:
    def __init__(self):
        self.cart = {}
    def add_product(self,product):
       self.cart[product.name] = product
    def removing (self, product_name):
        if product_name in self.cart:
            del self.cart[product_name]
        else:
            print("no items in such a type of name ")
#task 3 
class Celsius:
    def __init__(self, temperature):
        self.temperature = temperature
class Fahrenheit:
    def __init__(self, temperature):
        self.temperature = temperature
class Kelvin:
    def __init__(self, temperature):
        self.temperature = temperature
class TemperatureConverter:
    def celsius_to_fahrenheit(self, Celsius):
        return Celsius.temperature * 9/5 * 32
    def fahrenheit_to_kelvin(self, Fahrenheit):
        return(Fahrenheit.temperature - 32) * 9/5+273.15
    def kelvin_to_fahrenheit(self, Kelvin):
        return(Kelvin.temperature-273.15) * 9/5 +32
    def mass_conversion(temps, from_temperature, to_temperature):
        converted_temps = []
        for temp in temps:
            if from_temperature == "Celsius" and to_temperature == "fahrenheit":
                converted_temps.append(TemperatureConverter.celsius_to_fahrenheit(temp))
            elif from_temperature == "Fahrenheit" and to_temperature == "Kelvin":
                converted_temps.append(TemperatureConverter.fahrenheit_to_kelvin(temp))
            elif from_temperature == "Kelvin" and to_temperature == "Fahrenheit":
                converted_temps.append(TemperatureConverter.kelvin_to_fahrenheit(temp))
        return converted_temps
#task4
import datetime
class Ticket:
    def __init__(self, ticket_number, description, price, purchase_date):
        self.ticket_number = ticket_number
        self.description = description
        self.price = price 
        self.purchase_date = purchase_date 
class TicketManager:
    def __init__(self):
        self.tickets = []
    def add_tickets(self, ticket):
        self.tickets.append(ticket)
    def remove_ticket(self,ticket_number):
        for ticket in self.tickets:
            if ticket.ticket_number == ticket_number:
                self.tickets.remove(ticket)
                break
            else:
                print("ticket is not here")
    def total_cost(self):
        return sum(ticket.price for ticket in self.tickets)
    def filter(self):
        today = datetime.date.today()
        recent_tickets = []
        for ticket in self.tickets:
            if (today - ticket.purchase_date).days <=30:
                recent_tickets.append(ticket)
        return recent_tickets