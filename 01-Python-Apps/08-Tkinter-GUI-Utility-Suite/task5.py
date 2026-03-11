import tkinter as tk 
import random
from tkinter import messagebox

answers = [
    "yes",
    "no",
    "maybe",
    "Try again"
]
def ans():
    question = entry.get()
    if question.strip():
        n = random.choice(answers)
        messagebox.showinfo("magic Baal says:", f"{n}")
    else:
        messagebox.showerror("error", "please enter a question")


root =tk.Tk()
root.title("Magic Ball")

entry = tk.Entry(root, text = "Enter ur question:")
entry.pack(pady=10)

button = tk.Button(root, text="ask", command=ans)
button.pack(pady=5)

root.mainloop()


