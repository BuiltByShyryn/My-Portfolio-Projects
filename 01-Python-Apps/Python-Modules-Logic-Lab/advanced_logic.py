import random

#Task 1: High/Low Guessing Game
target_num = random.randint(1, 10)
print("I've picked a number between 1 and 10.")
while True:
    try:
        guess = int(input("Guess the number: "))
        if guess > 5:
            print("Your guess is > 5")
        elif guess < 5:
            print("Your guess is < 5")
        else:
            print("Your guess is exactly 5")

        if guess == target_num:
            print("Congrats! You guessed it!")
            break
        else:
            print("Wrong! Try again.")
    except ValueError:
        print("Please enter a valid number!")

#Task 4: Multiplication Tables (Nested Loops)
# This generates tables for 10 random numbers
random_list = [random.randint(1, 10) for _ in range(10)]
for number in random_list:
    print(f"--- Table for {number} ---")
    for i in range(1, 11):
        print(f"{number} x {i} = {number * i}")
    print()

#Task 6: Modular Calculator Simulation
def add(a, b): return a + b
def subtract(a, b): return a - b
def multiply(a, b): return a * b
def divide(a, b): return a / b if b != 0 else "Error: Division by zero"

print("--- Mini Calculator ---")
try:
    n1 = float(input("Number 1: "))
    n2 = float(input("Number 2: "))
    op = input("Symbol (+, -, *, /): ")
    
    if op == "+": print(f"Result: {add(n1, n2)}")
    elif op == "-": print(f"Result: {subtract(n1, n2)}")
    elif op == "*": print(f"Result: {multiply(n1, n2)}")
    elif op == "/": print(f"Result: {divide(n1, n2)}")
    else: print("Invalid operation")
except ValueError:
    print("Error: Invalid input type")