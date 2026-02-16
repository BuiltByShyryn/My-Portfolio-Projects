# --- Task 1: BMI Calculator ---
height = float(input("Enter height in meters: "))
weight = float(input("Enter weight in kg: "))
bmi = weight / (height ** 2)
print(f"Body Mass Index (BMI): {bmi:.2f}")

# --- Task 2: Rectangle Properties ---
a = int(input("Enter side a: "))
b = int(input("Enter side b: "))
area = a * b 
perimeter = 2 * (a + b)
print(f"Area: {area}, Perimeter: {perimeter}")

# --- Task 3: Triangle Area ---
base = int(input("Enter triangle base: "))
height_tri = int(input("Enter triangle height: "))
tri_area = 0.5 * base * height_tri 
print(f"Triangle Area: {tri_area}")

# --- Task 4: Circle Radius ---
c = float(input("Enter circumference: "))
pi = 3.14
radius = c / (2 * pi)
print(f"Circle Radius: {radius:.2f}")

# --- Task 5: Temperature Converter ---
fahrenheit = int(input("Enter Fahrenheit: "))
celsius = 5 / 9 * (fahrenheit - 32)
print(f"Celsius: {celsius:.2f}")
#task6 
cm = input("Введите значение длины в сантиметрах: ")
cm = float(cm)
dm = cm / 2.54
print("Длина в дюймах: ", dm)
# --- Task 6: Length Conversion ---
cm = float(input("Enter length in centimeters: "))
inches = cm / 2.54
print(f"Length in inches: {inches:.2f}")