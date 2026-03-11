import tkinter as tk
from tkinter import messagebox
import requests

root = tk.Tk()
root.title("Random Facts About Numbers")

def get_fact():
    number = entry.get()
    if number.isdigit():
        response = requests.get(f"http://numbersapi.com/{number}")
        if response.status_code == 200:
            fact = response.text
            messagebox.showinfo("Interesting Fact", fact)
        else:
            messagebox.showerror("Error", "Unable to retrieve fact.")
    else:
        messagebox.showerror("Error", "Please enter a valid number.")

label = tk.Label(root, text="Enter a number:")
label.pack()

entry = tk.Entry(root)
entry.pack()

button = tk.Button(root, text="Get Interesting Fact", command=get_fact)
button.pack()

root.mainloop()
