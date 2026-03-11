#task 1 
class BankClient:
    def __init__(self, name, id,parol):

         if len(id) != 12 or not id.isdigit():
             raise ValueError("12 numbers should be here")
         if not parol:
             raise ValueError("it shouldnt be empty")
     
    self.__name = name
    self.__id = id
    self.__parol = parol
    def set_name(self, new_name):
       self.__name = new_name
    def set_parol(self, new_parol):
       self.__parol = new_parol
#task2
class BankAccount:
    def __init__(self, bankclient, balance, accountnumber):
        if balance < 0:
            raise ValueError("no")
        self.__backclient = bankclient
        self.__balance = balance
        self.__accountnumber = accountnumber
        self.__transactions = []
        
        
    def set_owner(self, new_owner):
        self.__backclient = new_owner
        set.__transactions.append("owner changed")
    def deposit(self, amount):
        if amount <= 0:
            raise ValueError("should be positive")
        self.__balance += amount
        self.__transactions.append(f"Deposited: {amount}")
     def withdraw(self, amount):
        if amount <= 0:
            raise ValueError("Withdrawal amount must be positive")
        if self.__balance - amount < 0:
            raise ValueError("no balance")
        self.__balance -= amount
        self.__transactions.append(f"Withdrew: {amount}")
    def get_transactions(self):
        return self.__transactions
#task3
import random
class BankAccount:
    def __init__(self, bankclient, balance, accountnumber):
        if balance < 0:
            raise ValueError("Balance cannot be negative")
        self.__bankclient = bankclient 
        self.__balance = balance 
        self.__accountnumber = accountnumber
        self.__transactions = []
    def set_owner(self,new_owner):
        self.__bankclient = new_owner
        self.__transactions.append("owner changed")
    def deposit(self, amount):
        if amount <0:
            raise ValueError("its negatice ")
        self.__balance += amount
        self.__transactions.append(f"Deposited {amount}")
    def withdraw(self, amount):
        if amount < 0:
            raise ValueError("no negative number")
        self.__balance -= amount 
        self.__transactions.append(f"Withdrawal this {amount} of money")
    def get_transaction(self):
        return self.__transactions
    
  class CurrentAccount(BankAccount):
    def __init__(self, bankclient, balance):
        accountnumber = "KZ" + "".join([str(random.randint(0, 9)) for _ in range(20)])
        currency_code = "KZT"  
        super().__init__(bankclient, balance, accountnumber)
        self.__currency_code = currency_code
    
    def get_currency(self):
        return self.__currency_code  

class CardAccount(BankAccount):
    def __init__(self, bankclient, balance):
        accountnumber = "KZ" + "".join([str(random.randint(0, 9)) for _ in range(20)])
        super().__init__(bankclient, balance, accountnumber)
        
        self.__card_number = "".join([str(random.randint(0, 9)) for _ in range(16)])
        
        self.__pin = self.generate_valid_pin()
        
    def generate_valid_pin(self):
        while True:
            pin = "".join([str(random.randint(0, 9)) for _ in range(4)])
            if not self.is_invalid_pin(pin): 
                return pin
    def is_invalid_pin(self, pin):
        if len(set(pin)) == 1:  
            return True
        if 1900 <= int(pin) <= 2100:  
            return True
        return 
    def check_pin(self, pin):
        if pin == self.__pin:
            return True
        return False
     def change_pin(self, old_pin, new_pin):
        if old_pin == self.__pin:
            self.__pin = new_pin
            return "PIN successfully changed"
        return "Invalid old PIN or invalid new PIN"
    def get_card_number(self):
        return self.__card_number
    def get_pin(self):
        return self.__pin
    
client = BankClient("Jhon", "123456789012", "parole")
card_account = CardAccount(client, 1000)

print(f"Card Number: {card_account.get_card_number()}")
print(f"PIN: {card_account.get_pin()}")

#task4
     def get_account_info(self):
          return f"Account Number: {self.__accountnumber[-4:]}, Card Number: {self.__card_number[-4:]}, Balance: {self.__balance}"
#task5
class Bank:
    def __init__(self):
        self.__clients = [] 
    def add_client(self, client):
        self.__clients.append(client)
    def add_client(self, client):  
        self.__clients.append(client)
    def remove_client(self, id):
      client_to_remove = self.find_client_by_id(id)
    if client_to_remove:
        self.__clients.remove(client_to_remove)
    else:
          print("Client not found")
    def fing_client_by_id(self,id):
        for client in self.__clients:
            if client.id = id
            return client
     def transfer_money(self, from_client_id, to_client_id, amount):
        from_client = self.find_client_by_id(from_client_id)
        to_client = self.find_client_by_id(to_client_id)
        
        from_client.withdraw(amount)
        to_client.deposit(amount)
       