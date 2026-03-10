import random
import datetime
from faker import Faker

#Task 1: Magic 8-Ball 
responses = ["Yes, definitely!", "No, absolutely not!", "Maybe...", "Ask again later."]
input("Ask the Magic 8-Ball a question: ")
print(f"Answer: {random.choice(responses)}")

#Task 2: Number Guessing Game
target = random.randint(1, 10)
win = False
for i in range(3):
    guess = int(input("Guess the number (1-10): "))
    if guess == target:
        print("Congrats! You won!")
        win = True
        break
    else:
        print("Wrong! Try again.")
if not win: print(f"Game Over! The number was {target}.")

#Task 5: Synthetic Data Generator
def get_fake_user():
    fake = Faker()
    return f"Name: {fake.name()} | Address: {fake.address()} | Email: {fake.email()}"

#Task 7: Birthday Countdown
day = int(input("Enter birth day (DD): "))
month = int(input("Enter birth month (MM): "))
current = datetime.datetime.now()
next_bday = datetime.datetime(current.year, month, day)

if current > next_bday:
    next_bday = datetime.datetime(current.year + 1, month, day)

days_left = (next_bday - current).days
print(f"Days until next birthday: {days_left}")

#Task 8: Hangman Engine
names_list = ["Bob", "Rob", "Gary"]
secret_name = random.choice(names_list).lower()
progress = ["_"] * len(secret_name)

while "_" in progress:
    print(f"Current name: {' '.join(progress)}")
    guess = input("Guess a letter: ").lower()
    
    if guess in secret_name:
        for i in range(len(secret_name)):
            if secret_name[i] == guess:
                progress[i] = guess
        print("Correct!")
    else:
        print("Wrong!")
print(f"Congrats! You found it: {secret_name}")