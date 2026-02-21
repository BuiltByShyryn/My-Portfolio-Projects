#Task 1: Word Length Filter
words = ["apple", "banana", "orange", "cheese"]
n = int(input("Enter length threshold: "))
for word in words:
    if len(word) > n:
        print(f"Long word: {word}")

#Task 2: Average of User Input
numbers = []
for i in range(10):
    num = float(input(f"Enter number {i+1}/10: "))
    numbers.append(num)
print(f"Average: {sum(numbers) / len(numbers)}")

#Task 3 & 4: Filtering Logic
my_tuple = (1, 2, 3, 4, 5, 6, 7, 8, 9, 10)
for i in my_tuple:
    if i % 3 == 0:
        print(f"Divisible by 3: {i}")

#Task 5 & 6: Min/Max Algorithms
data_points = (1, 2, 3, 4, 5, 6, 7, 8, 9, 10)
print(f"Minimum: {min(data_points)}")
print(f"Maximum: {max(data_points)}")

# Task 7: Filter Below Average
nums = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
avg = sum(nums) / len(nums)
for i in nums:
    if i < avg:
        print(f"Below average: {i}")

#Task 8: Contact Search
contacts = [
    ["Almas", "+ 7 777 770 00 01"],
    ["Iskander", "+ 7 800 080 11 11"]
]
name_query = input("Enter contact name to search: ")
for person in contacts:
    if name_query == person[0]:
        print(f"Found: {person[0]} - {person[1]}")