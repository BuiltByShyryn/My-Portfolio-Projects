import tkinter as tk
import random
from tkinter import messagebox

# Function to start a new game
def new_game():
    global secret_number, attempts
    secret_number = random.randint(1, 100)  
    attempts = 0  
    label_result.config(text="Enter the number from 1 to 100 and press the button.")
    entry_guess.config(state=tk.NORMAL)  


def check_guess():
    global attempts
    try:
        guess = int(entry_guess.get())  
        attempts += 1  
        if guess < secret_number:
            label_result.config(text="too low number. Try again")
        elif guess > secret_number:
            label_result.config(text="too big number. Try again")
        else:
            messagebox.showinfo("good", f"U have guessed, ur attempts: {attempts}")
            new_game()  
    except ValueError:
        messagebox.showerror("Error", "type the enumber")   


root = tk.Tk()
root.title("Guess the number")

secret_number = random.randint(1, 100)  
attempts = 0  


label_instruction = tk.Label(root, text="Guess the number from 1 to 100")
label_instruction.pack(pady=10)


entry_guess = tk.Entry(root)
entry_guess.pack(pady=10)

button_check = tk.Button(root, text="Check", command=check_guess)
button_check.pack(pady=5)


label_result = tk.Label(root, text="Enter ur number and tap the button")
label_result.pack(pady=10)

button_reset = tk.Button(root, text="Delete", command=new_game)
button_reset.pack(pady=5)


root.mainloop()
