# --- Task 1: Age Categorization ---
num = int(input("Enter the age of a person: "))
if 0 <= num <= 12:
    print("Childhood")
elif 13 <= num <= 17:
    print("Teenage years")
elif 18 <= num <= 64:
    print("Adulthood")
elif 65 <= num:
    print("Retired")
else:
    print("Incorrect command")

# --- Task 2: Triangle Validation ---
num = int(input("Enter first number: "))
num1 = int(input("Enter second number: "))
num2 = int(input("Enter third number: "))
if num + num1 > num2 and num1 + num2 > num and num2 + num > num1:
    print("Triangle is possible")
else:
    print("Triangle is impossible")

# --- Task 3: Maximum Number Finder ---
num = int(input("Enter first number: "))
num1 = int(input("Enter second number: "))
num2 = int(input("Enter third number: "))
if num >= num1 and num >= num2:
    print("Maximum number:", num)
elif num1 >= num and num1 >= num2:
    print("Maximum number:", num1)
else:
    print("Maximum number:", num2)

# --- Task 4: Zodiac Sign Finder ---
b = int(input("Enter birth day: "))
b1 = int(input("Enter birth month: "))
if (b1 == 3 and b >= 21) or (b1 == 4 and b <= 20):
    print("Aries")
elif (b1 == 4 and b >= 21) or (b1 == 5 and b <= 20):
    print("Taurus")
elif (b1 == 5 and b >= 21) or (b1 == 6 and b <= 21):
    print("Gemini")
elif (b1 == 6 and b >= 22) or (b1 == 7 and b <= 22):
    print("Cancer")
elif (b1 == 7 and b >= 23) or (b1 == 8 and b <= 23):
    print("Leo")
elif (b1 == 8 and b >= 24) or (b1 == 9 and b <= 23):
    print("Virgo")
elif (b1 == 9 and b >= 24) or (b1 == 10 and b <= 23):
    print("Libra")
elif (b1 == 10 and b >= 24) or (b1 == 11 and b <= 22):
    print("Scorpio")
elif (b1 == 11 and b >= 23) or (b1 == 12 and b <= 21):
    print("Sagittarius")
elif (b1 == 12 and b >= 22) or (b1 == 1 and b <= 20):
    print("Capricorn")
elif (b1 == 1 and b >= 21) or (b1 == 2 and b <= 20):
    print("Aquarius")
elif (b1 == 2 and b >= 21) or (b1 == 3 and b <= 20):
    print("Pisces")
else:
    print("Invalid date")

# --- Task 5: Grading System ---
b = int(input("Enter score: "))
if b >= 90:
    print("Excellent")
elif b >= 70 and b < 90:
    print("Good")
elif b >= 50 and b < 69:
    print("Satisfactory")
else:
    print("Unsatisfactory")

# --- Task 6: Roman Numeral Converter ---
b = int(input("Enter number from 1 to 10: "))
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
    print("Cannot display number")