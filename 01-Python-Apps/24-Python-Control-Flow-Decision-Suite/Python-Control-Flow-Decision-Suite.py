#task1
num = int(input("the age of a person "))
if 0 <= num <= 12:
    print("Childhood")
elif 13 <= num <= 17:
    print("teenage years")
elif 18 <= num <= 64:
    print("Adulthood")
elif 65 <= num:
    print("Retired")
else:
    print("Incorrect command")

#task2
num = int(input("Введите первое число: "))
num1 = int(input("Введите второе число: "))
num2 = int(input("Введите третее число: "))
if num + num1 > num2 and num1 + num2 > num and num2 + num > num1:
    print("Треугольник возможен")
else:
    print("Треугольник невозможен")

#task3
num = int(input("Введите первое число: "))
num1 = int(input("Введите второе число: "))
num2 = int(input("Введите третее число: "))
if num >= num1 and num >= num2:
    print("Максимальное число:", num)
elif num1 >= num and num1 >= num2:
    print("Максимальное число:", num1)
else:
    print("Максимальное число:", num2)

#task4
b = int(input("Введите день рождения"))
b1= int(input("Введите месяц рождения"))
if (b1 == 3 and b >= 21) or (b1 == 4 and b <= 20):
    print("Овен")
elif (b1 == 4 and b >= 21) or (b1 == 5 and b <= 20):
    print("Телец")
elif (b1 == 5 and b >= 21) or (b1 == 6 and b <= 21):
    print("Близнецы")
elif (b1 == 6 and b >= 22) or (b1 == 7 and b <= 22):
    print("Рак")
elif (b1 == 7 and b >= 23) or (b1 == 8 and b <= 23):
    print("Лев")
elif (b1 == 8 and b >= 24) or (b1 == 9 and b <= 23):
    print("Дева")
elif (b1 == 9 and b >= 24) or (b1 == 10 and b <= 23):
    print("Весы")
elif (b1 == 10 and b >= 24) or (b1 == 11 and b <= 22):
    print("Скорпион")
elif (b1 == 11 and b >= 23) or (b1 == 12 and b <= 21):
    print("Стрелец")
elif (b1 == 12 and b >= 22) or (b1 == 1 and b <= 20):
    print("Козерог")
elif (b1 == 1 and b >= 21) or (b1 == 2 and b <= 20):
    print("Водолей")
elif (b1 == 2 and b >= 21) or (b1 == 3 and b <= 20):
    print("Рыбы")
else:
    print("Неверная дата")

#task5
b = int(input("Введите количество очков: "))
if b >= 90:
    print("Отлично")
elif b >= 70 and b < 90:
    print("Хорошо")
elif b >= 50 and b < 69:
    print("Удовлетворительно")
else:
    print("Неудовлетворительно")

#task6
b = int(input("Введите число от 1 до 10: "))
if b == 1:
    print("I")
elif b == 2:
    print("II")
elif b == 3:
    print("III")
elif b == 4:
    print("IV")
elif b == 5:
    print("V")
elif b == 6:
    print("VI")
elif b == 7:
    print("VII")
elif b == 8:
    print("VIII")
elif b == 9:
    print("IX")
elif b == 10:
    print("X")
else:
    print("Невозможно вывести число")