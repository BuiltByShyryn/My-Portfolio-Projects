#task1 
import tkinter as tk
import random 
from tkinter import messagebox

def throw():
    dice1 = random.randint(1,6)
    dice2 = random.randint(1,6)
    result = f"Result: {dice1}, {dice2}"
    messagebox.showinfo("result:", result)
root = tk.Tk()
root.title("Drop the Cube")

roll_button =tk.Button(root, text = "Tap the Button to drop the cube", command = throw)
roll_button.pack(pady = 20)

root.mainloop()
