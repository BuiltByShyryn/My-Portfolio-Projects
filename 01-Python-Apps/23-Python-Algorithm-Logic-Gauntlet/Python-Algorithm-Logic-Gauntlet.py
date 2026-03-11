#task 1
num = int(input("put the number"))
for i in range(1, num + 1 ):
  print(2 ** i)
#task 2
num = int(input("put the number: "))
while num != 0:
  print(num)
num = int(input("put the number: "))
#task 3 
count = 0
total_sum = 0
while count < 10:
  num = int(input("put the number"))
  if num == 0:
    break
  total_sum += num
  count += 1
  if count > 0: 
    average = total_sum / count
    print(average)
  else: 
    print("no numbers")

#task 4 
count = 0
while True:
    num = int(input("put the number"))

    if num < 0:
      break
    count += 1
    print(count)

#task 5 I have lost this one
sum = int(input("put the number thsat u want to take"))

if sum % 500 != 0:
  print("no money")
else:
  bills = [5000, 2000, 1000, 500]
  for bill in bills:
    count = sum // bill
    sum = sum % bill
    if count > 0:
      print(count)

#task 6 
num = int(input("put the number "))
sum = 0
for i in range(1, num):
  if num % i == 0:
    sum += i
if sum == num:
  print(nume)
else:
  print(nothing) 

#task 7
num = int(input("put the number "))
sum = 0
for i in range(1, num + 1):
  sum += i ** 2 
  print(num and sum)

#task 8
n = int(input("put the number "))
f = [0, 1]
if n == 0:
    f = [0]
elif n == 1:
    f = [0, 1]
else:
  while True:
        fib = f[-1] + f[-2]  
        if fib > n:
          break
        print(n)