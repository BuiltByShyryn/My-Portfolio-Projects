import tkinter as tk
from tkinter import messagebox

root = tk.Tk()
root.title("Sum of Numbers")

def calc():
    try:
        n = int(entry.get())
        if n <1:
            raise ValueError
        total = sum(range(1, n+1))
        messagebox.showinfo("result", f"sum{total}")
    except ValueError:
     messagebox.showerror("error", "error")
     
     
label = tk.Label(root, text="Enter the number")
label.pack(pady =10)

entry = tk.Entry(root, text="here")
entry.pack(pady =5)     

button = tk.Button(root, text="Tap the button", command=calc)
button.pack(pady =10)
root.mainloop()