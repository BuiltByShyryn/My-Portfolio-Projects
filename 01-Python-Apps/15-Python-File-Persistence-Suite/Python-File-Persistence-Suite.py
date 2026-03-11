#task1
user = input("enter the text")
with open("user_text.txt", "w") as file:
    file.write(user)
with open("user_text.txt","r") as file:
    content = file.read()
    print(content)
#task2
import os 
file_name = "notes.txt"

if not os.path.exists(file_name):
    choice = input("file is not found, do u want to create a new one? (Yes/No)")
    if choice == "yes":
        user_text = ("enter the new text for a file ")
        with open("file_name", "w") as file:
            file.write(user_text)
        print("it was Succesfully")
    else:
        print("U r wrong")
else:
    with open("file_name", "r") as file:
        content = file.read()
    print("what is inside of the file:")
    print(content)
with open(file_name, "a"):
    file.write("the file had been updated")
#task3 
file_name = "numbers.txt"

numbers = []
print("Введите 5 чисел:")
for _ in range(5):
    number = int(input("Число: "))
    numbers.append(number)
    
with open(file_name, "w")as file:
    for number in numbers:
        file.write(f"{number}")
total = sum(numbers)
print("sum of numbers {total}")

with open(file_name, "a")as file:
    file.write (f"sum: {total}")
#task4
file_name = input("name pls")
user_input= input("text pls")

with open(file_name, "w")as file:
    content = file.write(user_input)
with open(file_name, "r")as file:
    file.read()
    char_content = len(content)
print(f" There is this much of uhh symbols: {char_content}")
#task5
import os 
file_name = "students.txt"
if not os.path.exists(file_name):
    print("did not found")
    print("type students names")
    
    with open(file_name, "w")as file:
        while True:
            student = input("name ")
            if stuent =="":
                break
            file.write(student +"\n" )
else:
    with open(file_name,"r")as file:
        content = file.readlines()
        for line in content:
            print(line.strip())
new_student = input("new students name")
with open(file_name, "a")as file:
    file.write(new_student + "\n")
print(f"{new_student}")
         

    